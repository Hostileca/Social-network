import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Blog} from "../Models/Responses/Blog";
import {ApiConfig} from "../Consts/ApiConfig";

@Injectable({
  providedIn: 'root'
})
export class BlogService {
  constructor(private readonly _httpClient: HttpClient,
  ) { }

  public GetUserBlogs(): Observable<Blog[]> {
    return this._httpClient.get<Blog[]>(`${ApiConfig.BaseUrl}/api/v1/blogs/me`);
  }
}
