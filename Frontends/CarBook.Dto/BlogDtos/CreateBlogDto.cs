using CarBook.Dto.AuthorDtos;
using CarBook.Dto.BlogCategoryDtos;
using CarBook.Dto.BlogTagCloudDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Dto.BlogDtos
{
    public class CreateBlogDto
    {
        public string Title { get; set; }
        public Guid AuthorID { get; set; }
        public string CoverImageUrl { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public int BlogCategoryID { get; set; }
        public bool PublishState { get; set; } = true;
    }
}
