using BlazorProject.Application.DTOS;
using BlazorProject.Application.Exceptions.BlazorProject.Application.Exceptions;
using BlazorProject.Domain.Entities;
using BlazorProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorProject.Application.Queries
{
    public class GetItemByIdQueryHandler : IRequestHandler<GetItemByIdQuery, ItemDto>
    {
        private readonly IItemRepository _itemRepository;
        public GetItemByIdQueryHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<ItemDto> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _itemRepository.GetByIdAsync(request.Id);

            if (item == null)
            {
                throw new NotFoundException(nameof(Item), request.Id);
            }

            return new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Code = item.Code
            };
        }
    }
}
