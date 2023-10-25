using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> productRepository;
        private readonly IGenericRepository<ProductBrand> brandRepository;
        private readonly IGenericRepository<ProductType> typeRepository;

        public ProductsController(IGenericRepository<Product> productRepository, 
            IGenericRepository<ProductBrand> brandRepository,
            IGenericRepository<ProductType> typeRepository)
        {
            this.productRepository = productRepository;
            this.brandRepository = brandRepository;
            this.typeRepository = typeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await productRepository.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProducts(int id) => await productRepository.GetByIdAsync(id);

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands() => Ok(await productRepository.GetAllAsync());

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductTypes() => Ok(await productRepository.GetAllAsync());
    }
}
