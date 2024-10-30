using WebApplication1.Models;

namespace WebApplication1.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public ProductRepository(IEnumerable<ProductDataSet> productDataSets)
        {
            Products = productDataSets.SelectMany(pds => pds.Products);
        }

        public IEnumerable<ProductDTO> Products { get; }
    }
}
