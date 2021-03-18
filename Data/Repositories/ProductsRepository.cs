using Data.Repositories.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private static List<ProductEntity> _products = new List<ProductEntity>() 
        {
            new ProductEntity(){ID = 0, Name = "Lenovo Legion",Category = "Laptop",Description = "Оч крутой сам, юзаю", Cost = 1000,CodeProduct = "AA11" },
            new ProductEntity(){ID = 1, Name = "Asus Gaming",Category = "Laptop",Description = "Тоже крутой", Cost = 1200,CodeProduct = "AA12" },
            new ProductEntity(){ID = 2, Name = "Acer",Category = "Laptop",Description = "Крутой, но чуть хуже", Cost = 900,CodeProduct = "AA13" },
            new ProductEntity(){ID = 3, Name = "Xiaomi",Category = "Smartphone",Description = "Топ за свої гроші", Cost = 100,CodeProduct = "AA14" },
        };
        public bool AddProduct(ProductEntity product)
        {
            for(int i = 0; i < _products.Count; i++)
            {
                if(product.CodeProduct == _products[i].CodeProduct)
                {
                    return false;
                }
            }
            product.ID = _products[_products.Count - 1].ID + 1;
            _products.Add(product);
            return true;
        }

        public void DeleteProductByID(int id)
        {
            for(int i = 0; i < _products.Count; i++)
            {
                if(_products[i].ID == id)
                {
                    _products.RemoveAt(i);
                }
            }
        }

        public ProductEntity GetProductByID(int id)
        {
            for (int i = 0; i < _products.Count; i++)
            {
                if (_products[i].ID == id)
                {
                    return _products[i];
                }
            }
            return null;
        }

        public List<ProductEntity> GetProductsByName(string name)
        {
            List<ProductEntity> productEntities = new List<ProductEntity>();
            for (int i = 0; i < _products.Count; i++)
            {
                if(name == _products[i].Name)
                {
                    productEntities.Add(_products[i]);
                }
            }
            return productEntities;
        }

        public List<ProductEntity> GetProducts()
        {
            List<ProductEntity> productEntities = new List<ProductEntity>();
            for (int i = 0; i < _products.Count; i++)
            {
                productEntities.Add(_products[i]);
            }
            return productEntities;
        }

        public void UpdateProduct(ProductEntity product, string oldproductCode)
        {
            for(int i = 0; i < _products.Count; i++)
            {
                if(oldproductCode == _products[i].CodeProduct)
                {
                    _products[i].Name = product.Name;
                    _products[i].Category = product.Category;
                    _products[i].Description = product.Description;
                    _products[i].Cost = product.Cost;
                    _products[i].CodeProduct = product.CodeProduct;
                }
            }
        }

        public List<ProductEntity> GetBucketProducts(List<string> productsCodes)
        {
            List<ProductEntity> productEntities = new List<ProductEntity>();
            for (int i = 0; i < productsCodes.Count; i++)
            {
                for(int j = 0; j < _products.Count; j++)
                {
                    if(productsCodes[i] == _products[j].CodeProduct)
                    {
                        productEntities.Add(_products[j]);
                    }
                }
            }
            return productEntities;
        }

        public bool SearchProductByCODE(string productCode)
        {
            foreach(var product in _products)
            {
                if(product.CodeProduct == productCode)
                {
                    return true;
                }
            }
            return false;
        }
        public ProductEntity GetProductByCode(string productCode)
        {
            foreach (var product in _products)
            {
                if (product.CodeProduct == productCode)
                {
                    return product;
                }
            }
            return new ProductEntity();
        }
    }
}
