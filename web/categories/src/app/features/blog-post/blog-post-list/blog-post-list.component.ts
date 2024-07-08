import { Component, OnDestroy, OnInit } from '@angular/core';
import { BlogpostService } from '../services/blogpost.service';
import { blogpost } from '../models/blogpost.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-blog-post-list',
  templateUrl: './blog-post-list.component.html',
  styleUrls: ['./blog-post-list.component.css']
})
export class BlogPostListComponent implements OnInit{
  
  blogposts$?:Observable< blogpost[]>; 
  constructor(private blogpostservice:BlogpostService){ 

  } 
  
  ngOnInit(): void {
   this.blogposts$= this.blogpostservice.getAllblogpost(); 
  }

}
