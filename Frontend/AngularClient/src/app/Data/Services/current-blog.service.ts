import { Injectable } from '@angular/core';
import {Blog} from "../Models/Blog/Blog";
import {CookiesName} from "../Consts/CookiesName";
import {HttpClient} from "@angular/common/http";
import {AppCookieService} from "./app-cookie.service";
import {ChatHubService} from "../Hubs/chat-hub.service";

@Injectable({
  providedIn: 'root'
})
export class CurrentBlogService {
  public CurrentBlog: Blog | null = null

  public IsBlogSelected(): boolean{
    if(!this.CurrentBlog){
      this.CurrentBlog = this._appCookieService.Get<Blog>(CookiesName.Blog)
    }
    return !!this.CurrentBlog
  }

  constructor(private readonly _appCookieService: AppCookieService,
              //private readonly _chatHubService: ChatHubService
  ) { }

  public SelectBlog(blog: Blog){
    this.CurrentBlog = blog
    this._appCookieService.Save<Blog>(CookiesName.Blog, this.CurrentBlog)

    //this._chatHubService.Connect(this.CurrentBlog)
  }

  public Logout(){
    this.CurrentBlog = null
    this._appCookieService.Delete(CookiesName.Blog)
  }
}
