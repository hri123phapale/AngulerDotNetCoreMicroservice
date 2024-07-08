import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { addblogpost } from '../models/add-blogpost.model';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { blogpost } from '../models/blogpost.model';

@Injectable({
  providedIn: 'root'
})
export class BlogpostService {

  constructor(private httpClient:HttpClient) { }

  createBlogpost(model:addblogpost):Observable<addblogpost>
  {
    return this.httpClient.post<addblogpost>(`${environment.apibaseUrl}/api/BlogPosts/CreateBlogPost`,model);
  }
  getAllblogpost():Observable<blogpost[]>
  {
    return this.httpClient.get<blogpost[]>(`${environment.apibaseUrl}/api/BlogPosts/GetAllBlogsPosts`);
  }
  getBlogpostbyId(id:string):Observable<addblogpost>
  {
    return this.httpClient.get<addblogpost>(`${environment.apibaseUrl}/api/BlogPosts/GetBlogPost/${id}`);
  }
  updateBlogPost(model:addblogpost):Observable<addblogpost>
  {
    return this.httpClient.put<addblogpost>(`${environment.apibaseUrl}/api/BlogPosts/updateBlogPost`,model);
  }
  deleteBlogPost(id:string):Observable<addblogpost>
  {
    return this.httpClient.delete<addblogpost>(`${environment.apibaseUrl}/api/BlogPosts/deleteBlogPost/${id}`);
  }
}