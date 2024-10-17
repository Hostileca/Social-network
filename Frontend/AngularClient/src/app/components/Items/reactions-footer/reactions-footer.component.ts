import {Component, Input} from '@angular/core';
import {NgForOf, NgIf} from "@angular/common";
import {MessageItemComponent} from "../message-item/message-item.component";
import {Reaction} from "../../../Data/Models/Reaction/Reaction";
import {Events} from "../../../Data/Hubs/Events";
import {EventBusService} from "../../../Data/Services/event-bus.service";
import {ReactionService} from "../../../Data/Services/reaction.service";
import {CurrentBlogService} from "../../../Data/Services/current-blog.service";
import {Message} from "../../../Data/Models/Message/Message";

@Component({
  selector: 'app-reactions-footer',
  standalone: true,
    imports: [
        NgForOf,
        NgIf
    ],
  templateUrl: './reactions-footer.component.html',
  styleUrl: './reactions-footer.component.css'
})
export class ReactionsFooterComponent {
  @Input({required: true}) set MessageInput(message: Message) {
    this.Message = message
    this.LoadReactions()
    this.StartListening()
  }

  protected Message!: Message
  public Reactions: Reaction[] = []

  constructor(private readonly _reactionService: ReactionService,
              private readonly _eventBusService: EventBusService,
              private readonly _currentBlogService: CurrentBlogService) {}

  private LoadReactions(){
    this._reactionService.GetReactions(this.Message.id, this._currentBlogService.GetCurrentBlog().id).subscribe(reactions => {
      this.Reactions = reactions
    })
  }

  private StartListening(){
    this._eventBusService.On<Reaction>(Events.ReactionSent).subscribe({
      next: reaction => this.OnReactionSent(reaction)
    })

    this._eventBusService.On<Reaction>(Events.ReactionRemoved).subscribe({
      next: reaction => this.OnReactionRemoved(reaction)
    })
  }

  private OnReactionSent(reaction: Reaction){
    if(reaction.messageId == this.Message.id){
      this.Reactions = this.Reactions.filter(r => r.senderId != reaction.senderId)
      this.Reactions.push(reaction)
    }
  }

  private OnReactionRemoved(reaction: Reaction){
    console.log("check")
    this.Reactions = this.Reactions.filter(r => r.id != reaction.id)
  }

  public RemoveReaction(reaction: Reaction){
    if(!this.IsMyReaction(reaction)) return
    this._reactionService.RemoveReaction(this.Message.id, this._currentBlogService.GetCurrentBlog().id,
      reaction.id).subscribe()
  }

  public IsMyReaction(reaction: Reaction){
    return reaction.senderId == this._currentBlogService.GetCurrentBlog().id
  }
}
