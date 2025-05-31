using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Dtos.IdentityServerDtos
{
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
