using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Domain.Entities
{
    public class BlogTagCloud
    {
        public Guid BlogTagCloudID { get; set; }
        public Guid BlogID { get; set; }
        public Blog Blog { get; set; }
        public Guid TagCloudID { get; set; }
        public TagCloud TagCloud { get; set; }
    }
}
