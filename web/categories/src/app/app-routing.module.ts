import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryListComponent } from './features/category/category-list/category-list.component'; 
import { AddCategoryComponent } from './features/category/add-category/add-category.component';
import { EditCategoryComponent } from './features/category/edit-category/edit-category.component';
import { BlogPostListComponent } from './features/blog-post/blog-post-list/blog-post-list.component';
import { AddBlogPostComponent } from './features/blog-post/add-blog-post/add-blog-post.component';
import { HomeComponent } from './features/public/home/home.component';
import { BlogDetailsComponent } from './features/public/blog-details/blog-details.component';
import { LoginComponent } from './features/auth/login/login.component';
import { authGuard } from './features/guards/auth.guard';
import { PageNotFoundComponent } from './core/page-not-found/page-not-found.component';
 
const routes: Routes = [ 
{
  path:'',
   component:HomeComponent
},
{
  path:'login',
   component:LoginComponent
},
{
  path:'blog/:url',
   component:BlogDetailsComponent
},
{
 path:'admin/categories',
  component:CategoryListComponent,
  canActivate:[authGuard]
},
{
  path:'admin/categories/add',
   component:AddCategoryComponent,
   canActivate:[authGuard]
} ,
{
  path:'admin/categories/:id',
   component:EditCategoryComponent,
   canActivate:[authGuard]
} ,
{
  path:'admin/blogposts',
   component:BlogPostListComponent,
   canActivate:[authGuard]
},
{
  path:'admin/blogposts/add',
   component:AddBlogPostComponent,
   canActivate:[authGuard]
},
{
  path:'**',
   component:PageNotFoundComponent
}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
