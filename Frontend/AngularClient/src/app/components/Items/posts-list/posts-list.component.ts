import {Component} from '@angular/core';
import {Post} from "../../../Data/Models/Post/Post";
import {PostItemComponent} from "../post-item/post-item.component";
import {NgForOf, NgIf} from "@angular/common";
import {
  PaginationComponentBaseComponent
} from "../../base/pagination-component-base/pagination-component-base.component";

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
export class PostsListComponent extends PaginationComponentBaseComponent<Post>{
  constructor() {
    super()
  }
}
