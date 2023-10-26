using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> productRepository;
        private readonly IGenericRepository<ProductBrand> brandRepository;
        private readonly IGenericRepository<ProductType> typeRepository;
        private readonly IMapper mapper;

        public ProductsController(IGenericRepository<Product> productRepository, 
            IGenericRepository<ProductBrand> brandRepository,
            IGenericRepository<ProductType> typeRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.brandRepository = brandRepository;
            this.typeRepository = typeRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductWithTypesAndBrandsSpecification();
            
            var products = await productRepository.GetAllAsync(spec);

            var productToReturnDtos = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            return Ok(productToReturnDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProducts(int id)
        {
            var spec = new ProductWithTypesAndBrandsSpecification(id);

            var product = await productRepository.GetEntityWithSpec(spec);

            var productToReturnDto = mapper.Map<Product, ProductToReturnDto>(product);
            return productToReturnDto;
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands() => Ok(await productRepository.GetAllAsync());

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductTypes() => Ok(await productRepository.GetAllAsync());
    }
}
