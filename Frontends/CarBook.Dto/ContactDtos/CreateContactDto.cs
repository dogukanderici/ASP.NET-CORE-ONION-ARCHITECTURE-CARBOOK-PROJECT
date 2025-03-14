using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Dto.ContactDtos
{
    public class CreateContactDto
    {
        public Guid ContactID { get; set; }
        public Guid ReplyID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool MessageType { get; set; }
        public DateTimeOffset SendDate { get; set; }
    }
}
