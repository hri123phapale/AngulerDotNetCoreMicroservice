import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute,  Router } from '@angular/router';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/category.model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css']
})
export class EditCategoryComponent implements OnInit,OnDestroy {
 
  id:string | null=null;
  model?:Category;
  constructor(private route:ActivatedRoute,
    private categoryService:CategoryService,
    private router:Router
  ){}
  
  editCategorySubscription?:Subscription;

  ngOnInit(): void {
   this.editCategorySubscription= this.route.paramMap.subscribe(
      {
        next:(param)=>{
          this.id= param.get("id");
          if(this.id)
          {
            this.categoryService.getCategoryById(this.id)
            .subscribe({
              next:(response)=>{
                this.model=response;
              }
            })
          } 
        }
      } 
     );
  }

  OnSubmit(): void {
    if(this.model)
    {
      this.editCategorySubscription=this.categoryService.updateCategory(this.model)
      .subscribe({
        next:(response)=>{
          this.router.navigateByUrl('/admin/categories');
        }
      })
    } 
  }

  OnDelete():void  {
    if(this.id)
    {
      this.editCategorySubscription= this.categoryService.deleteCategory(this.id)
      .subscribe({
         next:()=>{ 
           this.router.navigateByUrl('/admin/categories');
         }

      })
    } 
  }
  ngOnDestroy(): void {
    this.editCategorySubscription?.unsubscribe();
  } 
}
