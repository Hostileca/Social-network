import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import { Observable } from 'rxjs';
import {Post} from "../Models/Post/Post";
import {ApiConfig} from "../Consts/ApiConfig";
import {PostCreate} from "../Models/Post/Post-create";
import {PageSettings} from "../Requests/PageSettings";
import {HttpHelper} from "../../Helpers/http-helper";

@Injectable({
  providedIn: 'root'
})
export class PostService {
  constructor(private readonly _httpClient: HttpClient) { }

  public GetBlogPosts(pageSettings: PageSettings, blogId: string): Observable<Post[]> {
    let params = new HttpParams()
    params = HttpHelper.AddPageSettingsToQuery(params, pageSettings)
    return this._httpClient.get<Post[]>(`${ApiConfig.BaseUrl}/blogs/${blogId}/posts`, {params})
  }

  public CreatePost(blogId: string, postCreate: PostCreate): Observable<Post> {
    const formData = new FormData();
    formData.append('content', postCreate.content);
    if(postCreate.attachments){
      for (const file of postCreate.attachments) {
        formData.append('attachments', file);
      }
    }
    return this._httpClient.post<Post>(`${ApiConfig.BaseUrl}/blogs/${blogId}/posts`, formData)
  }
}
