import {Component, Input} from '@angular/core';
import {CurrentBlogService} from "../../../Data/Services/current-blog.service";
import { Blog } from '../../../Data/Models/Blog/Blog';
import {Router} from "@angular/router";
import {NgIf, NgOptimizedImage} from "@angular/common";
import {BlogConfig} from "../../../Data/Consts/BlogConfig";
import {BlogService} from "../../../Data/Services/blog.service";

@Component({
  selector: 'app-my-blog-item',
  standalone: true,
  imports: [
    NgOptimizedImage,
    NgIf
  ],
  templateUrl: './my-blog-item.component.html',
  styleUrl: './my-blog-item.component.css'
})
export class MyBlogItemComponent {
  @Input() set blog(blog: Blog){
    this.Blog = blog
    this._blogService.GetBlogImageById(blog.id).subscribe({
      next: blob => {
        this.MainImageUrl = URL.createObjectURL(blob);
      }
    })
  }

  public Blog?: Blog
  public MainImageUrl?: string

  constructor(private readonly _router: Router,
              private readonly _currentBlogService: CurrentBlogService,
              private readonly _blogService: BlogService) {
  }

  public SelectBlog(){
    this._currentBlogService.SelectBlog(this.Blog)
    this._router.navigateByUrl("/my-chats")
  }

  protected readonly BlogConfig = BlogConfig;
}
