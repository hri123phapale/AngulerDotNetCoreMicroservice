import { Component, OnDestroy } from '@angular/core';
import { addblogpost } from '../models/add-blog.model';
import { BlogpostService } from '../services/blogpost.service';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-blog-post',
  templateUrl: './add-blog-post.component.html',
  styleUrls: ['./add-blog-post.component.css']
})
export class AddBlogPostComponent implements OnDestroy {

model:addblogpost;
addblogpostSubcritption?:Subscription;

 constructor(private blogpostservice:BlogpostService, private router:Router)
 {
  this.model= {
    title:'',
    shortDescription:'',
    urlhandle:'',
    content:'',
    featuredImageUrl:'',
    auther:'',
    isVisible:true,
    publishDate:new Date()  
  } 
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
