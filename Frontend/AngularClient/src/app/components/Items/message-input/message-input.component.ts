import {Component, Input} from '@angular/core';
import { MessageService } from '../../../Data/Services/message.service';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {CurrentBlogService} from "../../../Data/Services/current-blog.service";

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
    this._messageService.SendMessage(this.ChatId,
      (this._currentBlogService.CurrentBlog?.id, this.Form.value)).subscribe(r => console.log(r))
  }
}
