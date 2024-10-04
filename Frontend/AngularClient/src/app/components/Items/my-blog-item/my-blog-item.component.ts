import {Component, Input} from '@angular/core';
import {CurrentBlogService} from "../../../Data/Services/current-blog.service";
import { Blog } from '../../../Data/Models/Blog/Blog';
import {Router} from "@angular/router";
import {NgIf, NgOptimizedImage} from "@angular/common";
import {BlogConfig} from "../../../Data/Consts/BlogConfig";

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
  @Input() Blog!: Blog;

  constructor(private readonly _router: Router,
              private readonly _currentBlogService: CurrentBlogService) {
  }

  public SelectBlog(){
    this._currentBlogService.SelectBlog(this.Blog)
    this._router.navigateByUrl("/my-chats")
  }

  protected readonly BlogConfig = BlogConfig;
}
