using Domain;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mappers
{
    public static class ProductMapper
    {
        public static ProductEntity ToEntity(this Product product)
        {
            return new ProductEntity()
            {
                Name = product.Name,
                Category = product.Category,
                Cost = product.Cost,
                Description = product.Description,
                CodeProduct = product.CodeProduct
            };
        }
        public static Product ToDomain(this ProductEntity product)
        {
            if (product == null) return null;
            return new Product
            {
                Name = product.Name,
                Category = product.Category,
                Cost = product.Cost,
                Description = product.Description,
                CodeProduct = product.CodeProduct
            };
        }
    }
}

