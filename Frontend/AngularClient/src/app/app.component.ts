import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {ChatHubService} from "./Data/Hubs/chat-hub.service";
import {HeaderComponent} from "./components/Items/header/header.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    HeaderComponent
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'AngularClient';

  constructor(private _chatHubService: ChatHubService){

  }
}
