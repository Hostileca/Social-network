import { Component } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import { BlogService } from '../../../Data/Services/blog.service';
import { Blog } from '../../../Data/Models/Blog/Blog';
import {DatePipe, NgForOf, NgIf, NgOptimizedImage} from "@angular/common";
import {SubscriptionService} from "../../../Data/Services/subscription.service";
import {CurrentBlogService} from "../../../Data/Services/current-blog.service";
import {SubscribeToBlog} from "../../../Data/Models/Subscription/Subscribe-to-blog";
import {BlogItemComponent} from "../../Items/blog-item/blog-item.component";
import {Subscriber} from "../../../Data/Models/Subscription/Subscriber";
import {Subscription} from "../../../Data/Models/Subscription/Subscription";
import {Post} from "../../../Data/Models/Post/Post";
import {PostService} from "../../../Data/Services/post.service";
import {PostItemComponent} from "../../Items/post-item/post-item.component";
import {BlogConfig} from "../../../Data/Consts/BlogConfig";
import {PageSettings} from "../../../Data/Queries/PageSettings";
import {PostsListComponent} from "../../Items/posts-list/posts-list.component";
import {Observable} from "rxjs";
import {AgePipe} from "../../../Data/Pipes/age.pipe";
import {isFunction} from "rxjs/internal/util/isFunction";

@Component({
  selector: 'app-blog',
  standalone: true,
  imports: [
    NgIf,
    BlogItemComponent,
    NgForOf,
    PostItemComponent,
    NgOptimizedImage,
    PostsListComponent,
    DatePipe,
    AgePipe
  ],
  templateUrl: './blog.component.html',
  styleUrl: './blog.component.css'
})
export class BlogComponent {
  public Blog!: Blog;
  public Subscribers: Subscriber[] = [];
  public Subscriptions: Subscription[] = [];
  public PostsSource: (pageSettings: PageSettings) => Observable<Post[]>
  protected BlogConfig = BlogConfig;

  constructor(private readonly _route: ActivatedRoute,
              private readonly _blogService: BlogService,
              private readonly _subscriptionService: SubscriptionService,
              private readonly _currentBlogService: CurrentBlogService,
              private readonly _postService: PostService) {
    const blogId = _route.snapshot.params['blogId'];

    this.PostsSource = (pageSettings: PageSettings) =>
      this._postService.GetBlogPosts(blogId, pageSettings)

    this.LoadBlog(blogId)
  }

  public AmISubscribed(): boolean{
    return this.Subscribers.some(x => x.blog.id === this._currentBlogService.GetCurrentBlog().id)
  }

  public IsMe(): boolean{
    return this.Blog.id === this._currentBlogService.GetCurrentBlog().id
  }

  private LoadBlog(blogId: string){
    this._blogService.GetBlogById(blogId).subscribe(blog => {
      this.Blog = blog
      this.LoadSubscribers()
      this.LoadSubscriptions()
    })
  }

  private LoadSubscribers(){
    const pageSettings: PageSettings = {
      pageNumber: 1,
      pageSize: this.Blog.subscribersCount
    }
     this._subscriptionService.GetBlogSubscribers(pageSettings, this.Blog.id).subscribe(subscribers => {
       this.Subscribers = subscribers
     })
  }

  private LoadSubscriptions(){
    const pageSettings: PageSettings = {
      pageNumber: 1,
      pageSize: this.Blog.subscriptionsCount
    }
     this._subscriptionService.GetBlogSubscriptions(pageSettings, this.Blog.id).subscribe(subscriptions => {
       this.Subscriptions = subscriptions
     })
  }

  public SubscribeToBlog(){
    const subscribeToBlog: SubscribeToBlog = {
      SubscribeAtId: this.Blog.id,
    }
    this._subscriptionService.SubscribeToBlog(this._currentBlogService.GetCurrentBlog().id, subscribeToBlog).subscribe({
        next: subscription =>{
          const mySubscription: Subscriber = {
            id: subscription.id,
            blog: this._currentBlogService.GetCurrentBlog()
          }
          this.Subscribers.push(mySubscription)
        },
        error: error => console.log(error)
    })
  }

  public UnsubscribeFromBlog(){
    const mySubscription = this.Subscribers.find(x => x.blog.id === this._currentBlogService.GetCurrentBlog().id)!
    this._subscriptionService.UnsubscribeFromBlog(this._currentBlogService.GetCurrentBlog().id, mySubscription.id).subscribe(subscription =>{
      this.Subscribers = this.Subscribers.filter(x => x.id !== subscription.id)
    })
  }

  protected readonly isFunction = isFunction;
}
