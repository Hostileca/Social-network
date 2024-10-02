import { Component } from '@angular/core';
import { Blog } from '../../../Data/Models/Blog/Blog';
import {BlogService} from "../../../Data/Services/blog.service";
import {NgForOf, NgIf} from "@angular/common";
import {BlogItemComponent} from "../../Items/blog-item/blog-item.component";

@Component({
  selector: 'app-blogs',
  standalone: true,
  imports: [
    NgIf,
    BlogItemComponent,
    NgForOf
  ],
  templateUrl: './blogs.component.html',
  styleUrl: './blogs.component.css'
})
export class BlogsComponent {
  public Blogs: Blog[] = []

  constructor(private readonly _blogService: BlogService) {
    _blogService.GetAllBlogs().subscribe(
      value => {
        this.Blogs = value
      }
    )
  }
}
