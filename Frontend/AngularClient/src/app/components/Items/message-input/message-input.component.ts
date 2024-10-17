import {Component, Input, ViewChild} from '@angular/core';
import {MessageService} from '../../../Data/Services/message.service';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {CurrentBlogService} from "../../../Data/Services/current-blog.service";
import {
  SendMessageContextMenuComponent
} from "../context-menu/send-message-context-menu/send-message-context-menu.component";
import {SimpleValidators} from "../../../Helpers/simple-validators";
import {NgIf} from "@angular/common";
import {MessageTypes} from "../context-menu/send-message-context-menu/message-types";

@Component({
  selector: 'app-message-input',
  standalone: true,
  imports: [
    FormsModule,
    ReactiveFormsModule,
    SendMessageContextMenuComponent,
    NgIf
  ],
  templateUrl: './message-input.component.html',
  styleUrl: './message-input.component.css'
})
export class MessageInputComponent {
  @Input() ChatId!: string
  @ViewChild(SendMessageContextMenuComponent) private SendMessageContextMenu!: SendMessageContextMenuComponent

  public Form: FormGroup;

  constructor(protected readonly _messageService: MessageService,
              protected readonly _currentBlogService: CurrentBlogService) {
    this.Form = new FormGroup({
      text: new FormControl<string>('', [Validators.required, Validators.minLength(1)]),
      schedule: new FormControl<Date | null>(null, [SimpleValidators.IsFutureDate])
    });
  }

  protected get MessageType() {
    if(this.SendMessageContextMenu){
      return this.SendMessageContextMenu.MessageType
    }
    return MessageTypes.simple
  }

  public SendMessage() {
    if(this.Form.invalid){
      return
    }

    this._messageService.SendMessage(this.ChatId, this._currentBlogService.GetCurrentBlog().id,
      this.Form.value).subscribe({
        next: (response) => {
        },
        error: (error) => {
        }
      }
    )

    this.Form.reset();
  }

  public SendDelayedMessage(){
    if(this.Form.invalid || !this.Form.value.schedule){
      return
    }

    this._messageService.SendDelayedMessage(this.ChatId, this._currentBlogService.GetCurrentBlog().id,
      this.Form.value).subscribe({
        next: (response) => {
        },
        error: (error) => {
        }
      }
    )

    this.Form.reset();
  }

  public OpenContextMenu(event: MouseEvent) {
    if(this.SendMessageContextMenu){
      event.preventDefault();
      this.SendMessageContextMenu.ShowMenu(event.clientX - 120, event.clientY - 80);
    }
  }

  protected readonly MessageTypes = MessageTypes;
  protected readonly Today = new Date().toISOString().slice(0, 16);
}
