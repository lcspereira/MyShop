using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;


namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;


        public ProductManagerController()
        {
            context = new ProductRepository();
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
            Product product = new Product();
            return View(product);
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(string id)
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

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
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
