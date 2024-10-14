import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {ChatItemComponent} from "../chat-item/chat-item.component";
import {Chat} from "../../../Data/Models/Chat/Chat";
import {NgForOf, NgIf} from "@angular/common";
import {
  PaginationComponentBaseComponent
} from "../../base/pagination-component-base/pagination-component-base.component";

@Component({
  selector: 'app-chats-list',
  standalone: true,
  imports: [
    ChatItemComponent,
    NgIf,
    NgForOf
  ],
  templateUrl: './chats-list.component.html',
  styleUrl: './chats-list.component.css'
})
export class ChatsListComponent extends PaginationComponentBaseComponent<Chat>{
  @Output() OnSelectChat = new EventEmitter<Chat>();

  constructor() {
    super();
  }

  public SelectChat(chat: Chat) {
    this.OnSelectChat.emit(chat)
  }
}
