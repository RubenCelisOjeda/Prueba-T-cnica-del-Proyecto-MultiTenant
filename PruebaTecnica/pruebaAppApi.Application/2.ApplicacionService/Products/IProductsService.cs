using pruebaAppApi.Application._1.Dto.Product.GetAllProduct.Response;
using pruebaAppApi.Application._1.Dto.Product.GetProduct.Response;
using pruebaAppApi.Application._1.Dto.Products.AddProducts.Request;
using pruebaAppApi.Application._1.Dto.Products.UpdateProducts.Request;
using pruebaAppApi.Application._3.Common;

namespace pruebaAppApi.Application._2.ApplicacionService.Products
{
    public interface IProductsService
    {
        public Task<BaseApiResponse<IEnumerable<GetAllProductResponseDto>>> GetAllProduct();
        public Task<BaseApiResponse<GetProductResponseDto>> GetProduct(int id);
        public Task<BaseApiResponse<int>> AddProduct(AddProductRequestDto itemRequest);
        public Task<BaseApiResponse<int>> UpdateProduct(UpdateProductRequestDto itemRequest);
        public Task<BaseApiResponse<int>> DeleteProduct(int id);
    }
}
