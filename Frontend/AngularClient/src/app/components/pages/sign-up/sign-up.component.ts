import { Component } from '@angular/core';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {AuthService} from "../../../Data/Services/auth.service";
import {SimpleValidators} from "../../../Helpers/SimpleValidators";
import {Router} from "@angular/router";

@Component({
  selector: 'app-sign-up',
  standalone: true,
    imports: [
        ReactiveFormsModule
    ],
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.css'
})
export class SignUpComponent {
  constructor(private readonly _authService: AuthService,
              private readonly _router: Router
  ) {}

  public Form: FormGroup = new FormGroup({
    username: new FormControl(null, [Validators.required, Validators.minLength(4)]),
    email: new FormControl(null, [Validators.required, Validators.minLength(4)]),
    password: new FormControl(null, [Validators.required, Validators.minLength(4)]),
    confirmPassword: new FormControl(null, [Validators.required, Validators.minLength(4), SimpleValidators.equalTo('password')])
  })

  public OnSubmit(){
    if(!this.Form.valid){ return; }

    this._authService.Register(this.Form.value).subscribe({
        next: (response) => {
          this._router.navigate(['/sign-in']);
        },
        error: (error) => {
          console.error('Registration failed', error);
        }
      }
    )
  }

}
