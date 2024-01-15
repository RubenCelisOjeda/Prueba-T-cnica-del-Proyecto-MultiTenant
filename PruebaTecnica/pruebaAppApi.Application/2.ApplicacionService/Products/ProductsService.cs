using AutoMapper;
using Microsoft.Extensions.Logging;
using pruebaAppApi.Application._1.Dto.Product.GetAllProduct.Response;
using pruebaAppApi.Application._1.Dto.Product.GetProduct.Response;
using pruebaAppApi.Application._1.Dto.Products.AddProducts.Request;
using pruebaAppApi.Application._1.Dto.Products.UpdateProducts.Request;
using pruebaAppApi.Application._3.Common;
using pruebaAppApi.Domain._2.Repository.Products;

namespace pruebaAppApi.Application._2.ApplicacionService.Products
{
    public class ProductsService : IProductsService
    {
        #region [Properties]
        private readonly ILogger<ProductsService> _logger;
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;
        #endregion

        #region [Constructor]
        public ProductsService(ILogger<ProductsService> logger, IProductsRepository productsRepository, IMapper mapper)
        {
            _logger = logger;
            _productsRepository = productsRepository;
            _mapper = mapper;
        }
        #endregion

        #region [Methods]
        public async Task<BaseApiResponse<IEnumerable<GetAllProductResponseDto>>> GetAllProduct()
        {
            try
            {
                _logger.LogInformation("------GetAllProduct Started------");

                #region [1.Request]
                #endregion

                #region [2.GetAll]
                var response = await _productsRepository.GetAllProduct();
                var mapperResponse = _mapper.Map<IEnumerable<GetAllProductResponseDto>>(response);
                #endregion

                #region [3.Response]
                return new BaseApiResponse<IEnumerable<GetAllProductResponseDto>>
                {
                    status = Constant.ResponseCode.SuccessCode,
                    statusText = Constant.ResponseMessage.SuccessGetMessage,
                    Data = mapperResponse
                };
                #endregion
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new BaseApiResponse<IEnumerable<GetAllProductResponseDto>>
                {
                    status = Constant.ResponseCode.ErrorCode,
                    statusText = Constant.ResponseMessage.ErrorMessage
                };
            }
            finally
            {
                _logger.LogInformation("------GetAllProduct Finished------");
            }
        }

        public async Task<BaseApiResponse<GetProductResponseDto>> GetProduct(int id)
        {
            try
            {
                _logger.LogInformation("------GetProduct Started------");

                #region [1.Request]
                #endregion

                #region [2.Get]
                var response = await _productsRepository.GetProduct(id);
                var mapperResponse = _mapper.Map<GetProductResponseDto>(response);
                #endregion

                #region [3.Response]
                return new BaseApiResponse<GetProductResponseDto>
                {
                    status = Constant.ResponseCode.SuccessCode,
                    statusText = Constant.ResponseMessage.SuccessGetMessage,
                    Data = mapperResponse
                };
                #endregion
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new BaseApiResponse<GetProductResponseDto>
                {
                    status = Constant.ResponseCode.ErrorCode,
                    statusText = Constant.ResponseMessage.ErrorMessage
                };
            }
            finally
            {
                _logger.LogInformation("------GetProduct Finished------");
            }
        }

        public async Task<BaseApiResponse<int>> AddProduct(AddProductRequestDto itemRequest)
        {
            try
            {
                _logger.LogInformation("------AddProduct Started------");

                #region [1.Request]
                var itemRequestMapper = _mapper.Map<pruebaAppApi.Domain._1.Entities.Products>(itemRequest);
                #endregion

                #region [2.Add]
                var response = await _productsRepository.AddProduct(itemRequestMapper);
                #endregion

                #region [3.Response]
                return new BaseApiResponse<int>
                {
                    status = response > 0 ? Constant.ResponseCode.SuccessCode : Constant.ResponseCode.WarningCode,
                    statusText = response > 0 ? Constant.ResponseMessage.SuccessAddMessage : Constant.ResponseMessage.WarningAddMessage,
                    Data = response
                };
                #endregion
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new BaseApiResponse<int>
                {
                    status = Constant.ResponseCode.ErrorCode,
                    statusText = Constant.ResponseMessage.ErrorMessage
                };
            }
            finally
            {
                _logger.LogInformation("------AddProduct Finished------");
            }
        }

        public async Task<BaseApiResponse<int>> UpdateProduct(UpdateProductRequestDto itemRequest)
        {
            try
            {
                _logger.LogInformation("------UpdateProduct Started------");

                #region [1.Request]
                var itemRequestMapper = _mapper.Map<pruebaAppApi.Domain._1.Entities.Products>(itemRequest);
                #endregion

                #region [2.Update]
                var response = await _productsRepository.UpdateProduct(itemRequestMapper);
                #endregion

                #region [3.Response]
                return new BaseApiResponse<int>
                {
                    status = response > 0 ? Constant.ResponseCode.SuccessCode : Constant.ResponseCode.WarningCode,
                    statusText = response > 0 ? Constant.ResponseMessage.SuccessUpdateMessage : Constant.ResponseMessage.WarningAddMessage,
                    Data = response
                };
                #endregion
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new BaseApiResponse<int>
                {
                    status = Constant.ResponseCode.ErrorCode,
                    statusText = Constant.ResponseMessage.ErrorMessage
                };
            }
            finally
            {
                _logger.LogInformation("------UpdateProduct Finished------");
            }
        }

        public async Task<BaseApiResponse<int>> DeleteProduct(int id)
        {
            try
            {
                _logger.LogInformation("------DeleteProduct Started------");

                #region [1.Request]
                #endregion

                #region [2.Delete]
                var response = await _productsRepository.DeleteProduct(id);
                #endregion

                #region [3.Response]
                return new BaseApiResponse<int>
                {
                    status = response > 0 ? Constant.ResponseCode.SuccessCode : Constant.ResponseCode.WarningCode,
                    statusText = response > 0 ? Constant.ResponseMessage.SuccessDeleteMessage : Constant.ResponseMessage.WarningAddMessage,
                    Data = response
                };
                #endregion
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new BaseApiResponse<int>
                {
                    status = Constant.ResponseCode.ErrorCode,
                    statusText = Constant.ResponseMessage.ErrorMessage
                };
            }
            finally
            {
                _logger.LogInformation("------DeleteProduct Finished------");
            }
        }
        #endregion

        #region [Functions]

        #endregion
    }
}
