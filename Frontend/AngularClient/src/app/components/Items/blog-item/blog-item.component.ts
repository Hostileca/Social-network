import {Component, Input} from '@angular/core';
import {Blog} from "../../../Data/Models/Responses/Blog";
import {CurrentBlogService} from "../../../Data/Services/current-blog.service";

@Component({
  selector: 'app-blog-item',
  standalone: true,
  imports: [],
  templateUrl: './blog-item.component.html',
  styleUrl: './blog-item.component.css'
})
export class BlogItemComponent {
  @Input() Blog!: Blog;

  constructor(private readonly _currentBlogService: CurrentBlogService) {
  }

  public SelectBlog(){
    this._currentBlogService.SelectBlog(this.Blog)
  }
}
