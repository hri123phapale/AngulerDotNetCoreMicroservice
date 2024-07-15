import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Category } from '../models/category.model';
import { AddCategoryRequestModel } from '../models/add-category-request.model';
import { CookieService } from 'ngx-cookie-service';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http:HttpClient,
    private cookieService:CookieService
  ) { }

  getcategories():Observable<Category[]>{ 
    return this.http.get<Category[]>(`${environment.apibaseUrl}/Categories/GetAllCategories`);
  }

  addCategory(model:AddCategoryRequestModel):Observable<void>
  {
   return this.http.post<void>(`${environment.apibaseUrl}/Categories/CreateCategory?addAuth=true`,model);
  }
  getCategoryById(id:string):Observable<Category>
  {
    return this.http.get<Category>(`${environment.apibaseUrl}/Categories/GetCategory/${id}`)
  }

  updateCategory(model:Category):Observable<void>
  {
      return this.http.put<void>(`${environment.apibaseUrl}/Categories/updateCategory?addAuth=true`,
      model);
  }

  deleteCategory(id:string):Observable<void>
  {
    return this.http.delete<void>(`${environment.apibaseUrl}/Categories/deleteCategory/${id}?addAuth=true`);
  }
}
