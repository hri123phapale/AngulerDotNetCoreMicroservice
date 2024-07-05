import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';  
@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css']
})
export class EditCategoryComponent implements OnInit, OnDestroy {
   id :string | null=null; 
   paramSubcription?:Subscription;
    constructor(private route:ActivatedRoute){   
  }
  ngOnDestroy(): void {
    this.paramSubcription?.unsubscribe();
  }
  ngOnInit(): void {
    this.paramSubcription=this.route.paramMap.subscribe({
     next:(param)=>{
        this.id=param.get('id');
        //  if(this.id)
        //   {
        //     this.categoryService1.getCatagory(this.id)
        //     .subscribe({
        //       next:(response)=>
        //         this.category=response
        //     } 
        //     );
        //   }
      } 
    })
  }
}
