using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories = new List<ProductCategory>();

        public ProductCategoryRepository()
        {
            productCategories = cache["products"] as List<ProductCategory>;

            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            cache["products"] = productCategories;
        }

        public void Insert(ProductCategory p)
        {
            productCategories.Add(p);
        }

        //TODO: Use find method.
        public void Update(ProductCategory productCategory)
        {
            int prodCatToUpdateIdx = productCategories.IndexOf(productCategories.Find(p => p.Id == productCategory.Id));

            if (prodCatToUpdateIdx != -1)
            {
                productCategories[prodCatToUpdateIdx] = productCategory;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public ProductCategory Find(string id)
        {
            ProductCategory productCategory = productCategories.Find(p => p.Id == id);

            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }

        //TODO: Use find method.
        public void Delete(string id)
        {
            ProductCategory prodCatToDelete = productCategories.Find(p => p.Id == id);

            if (prodCatToDelete != null)
            {
                productCategories.Remove(prodCatToDelete);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}
