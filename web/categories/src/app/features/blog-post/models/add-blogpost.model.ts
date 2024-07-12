export interface addblogpost{
    title:string,
    urlHandle:string,
    shortDescription:string,
    content:string,
    featuredImageUrl:string, 
    publishDate:Date,
    auther:string,
    isVisible:boolean, 
    categories:string[]; 
}