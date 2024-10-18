import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {ChatItemComponent} from "../../chat-item/chat-item.component";
import {Chat} from "../../../../Data/Models/Chat/Chat";
import {NgForOf, NgIf} from "@angular/common";
import {
  PaginationBaseComponent
} from "../pagination-component-base/pagination-base.component";
import {ChatDetailsComponent} from "../../chat-details/chat-details.component";
import {CheckLoadingNecessaryFromBottom} from "../../../../Helpers/check-loading-necessary";

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
    this._loadingContainerId = 'chats-container'
    this.CheckLoadingNecessary = CheckLoadingNecessaryFromBottom
  }

  public SelectChat(chat: Chat) {
    this.OnSelectChat.emit(chat)
  }
}
