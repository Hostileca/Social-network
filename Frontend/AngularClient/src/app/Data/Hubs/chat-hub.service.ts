import { Injectable } from '@angular/core';
import {ApiConfig} from "../Consts/ApiConfig";
import {Blog} from "../Models/Blog/Blog";
import {AuthService} from "../Services/auth.service";
import * as signalR from '@microsoft/signalr';
import {IHttpConnectionOptions} from "@microsoft/signalr";
import {EventBusService} from "../Services/event-bus.service";
import {Events} from "./Events";
import {Message} from "../Models/Message/Message";
import {Chat} from "../Models/Chat/Chat";
import {ChatMember} from "../Models/ChatMember/Chat-member";
import {Reaction} from "../Models/Reaction/Reaction";


@Injectable({
  providedIn: 'root'
})
export class ChatHubService {
  private _hubConnection!: signalR.HubConnection;

  constructor(private readonly _authService: AuthService,
              private readonly _eventBusService: EventBusService) {
  }

  public Connect(blog: Blog) {
    const accessToken = this._authService.Tokens?.accessToken.value;
    const options: IHttpConnectionOptions = {
      skipNegotiation: true,
      transport: signalR.HttpTransportType.WebSockets,
      accessTokenFactory: () => accessToken!
    };

    this._hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${ApiConfig.BaseUrl}/chats/hub?userBlogId=${blog.id}`, options)
      .build();

    this.StartConnection();
  }

  private StartConnection() {
    this._hubConnection
      .start()
      .then(() => {
        this.StartEmit()
      })
      .catch(err => console.error('Error while starting connection: ' + err));
  }

  private StartEmit(){
    this._hubConnection.on(Events.ChatCreated, (chat: Chat) => {
      this._eventBusService.Emit(Events.ChatCreated, chat);
    })
    this._hubConnection.on(Events.ChatDeleted, (chat: Chat) => {
      this._eventBusService.Emit(Events.ChatDeleted, chat);
    })
    this._hubConnection.on(Events.ChatMemberAdded, (chatMember: ChatMember) => {
      this._eventBusService.Emit(Events.ChatMemberAdded, chatMember);
    })
    this._hubConnection.on(Events.ChatMemberRemoved, (chatMember: ChatMember) => {
      this._eventBusService.Emit(Events.ChatMemberRemoved, chatMember);
    })
    this._hubConnection.on(Events.ChatMemberUpdated, (chatMember: ChatMember) => {
      this._eventBusService.Emit(Events.ChatMemberUpdated, chatMember);
    })
    this._hubConnection.on(Events.ChatMemberLeft, (chatMember: ChatMember) => {
      this._eventBusService.Emit(Events.ChatMemberLeft, chatMember);
    })
    this._hubConnection.on(Events.MessageSent, (message: Message) => {
      this._eventBusService.Emit(Events.MessageSent, message);
    })
    this._hubConnection.on(Events.ReactionSent, (reaction: Reaction) => {
      this._eventBusService.Emit(Events.ReactionSent, reaction);
    })
    this._hubConnection.on(Events.ReactionRemoved, (reaction: Reaction) => {
      this._eventBusService.Emit(Events.ReactionRemoved, reaction);
    })
  }
}
