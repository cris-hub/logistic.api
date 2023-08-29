using LogisticAPI.Entities;
using LogisticAPI.models;
using LogisticAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogisticAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private IProductService @object;

        public ProductController(IProductService @object)
        {
            this.@object = @object;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProduct(ProductRequest request)
        {

            ProductResponse reponse = await @object.CreateProduct(request);
            return Ok(reponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            ProductResponse reponse = await @object.GetById(id);
            return Ok(reponse);
        }
        [HttpGet("User")]
        [Authorize]
        public async Task<IActionResult> GetByUserId(string id)
        {
            IEnumerable<ProductResponse> reponse = await @object.GetProdutsByUserId(id);
            return Ok(reponse);
        }
    }
}