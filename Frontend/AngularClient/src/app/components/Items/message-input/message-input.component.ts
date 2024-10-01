import {Component, Input} from '@angular/core';
import { MessageService } from '../../../Data/Services/message.service';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {CurrentBlogService} from "../../../Data/Services/current-blog.service";
import {SendMessage} from "../../../Data/Models/Message/Send-message";

@Component({
  selector: 'app-message-input',
  standalone: true,
  imports: [
    FormsModule,
    ReactiveFormsModule
  ],
  templateUrl: './message-input.component.html',
  styleUrl: './message-input.component.css'
})
export class MessageInputComponent {
  @Input() ChatId!: string

  public Form: FormGroup = new FormGroup({
    text: new FormControl(null, [Validators.required, Validators.minLength(1)]),
  });

  constructor(private readonly _messageService: MessageService,
              private readonly _currentBlogService: CurrentBlogService) {
  }

  public SendMessage() {
    const sendMessage: SendMessage = {
      text: this.Form.value.text,
      userBlogId: this._currentBlogService.CurrentBlog!.id
    }
    this._messageService.SendMessage(this.ChatId, sendMessage).subscribe({
        next: (response) => {
        },
        error: (error) => {
        }
      }
    )
  }
}
