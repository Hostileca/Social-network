import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {ApiConfig} from "../Consts/ApiConfig";
import {SubscribeToBlog} from "../Models/Subscription/Subscribe-to-blog";
import { UnsubscribeFromBlog } from '../Models/Subscription/Unsubscribe-from-blog';
import { Blog } from '../Models/Blog/Blog';
import {Subscriber} from "../Models/Subscription/Subscriber";
import {Subscription} from "../Models/Subscription/Subscription";

@Injectable({
  providedIn: 'root'
})
export class SubscriptionService {

  constructor(private readonly _httpClient: HttpClient)
  { }

  public GetBlogSubscribers(blogId: string): Observable<Subscriber[]> {
    return this._httpClient.get<Blog[]>(`${ApiConfig.BaseUrl}/blogs/${blogId}/subscribers`)
  }

  public GetBlogSubscriptions(blogId: string): Observable<Subscription[]> {
    return this._httpClient.get<Blog[]>(`${ApiConfig.BaseUrl}/blogs/${blogId}/subscriptions`)
  }

  public SubscribeToBlog(blogId: string, subscribeToBlog: SubscribeToBlog): Observable<Blog[]> {
    return this._httpClient.post<Blog[]>(`${ApiConfig.BaseUrl}/blogs/${blogId}/subscriptions`, subscribeToBlog)
  }

  public UnsubscribeFromBlog(blogId: string, unsubscribeToBlog: UnsubscribeFromBlog): Observable<Blog[]> {
    return this._httpClient.post<Blog[]>(`${ApiConfig.BaseUrl}/blogs/${blogId}/subscriptions`, unsubscribeToBlog)
  }
}
