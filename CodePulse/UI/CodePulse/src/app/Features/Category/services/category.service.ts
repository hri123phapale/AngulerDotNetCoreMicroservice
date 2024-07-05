import { Injectable } from '@angular/core';
import { addcategoryRequest } from '../Models/add-category-request.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http'; 
import { environment } from 'src/environments/environment'; 
import { Category } from '../Models/category.model';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http:HttpClient) { }

  addCategory(model:addcategoryRequest):Observable<void>
  {
   return this.http.post<void>(`${environment.apibaseUrl}/api/Categories/CreateCategory`,model);
  }
   
  getCategories():Observable<Category[]>
  { 
    return this.http.get<Category[]>(`${environment.apibaseUrl}/api/Categories/GetAllCategories`);
  }
  getCatagory(id : string):Observable<Category>
  {
   return this.http.get<Category>(`${environment.apibaseUrl}/api/categories/GetCategory/${id}`);
  }
}
