import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {Chat} from "../Models/Chat/Chat";
import { ApiConfig } from '../Consts/ApiConfig';
import {CurrentBlogService} from "./current-blog.service";
import { CreateChat } from '../Models/Chat/Create-chat';
import {HttpHelper} from "../../Helpers/http-helper";

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  constructor(private readonly _httpClient: HttpClient,
              private readonly currentBlogService: CurrentBlogService
  ) { }

  public GetMyChats(): Observable<Chat[]> {
    return this._httpClient.get<Chat[]>(`${ApiConfig.BaseUrl}/chats?userBlogId=${this.currentBlogService.CurrentBlog?.id}`);
  }

  public GetChatById(chatId: string, userBlogId: string): Observable<Chat> {
    let params = new HttpParams()
    params = HttpHelper.AddUserBlogIdToQuery(params, userBlogId)
    return this._httpClient.get<Chat>(`${ApiConfig.BaseUrl}/chats/${chatId}`, {params});
  }

  public CreateChat(chat: CreateChat): Observable<Chat> {
    return this._httpClient.post<Chat>(`${ApiConfig.BaseUrl}/chats`, chat);
  }
}
