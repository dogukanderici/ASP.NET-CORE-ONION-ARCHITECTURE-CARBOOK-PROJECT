using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Domain.Entities
{
    public class Blog
    {
        [Key]
        public Guid BlogID { get; set; }
        public string Title { get; set; }
        public Guid AuthorID { get; set; }
        public Author Author { get; set; }
        public string CoverImageUrl { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public int BlogCategoryID { get; set; }
        public bool PublishState { get; set; }
        public BlogCategory BlogCategory { get; set; }
        public List<BlogTagCloud> BlogTagClouds { get; set; }
        public List<BlogComment> BlogComments { get; set; }
    }
}
