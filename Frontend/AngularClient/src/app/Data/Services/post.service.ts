import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import { Observable } from 'rxjs';
import {Post} from "../Models/Post/Post";
import {ApiConfig} from "../Consts/ApiConfig";
import {PostCreate} from "../Models/Post/Post-create";

@Injectable({
  providedIn: 'root'
})
export class PostService {
  constructor(private readonly _httpClient: HttpClient) { }

  public GetBlogPosts(blogId: string): Observable<Post[]> {
    return this._httpClient.get<Post[]>(`${ApiConfig.BaseUrl}/blogs/${blogId}/posts`)
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
