using CodePulse.Api.Models.Domain;

namespace CodePulse.Api.Models.DTO
{
    public class BlogpostDto
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
        public List<CategoryRequestDto> Categories { get; set; }=new List<CategoryRequestDto>();
    }
}
