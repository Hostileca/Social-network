import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {Blog} from "../Models/Blog/Blog";
import {ApiConfig} from "../Consts/ApiConfig";
import {PageSettings} from "../Queries/PageSettings";
import {HttpHelper} from "../../Helpers/http-helper";
import {BlogUpdate} from "../Models/Blog/Blog-update";

@Injectable({
  providedIn: 'root'
})
export class BlogService {
  constructor(private readonly _httpClient: HttpClient) { }

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

  public GetBlogImageById(blogId: string): Observable<Blob> {
    return this._httpClient.get(`${ApiConfig.BaseUrl}/blogs/${blogId}/main-image`, { responseType: 'blob' });
  }

  public CreateBlog(blog: Blog): Observable<Blog> {
    return this._httpClient.post<Blog>(`${ApiConfig.BaseUrl}/blogs`, blog);
  }

  public PatchBlog(blogId: string, blogUpdate: BlogUpdate): Observable<Blog> {
    let form = new FormData();
    form.append('username', blogUpdate.username);

    if(blogUpdate.bio) {
      form.append('bio', blogUpdate.bio);
    }

    if(blogUpdate.mainImage){
      form.append('mainImage', blogUpdate.mainImage);
    }

    return this._httpClient.patch<Blog>(`${ApiConfig.BaseUrl}/blogs/${blogId}`, form);
  }
}
