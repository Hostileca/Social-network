import {Component, Input, OnInit} from '@angular/core';
import {PageSettings} from "../../../Data/Queries/PageSettings";
import {fromEvent, Observable, tap} from "rxjs";
import {Post} from "../../../Data/Models/Post/Post";
import {PostConfig} from "../../../Data/Consts/PostConfig";
import {CurrentBlogService} from "../../../Data/Services/current-blog.service";
import {PostItemComponent} from "../post-item/post-item.component";
import {NgForOf, NgIf} from "@angular/common";

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
export class PostsListComponent implements OnInit {
  @Input() PostsSource!: (pageSettings: PageSettings) => Observable<Post[]>

  public Posts: Post[] = []
  private _pageSettings: PageSettings = {
    pageSize: PostConfig.PostsLoadingSize,
    pageNumber: 1
  }
  private _loadingThreshold = 40;
  public IsLoading: boolean = false
  public IsEnded: boolean = false

  constructor() {
  }

  ngOnInit(): void {
    this.LoadPosts()

    fromEvent(document, 'scroll')
      .pipe(
        tap(() => {
          this.ReachingBottomHandler()
        })
      )
      .subscribe();
  }

  private ReachingBottomHandler(){
    let fullDocumentHeight = Math.max(
      document.body.scrollHeight,
      document.documentElement.scrollHeight,
      document.body.offsetHeight,
      document.documentElement.offsetHeight,
      document.body.clientHeight,
      document.documentElement.clientHeight
    );
    const haveIReachedBottom =
      fullDocumentHeight - this._loadingThreshold <
      window.scrollY + document.documentElement.clientHeight;
    if (haveIReachedBottom) {
      this.LoadPosts();
    }
  }

  public LoadPosts(){
    if (this.IsLoading || this.IsEnded) {
      console.log(this.IsLoading, this.IsEnded)
      return
    }

    this.IsLoading = true
    this.PostsSource(this._pageSettings).subscribe({
      next: posts => {
        this.Posts = [...this.Posts, ...posts]
        this.CheckIsPostsEnded(posts)
        this._pageSettings.pageNumber += 1
      },
      error: err => {
        console.error(err)
      },
      complete: () => {
        this.IsLoading = false
      }
    })
  }

  private CheckIsPostsEnded(posts: Post[]) {
    this.IsEnded = posts.length < PostConfig.PostsLoadingSize
  }

  onScroll(event: any) {
    const element = event.target;
    if (element.scrollHeight - element.scrollTop === element.clientHeight) {
      this.LoadPosts()
    }
  }
}
