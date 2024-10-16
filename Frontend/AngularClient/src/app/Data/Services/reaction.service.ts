import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {Reaction} from "../Models/Reaction/Reaction";
import {Observable} from "rxjs";
import {ApiConfig} from "../Consts/ApiConfig";
import {HttpHelper} from "../../Helpers/http-helper";
import {SendReaction} from "../Models/Reaction/Send-reaction";

@Injectable({
  providedIn: 'root'
})
export class ReactionService {
  constructor(private readonly _httpClient: HttpClient) { }

  public GetReactions(messageId: string, userBlogId: string): Observable<Reaction[]> {
    let params = new HttpParams()
    params = HttpHelper.AddUserBlogIdToQuery(params, userBlogId)
    return this._httpClient.get<Reaction[]>(`${ApiConfig.BaseUrl}/messages/${messageId}/reactions`, {params})
  }

  public SendReaction(messageId: string, userBlogId: string, sendReaction: SendReaction): Observable<Reaction> {
    let params = new HttpParams()
    params = HttpHelper.AddUserBlogIdToQuery(params, userBlogId)
    return this._httpClient.post<Reaction>(`${ApiConfig.BaseUrl}/messages/${messageId}/reactions`, sendReaction, {params})
  }

  public RemoveReaction(messageId: string, userBlogId: string, reactionId: string): Observable<Reaction> {
    let params = new HttpParams()
    params = HttpHelper.AddUserBlogIdToQuery(params, userBlogId)
    return this._httpClient.delete<Reaction>(`${ApiConfig.BaseUrl}/messages/${messageId}/reactions/${reactionId}`, {params})
  }
}
