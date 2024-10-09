import { Injectable } from '@angular/core';
import {Blog} from "../Models/Blog/Blog";
import {CookiesName} from "../Consts/CookiesName";
import {AppCookieService} from "./app-cookie.service";
import {BlogService} from "./blog.service";

@Injectable({
  providedIn: 'root'
})
export class CurrentBlogService {
  public CurrentBlog: Blog | null = null

  public IsBlogSelected(): boolean{
    if(!this.CurrentBlog){
      this.CurrentBlog = this._appCookieService.Get<Blog>(CookiesName.CurrentBlog)
    }
    let isBlogSelected = !!this.CurrentBlog
    if(isBlogSelected){
      this.UpdateBlog(this.CurrentBlog!.id)
    }
    return isBlogSelected
  }

  constructor(private readonly _appCookieService: AppCookieService,
              private readonly _blogService: BlogService
  ) { }

  public SelectBlog(blog: Blog){
    this.CurrentBlog = blog
    this._appCookieService.Save<Blog>(CookiesName.CurrentBlog, this.CurrentBlog)
  }

  public Logout(){
    this.CurrentBlog = null
    this._appCookieService.Delete(CookiesName.CurrentBlog)
  }

  private UpdateBlog(id: string){
    this._blogService.GetBlogById(id).subscribe({
      next: blog => {
        this.CurrentBlog = blog
      },
      error: error => {
        this.Logout()
      }
    })
  }
}
