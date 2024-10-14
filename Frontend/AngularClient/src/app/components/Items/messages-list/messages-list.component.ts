import {Component, Input} from '@angular/core';
import {
  PaginationBaseComponent
} from "../../base/pagination-component-base/pagination-base.component";
import {Message} from "../../../Data/Models/Message/Message";
import {NgForOf} from "@angular/common";
import {MessageItemComponent} from "../message-item/message-item.component";
import {Events} from "../../../Data/Hubs/Events";
import {EventBusService} from "../../../Data/Services/event-bus.service";

@Component({
  selector: 'app-messages-list',
  standalone: true,
  imports: [
    NgForOf,
    MessageItemComponent
  ],
  templateUrl: './messages-list.component.html',
  styleUrl: './messages-list.component.css'
})
export class MessagesListComponent extends PaginationBaseComponent<Message>{
  constructor(private readonly _eventBusService: EventBusService) {
    super();
  }

  @Input() set ChatId(chatId: string) {
    this._chatId = chatId
    this.StartListening()
  }

  private _chatId!: string

  private StartListening(){
    this._eventBusService.On<Message>(Events.MessageSent).subscribe(message => {
      this.OnMessageReceive(message)
    })
  }

  private OnMessageReceive(message: Message){
    console.log(message)
    console.log(this._chatId)
    if(message.chatId == message.chatId){
      this.Entities.push(message)
    }
  }

  override ngOnChanges() {
    this.StartListening()
    super.ngOnChanges();
  }
}
