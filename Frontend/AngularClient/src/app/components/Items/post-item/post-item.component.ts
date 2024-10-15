import {Component, Input, OnInit} from '@angular/core';
import { Post } from '../../../Data/Models/Post/Post';
import {ChatItemComponent} from "../chat-item/chat-item.component";
import {DatePipe, NgForOf, NgIf, NgOptimizedImage} from "@angular/common";
import {ApiConfig} from "../../../Data/Consts/ApiConfig";
import {AttachmentService} from "../../../Data/Services/attachment.service";
import {Blog} from "../../../Data/Models/Blog/Blog";
import {BlogService} from "../../../Data/Services/blog.service";
import {BlogConfig} from "../../../Data/Consts/BlogConfig";

@Component({
  selector: 'app-post-item',
  standalone: true,
  imports: [
    ChatItemComponent,
    NgForOf,
    NgIf,
    NgOptimizedImage,
    DatePipe
  ],
  templateUrl: './post-item.component.html',
  styleUrl: './post-item.component.css'
})
export class PostItemComponent implements OnInit{
  @Input() Post!: Post;
  public Owner!: Blog
  public OwnerAvatarUrl: string = ''
  public AttachmentsUrls: string[] = [];

  constructor(private readonly _attachmentService: AttachmentService,
              private readonly _blogService: BlogService) {
  }

  ngOnInit(): void {
    this.CreateAttachmentsUrl()
    this.LoadOwner()
  }

  private CreateAttachmentsUrl(){
    for(let attachmentId of this.Post.attachmentsId){
      this._attachmentService.GetPostAttachment(attachmentId).subscribe(attachmentBlob => {
        const attachmentUrl = window.URL.createObjectURL(attachmentBlob)
        this.AttachmentsUrls.push(attachmentUrl)
      })
    }
  }

  private LoadOwner(){
    this._blogService.GetBlogById(this.Post.ownerId).subscribe({
      next: blog => {
        this.Owner = blog
        this.LoadOwnerImage()
      }
    })
  }

  private LoadOwnerImage(){
    this._blogService.GetBlogImageById(this.Owner.id).subscribe({
      next: blob => {
        this.OwnerAvatarUrl = URL.createObjectURL(blob);
      }
    })
  }

  protected readonly BlogConfig = BlogConfig;
}
