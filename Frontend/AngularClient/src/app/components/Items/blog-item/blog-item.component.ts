import {Component, Input} from '@angular/core';
import {Blog} from "../../../Data/Models/Blog/Blog";
import {CurrentBlogService} from "../../../Data/Services/current-blog.service";
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

  constructor(private readonly _currentBlogService: CurrentBlogService,
              private readonly _router: Router) {
  }

  public SelectBlog(){
    this._currentBlogService.SelectBlog(this.Blog)
    this._router.navigate(['/my-chats']);
  }
}
