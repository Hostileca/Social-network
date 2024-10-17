import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {ChatItemComponent} from "../../chat-item/chat-item.component";
import {Chat} from "../../../../Data/Models/Chat/Chat";
import {NgForOf, NgIf} from "@angular/common";
import {
  PaginationBaseComponent
} from "../pagination-component-base/pagination-base.component";
import {ChatDetailsComponent} from "../../chat-details/chat-details.component";

@Component({
  selector: 'app-chats-list',
  standalone: true,
  imports: [
    ChatItemComponent,
    NgIf,
    NgForOf,
    ChatDetailsComponent
  ],
  templateUrl: './chats-list.component.html',
  styleUrl: './chats-list.component.css'
})
export class ChatsListComponent extends PaginationBaseComponent<Chat>{
  @Output() OnSelectChat = new EventEmitter<Chat>();

  constructor() {
    super();
  }

  public SelectChat(chat: Chat) {
    this.OnSelectChat.emit(chat)
  }
}
