using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Commands.BlogCategoryCommands
{
    public class RemoveBlogCategoryCommand
    {
        public int Id { get; set; }

        public RemoveBlogCategoryCommand(int id)
        {
            Id = id;
        }
    }
}
