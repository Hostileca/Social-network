import {Component, ElementRef, HostListener, Input, ViewChild} from '@angular/core';
import {NgIf} from "@angular/common";
import { Message } from '../../../../Data/Models/Message/Message';
import {ContextMenuBaseComponent} from "../context-menu-base/context-menu-base.component";
import {ReactionsContextMenuComponent} from "../reactions-context-menu/reactions-context-menu.component";
import {ReactionService} from "../../../../Data/Services/reaction.service";
import {CurrentBlogService} from "../../../../Data/Services/current-blog.service";
import { SendReaction } from '../../../../Data/Models/Reaction/Send-reaction';

@Component({
  selector: 'app-message-context-menu',
  standalone: true,
  imports: [
    NgIf,
    ReactionsContextMenuComponent
  ],
  templateUrl: './message-context-menu.component.html',
  styleUrl: './message-context-menu.component.css'
})
export class MessageContextMenuComponent extends ContextMenuBaseComponent {
  @Input({required: true}) Message?: Message
  @ViewChild(ReactionsContextMenuComponent) ReactionsContextMenu?: ReactionsContextMenuComponent
  protected OnReactionSelect!: (emoji: string) => void

  constructor(private readonly _reactionService: ReactionService,
              private readonly _currentBlogService: CurrentBlogService) {
    super();
    this.OnReactionSelect = (emoji: string) => this.SendReaction(emoji)
  }

  protected CopyMessage() {
    if (this.Message) {
      navigator.clipboard.writeText(this.Message.text)
    }
  }

  private SendReaction(emoji: string) {
    const sendReaction: SendReaction = {
      emoji: emoji
    }
    this._reactionService.SendReaction(this.Message!.id, this._currentBlogService.GetCurrentBlog().id, sendReaction).subscribe({
      next: (reaction) => console.log(reaction),
      error: (error) => console.log(error)
    })
  }

  protected ShowReactions(){
    this.ReactionsContextMenu?.ShowMenu(this.XPos + 100, this.YPos)
  }
}
