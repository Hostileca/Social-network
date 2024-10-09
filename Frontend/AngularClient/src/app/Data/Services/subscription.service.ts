import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {ApiConfig} from "../Consts/ApiConfig";
import {SubscribeToBlog} from "../Models/Subscription/Subscribe-to-blog";
import {Subscriber} from "../Models/Subscription/Subscriber";
import {Subscription} from "../Models/Subscription/Subscription";
import {PageSettings} from "../Requests/PageSettings";
import {HttpHelper} from "../../Helpers/http-helper";

@Injectable({
  providedIn: 'root'
})
export class SubscriptionService {

  constructor(private readonly _httpClient: HttpClient)
  { }

  public GetBlogSubscribers(pageSettings: PageSettings, blogId: string): Observable<Subscriber[]> {
    let params = new HttpParams()
    params = HttpHelper.AddPageSettingsToQuery(params, pageSettings)
    return this._httpClient.get<Subscriber[]>(`${ApiConfig.BaseUrl}/blogs/${blogId}/subscribers`, {params})
  }

  public GetBlogSubscriptions(pageSettings: PageSettings, blogId: string): Observable<Subscription[]> {
    let params = new HttpParams()
    params = HttpHelper.AddPageSettingsToQuery(params, pageSettings)
    return this._httpClient.get<Subscription[]>(`${ApiConfig.BaseUrl}/blogs/${blogId}/subscriptions`, {params})
  }

  public SubscribeToBlog(blogId: string, subscribeToBlog: SubscribeToBlog): Observable<Subscription> {
    return this._httpClient.post<Subscription>(`${ApiConfig.BaseUrl}/blogs/${blogId}/subscriptions`, subscribeToBlog)
  }

  public UnsubscribeFromBlog(blogId: string, id: string): Observable<Subscription> {
    return this._httpClient.delete<Subscription>(`${ApiConfig.BaseUrl}/blogs/${blogId}/subscriptions/${id}`)
  }
}
