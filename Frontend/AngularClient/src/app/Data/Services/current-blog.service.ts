import {Inject, Injectable} from '@angular/core';
import {Blog} from "../Models/Blog/Blog";
import {CookiesName} from "../Consts/CookiesName";
import {AppCookieService} from "./app-cookie.service";
import {BlogService} from "./blog.service";

@Injectable({
  providedIn: 'root'
})
export class CurrentBlogService {
  private _currentBlog: Blog | null = null

  constructor(private readonly _appCookieService: AppCookieService,
              private readonly _blogService: BlogService) {
    if(!this._currentBlog){
      this._currentBlog = this._appCookieService.Get<Blog>(CookiesName.CurrentBlog)
    }
  }

  public IsBlogSelected(): boolean{
    return !!this._currentBlog
  }

  public GetCurrentBlog(): Blog{
    return this._currentBlog!
  }

  public SelectBlog(blog?: Blog){
    if(!blog){
      return
    }
    this._currentBlog = blog
    this._appCookieService.Save<Blog>(CookiesName.CurrentBlog, this._currentBlog)
  }

  public Logout(){
    this._currentBlog = null
    this._appCookieService.Delete(CookiesName.CurrentBlog)
  }
}
