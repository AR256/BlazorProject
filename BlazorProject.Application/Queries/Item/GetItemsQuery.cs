﻿using BlazorProject.Application.DTOS;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorProject.Application.Queries
{
    public class GetItemsQuery : IRequest<List<ItemDto>>
    {
    }
}
