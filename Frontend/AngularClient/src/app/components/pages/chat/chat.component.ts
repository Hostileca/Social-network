import {Component} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {Chat} from "../../../Data/Models/Chat/Chat";
import {ChatService} from "../../../Data/Services/chat.service";
import {MessageService} from "../../../Data/Services/message.service";
import {Message} from "../../../Data/Models/Message/Message";
import {MessageItemComponent} from "../../Items/message-item/message-item.component";
import {NgForOf, NgIf} from "@angular/common";
import {MessageInputComponent} from "../../Items/message-input/message-input.component";
import {CurrentBlogService} from "../../../Data/Services/current-blog.service";
import {EventBusService} from "../../../Data/Services/event-bus.service";
import {Events} from "../../../Data/Hubs/Events";

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [
    MessageItemComponent,
    NgForOf,
    MessageInputComponent,
    NgIf
  ],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css'
})
export class ChatComponent {
  public Chat!: Chat;
  public Messages: Message[] = [];

  constructor(private readonly _route: ActivatedRoute,
              private readonly _chatService: ChatService,
              private readonly _messageService: MessageService,
              private readonly _currentBlogService: CurrentBlogService,
              private readonly _eventBusService: EventBusService) {
    const chatId = _route.snapshot.params['id'];

    this.LoadChat(chatId)
    this.LoadMessages(chatId)
    this.StartListening(chatId)
  }

  private LoadChat(chatId: string) {
    this._chatService.GetChatById(chatId, this._currentBlogService.CurrentBlog!.id).subscribe(chat => {
      this.Chat = chat
    })
  }

  private LoadMessages(chatId: string) {
    this._messageService.GetChatMessages(chatId, this._currentBlogService.CurrentBlog!.id).subscribe(messages => {
      this.Messages = messages
    })
  }

  private StartListening(chatId: string){
    this._eventBusService.On<Message>(Events.MessageSent).subscribe(message => {
      this.OnMessageReceive(message, chatId)
    })
  }

  private OnMessageReceive(message: Message, chatId: string){
    if(message.chatId == chatId){
      this.Messages.push(message)
      console.log("chat:" + message)
    }
    console.log("smth")
  }
}
