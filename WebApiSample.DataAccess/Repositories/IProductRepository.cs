using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiSample.DataAccess.Models;

namespace WebApiSample.DataAccess.Repositories
{
    public interface IProductsRepository
    {


        Task<List<Product>> GetDiscontinuedProductsAsync();
        Task<List<Product>> GetProductsAsync();

        Task<Product> GetProductAsync(int id);

        Task<int> AddProductAsync(Product product);
    }
}
