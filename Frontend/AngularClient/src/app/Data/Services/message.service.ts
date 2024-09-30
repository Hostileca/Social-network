import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {CurrentBlogService} from "./current-blog.service";
import {Observable} from "rxjs";
import {Message} from "../Models/Message/Message";
import {ApiConfig} from "../Consts/ApiConfig";
import { SendMessage } from '../Models/Message/Send-message';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor(private readonly _httpClient: HttpClient,
              private readonly currentBlogService: CurrentBlogService
  ) { }

  public GetChatMessages(chatId: string): Observable<Message[]> {
    return this._httpClient.get<Message[]>(`${ApiConfig.BaseUrl}/chats/${chatId}/messages`)
  }

  public SendMessage(chatId: string, sendMessage: SendMessage): Observable<Message> {
    return this._httpClient.post<Message>(`${ApiConfig.BaseUrl}/chats/${chatId}/messages`, sendMessage)
  }
}
