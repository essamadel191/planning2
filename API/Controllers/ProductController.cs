using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IGenericRepository<Products> repo) : ControllerBase
    {

        // ActionResults Allow Us to return HTTP responses
        // IEnumerable Type of List of Type Prodcut
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Products>>> GetProducts(string? brand,
            string? type,string? sort)
        {
            var spec = new ProductSpecification(brand,type,sort);
            var products = await repo.ListAsync(spec);

            return Ok(products);
        }

        // For Specific Product
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Products>> GetProduct(int id)
        {
            var Product = await repo.GetByIdAsync(id);

            // Definsive Check for the product
            if(Product == null) return NotFound();

            return Product;
        }

        [HttpPost]
        public async Task<ActionResult<Products>> CreateProduct(Products product)
        {

            repo.Add(product);

            if(await repo.SaveAllAsync()) 
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
            repo.Update(product);

            if(await repo.SaveAllAsync())
            {
                return NoContent();
            }

            return BadRequest("Problem updating the product");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await repo.GetByIdAsync(id);

            if (product == null) return NotFound("This Product of id : "+id+" is not found");

            // This mean that dotnet is tracking this product
            repo.Remove(product);

            // Then Update the database 
            await repo.SaveAllAsync();

            if(await repo.SaveAllAsync())
            {
                return NoContent();
            }

            return BadRequest("Problem deleting the product");
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            var spec = new BrandListSpecification();

            return Ok(await repo.ListAsync(spec));
        }  

        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            var spec = new TypeListSpecification();

            return Ok(await repo.ListAsync(spec));
        }

        private bool ProductExists(int id)
        {
            return repo.Exists(id);
        }
    }
}
