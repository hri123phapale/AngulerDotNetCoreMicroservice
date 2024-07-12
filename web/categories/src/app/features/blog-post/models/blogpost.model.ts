import { Category } from "../../category/models/category.model";

export interface blogpost{
    id:string;
    title:string;
    urlhandle:string;
    shortDescription:string;
    content:string;
    featuredImageUrl:string; 
    publishDate:Date;
    auther:string;
    isVisible:boolean;
    categories:Category[];
}