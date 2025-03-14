using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Dto.BlogCategoryDtos
{
    public class UpdateBlogCategoryDto
    {
        public int BlogCategoryID { get; set; }
        public string Name { get; set; }
        public bool CategoryStatus { get; set; }
    }
}
