import { Component } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {Blog} from "../../../Data/Models/Blog/Blog";
import {BlogService} from "../../../Data/Services/blog.service";
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {SimpleValidators} from "../../../Helpers/simple-validators";
import {blogGuard} from "../../../Guards/blog.guard";

@Component({
  selector: 'app-blog-edit',
  standalone: true,
  imports: [
    FormsModule,
    ReactiveFormsModule
  ],
  templateUrl: './blog-edit.component.html',
  styleUrl: './blog-edit.component.css'
})
export class BlogEditComponent {
  public Blog!: Blog

  public Form: FormGroup = new FormGroup({
    username: new FormControl<string>('', [Validators.required, Validators.minLength(1)]),
    bio: new FormControl<string>(''),
    mainImage: new FormControl<File | null>(null, [SimpleValidators.ImageFileValidator]),
    dateOfBirth: new FormControl<Date>(new Date())
  })

  constructor(private readonly _route: ActivatedRoute,
              private readonly _blogService: BlogService) {
    const blogId = _route.snapshot.params['blogId'];

    this.LoadBlog(blogId)
  }

  private LoadBlog(blogId: string){
    this._blogService.GetBlogById(blogId).subscribe(blog => {
      this.Blog = blog

      this.Form.controls['username'].setValue(this.Blog.username)
      this.Form.controls['bio'].setValue(this.Blog.bio)
      this.Form.controls['dateOfBirth'].setValue(this.Blog.dateOfBirth)
    })
  }

  public OnSubmit(){
    if(this.Form.valid){
      this._blogService.PatchBlog(this.Blog.id, this.Form.value).subscribe({
        next: blog => {
          this.Blog = blog
        },
        error: err => console.error(err)
      })
    }
  }
}
