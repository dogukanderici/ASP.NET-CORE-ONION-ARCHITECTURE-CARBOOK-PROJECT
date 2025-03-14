using CarBook.Dto.BlogDtos;
using CarBook.Dto.TagCloudDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Dto.BlogTagCloudDtos
{
    public class ResultBlogTagCloudForBlogDto
    {
        public Guid BlogTagCloudID { get; set; }
        public Guid TagCloudID { get; set; }
        public ResultTagCloudDto TagCloud { get; set; }
    }
}
