using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MuscleStore.Data;
using MuscleStore.Models;

namespace MuscleStore.Controllers
{
    public class ProductsController : Controller
    {
        private readonly MuscleStoreContext _context;

        public ProductsController(MuscleStoreContext context)
        {
            _context = context;
        }




        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        ///                                                         Products Main Page:
        ///                                                  
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // View Products Page
        public IActionResult Errors()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Brands = new SelectList(_context.Brand.ToList(), "Id", "Name");
            ViewBag.Categories = new SelectList(_context.Category.ToList(), "Id", "Name");
            return View(await _context.Product.Include(i => i.Category).Include(t => t.Brand).ToListAsync());
        }

        //returns newst products from DB to the JS ProductIndex to be inserted in the html(in JS we always take 6 and show them)
        //returns 6 products filtered by params
        public async Task<IActionResult> getProducts(string productName, int brandId, int categoryId, string sort, int skipCount, int takeCount)
        {
            var products = await _context.Product.Include(i => i.Category).Include(t => t.Brand).Where(p=>p.InStock==true).ToListAsync();

            if (productName != null)
                 products.RemoveAll(p => p.Name != productName);

            if (brandId != 0)
                products.RemoveAll(b => b.Brand.Id != brandId);

            if(categoryId != 0)
                products.RemoveAll(c => c.Category.Id != categoryId);


            //by default the list ordered by newest
            if (sort == "low2high")
                products.Sort((a, b)=>a.Price.CompareTo(b.Price));

            if (sort == "high2low")
            {
                products.Sort((a, b) => a.Price.CompareTo(b.Price));
                products.Reverse();
            }
                
            if (sort == "alphabetical")
                products.Sort((a, b) => a.Name.CompareTo(b.Name));


            if (sort == "newest")
                products.Sort((a, b) => b.Id.CompareTo(a.Id));


            var query =
                from product in products
                select new
                {
                    id = product.Id,
                    imageUrl = product.ImageUrl,
                    name = product.Name,
                    price = product.Price,

                };

            return Json(query.Skip(skipCount).Take(takeCount)); 
        }



        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        ///                                                         Products Detalis Page:
        ///                                                  
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Errors", "Products");
            }

            var product = await _context.Product.Include(b => b.Brand).Include(i => i.Reviews).ThenInclude(c => c.SentBy).FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return RedirectToAction("Errors", "Products");
            }



            return View(product);
        

    }


   
  
      
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        ///                                                         SearchBox Method for Upper Nav:
        ///                                                  
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Search(string term)
        {
            var result = from p in _context.Product
                         where p.Name.Contains(term)
                         select new { id = p.Id, label = p.Name, value = p.Id };
            return Json(await result.ToListAsync());
        }











        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        ///                                                         ADMIN PAGES:
        ///                                                  
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Admin()
        {
            return View(await _context.Product.Include(i => i.Category).Include(t => t.Brand).ToListAsync());
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// CREATE Methods:
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.Brands = new SelectList(_context.Brand.ToList(), "Id", "Name");
            ViewBag.Categories = new SelectList(_context.Category.ToList(), "Id", "Name");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Description,ImageUrl,FrontImageUrl,BackImageUrl,UnitsInStock")] Product product, int BrandId, int CategoryId)
        {


            product.TotalPurchases = 0;
            product.Brand = _context.Brand.First(b => b.Id == BrandId);
            product.Category = _context.Category.First(c => c.Id == CategoryId);

            if (product.ImageUrl == null)
                product.ImageUrl = "/images/Default.png";
            if (product.BackImageUrl == null)
                product.BackImageUrl = "/images/Default.png";
            if (product.FrontImageUrl == null)
                product.FrontImageUrl = "/images/Default.png";

            if (product.Brand == null)
                ModelState.AddModelError("Brand", "Please select a brand.");

            if (product.Category == null)
                ModelState.AddModelError("Category", "Please select a category.");

            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// EDIT Methods:
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Errors", "Products");
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return RedirectToAction("Errors", "Products");
            }
            ViewBag.Brands = new SelectList(_context.Brand.ToList(), "Id", "Name");
            ViewBag.Categories = new SelectList(_context.Category.ToList(), "Id", "Name");
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Description,ImageUrl,FrontImageUrl,BackImageUrl,UnitsInStock")] Product product, int BrandId, int CategoryId)
        {
            if (id != product.Id)
            {
                return RedirectToAction("Errors", "Products");

            }



            if (product.ImageUrl == null)
                product.ImageUrl = "/images/Default.png";
            if (product.BackImageUrl == null)
                product.BackImageUrl = "/images/Default.png";
            if (product.FrontImageUrl == null)
                product.FrontImageUrl = "/images/Default.png";
            product.Brand = _context.Brand.FirstOrDefault(b => b.Id == BrandId);
            product.Category = _context.Category.FirstOrDefault(c => c.Id == CategoryId);

   

            //if (product.Brand == null)
            //    ModelState.AddModelError("Brand", "Please select a brand.");

            //if (product.Category == null)
            //    ModelState.AddModelError("Category", "Please select a category.");


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return RedirectToAction("Errors", "Products");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }




        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// DELETE Methods:
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Errors", "Products");
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return RedirectToAction("Errors", "Products");
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);

            //Delete assosiated reviews and shoppingcart items, orderItems will get null,
            _context.Review.RemoveRange(_context.Review.Where(p => p.Product == product));
            _context.ShoppingCartItem.RemoveRange(_context.ShoppingCartItem.Where(p => p.Product == product));
            _context.Product.Remove(product);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Admin));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }


    }
}
