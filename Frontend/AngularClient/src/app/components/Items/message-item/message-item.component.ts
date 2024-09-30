import {Component, Input} from '@angular/core';
import {Message} from "../../../Data/Models/Message/Message";

@Component({
  selector: 'app-message-item',
  standalone: true,
  imports: [],
  templateUrl: './message-item.component.html',
  styleUrl: './message-item.component.css'
})
export class MessageItemComponent {
  @Input() Message!: Message
}
