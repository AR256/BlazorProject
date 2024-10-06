using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorProject.Application.Commands
{
    public class UpdateItemCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public UpdateItemCommand(Guid id, string name, string code)
        {
            Id = id;
            Name = name;
            Code = code;
        }
    }
}
