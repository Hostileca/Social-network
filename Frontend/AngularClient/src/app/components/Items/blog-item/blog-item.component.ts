import { Component, Input } from '@angular/core';
import { Blog } from '../../../Data/Models/Blog/Blog';
import {NgOptimizedImage} from "@angular/common";
import {BlogConfig} from "../../../Data/Consts/BlogConfig";

@Component({
  selector: 'app-blog-item',
  standalone: true,
  imports: [
    NgOptimizedImage
  ],
  templateUrl: './blog-item.component.html',
  styleUrl: './blog-item.component.css'
})
export class BlogItemComponent {
  @Input() Blog!: Blog
  BlogConfig = BlogConfig;

  constructor() {

  }
}
