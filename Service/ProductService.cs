using EcommerConsoleApp.Models;
using EcommerConsoleApp.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EcommerConsoleApp.Service
{
    public class ProductService : IProductsInterface
    {
        private readonly HttpClient _httpClient;
        public readonly string _url = "http://localhost:3000/products";
        public ProductService()
        {
            _httpClient = new HttpClient();
        }
        public async Task<SuccessMessage> CreateProductAsync(AddItems product)
        {
            var content = JsonConvert.SerializeObject(product);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(_url, bodyContent);

            if (response.IsSuccessStatusCode)
            {
                return new SuccessMessage { Message = "Product Added Successfully " };
            }
            throw new Exception("Product Creation Failed!");

        }

        public async Task<SuccessMessage> DeleteProductAsync(string id)
        {
            var response = await _httpClient.DeleteAsync(_url + "/" + id);

            if (response.IsSuccessStatusCode)
            {
                return new SuccessMessage { Message = "Product Deleted Successfully " };
            }
            throw new Exception("Product Deletion Failed!");
        }

        public async Task<List<Items>> GetAllProductsAsync()
        {
            var response = await _httpClient.GetAsync(_url);
            var products = JsonConvert.DeserializeObject<List<Items>>(await response.Content.ReadAsStringAsync());

            if (response.IsSuccessStatusCode)
            {
                return products;
            }
            throw new Exception("Products Unavailable");
        }

        public async Task<Items> GetProductAsync(string id)
        {
            var response = await _httpClient.GetAsync(_url + "/" + id);
            var product = JsonConvert.DeserializeObject<Items>(await response.Content.ReadAsStringAsync());

            if (response.IsSuccessStatusCode)
            {
                return product;
            }
            throw new Exception("Product Unvalable");
        }

        public async Task<SuccessMessage> UpdateProductAsync(Items product)
        {
            var content = JsonConvert.SerializeObject(product);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(_url + "/" + product.Id, bodyContent);

            if (response.IsSuccessStatusCode)
            {
                return new SuccessMessage { Message = "The Product Updated successfully " };
            }
            throw new Exception("Update Fail!");
        }
    }
}
