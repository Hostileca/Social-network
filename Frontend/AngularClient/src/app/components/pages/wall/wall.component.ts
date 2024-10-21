import { Component } from '@angular/core';
import {Post} from "../../../Data/Models/Post/Post";
import {PostService} from "../../../Data/Services/post.service";
import {NgForOf} from "@angular/common";
import {PostItemComponent} from "../../Items/post-item/post-item.component";
import {PageSettings} from "../../../Data/Queries/PageSettings";
import {PaginationConfig} from "../../../Data/Consts/PaginationConfig";
import {CurrentBlogService} from "../../../Data/Services/current-blog.service";
import {PostsListComponent} from "../../Items/pagination/posts-list/posts-list.component";
import {Observable} from "rxjs";

@Component({
  selector: 'app-wall',
  standalone: true,
  imports: [
    NgForOf,
    PostItemComponent,
    PostsListComponent
  ],
  templateUrl: './wall.component.html',
  styleUrl: './wall.component.css'
})
export class WallComponent {
  public PostsSource: (pageSettings: PageSettings) => Observable<Post[]>

  constructor(private readonly _postService: PostService,
              private readonly _currentBlogService: CurrentBlogService) {
    this.PostsSource = (pageSettings: PageSettings) =>
      this._postService.GetWall(_currentBlogService.GetCurrentBlog().id, pageSettings);
  }

  protected readonly PaginationConfig = PaginationConfig;
}
