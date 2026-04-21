using COMP003.LectureActivity5.Data;
using COMP003.LectureActivity5.Models;
using Microsoft.AspNetCore.Mvc;

namespace COMP003.LectureActivity5.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductsController : Controller
	{
		// GET: api/products
		[HttpGet]
		public ActionResult<List<Product>> GetProduct()
		{
			return Ok(ProductStore.Products);
		}

		// GET: api/products/{id}
		[HttpGet("{id}")]
		public ActionResult<Product> GetProduct(int id)
		{
			var product = ProductStore.Products.FirstOrDefault(p => p.Id == id);

			if (product is null)
				return NotFound();

			return Ok(product);
		}

		// POST: api/products
		[HttpPost]
		public ActionResult<Product> CreateProduct(Product product)
		{
			product.Id = ProductStore.Products.Max(p => p.Id) +1;

			ProductStore.Products.Add(product);

			return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
		}

		// PUT: api/product/{id}
		[HttpPut("{id}")]
		public IActionResult UpdateProduct(int id, Product updatedProduct)
		{
			var existingProduct = ProductStore.Products.FirstOrDefault(p => p.Id == id);

			if (existingProduct is null)
				return NotFound();

			existingProduct.Name = updatedProduct.Name;
			existingProduct.Price = updatedProduct.Price;

			return NoContent();
		}

		// DELETE: api/products/{id}
		[HttpDelete("{id}")]
		public IActionResult DeleteProduct(int id)
		{
			var product = ProductStore.Products.FirstOrDefault(p => p.Id == id);

			if (product is null)
				return NotFound();

			ProductStore.Products.Remove(product);

			return NoContent();
		}

		// GET: api/products/filter?price={price}
		[HttpGet("filter")]
		public ActionResult<List<Product>> FilterProducts(decimal price)
		{
			var filteredProducts = ProductStore.Products
				.Where(p  => p.Price <= price)
				.OrderBy(p => p.Price)
				.ToList();

			return Ok(filteredProducts);

		}
		// GET: api/product/names
		[HttpGet("names")]
		public ActionResult<List<string>> GetProductNames()
		{
			var productNames = ProductStore.Products
				.OrderBy(p => p.Name)
				.Select(p => p.Name)
				.ToList();

			return Ok(productNames);
		}
	}
}
