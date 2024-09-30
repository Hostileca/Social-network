import {Component, Input} from '@angular/core';
import { Chat } from '../../../Data/Models/Chat/Chat';
import {Router} from "@angular/router";

@Component({
  selector: 'app-chat-item',
  standalone: true,
  imports: [],
  templateUrl: './chat-item.component.html',
  styleUrl: './chat-item.component.css'
})
export class ChatItemComponent {
  @Input() Chat!: Chat

  constructor(private readonly _router: Router) {
  }
}
