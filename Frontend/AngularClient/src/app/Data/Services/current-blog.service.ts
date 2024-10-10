import {Inject, Injectable} from '@angular/core';
import {Blog} from "../Models/Blog/Blog";
import {CookiesName} from "../Consts/CookiesName";
import {AppCookieService} from "./app-cookie.service";
import {BlogService} from "./blog.service";

@Injectable({
  providedIn: 'root'
})
export class CurrentBlogService {
  public CurrentBlog: Blog | null = null

  constructor(private readonly _appCookieService: AppCookieService,
              private readonly _blogService: BlogService) {
    if(!this.CurrentBlog){
      this.CurrentBlog = this._appCookieService.Get<Blog>(CookiesName.CurrentBlog)
    }
  }
  public IsBlogSelected(): boolean{
    return !!this.CurrentBlog
  }

  public SelectBlog(blog: Blog){
    this.CurrentBlog = blog
    this._appCookieService.Save<Blog>(CookiesName.CurrentBlog, this.CurrentBlog)
  }

  public Logout(){
    this.CurrentBlog = null
    this._appCookieService.Delete(CookiesName.CurrentBlog)
  }
}
