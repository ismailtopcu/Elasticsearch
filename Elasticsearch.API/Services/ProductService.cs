using Elasticsearch.API.DTOs;
using Elasticsearch.API.Model;
using Elasticsearch.API.Repositories;
using System.Collections.Immutable;
using System.Drawing;
using System.Net;

namespace Elasticsearch.API.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;

        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ResponseDto<ProductDto>> SaveAsync(ProductCreateDto request)
        {

            var responseProduct = await _productRepository.SaveAsync(request.CreateProduct());

            if (responseProduct == null) 
            {
                return ResponseDto<ProductDto>.Fail(new List<string> { "kayıt esnasında hata meydana geldi" },System.Net.HttpStatusCode.InternalServerError);
            }

            return ResponseDto<ProductDto>.Success(responseProduct.CreateDto(), System.Net.HttpStatusCode.Created);
        }

        public async Task<ResponseDto<List<ProductDto>>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var productListDto = new List<ProductDto>();
           // var productListDto= products.Select(x=> new ProductDto(x.Id,x.Name,x.Price,x.Stock,new ProductFeatureDto(x.Feature.Width,x.Feature.Height,x.Feature.Color))).ToList();

            foreach (var x in products)
            {
                if (x.Feature is null)
                {
                    productListDto.Add(new ProductDto(x.Id,x.Name,x.Price,x.Stock,null));
                }

                else
                {
                    productListDto.Add(new ProductDto(x.Id, x.Name, x.Price, x.Stock, new ProductFeatureDto(x.Feature.Width, x.Feature.Height, x.Feature.Color)));
                }

            }

            return ResponseDto<List<ProductDto>>.Success(productListDto, HttpStatusCode.OK);
        }
    }
}
