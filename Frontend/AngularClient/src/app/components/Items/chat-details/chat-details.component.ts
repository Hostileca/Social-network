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
import {MessagesListComponent} from "../pagination/messages-list/messages-list.component";
import {PageSettings} from "../../../Data/Queries/PageSettings";
import {Observable} from "rxjs";
import {PaginationConfig} from "../../../Data/Consts/PaginationConfig";

@Component({
  selector: 'app-chat-details',
  standalone: true,
  imports: [
    MessageItemComponent,
    NgForOf,
    MessageInputComponent,
    NgIf,
    MessagesListComponent
  ],
  templateUrl: './chat-details.component.html',
  styleUrl: './chat-details.component.css'
})
export class ChatDetailsComponent {

  @Input() set SelectedChat(chat: Chat) {
    if (chat) {
      this.LoadChat(chat.id.toString())
    }
  }

  public Chat!: Chat;
  public MessagesSource!: (pageSettings: PageSettings) => Observable<Message[]>

  constructor(private readonly _route: ActivatedRoute,
              private readonly _chatService: ChatService,
              private readonly _messageService: MessageService,
              private readonly _currentBlogService: CurrentBlogService) {
  }

  private LoadChat(chatId: string) {
    this._chatService.GetChatById(chatId, this._currentBlogService.GetCurrentBlog().id).subscribe(chat => {
      this.MessagesSource = (pageSettings: PageSettings) =>
        this._messageService.GetChatMessages(chatId, this._currentBlogService.GetCurrentBlog().id, pageSettings)

      this.Chat = chat
    })
  }

  protected readonly PaginationConfig = PaginationConfig;
}
