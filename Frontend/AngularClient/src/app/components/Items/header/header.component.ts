import { Component } from '@angular/core';
import {AuthService} from "../../../Data/Services/auth.service";
import {CurrentBlogService} from "../../../Data/Services/current-blog.service";
import {NgIf} from "@angular/common";
import {Router} from "@angular/router";

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    NgIf
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  public IsBlogMenuOpen = false;

  constructor(private readonly _authService: AuthService,
              private readonly _currentBlogService: CurrentBlogService,
              private readonly _router: Router) {
  }

  public IsLoggedIn(): boolean {
    return this._authService.IsAuth() && this._currentBlogService.IsBlogSelected()
  }

  public Logout(): void {
    this._authService.Logout()
    this._currentBlogService.Logout()
    this._router.navigateByUrl('/')
  }

  public ChangeCurrentBlog(): void {
    this._currentBlogService.Logout()
    this._router.navigateByUrl('/my-blogs')
  }

  public OnBlogClick(){
    this.IsBlogMenuOpen = !this.IsBlogMenuOpen
  }

  public CurrentBlogUsername(): string{
    return this._currentBlogService.GetCurrentBlog().username
  }
}
