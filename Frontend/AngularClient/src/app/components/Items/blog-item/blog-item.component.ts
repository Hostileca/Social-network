import { Component, Input } from '@angular/core';
import { Blog } from '../../../Data/Models/Blog/Blog';

@Component({
  selector: 'app-blog-item',
  standalone: true,
  imports: [],
  templateUrl: './blog-item.component.html',
  styleUrl: './blog-item.component.css'
})
export class BlogItemComponent {
  @Input() Blog!: Blog

  constructor() {

  }
}
