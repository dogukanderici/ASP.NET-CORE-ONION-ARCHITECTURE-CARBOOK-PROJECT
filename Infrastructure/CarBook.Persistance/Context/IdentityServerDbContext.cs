using CarBook.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Persistance.Context
{
    public class IdentityServerDbContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityServerDbContext(DbContextOptions<IdentityServerDbContext> options) : base(options)
        {
        }
    }
}
