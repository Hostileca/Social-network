import {Component, EventEmitter, Input, Output} from '@angular/core';
import {ChatItemComponent} from "../chat-item/chat-item.component";
import {Chat} from "../../../Data/Models/Chat/Chat";
import {ChatService} from "../../../Data/Services/chat.service";
import {NgForOf, NgIf} from "@angular/common";

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
export class ChatsListComponent {
  @Input() Chats: Chat[] = []
  @Output() OnSelectChat = new EventEmitter<Chat>();

  public SelectChat(chat: Chat) {
    this.OnSelectChat.emit(chat)
  }
}
