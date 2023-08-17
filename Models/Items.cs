using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerConsoleApp.Models
{
    public class Items
    {
        public string Id { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string ProductDescription { get; set; } = string.Empty;

        public string ProductPrice { get; set; } = string.Empty;

        public string ProductCategory { get; set; } = string.Empty;
    }
}
