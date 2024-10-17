import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {Message} from "../Models/Message/Message";
import {ApiConfig} from "../Consts/ApiConfig";
import { SendMessage } from '../Models/Message/Send-message';
import {HttpHelper} from "../../Helpers/http-helper";
import {PageSettings} from "../Queries/PageSettings";
import {SendDelayedMessage} from "../Models/Message/Send-delayed-message";
import {DelayedMessage} from "../Models/Message/Delayed-message";

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  constructor(private readonly _httpClient: HttpClient) { }

  public GetChatMessages(chatId: string, userBlogId: string, pageSettings: PageSettings): Observable<Message[]> {
    let params = new HttpParams()
    params = HttpHelper.AddPageSettingsToQuery(params, pageSettings)
    params = HttpHelper.AddUserBlogIdToQuery(params, userBlogId)
    return this._httpClient.get<Message[]>(`${ApiConfig.BaseUrl}/chats/${chatId}/messages`, {params})
  }

  public SendMessage(chatId: string, userBlogId: string, sendMessage: SendMessage): Observable<Message> {
    let formData = new FormData();
    formData = this.FillSendMessageForm(formData, sendMessage)

    let params = new HttpParams()
    params = HttpHelper.AddUserBlogIdToQuery(params, userBlogId)
    return this._httpClient.post<Message>(`${ApiConfig.BaseUrl}/chats/${chatId}/messages`, formData, {params})
  }

  public SendDelayedMessage(chatId: string, userBlogId: string, sendDelayedMessage: SendDelayedMessage): Observable<Message> {
    let formData = new FormData();
    formData = this.FillSendMessageForm(formData, sendDelayedMessage)
    const now = new Date(Date.now());

    const dateTimeOffset = new Date(now.getTime() - sendDelayedMessage.Date.getTime())
    formData.append('delay', dateTimeOffset.toString())

    let params = new HttpParams()
    params = HttpHelper.AddUserBlogIdToQuery(params, userBlogId)

    return this._httpClient.post<DelayedMessage>(`${ApiConfig.BaseUrl}/chats/${chatId}/delayed-messages`, formData, {params})
  }

  private FillSendMessageForm(formData: FormData, sendMessage: SendMessage) {
    formData.append('text', sendMessage.text);

    if(sendMessage.attachments){
      for (const file of sendMessage.attachments) {
        formData.append('attachments', file);
      }
    }

    return formData
  }
}
