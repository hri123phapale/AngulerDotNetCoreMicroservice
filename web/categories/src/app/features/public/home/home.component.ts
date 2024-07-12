import { Component, OnInit } from '@angular/core';
import { BlogpostService } from '../../blog-post/services/blogpost.service';
import { Observable } from 'rxjs';
import { blogpost } from '../../blog-post/models/blogpost.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

blogposts$?:Observable<blogpost[]>;

 constructor(private blogpostService:BlogpostService)
 {

 }
  ngOnInit(): void { 
    this.blogposts$=this.blogpostService.getAllblogpost(); 
  }
}
