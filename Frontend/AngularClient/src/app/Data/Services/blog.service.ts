import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {Blog} from "../Models/Blog/Blog";
import {ApiConfig} from "../Consts/ApiConfig";
import {PageSettings} from "../Requests/PageSettings";
import {HttpHelper} from "../../Helpers/http-helper";

@Injectable({
  providedIn: 'root'
})
export class BlogService {
  constructor(private readonly _httpClient: HttpClient,
  ) { }

  public GetUserBlogs(): Observable<Blog[]> {
    return this._httpClient.get<Blog[]>(`${ApiConfig.BaseUrl}/blogs/me`);
  }

  public GetAllBlogs(pageSettings: PageSettings): Observable<Blog[]> {
    let params = new HttpParams()
    params = HttpHelper.AddPageSettingsToQuery(params, pageSettings)
    return this._httpClient.get<Blog[]>(`${ApiConfig.BaseUrl}/blogs`, { params });
  }

  public GetBlogById(id: string): Observable<Blog> {
    return this._httpClient.get<Blog>(`${ApiConfig.BaseUrl}/blogs/${id}`);
  }

  public CreateBlog(blog: Blog): Observable<Blog> {
    return this._httpClient.post<Blog>(`${ApiConfig.BaseUrl}/blogs`, blog);
  }
}
