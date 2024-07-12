import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BlogpostService } from '../../blog-post/services/blogpost.service';
import { Observable } from 'rxjs';
import { blogpost } from '../../blog-post/models/blogpost.model';

@Component({
  selector: 'app-blog-details',
  templateUrl: './blog-details.component.html',
  styleUrls: ['./blog-details.component.css']
})
export class BlogDetailsComponent implements OnInit{
  
  url:string | null=null;
  
  blogpost$?:Observable<blogpost>
constructor(private route:ActivatedRoute,
  private blogpostService: BlogpostService
){


}

  ngOnInit(): void {
     this.route.paramMap.subscribe({
        next:(param)=>{ 
        this.url= param.get("url"); 
        } 
     })

     if(this.url)
     { 
       this.blogpost$=this.blogpostService.getBlogpostbyUrl(this.url);
     }
  }

}
