import { Component } from '@angular/core';
import {MyBlogItemComponent} from "../../Items/my-blog-item/my-blog-item.component";
import {NgForOf, NgIf} from "@angular/common";
import {ChatItemComponent} from "../../Items/chat-item/chat-item.component";
import {ChatsListComponent} from "../../Items/chats-list/chats-list.component";
import {Chat} from "../../../Data/Models/Chat/Chat";
import {ChatService} from "../../../Data/Services/chat.service";
import {ChatDetailsComponent} from "../../Items/chat/chat-details.component";
import {CurrentBlogService} from "../../../Data/Services/current-blog.service";
import {PageSettings} from "../../../Data/Queries/PageSettings";
import {Observable} from "rxjs";
import {PaginationConfig} from "../../../Data/Consts/PaginationConfig";

@Component({
  selector: 'app-my-chats',
  standalone: true,
  imports: [
    MyBlogItemComponent,
    NgForOf,
    NgIf,
    ChatItemComponent,
    ChatDetailsComponent,
    ChatsListComponent
  ],
  templateUrl: './my-chats.component.html',
  styleUrl: './my-chats.component.css'
})
export class MyChatsComponent {
  public SelectedChat: Chat | null = null
  public ChatsSource: (pageSettings: PageSettings) => Observable<Chat[]>

  constructor(private readonly _chatService: ChatService,
              private readonly _currentBlogService: CurrentBlogService) {
    this.ChatsSource = (pageSettings: PageSettings) => this._chatService.GetBlogChats(
      this._currentBlogService.GetCurrentBlog().id, pageSettings)
  }

  public SelectChat(chat: Chat) {
    this.SelectedChat = chat
  }

  protected readonly PaginationConfig = PaginationConfig;
}
