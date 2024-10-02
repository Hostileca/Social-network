import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Blog} from "../Models/Blog/Blog";
import {ApiConfig} from "../Consts/ApiConfig";

@Injectable({
  providedIn: 'root'
})
export class BlogService {
  constructor(private readonly _httpClient: HttpClient,
  ) { }

  public GetUserBlogs(): Observable<Blog[]> {
    return this._httpClient.get<Blog[]>(`${ApiConfig.BaseUrl}/blogs/me`);
  }

  public GetAllBlogs(): Observable<Blog[]> {
    return this._httpClient.get<Blog[]>(`${ApiConfig.BaseUrl}/blogs`);
  }

  public GetBlogById(id: string): Observable<Blog> {
    return this._httpClient.get<Blog>(`${ApiConfig.BaseUrl}/blogs/${id}`);
  }

  public CreateBlog(blog: Blog): Observable<Blog> {
    return this._httpClient.post<Blog>(`${ApiConfig.BaseUrl}/blogs`, blog);
  }
}
