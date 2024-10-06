using BlazorProject.Application.Exceptions.BlazorProject.Application.Exceptions;
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
    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, Unit>
    {
        private readonly IItemRepository _itemRepository;
        public UpdateItemCommandHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public async Task<Unit> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _itemRepository.GetByIdAsync(request.Id);
            if (item == null)
            {
                throw new NotFoundException(nameof(Item), request.Id);
            }

            item.UpdateDetails(request.Name, request.Code);
            await _itemRepository.UpdateAsync(item);

            return Unit.Value;
        }
    }
}
