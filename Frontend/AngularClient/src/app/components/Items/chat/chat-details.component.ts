import {Component, Input} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {Chat} from "../../../Data/Models/Chat/Chat";
import {ChatService} from "../../../Data/Services/chat.service";
import {MessageService} from "../../../Data/Services/message.service";
import {Message} from "../../../Data/Models/Message/Message";
import {MessageItemComponent} from "../message-item/message-item.component";
import {NgForOf, NgIf} from "@angular/common";
import {MessageInputComponent} from "../message-input/message-input.component";
import {CurrentBlogService} from "../../../Data/Services/current-blog.service";
import {EventBusService} from "../../../Data/Services/event-bus.service";
import {Events} from "../../../Data/Hubs/Events";
import {HttpErrorResponse} from "@angular/common/http";

@Component({
  selector: 'app-chat-details',
  standalone: true,
  imports: [
    MessageItemComponent,
    NgForOf,
    MessageInputComponent,
    NgIf
  ],
  templateUrl: './chat-details.component.html',
  styleUrl: './chat-details.component.css'
})
export class ChatDetailsComponent {
  @Input() public Chat!: Chat;
  public Messages: Message[] = [];

  constructor(private readonly _route: ActivatedRoute,
              private readonly _chatService: ChatService,
              private readonly _messageService: MessageService,
              private readonly _currentBlogService: CurrentBlogService,
              private readonly _eventBusService: EventBusService) {
  }

  private LoadChat(chatId: string) {
    this._chatService.GetChatById(chatId, this._currentBlogService.CurrentBlog!.id).subscribe(chat => {
      this.Chat = chat
      this.LoadMessages()
      this.StartListening()
    })
  }

  private LoadMessages() {
    this._messageService.GetChatMessages(this.Chat.id, this._currentBlogService.CurrentBlog!.id).subscribe(messages => {
      this.Messages = messages
    })
  }

  private StartListening(){
    this._eventBusService.On<Message>(Events.MessageSent).subscribe(message => {
      this.OnMessageReceive(message, this.Chat.id)
    })
  }

  private OnMessageReceive(message: Message, chatId: string){
    if(message.chatId == chatId){
      this.Messages.push(message)
    }
  }
}
