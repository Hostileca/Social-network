import {Injectable, OnInit} from '@angular/core';
import * as signalR from '@microsoft/signalr';
import {ApiConfig} from "../Consts/ApiConfig";
import {AuthService} from "../Services/auth.service";
import {IHttpConnectionOptions} from "@microsoft/signalr";
import {EventBusService} from "../Services/event-bus.service";
import {Events} from "./Events";
import {Chat} from "../Models/Chat/Chat";
import {ChatMember} from "../Models/ChatMember/Chat-member";
import {Message} from "../Models/Message/Message";
import {Reaction} from "../Models/Reaction/Reaction";
import {CurrentBlogService} from "../Services/current-blog.service";


@Injectable({
  providedIn: 'root'
})
export class ChatHubService{
  private _hubConnection!: signalR.HubConnection;

  constructor(private readonly _authService: AuthService,
              private readonly _eventBusService: EventBusService,
              private readonly _currentBlogService: CurrentBlogService) {
    this.Connect()
  }

  private Connect() {
    if(!this._authService.IsAuth() || !this._currentBlogService.IsBlogSelected()){
      return
    }

    const accessToken = this._authService.Tokens?.accessToken.value;
    const options: IHttpConnectionOptions = {
      skipNegotiation: true,
      transport: signalR.HttpTransportType.WebSockets,
      accessTokenFactory: () => `${accessToken}`
    };

    this._hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${ApiConfig.BaseUrl}/chats/hub?userBlogId=${this._currentBlogService.CurrentBlog!.id}`, options)
      .build()

    this.startConnection();
  }

  private startConnection() {
    this._hubConnection
      .start()
      .then(() => {
        console.log("Connected to chat hub")
        this.StartEmit()
      })
      .catch(err => console.error('Error while starting connection: ' + err));
  }

  private StartEmit(){
    console.log('Setting up event listeners...')

    this._hubConnection.on(Events.ChatCreated, (chat: Chat) => {
      this._eventBusService.Emit(Events.ChatCreated, chat)
    })
    this._hubConnection.on(Events.ChatDeleted, (chat: Chat) => {
      this._eventBusService.Emit(Events.ChatDeleted, chat)
    })
    this._hubConnection.on(Events.ChatMemberAdded, (chatMember: ChatMember) => {
      this._eventBusService.Emit(Events.ChatMemberAdded, chatMember)
    })
    this._hubConnection.on(Events.ChatMemberRemoved, (chatMember: ChatMember) => {
      this._eventBusService.Emit(Events.ChatMemberRemoved, chatMember)
    })
    this._hubConnection.on(Events.ChatMemberUpdated, (chatMember: ChatMember) => {
      this._eventBusService.Emit(Events.ReactionSent, chatMember)
    })
    this._hubConnection.on(Events.ChatMemberLeft, (chatMember: ChatMember) => {
      this._eventBusService.Emit(Events.ChatMemberLeft, chatMember)
    })
    this._hubConnection.on(Events.MessageSent, (message: Message) => {
      console.log("hub:" + message)
      this._eventBusService.Emit(Events.MessageSent, message)
    })
    this._hubConnection.on(Events.ReactionSent, (reaction: Reaction) => {
      this._eventBusService.Emit(Events.ReactionSent, reaction)
    })
    this._hubConnection.on(Events.ReactionRemoved, (reaction: Reaction) => {
      this._eventBusService.Emit(Events.ReactionRemoved, reaction)
    })
    console.log('Setting up event listeners... done')
  }
}
