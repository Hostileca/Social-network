import {Component, Input} from '@angular/core';
import {
  PaginationBaseComponent
} from "../pagination-component-base/pagination-base.component";
import {Message} from "../../../../Data/Models/Message/Message";
import {NgForOf} from "@angular/common";
import {MessageItemComponent} from "../../message-item/message-item.component";
import {Events} from "../../../../Data/Hubs/Events";
import {EventBusService} from "../../../../Data/Services/event-bus.service";
import {
  CheckLoadingNecessaryFromBottom,
  CheckLoadingNecessaryFromTop
} from "../../../../Helpers/check-loading-necessary";

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
    this._loadingContainerId = 'messages-container'
    this.CheckLoadingNecessary = CheckLoadingNecessaryFromTop
    this.StartListening()
  }

  @Input() set ChatId(chatId: string) {
    this._chatId = chatId
  }

  private _chatId!: string

  private StartListening(){
    this._eventBusService.On<Message>(Events.MessageSent).subscribe(message => {
      this.OnMessageReceive(message)
    })
  }

  private OnMessageReceive(message: Message){
    if(this._chatId && message.chatId == this._chatId){
      this.Entities.push(message)
    }
  }

  override ngOnChanges() {
    super.ngOnChanges();
  }

  // override OnLoadEntities(entities: Message[]) {
  //   entities = entities.reverse()
  //   this.Entities = [...entities, ...this.Entities]
  // }
}
