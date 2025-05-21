using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Dto.BlogDtos
{
    public class ResultBlogTotalDataDto
    {
        public List<ResultBlogDto> BlogDatas { get; set; }
        public int TotalBlogCount { get; set; }
    }
}
