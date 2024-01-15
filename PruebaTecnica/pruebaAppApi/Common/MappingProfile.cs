using AutoMapper;
using pruebaAppApi.Application._1.Dto.Product.GetAllProduct.Response;
using pruebaAppApi.Application._1.Dto.Product.GetProduct.Response;
using pruebaAppApi.Application._1.Dto.Products.AddProducts.Request;
using pruebaAppApi.Application._1.Dto.Products.UpdateProducts.Request;

namespace pruebaAppApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region [Product]
            CreateMap<AddProductRequestDto, pruebaAppApi.Domain._1.Entities.Products>();
            CreateMap<UpdateProductRequestDto, pruebaAppApi.Domain._1.Entities.Products>();
            CreateMap<pruebaAppApi.Domain._1.Entities.Products, GetProductResponseDto>();
            CreateMap<pruebaAppApi.Domain._1.Entities.Products, GetAllProductResponseDto>();
            #endregion
        }
    }
}
