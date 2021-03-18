using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Abstract
{
    public interface IProductsRepository
    {
        bool AddProduct(ProductEntity product);
        void UpdateProduct(ProductEntity product, string oldproductCode);
        ProductEntity GetProductByID(int id);
        List<ProductEntity> GetProductsByName(string name);
        void DeleteProductByID(int id);
        List<ProductEntity> GetProducts();
        List<ProductEntity> GetBucketProducts(List<string> productsCodes);
        public ProductEntity GetProductByCode(string productCode);
        public bool SearchProductByCODE(string productCode);
    }
}
