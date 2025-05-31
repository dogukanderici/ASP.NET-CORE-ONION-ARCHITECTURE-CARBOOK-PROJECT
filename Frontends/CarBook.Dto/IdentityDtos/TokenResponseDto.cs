using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Dto.IdentityDtos
{
    public class TokenResponseDto
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string ExpiresIn { get; set; }
        public List<ClaimDto> Claims { get; set; }
    }

    public class ClaimDto
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
