import {Component, Input, OnInit} from '@angular/core';
import { Post } from '../../../Data/Models/Post/Post';
import {ChatItemComponent} from "../chat-item/chat-item.component";
import {NgForOf, NgIf} from "@angular/common";
import {ApiConfig} from "../../../Data/Consts/ApiConfig";
import {AttachmentService} from "../../../Data/Services/attachment.service";

@Component({
  selector: 'app-post-item',
  standalone: true,
  imports: [
    ChatItemComponent,
    NgForOf,
    NgIf
  ],
  templateUrl: './post-item.component.html',
  styleUrl: './post-item.component.css'
})
export class PostItemComponent implements OnInit{
  @Input() Post!: Post;
  public AttachmentsUrls: string[] = [];

  constructor(private readonly _attachmentService: AttachmentService) {
  }

  ngOnInit(): void {
    this.CreateAttachmentsUrl()
  }

  private CreateAttachmentsUrl(){
    for(let attachmentId of this.Post.attachmentsId){
      this._attachmentService.GetPostAttachment(attachmentId).subscribe(attachmentBlob => {
        const attachmentUrl = window.URL.createObjectURL(attachmentBlob)
        this.AttachmentsUrls.push(attachmentUrl)
      })
    }
  }

  protected readonly ApiConfig = ApiConfig;
}
