namespace CodePulse.Api.Models.DTO
{
    public class BlogpostModel
    {
        public Guid Id { get;  set; }
        public string Title { get;  set; }
        public string UrlHandle { get;  set; }

        public string ShortDescription { get;  set; }
        public string Content { get;  set; }
        public string FeaturedImageUrl { get;  set; }
        public DateTime PublishDate { get;  set; }
        public string Auther { get;  set; }
        public bool IsVisible { get;  set; }
    }
}
