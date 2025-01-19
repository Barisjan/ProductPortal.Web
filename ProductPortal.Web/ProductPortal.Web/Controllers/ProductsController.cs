using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductPortal.Web.Data;
using ProductPortal.Web.Models;
using ProductPortal.Web.Models.Entities;


namespace ProductPortal.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ProductsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
       
        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel viewModel)
        {
            var product = new Product
            {
                Name = viewModel.Name,
                Price = viewModel.Price,
                StockQuantity = viewModel.StockQuantity,
            };
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();


            return RedirectToAction("Products", "List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
           var products = await dbContext.Products.ToListAsync();

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await dbContext.Products.FindAsync (id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult>Edit(Product viewModel)
        {
            var product = await dbContext.Products.FindAsync(viewModel.Id);

            if (product is not null)
            {
                product.Name = viewModel.Name;
                product.Price = viewModel.Price;
                product.StockQuantity = viewModel.StockQuantity;

                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Products");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(Product viewModel)
        {
            var product = await dbContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == viewModel.Id);
           
            if (product is not null)
            {
                dbContext.Products.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Products");
        }


    }
}
