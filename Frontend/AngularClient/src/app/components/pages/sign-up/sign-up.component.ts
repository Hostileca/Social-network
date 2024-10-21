import { Component } from '@angular/core';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {AuthService} from "../../../Data/Services/auth.service";
import {SimpleValidators} from "../../../Helpers/simple-validators";
import {NgIf} from "@angular/common";

@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    NgIf
  ],
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.css'
})
export class SignUpComponent {
  constructor(private readonly _authService: AuthService) {
  }

  public Form: FormGroup = new FormGroup({
    username: new FormControl<string>('', [Validators.required, Validators.minLength(4)]),
    email: new FormControl<string>('', [Validators.required, Validators.email]),
    password: new FormControl<string>('', [Validators.required, Validators.minLength(4)]),
    confirmPassword: new FormControl<string>('', [Validators.required, SimpleValidators.EqualTo('password')])
  })

  public Username(){
    return this.Form.controls['username'];
  }

  public Email(){
    return this.Form.controls['email'];
  }

  public Password(){
    return this.Form.controls['password']
  }

  public ConfirmPassword(){
    return this.Form.controls['confirmPassword']
  }

  public OnSubmit(){
    if(!this.Form.valid){ return; }

    this._authService.Register(this.Form.value).subscribe({
        next: (response) => {
          console.log('Registration successful', response);
        },
        error: (error) => {
          console.error('Registration failed', error);
        }
      }
    )
  }

}
