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

  public Form: FormGroup;

  constructor(private readonly _messageService: MessageService,
              private readonly _currentBlogService: CurrentBlogService) {
    this.Form = new FormGroup({
      text: new FormControl<string>('', [Validators.required, Validators.minLength(1)]),
      userBlogId: new FormControl<string>(this._currentBlogService.GetCurrentBlog().id, [Validators.required])
    });
  }

  public SendMessage() {
    this._messageService.SendMessage(this.ChatId, this.Form.value).subscribe({
        next: (response) => {
        },
        error: (error) => {
        }
      }
    )
  }
}
