using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Domain.Entities
{
    public class TagCloud
    {
        [Key]
        public Guid TagCloudID { get; set; }
        public string TagName { get; set; }
        public bool TagStatus { get; set; }
        public List<BlogTagCloud> BlogTagClouds { get; set; }
    }
}
