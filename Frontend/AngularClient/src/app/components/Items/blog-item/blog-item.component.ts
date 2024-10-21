import { Component, Input } from '@angular/core';
import { Blog } from '../../../Data/Models/Blog/Blog';
import {NgIf, NgOptimizedImage} from "@angular/common";
import {BlogConfig} from "../../../Data/Consts/BlogConfig";
import {BlogService} from "../../../Data/Services/blog.service";

@Component({
  selector: 'app-blog-item',
  standalone: true,
  imports: [
    NgOptimizedImage,
    NgIf
  ],
  templateUrl: './blog-item.component.html',
  styleUrl: './blog-item.component.css'
})
export class BlogItemComponent {
  @Input() set blog(blog: Blog){
    this.Blog = blog
    this._blogService.GetBlogImageById(blog.id).subscribe({
      next: blob => {
        this.MainImageUrl = URL.createObjectURL(blob);
      }
    })
  }

  protected BlogConfig = BlogConfig;

  public Blog?: Blog;
  public MainImageUrl?: string;

  constructor(private readonly _blogService: BlogService) {

  }
}
