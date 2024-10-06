using BlazorProject.Domain.Entities;
using BlazorProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorProject.Application.Commands
{
    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, Guid>
    {
        private readonly IItemRepository _itemRepository;
        public CreateItemCommandHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public async Task<Guid> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            var newItem = new Item(Guid.NewGuid(), request.Name, request.Code);
            await _itemRepository.AddAsync(newItem);
            return newItem.Id;
        }
    }
}
