import { Component } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {PostService} from "../../../Data/Services/post.service";
import {NgIf} from "@angular/common";

@Component({
  selector: 'app-create-post',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    NgIf
  ],
  templateUrl: './create-post.component.html',
  styleUrl: './create-post.component.css'
})
export class CreatePostComponent {
  public Form: FormGroup;
  private readonly _blogId;
  constructor(private readonly _route: ActivatedRoute,
              private readonly _postService: PostService) {
    this._blogId = _route.snapshot.params['blogId'];

    this.Form = new FormGroup({
      content: new FormControl<string>('', Validators.required),
      attachments: new FormControl<File[]>([]),
    })
  }

  public OnCreatePost(){
    this._postService.CreatePost(this._blogId, this.Form.value).subscribe({
      next: res => {
        console.log(res);
      },
      error: err => {
        console.log(err);
      }
    })
  }
}
