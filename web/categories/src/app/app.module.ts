import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavabarComponent } from './core/components/navabar/navabar.component';
import { CategoryListComponent } from './features/category/category-list/category-list.component'; 
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AddCategoryComponent } from './features/category/add-category/add-category.component';
import { EditCategoryComponent } from './features/category/edit-category/edit-category.component';
import { BlogPostListComponent } from './features/blog-post/blog-post-list/blog-post-list.component';
import { AddBlogPostComponent } from './features/blog-post/add-blog-post/add-blog-post.component'; 
import { MarkdownModule } from 'ngx-markdown';

@NgModule({
  declarations: [
    AppComponent,
    NavabarComponent,
    CategoryListComponent,
    AddCategoryComponent,
    EditCategoryComponent,
    BlogPostListComponent,
    AddBlogPostComponent 
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule ,
    MarkdownModule.forRoot()  
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
