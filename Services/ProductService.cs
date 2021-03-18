using Data.Repositories.Abstract;
using Domain;
using Services.Abstract;
using System;
using System.Collections.Generic;
using Mappers;
using System.Text;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IProductsRepository _productRepository;
        public ProductService(IProductsRepository productsRepository)
        {
            _productRepository = productsRepository;
        }

        Data.Repositories.ProductsRepository productRepository = new Data.Repositories.ProductsRepository();

        public bool AddProduct(Product product)
        {
            if (productRepository.AddProduct(product.ToEntity()) == true)
            {
                return true;
            }
            return false;
        }

        public void DeleteProductByID(int id)
        {
            productRepository.DeleteProductByID(id);
        }

        public Product GetProductByID(int id)
        {
            return ProductMapper.ToDomain(productRepository.GetProductByID(id));
        }

        public Product GetProductByCode(string productCode)
        {
            return ProductMapper.ToDomain(productRepository.GetProductByCode(productCode));
        }

        public List<Product> GetProductsByName(string name)
        {
            List<Product> products = new List<Product>();
            for (int i = 0; i < productRepository.GetProductsByName(name).Count; i++)
            {
                products.Add(ProductMapper.ToDomain(productRepository.GetProductsByName(name)[i]));
            }
            return products;
        }

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            for (int i = 0; i < productRepository.GetProducts().Count; i++)
            {
                products.Add(ProductMapper.ToDomain(productRepository.GetProducts()[i]));
            }
            return products;
        }

        public void UpdateProduct(Product product, string oldproductCode)
        {
            productRepository.UpdateProduct(ProductMapper.ToEntity(product), oldproductCode);
        }
        public List<Product> GetBucketProducts(List<string> productsCodes)
        {
            List<Product> products = new List<Product>();
            for (int i = 0; i < productRepository.GetBucketProducts(productsCodes).Count; i++)
            {
                products.Add(ProductMapper.ToDomain(productRepository.GetBucketProducts(productsCodes)[i]));
            }
            return products;
        }
        public bool SearchProductByCODE(string productCode)
        {
            if (productRepository.SearchProductByCODE(productCode))
            {
                return true;
            }
            return false;
        }
    }
}
