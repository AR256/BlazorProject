using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorProject.Application.Commands
{
    public class DeleteItemCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public DeleteItemCommand(Guid id)
        {
            Id = id;
        }
    }
}
