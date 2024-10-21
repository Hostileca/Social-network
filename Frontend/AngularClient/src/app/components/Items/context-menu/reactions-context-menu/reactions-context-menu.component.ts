import {Component, Input} from '@angular/core';
import {ContextMenuBaseComponent} from "../context-menu-base/context-menu-base.component";
import {NgForOf, NgIf} from "@angular/common";
import {ReactionsList} from "../../../../Helpers/reactions-list";

@Component({
  selector: 'app-reactions-context-menu',
  standalone: true,
  imports: [
    NgIf,
    NgForOf
  ],
  templateUrl: './reactions-context-menu.component.html',
  styleUrl: './reactions-context-menu.component.css'
})
export class ReactionsContextMenuComponent extends ContextMenuBaseComponent{
  @Input({required: true}) set OnReactionSelect(onReactionSelect: (emoji: string) => void){
    this._onReactionSelect = onReactionSelect
  }

  private _onReactionSelect?: (emoji: string) => void

  protected OnReactionClick(emoji: string) {
    if(this._onReactionSelect){
      this._onReactionSelect(emoji)
    }
  }

  protected ReactionList = ReactionsList
}
