import {Component} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {Chat} from "../../../Data/Models/Chat/Chat";
import {ChatService} from "../../../Data/Services/chat.service";
import {MessageService} from "../../../Data/Services/message.service";
import {Message} from "../../../Data/Models/Message/Message";
import {MessageItemComponent} from "../../Items/message-item/message-item.component";
import {NgForOf} from "@angular/common";
import {MessageInputComponent} from "../../Items/message-input/message-input.component";

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [
    MessageItemComponent,
    NgForOf,
    MessageInputComponent
  ],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css'
})
export class ChatComponent {
  public Chat!: Chat;
  public Messages: Message[] = [];

  constructor(private readonly _route: ActivatedRoute,
              private readonly _chatService: ChatService,
              private readonly _messageService: MessageService) {
   const chatId = _route.snapshot.params['id'];

   this.LoadChat(chatId)
   this.LoadMessages(chatId)
  }

  private LoadChat(chatId: string) {
    this._chatService.GetChatById(chatId).subscribe(chat => {
      this.Chat = chat
    })
  }

  private LoadMessages(chatId: string) {
    this._messageService.GetChatMessages(chatId).subscribe(messages => {
      this.Messages = messages
    })
  }
}
