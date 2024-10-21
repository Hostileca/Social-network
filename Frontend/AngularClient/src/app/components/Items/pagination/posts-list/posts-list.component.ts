import {Component} from '@angular/core';
import {Post} from "../../../../Data/Models/Post/Post";
import {PostItemComponent} from "../../post-item/post-item.component";
import {NgForOf, NgIf} from "@angular/common";
import {
  PaginationBaseComponent
} from "../pagination-component-base/pagination-base.component";

@Component({
  selector: 'app-posts-list',
  standalone: true,
  imports: [
    PostItemComponent,
    NgForOf,
    NgIf
  ],
  templateUrl: './posts-list.component.html',
  styleUrl: './posts-list.component.css'
})
export class PostsListComponent extends PaginationBaseComponent<Post>{
  constructor() {
    super();
    this._loadingContainerId = 'posts-container'
  }
}
