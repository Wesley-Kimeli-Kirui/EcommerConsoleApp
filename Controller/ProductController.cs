using EcommerConsoleApp.Helpers;
using EcommerConsoleApp.Service;
using EcommerConsoleApp.Models;
using EcommerConsoleApp.Service.IService;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerConsoleApp.Controller
{
    public class ProductController
    {
        ProductService productService = new ProductService();
        public async static Task Init()
        {
            Console.WriteLine("Welcome Ecommerce Console!!");
            Console.WriteLine("#### Menu ####");
            Console.WriteLine("1. Add a Product");
            Console.WriteLine("2. View Products");
            Console.WriteLine("3. Update a Product");
            Console.WriteLine("4. Delete a Product");

            var input = Console.ReadLine();
            var results = Validation.Validate(new List<string> { input });

            if (!results)
            {
                await Init();
            }
            else
            {
                await new ProductController().CallMenu(input);
            }
        }
        public async Task CallMenu(string id)
        {
            switch (id)
            {
                case "1":
                    await AddNewProduct();
                    break;
                case "2":
                    await ViewProducts();
                    break;
                case "3":
                    await UpdateProduct();
                    break;
                case "4":
                    await DeleteProduct();
                    break;
                default:
                    await ProductController.Init();
                    break;
            }
        }


        public async Task AddNewProduct()
        {
            Console.Write("Enter product name: ");
            var productName = Console.ReadLine();
            if (productName == "")
            {
                Console.WriteLine("Product Cannot be Null!");
                await AddNewProduct();
            }

            Console.Write("Product Description: ");
            var productDescription = Console.ReadLine();

            if (productDescription == "")
            {
                Console.WriteLine("Product description Cannot be Null");
                await AddNewProduct();
            }

            Console.Write("Enter product price: ");
            var productPrice = Console.ReadLine();
           
            if (!int.TryParse(productPrice, out var price))
            {
                Console.WriteLine("Product price must be a number");
                await AddNewProduct();
            }
            if (productPrice == "")
            {
                Console.WriteLine("Product price cannot be empty");
                await AddNewProduct();
            }

            Console.Write("Enter category: ");
            var productCategory = Console.ReadLine();
            if (productCategory == "")
            {
                Console.WriteLine("Product Cannot be Null!");
                await AddNewProduct();
            }
            //    foreach (var item in Enum.GetValues(typeof(ProductsCategory)))
            //    {
            //        Console.WriteLine($"{(int)item}. {item}");
            //    }

            //    if (!int.TryParse(Console.ReadLine(), out var selectedCategory) ||
            //!Enum.IsDefined(typeof(ProductsCategory), selectedCategory))
            //    {
            //        Console.WriteLine("Invalid category selection");
            //        await AddNewProduct();

            //   }

            var newProduct = new AddItems()
            {
                ProductName = productName,
                ProductDescription = productDescription,
                ProductPrice = productPrice,
                ProductCategory = productCategory
            };

            try
            {
                var res = await productService.CreateProductAsync(newProduct);
                Console.WriteLine(res.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task ViewProducts()
        {
            try
            {
                var products = await productService.GetAllProductsAsync();
                foreach (var product in products)
                {
                    await Console.Out.WriteLineAsync($"{product.Id}. {product.ProductName}, {product.ProductDescription} @ {product.ProductPrice}");
                }
                Console.Write("View a Single Item: ");

                var id = Console.ReadLine();

                await ViewOneProduct(id);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task ViewOneProduct(string id)
        {
            try
            {
                var product = await productService.GetProductAsync(id);
                await Console.Out.WriteLineAsync($"{product.Id}. {product.ProductName}, {product.ProductDescription} @ {product.ProductPrice}, {product.ProductCategory}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write("Please enter a valid product id: ");

                var input = Console.ReadLine();
                var results = Validation.Validate(new List<string> { input });
                await ViewOneProduct(input);
            }
        }

        // Update a product logic implementation
        public async Task UpdateProduct()
        {
            try
            {
                var products = await productService.GetAllProductsAsync();
                foreach (var product in products)
                {
                    await Console.Out.WriteLineAsync($"{product.Id}. {product.ProductName}, {product.ProductDescription} @ {product.ProductPrice}, {product.ProductCategory}");
                }
                Console.Write("Enter Item id: ");
                var id = Console.ReadLine();


                var current_product = products.FirstOrDefault(x => x.Id == id);

                Console.Write($"Enter Item Name: {current_product.ProductName}: Name: ");
                var productName = Console.ReadLine();
                if (productName == "")
                {
                    productName = current_product.ProductName;
                }

                Console.Write($"Enter product description: {current_product.ProductDescription}: Description: ");
                var productDescription = Console.ReadLine();
                if (productDescription == "")
                {
                    productDescription = current_product.ProductDescription;
                }

                Console.Write($"Enter product price: {current_product.ProductPrice}: Price: ");
                var productPrice = Console.ReadLine();
                if (productPrice == "")
                {
                    productPrice = current_product.ProductPrice;
                }

                Console.Write($"Enter product category: {current_product.ProductCategory}: Category: ");
                var productCategory = Console.ReadLine();
                if (productCategory == "")
                {
                    productCategory = current_product.ProductCategory;
                }

                var newProduct = new Items()
                {
                    Id = id,
                    ProductName = productName,
                    ProductDescription = productDescription,
                    ProductPrice = productPrice,
                    ProductCategory = productCategory
                };

                try
                {
                    var res = await productService.UpdateProductAsync(newProduct);
                    Console.WriteLine(res.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task DeleteProduct()
        {
            try
            {
                var products = await productService.GetAllProductsAsync();
                foreach (var product in products)
                {
                    await Console.Out.WriteLineAsync($"{product.Id}. {product.ProductName}, {product.ProductDescription} @ {product.ProductPrice}");
                }
                Console.Write("To Delete Enter product Id: ");
                var id = Console.ReadLine();

                try
                {
                    var res = await productService.DeleteProductAsync(id);
                    Console.WriteLine(res.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
