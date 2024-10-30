using WebApplication1.Models;

namespace WebApplication1.Data.Repositories
{
    public interface IProductRepository
    {
        public IEnumerable<ProductDTO> Products { get; }
    }
}
