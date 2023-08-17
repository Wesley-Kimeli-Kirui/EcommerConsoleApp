using EcommerConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerConsoleApp.Service.IService
{
    public interface IProductsInterface
    {
        Task<SuccessMessage> CreateProductAsync(AddItems product);
       
        Task<SuccessMessage> UpdateProductAsync(Items product);
        
        Task<SuccessMessage> DeleteProductAsync(string id);
       
        Task<Items> GetProductAsync(string id);
        
        Task<List<Items>> GetAllProductsAsync();
    }
}
