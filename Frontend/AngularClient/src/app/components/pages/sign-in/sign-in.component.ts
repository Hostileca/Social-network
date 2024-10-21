import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import {AuthService} from "../../../Data/Services/auth.service";
import {Router} from "@angular/router";
import {NgIf} from "@angular/common";

@Component({
  selector: 'app-sign-in',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    NgIf
  ],
  templateUrl: './sign-in.component.html',
  styleUrl: './sign-in.component.css'
})
export class SignInComponent {
  constructor(private readonly _router: Router,
              private readonly _authService: AuthService
  ){}

  public Form: FormGroup = new FormGroup({
    email: new FormControl(null, [Validators.required, Validators.email]),
    password: new FormControl(null, [Validators.required, Validators.minLength(4)])
  });

  public Email(){
    return this.Form.controls['email'];
  }

  public Password(){
    return this.Form.controls['password']
  }

  public OnSubmit(){
    if(!this.Form.valid){ return; }

    this._authService.Login(this.Form.value).subscribe({
        next: (response) => {
          this._router.navigateByUrl("/my-blogs")
        },
        error: (error) => {
          console.error('Login failed', error);
        }
      }
    )
  }
}
