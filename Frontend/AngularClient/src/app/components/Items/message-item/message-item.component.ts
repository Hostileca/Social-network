import {Component, Input, OnInit} from '@angular/core';
import {Message} from "../../../Data/Models/Message/Message";
import {ChatMember} from "../../../Data/Models/ChatMember/Chat-member";
import {BlogService} from "../../../Data/Services/blog.service";
import {Blog} from "../../../Data/Models/Blog/Blog";
import {DatePipe} from "@angular/common";

@Component({
  selector: 'app-message-item',
  standalone: true,
  imports: [
    DatePipe
  ],
  templateUrl: './message-item.component.html',
  styleUrl: './message-item.component.css'
})
export class MessageItemComponent implements OnInit {
  @Input() Message!: Message
  public Sender!: Blog

  constructor(private readonly _blogService: BlogService) {
  }

  ngOnInit(): void {
    this._blogService.GetBlogById(this.Message.senderId).subscribe(blog => {
      this.Sender = blog
    })
    console.log(this.Message)
  }
}
