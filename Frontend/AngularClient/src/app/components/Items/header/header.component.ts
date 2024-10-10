import { Component } from '@angular/core';
import {AuthService} from "../../../Data/Services/auth.service";
import {CurrentBlogService} from "../../../Data/Services/current-blog.service";
import {NgIf} from "@angular/common";

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
              private readonly _currentBlogService: CurrentBlogService) {
  }

  public IsLoggedIn(): boolean {
    return this._authService.IsAuth() && this._currentBlogService.IsBlogSelected()
  }

  public Logout(): void {
    this._authService.Logout()
    this._currentBlogService.Logout()
  }

  public OnBlogClick(){
    this.IsBlogMenuOpen = !this.IsBlogMenuOpen
  }

  public CurrentBlogUsername(): string{
    return this._currentBlogService.GetCurrentBlog().username
  }
}
