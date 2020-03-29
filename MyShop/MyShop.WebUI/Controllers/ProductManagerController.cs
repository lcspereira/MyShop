using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;


namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;


        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoriesContext)
        {
            context = productContext;
            productCategories = productCategoriesContext;
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
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(product);
            //}
            //else
            //{

            if (file != null)
            {
                product.Image = product.Id + Path.GetExtension(file.FileName);
                file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);
            }
            context.Insert(product);
            context.Commit();

            return RedirectToAction("Index");
            //}
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
        public ActionResult Edit(string id, Product product, HttpPostedFileBase file)
        {
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    return View(product);
                //}
                if (file != null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);
                }
                context.Update(product);
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
