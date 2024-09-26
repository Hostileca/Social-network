import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import {AuthService} from "../../../Data/Services/auth.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-sign-in',
  standalone: true,
    imports: [
      ReactiveFormsModule
    ],
  templateUrl: './sign-in.component.html',
  styleUrl: './sign-in.component.css'
})
export class SignInComponent {
  constructor(private readonly _authService: AuthService,
              private readonly _router: Router
  ){}

  public Form: FormGroup = new FormGroup({
    email: new FormControl(null, [Validators.required, Validators.minLength(4)]),
    password: new FormControl(null, [Validators.required, Validators.minLength(4)])
  });

  public OnSubmit(){
    if(!this.Form.valid){ return; }

    this._authService.Login(this.Form.value).subscribe({
        next: (response) => {
          this._router.navigate(['/my-blogs']);
        },
        error: (error) => {
          console.error('Login failed', error);
        }
      }
    )
  }
}
