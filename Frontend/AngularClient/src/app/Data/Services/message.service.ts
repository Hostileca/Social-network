import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Message} from "../Models/Message/Message";
import {ApiConfig} from "../Consts/ApiConfig";
import { SendMessage } from '../Models/Message/Send-message';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  constructor(private readonly _httpClient: HttpClient) { }

  public GetChatMessages(chatId: string, userBlogId: string): Observable<Message[]> {
    return this._httpClient.get<Message[]>(`${ApiConfig.BaseUrl}/chats/${chatId}/messages?userBlogId=${userBlogId}`)
  }

  public SendMessage(chatId: string, sendMessage: SendMessage): Observable<Message> {
    const formData = new FormData();
    formData.append('text', sendMessage.text);
    formData.append('userBlogId', sendMessage.userBlogId);
    if(sendMessage.attachments){
      for (const file of sendMessage.attachments) {

        formData.append('attachments', file);
      }
    }
    return this._httpClient.post<Message>(`${ApiConfig.BaseUrl}/chats/${chatId}/messages`, formData)
  }
}
