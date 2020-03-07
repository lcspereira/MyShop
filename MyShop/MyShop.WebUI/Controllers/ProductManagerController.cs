using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;


namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        InMemoryRepository<Product> context;
        InMemoryRepository<ProductCategory> productCategories;


        public ProductManagerController()
        {
            context = new InMemoryRepository<Product>();
            productCategories = new InMemoryRepository<ProductCategory>();
        }
        // GET: Product
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();

            viewModel.Product = new Product();
            viewModel.ProductCategories = productCategories.Collection();
            return View(viewModel);
        }

        // POST: Product/Create
        [HttpPost]
        //TODO: INSERT ERROR. GET PRODUCT FROM VIEWMODEL
        public ActionResult Create(ProductManagerViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            else
            {

                context.Insert(viewModel.Product);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(string id)
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();

            try
            {
                viewModel.Product = context.Find(id);
                viewModel.ProductCategories = productCategories.Collection();
                return View(viewModel);
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, ProductManagerViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(viewModel.Product);
                }
                context.Update(viewModel.Product);
                context.Commit();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(string id)
        {
            Product product;
            try
            {
                product = context.Find(id);
                return View(product);
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        // POST: Product/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            try
            {
                context.Delete(id);
                context.Commit();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }
    }
}
