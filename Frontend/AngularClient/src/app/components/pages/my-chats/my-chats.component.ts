import { Component } from '@angular/core';
import {Chat} from "../../../Data/Models/Chat/Chat";
import {ChatService} from "../../../Data/Services/chat.service";
import {BlogItemComponent} from "../../Items/blog-item/blog-item.component";
import {NgForOf, NgIf} from "@angular/common";
import {ChatItemComponent} from "../../Items/chat-item/chat-item.component";

@Component({
  selector: 'app-my-chats',
  standalone: true,
  imports: [
    BlogItemComponent,
    NgForOf,
    NgIf,
    ChatItemComponent
  ],
  templateUrl: './my-chats.component.html',
  styleUrl: './my-chats.component.css'
})
export class MyChatsComponent {
  public Chats: Chat[] = []

  constructor(private readonly _chatService: ChatService) {
    this._chatService.GetMyChats().subscribe(
      value => {
        this.Chats = value
      }
    )
  }
}
