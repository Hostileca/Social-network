import { Component } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import { BlogService } from '../../../Data/Services/blog.service';
import { Blog } from '../../../Data/Models/Blog/Blog';
import {NgForOf, NgIf} from "@angular/common";
import {SubscriptionService} from "../../../Data/Services/subscription.service";
import {CurrentBlogService} from "../../../Data/Services/current-blog.service";
import {SubscribeToBlog} from "../../../Data/Models/Subscription/Subscribe-to-blog";
import { UnsubscribeFromBlog } from '../../../Data/Models/Subscription/Unsubscribe-from-blog';
import {BlogItemComponent} from "../../Items/blog-item/blog-item.component";

@Component({
  selector: 'app-blog',
  standalone: true,
  imports: [
    NgIf,
    BlogItemComponent,
    NgForOf
  ],
  templateUrl: './blog.component.html',
  styleUrl: './blog.component.css'
})
export class BlogComponent {
  public Blog!: Blog;
  public Subscribers!: Blog[];
  public Subscriptions!: Blog[];

  constructor(private readonly _route: ActivatedRoute,
              private readonly _blogService: BlogService,
              private readonly _subscriptionService: SubscriptionService,
              private readonly _currentBlogService: CurrentBlogService) {
    const blogId = _route.snapshot.params['id'];

    this.LoadBlog(blogId)
    this.LoadSubscribers(blogId)
    this.LoadSubscriptions(blogId)
  }

  public AmISubscribed(): boolean{
    return this.Subscribers.some(x => x.id === this._currentBlogService.CurrentBlog!.id)
  }

  public IsMe(): boolean{
    return this.Blog.id === this._currentBlogService.CurrentBlog!.id
  }

  private LoadBlog(blogId: string){
    this._blogService.GetBlogById(blogId).subscribe(blog => {
      this.Blog = blog
    })
  }

  private LoadSubscribers(blogId: string){
    this._subscriptionService.GetBlogSubscribers(blogId).subscribe(subscribers => {
      this.Subscribers = subscribers
    })
  }

  private LoadSubscriptions(blogId: string){
    this._subscriptionService.GetBlogSubscriptions(blogId).subscribe(subscriptions => {
      this.Subscriptions = subscriptions
    })
  }

  public SubscribeToBlog(){
    const subscribeToBlog: SubscribeToBlog = {
      SubscribeAtId: this.Blog.id
    }
    this._subscriptionService.SubscribeToBlog(this._currentBlogService.CurrentBlog!.id, subscribeToBlog).subscribe(() =>{
      this.Subscribers.push(this._currentBlogService.CurrentBlog!)
    })
  }

  public UnsubscribeFromBlog(){
    const unsubscribeToBlog: UnsubscribeFromBlog = {
      unSubscribeFromId: this.Blog.id
    }
    this._subscriptionService.UnsubscribeFromBlog(this._currentBlogService.CurrentBlog!.id, unsubscribeToBlog).subscribe(() =>{
      this.Subscribers = this.Subscribers.filter(x => x.id !== this._currentBlogService.CurrentBlog!.id)
    })
  }
}
