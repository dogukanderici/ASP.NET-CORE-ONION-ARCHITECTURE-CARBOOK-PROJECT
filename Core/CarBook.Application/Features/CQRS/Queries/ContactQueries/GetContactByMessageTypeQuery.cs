using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Queries.ContactQueries
{
    public class GetContactByMessageTypeQuery
    {
        public bool MessageType { get; set; }

        public GetContactByMessageTypeQuery(bool messageType)
        {
            MessageType = messageType;
        }
    }
}
