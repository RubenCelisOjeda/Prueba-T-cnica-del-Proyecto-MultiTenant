using Microsoft.AspNetCore.Mvc;
using pruebaAppApi.Application._1.Dto.Products.AddProducts.Request;
using pruebaAppApi.Application._1.Dto.Products.UpdateProducts.Request;
using pruebaAppApi.Application._2.ApplicacionService.Products;
using pruebaAppApi.Controllers.Base;

namespace pruebaAppApi.Controllers.Services
{
    [Route("{slugTenant}/products")]
    [ApiController]
    public class ProductsController : BaseController
    {
        #region [Properties]
        private readonly IProductsService _productsService;
        #endregion

        #region [Constructor]
        public ProductsController(IProductsService productsService) => _productsService = productsService;

        #endregion

        #region [Apis]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _productsService.GetAllProduct();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _productsService.GetProduct(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddProductRequestDto itemRequest)
        {
            var response = await _productsService.AddProduct(itemRequest);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateProductRequestDto itemRequest)
        {
            var response = await _productsService.UpdateProduct(itemRequest);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _productsService.DeleteProduct(id);
            return Ok(response);
        }
        #endregion
    }
}
