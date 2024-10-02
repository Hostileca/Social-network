import { Component } from '@angular/core';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {BlogService} from "../../../Data/Services/blog.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-create-blog',
  standalone: true,
  imports: [
    FormsModule,
    ReactiveFormsModule
  ],
  templateUrl: './create-blog.component.html',
  styleUrl: './create-blog.component.css'
})
export class CreateBlogComponent {
  public Form: FormGroup;

  constructor(private readonly _blogService: BlogService,
              private readonly _router: Router){
    this.Form = new FormGroup({
      username: new FormControl<string>('', [Validators.required, Validators.minLength(4)])
    });
  }


  public OnSubmit(){
    if(!this.Form.valid){ return; }

    this._blogService.CreateBlog(this.Form.value).subscribe({
        next: (response) => {
          this._router.navigateByUrl('/my-blogs');
        },
        error: (error) => {
          console.error('Create blog failed', error);
        }
      }
    )
  }
}
