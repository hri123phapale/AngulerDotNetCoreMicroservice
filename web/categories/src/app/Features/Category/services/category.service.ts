import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Category } from '../models/category.model';
import { AddCategoryRequestModel } from '../models/add-category-request.model';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http:HttpClient) { }

  getcategories():Observable<Category[]>{ 
    return this.http.get<Category[]>("http://localhost:5146/api/Categories/GetAllCategories");
  }

  addCategory(model:AddCategoryRequestModel):Observable<void>
  {
   return this.http.post<void>('http://localhost:5146/api/Categories/CreateCategory',model);
  }
  getCategoryById(id:string):Observable<Category>
  {
    return this.http.get<Category>(`http://localhost:5146/api/Categories/GetCategory/${id}`)
  }

  updateCategory(model:Category):Observable<void>
  {
    return this.http.put<void>('http://localhost:5146/api/Categories/updateCategory',model);
  }

  deleteCategory(id:string):Observable<void>
  {
    return this.http.delete<void>(`http://localhost:5146/api/Categories/deleteCategory/${id}`);
  }
}
