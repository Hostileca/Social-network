import { Injectable } from '@angular/core';
import {ApiConfig} from "../Consts/ApiConfig";
import {Blog} from "../Models/Blog/Blog";
import {AuthService} from "../Services/auth.service";
import signalR, {IHttpConnectionOptions} from "@microsoft/signalr";


@Injectable({
  providedIn: 'root'
})
export class ChatHubService {
  private _hubConnection!: signalR.HubConnection;

  constructor(private readonly _authService: AuthService) {
  }

  public Connect(blog: Blog) {
    const accessToken = this._authService.Tokens?.accessToken.value;
    const options: IHttpConnectionOptions = {
      skipNegotiation: true,
      transport: signalR.HttpTransportType.WebSockets,
    };

    this._hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${ApiConfig.BaseHttpsUrl}/chatHub?userBlogId=${blog.id}`, options)
      .build();

    this.startConnection();
  }

  private startConnection() {
    this._hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.error('Error while starting connection: ' + err));
  }
}
