import { Component } from '@angular/core';
import {MyBlogItemComponent} from "../../Items/my-blog-item/my-blog-item.component";
import {NgForOf, NgIf} from "@angular/common";
import {ChatItemComponent} from "../../Items/chat-item/chat-item.component";
import {ChatDetailsComponent} from "../../Items/chat-details/chat-details.component";
import {ChatsListComponent} from "../../Items/chats-list/chats-list.component";
import {Chat} from "../../../Data/Models/Chat/Chat";
import {ChatService} from "../../../Data/Services/chat.service";

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
  public Chats: Chat[] = []
  public SelectedChat: Chat | null = null

  constructor(private readonly _chatService: ChatService) {
    this._chatService.GetMyChats().subscribe(
      value => {
        this.Chats = value
      }
    )
  }

  public SelectChat(chat: Chat) {
    this.SelectedChat = chat
  }
}
