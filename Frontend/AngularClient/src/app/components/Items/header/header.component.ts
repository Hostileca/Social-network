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

  constructor(private readonly authService: AuthService,
              private readonly currentBlogService: CurrentBlogService) {
  }

  public IsLoggedIn(): boolean {
    return this.authService.IsAuth() && this.currentBlogService.IsBlogSelected()
  }

  public Logout(): void {
    this.authService.Logout()
    this.currentBlogService.Logout()
  }

  public OnBlogClick(){
    this.IsBlogMenuOpen = !this.IsBlogMenuOpen
    console.log("123")
  }
}
