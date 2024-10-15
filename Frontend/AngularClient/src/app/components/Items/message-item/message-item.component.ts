import {Component, Input, OnInit} from '@angular/core';
import {Message} from "../../../Data/Models/Message/Message";
import {ChatMember} from "../../../Data/Models/ChatMember/Chat-member";
import {BlogService} from "../../../Data/Services/blog.service";
import {Blog} from "../../../Data/Models/Blog/Blog";
import {DatePipe, NgIf, NgOptimizedImage} from "@angular/common";
import {CurrentBlogService} from "../../../Data/Services/current-blog.service";
import {BlogConfig} from "../../../Data/Consts/BlogConfig";

@Component({
  selector: 'app-message-item',
  standalone: true,
  imports: [
    DatePipe,
    NgIf,
    NgOptimizedImage
  ],
  templateUrl: './message-item.component.html',
  styleUrl: './message-item.component.css'
})
export class MessageItemComponent implements OnInit {
  @Input() Message!: Message
  public Sender!: Blog

  constructor(private readonly _blogService: BlogService,
              private readonly _currentBlogService: CurrentBlogService) {
  }

  ngOnInit(): void {
    this._blogService.GetBlogById(this.Message.senderId).subscribe(blog => {
      this.Sender = blog
    })
  }

  public IsMe() : boolean{
    return this._currentBlogService.GetCurrentBlog().id == this.Message.senderId
  }

  protected readonly BlogConfig = BlogConfig;
}
