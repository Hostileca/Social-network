import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Chat} from "../Models/Chat/Chat";
import { ApiConfig } from '../Consts/ApiConfig';
import {CurrentBlogService} from "./current-blog.service";

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

  public GetChatById(chatId: string): Observable<Chat> {
    return this._httpClient.get<Chat>(`${ApiConfig.BaseUrl}/chats/${chatId}`);
  }
}
