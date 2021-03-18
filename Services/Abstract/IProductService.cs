using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstract
{
    public interface IProductService
    {
        bool AddProduct(Product product);
        void UpdateProduct(Product product, string oldproductCode);
        Product GetProductByCode(string productCode);
        Product GetProductByID(int id);
        void DeleteProductByID(int id);
        List<Product> GetProducts();
        List<Product> GetBucketProducts(List<string> productsCodes);
        List<Product> GetProductsByName(string name);
        bool SearchProductByCODE(string productCode);
    }
}
