using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductRepository repo) : ControllerBase
    {

        // ActionResults Allow Us to return HTTP responses
        // IEnumerable Type of List of Type Prodcut
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Products>>> GetProducts(string? brand,
            string? type,string? sort)
        {
            return Ok(await repo.GetProductAsync(brand,type,sort));
        }

        // For Specific Product
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Products>> GetProduct(int id)
        {
            var Product = await repo.GetProductByIdAsync(id);

            // Definsive Check for the product
            if(Product == null) return NotFound();

            return Product;
        }

        [HttpPost]
        public async Task<ActionResult<Products>> CreateProduct(Products product)
        {

            repo.Addproduct(product);

            if(await repo.SaveChangesAsync()) 
            {
                return CreatedAtAction("GetProduct", new {id = product.Id},product); 
            }

            return BadRequest("Problem creating product");
        }
        
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id,Products product)
        {
            
            if (product.Id != id || !ProductExists(id)) 
                return BadRequest("Connot Update this product");

            // This tell the entity framework tracker that we're passing in here is 
            // an entity effectivily abd has been modified
            repo.UpdateProduct(product);

            if(await repo.SaveChangesAsync())
            {
                return NoContent();
            }

            return BadRequest("Problem updating the product");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await repo.GetProductByIdAsync(id);

            if (product == null) return NotFound("This Product of id : "+id+" is not found");

            // This mean that dotnet is tracking this product
            repo.DeleteProduct(product);

            // Then Update the database 
            await repo.SaveChangesAsync();

            if(await repo.SaveChangesAsync())
            {
                return NoContent();
            }

            return BadRequest("Problem deleting the product");
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            return Ok(await repo.GetBrandsAsync());
        }  

        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            return Ok(await repo.GetTypesAsync());
        }

        private bool ProductExists(int id)
        {
            return repo.ProductExists(id);
        }
    }
}
