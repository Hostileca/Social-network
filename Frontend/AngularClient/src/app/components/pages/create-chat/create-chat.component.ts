import { Component } from '@angular/core';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import { CurrentBlogService } from '../../../Data/Services/current-blog.service';
import {NgForOf} from "@angular/common";
import {Blog} from "../../../Data/Models/Blog/Blog";
import {SubscriptionService} from "../../../Data/Services/subscription.service";
import {ChatService} from "../../../Data/Services/chat.service";
import {PageSettings} from "../../../Data/Requests/PageSettings";

@Component({
  selector: 'app-create-chat-details',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    NgForOf
  ],
  templateUrl: './create-chat.component.html',
  styleUrl: './create-chat.component.css'
})
export class CreateChatComponent {
  public Form: FormGroup;

  public MyContacts: Blog[] = []


  constructor(private readonly _router: Router,
              private readonly _chatService: ChatService,
              private readonly _currentBlogService: CurrentBlogService,
              private readonly _subscriptionService: SubscriptionService) {
    this.Form = new FormGroup({
      userBlogId: new FormControl<string>(this._currentBlogService.CurrentBlog!.id, Validators.required),
      name: new FormControl<string>('', [Validators.required, Validators.minLength(1)]),
      otherMembers: new FormControl<string[]>([], Validators.minLength(1))
    })

    this.LoadMyContacts()
  }

  public LoadMyContacts(){
    this._currentBlogService.CurrentBlog!.subscribersCount

    let pageSettings: PageSettings = {
      pageNumber: 1,
      pageSize: this._currentBlogService.CurrentBlog!.subscribersCount
    }
    this._subscriptionService.GetBlogSubscribers(pageSettings, this._currentBlogService.CurrentBlog!.id).subscribe(subscribers => {
      subscribers.forEach(s => {this.MyContacts.push(s.blog)})
    })
    pageSettings.pageSize = this._currentBlogService.CurrentBlog!.subscriptionsCount
    this._subscriptionService.GetBlogSubscriptions(pageSettings, this._currentBlogService.CurrentBlog!.id).subscribe(subscriptions => {
      subscriptions.forEach(s => {this.MyContacts.push(s.blog)})
    })
  }

  public OnMemberSelect(memberId: string, isChecked: boolean ) {
    const otherMembers = this.Form.controls['otherMembers'].value as string[];
    if (isChecked) {
      this.Form.controls['otherMembers'].setValue([...otherMembers, memberId]);
    }
    else {
      this.Form.controls['otherMembers'].setValue(otherMembers.filter(id => id !== memberId));
    }
  }

  public OnCreateChat(){
    if(this.Form.valid){
      this._chatService.CreateChat(this.Form.value).subscribe({
        next: (chat) => {
          this._router.navigate([`/chat${chat.id}`])
        }
      })
    }
  }
}
