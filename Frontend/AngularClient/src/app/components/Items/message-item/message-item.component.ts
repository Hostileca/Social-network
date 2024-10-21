import {Component, HostListener, Input, OnInit, ViewChild} from '@angular/core';
import {Message} from "../../../Data/Models/Message/Message";
import {ChatMember} from "../../../Data/Models/ChatMember/Chat-member";
import {BlogService} from "../../../Data/Services/blog.service";
import {Blog} from "../../../Data/Models/Blog/Blog";
import {DatePipe, NgForOf, NgIf, NgOptimizedImage} from "@angular/common";
import {CurrentBlogService} from "../../../Data/Services/current-blog.service";
import {BlogConfig} from "../../../Data/Consts/BlogConfig";
import {MessageContextMenuComponent} from "../context-menu/message-context-menu/message-context-menu.component";
import {Reaction} from "../../../Data/Models/Reaction/Reaction";
import {ReactionService} from "../../../Data/Services/reaction.service";
import {EventBusService} from "../../../Data/Services/event-bus.service";
import {Events} from "../../../Data/Hubs/Events";
import {ReactionsFooterComponent} from "../reactions-footer/reactions-footer.component";
import {AttachmentService} from "../../../Data/Services/attachment.service";
import {Attachment} from "../../../Data/Models/Attachment/Attachment";

@Component({
  selector: 'app-message-item',
  standalone: true,
  imports: [
    DatePipe,
    NgIf,
    NgOptimizedImage,
    MessageContextMenuComponent,
    NgForOf,
    ReactionsFooterComponent
  ],
  templateUrl: './message-item.component.html',
  styleUrl: './message-item.component.css'
})
export class MessageItemComponent{
  @ViewChild(MessageContextMenuComponent) ContextMenu!: MessageContextMenuComponent;

  @Input() set MessageInput(message: Message){
    this.Message = message
    this.LoadSender()
    this.LoadAttachments()
  }

  public Message!: Message
  public Sender!: Blog
  public Attachments: Attachment[] = []

  constructor(private readonly _blogService: BlogService,
              private readonly _currentBlogService: CurrentBlogService,
              private readonly _attachmentsService: AttachmentService) {
  }

  public IsMe() : boolean{
    return this._currentBlogService.GetCurrentBlog().id == this.Message.senderId
  }

  public OnRightClick(event: MouseEvent) {
    event.preventDefault();
    if(this.ContextMenu){
      this.ContextMenu.ShowMenu(event.clientX, event.clientY);
    }
  }

  private LoadSender(){
    this._blogService.GetBlogById(this.Message.senderId).subscribe(blog => {
      this.Sender = blog
    })
  }

  private LoadAttachments(){
    this.Message.attachmentsId.forEach(id =>{
      this._attachmentsService.GetMessageAttachment(this.Message.chatId, this.Message.id, id,
          this._currentBlogService.GetCurrentBlog().id).subscribe({
        next: attachment => {
          this.Attachments.push(attachment)
        }
      })
    })
  }

  protected readonly BlogConfig = BlogConfig;
}
