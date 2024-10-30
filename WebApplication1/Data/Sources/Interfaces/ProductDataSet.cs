using AutoMapper;
using System;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public abstract class ProductDataSet
    {
        protected readonly IMapper _mapper;

        protected ProductDataSet(IMapper mapper)
        {
            _mapper = mapper;
        }

        public abstract ProductDTO[] Products { get; }
    }
}
