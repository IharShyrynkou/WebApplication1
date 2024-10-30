using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data.Repositories;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductsController(ILogger<ProductsController> logger, IMapper mapper, IProductRepository productRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        [HttpGet(Name = "GetProducts")]
        public IActionResult Get(
            int capacity, 
            string productName = null, 
            string destination = null,
            string supplier = null,
            int maxPrice = default,
            int pageSize = 10,
            int pageIndex = 0
            )
        {
            var products = _productRepository.Products.Where(p=> p.Capacity >= capacity);

            if (!string.IsNullOrWhiteSpace(productName)) products = products.Where(p => p.Name == productName);
            if (!string.IsNullOrWhiteSpace(destination)) products = products.Where(p => p.Destination == destination);
            if (!string.IsNullOrWhiteSpace(supplier)) products = products.Where(p => p.Supplier == supplier);
            if (maxPrice != default) products = products.Where(p => p.Price <= maxPrice);

            products = products.Skip(pageSize * pageIndex);
            products = products.Take(pageSize);

            return Ok(_mapper.Map<ProductResponse[]>(products));
;        }
    }
}
