import { Component, OnDestroy, OnInit } from '@angular/core';
import { addblogpost } from '../models/add-blogpost.model';
import { BlogpostService } from '../services/blogpost.service';
import { Observable, Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { CategoryService } from '../../category/services/category.service';
import { Category } from '../../category/models/category.model';

@Component({
  selector: 'app-add-blog-post',
  templateUrl: './add-blog-post.component.html',
  styleUrls: ['./add-blog-post.component.css']
})
export class AddBlogPostComponent implements OnDestroy,OnInit {

model:addblogpost;
addblogpostSubcritption?:Subscription;
categoris$?:Observable<Category[]>;

 constructor(private blogpostservice:BlogpostService,
   private router:Router,
  private categoriService:CategoryService)
 {
  this.model= {
    title:'',
    shortDescription:'',
    urlHandle:'',
    content:'',
    featuredImageUrl:'',
    auther:'',
    isVisible:true,
    publishDate:new Date(),
    categories: []
  } 
 }

  ngOnInit(): void {
    this.categoris$= this.categoriService.getcategories();
  }
  

 OnFormSubmit():void {
  this.addblogpostSubcritption= this.blogpostservice.createBlogpost(this.model)
   .subscribe({
    next:(response)=>
    {
      this.router.navigateByUrl("/admin/blogposts");
    } 
   })
}

ngOnDestroy(): void {
  this.addblogpostSubcritption?.unsubscribe();
}
}
