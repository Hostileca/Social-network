import { Component } from '@angular/core';
import {BlogService} from "../../../Data/Services/blog.service";
import {BlogItemComponent} from "../../Items/blog-item/blog-item.component";
import {NgForOf, NgIf} from "@angular/common";
import {Blog} from "../../../Data/Models/Blog/Blog";

@Component({
  selector: 'app-my-blogs',
  standalone: true,
  imports: [
    BlogItemComponent,
    NgIf,
    NgForOf
  ],
  templateUrl: './my-blogs.component.html',
  styleUrl: './my-blogs.component.css'
})
export class MyBlogsComponent {
  public Blogs: Blog[] = [];

  constructor(private readonly _blogService: BlogService
  ) {
    this._blogService.GetUserBlogs().subscribe(
      value => this.Blogs = value
    )
  }
}
