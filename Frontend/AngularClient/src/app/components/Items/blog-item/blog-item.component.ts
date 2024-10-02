import {Component, Input} from '@angular/core';
import {CurrentBlogService} from "../../../Data/Services/current-blog.service";
import { Blog } from '../../../Data/Models/Blog/Blog';
import {Router} from "@angular/router";

@Component({
  selector: 'app-blog-item',
  standalone: true,
  imports: [],
  templateUrl: './blog-item.component.html',
  styleUrl: './blog-item.component.css'
})
export class BlogItemComponent {
  @Input() Blog!: Blog;

  constructor(private readonly _router: Router,
              private readonly _currentBlogService: CurrentBlogService) {
  }

  public SelectBlog(){
    this._currentBlogService.SelectBlog(this.Blog)
    this._router.navigateByUrl("/my-chats")
  }
}
