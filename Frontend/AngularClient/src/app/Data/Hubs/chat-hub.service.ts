import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import {ApiConfig} from "../Consts/ApiConfig";
import {Blog} from "../Models/Blog/Blog";
import {AuthService} from "../Services/auth.service";


@Injectable({
  providedIn: 'root'
})
export class ChatHubService {
  private _hubConnection!: signalR.HubConnection;

  constructor() {
  }

  public Connect(blog: Blog) {
    this._hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${ApiConfig.BaseUrl}/chatHub?blogId=${blog.id}`)
      .build();
  }

  private startConnection() {
    this._hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.error('Error while starting connection: ' + err));
  }
}
