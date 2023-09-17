﻿using Elasticsearch.API.DTOs;
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
                    productListDto.Add(new ProductDto(x.Id, x.Name, x.Price, x.Stock, new ProductFeatureDto(x.Feature.Width, x.Feature.Height, x.Feature.Color.ToString())));
                }

            }

            return ResponseDto<List<ProductDto>>.Success(productListDto, HttpStatusCode.OK);
        }

        public async Task<ResponseDto<ProductDto>> GetByIdAsync(string id)
        {
            var hasProduct = await _productRepository.GetByIdAsync(id);
            if (hasProduct==null)
            {
                return ResponseDto<ProductDto>.Fail("ürün bulunadı", HttpStatusCode.NotFound);
            }

            return ResponseDto<ProductDto>.Success(hasProduct.CreateDto(),HttpStatusCode.OK);

        }

        public async Task<ResponseDto<bool>> UpdateAsync(ProductUpdateDto updateProduct)
        {
            var isSuccess = await _productRepository.UpdateAsync(updateProduct);
            if (!isSuccess)
            {
                return ResponseDto<bool>.Fail(new List<string> { "Update esnasında bir hata meydana geldi" }, HttpStatusCode.InternalServerError);
            }

            return ResponseDto<bool>.Success(true, HttpStatusCode.NoContent);
        }
        public async Task<ResponseDto<bool>> DeleteAsync(string id)
        {
            var isSuccess = await _productRepository.DeleteAsync(id);
            if (!isSuccess)
            {
                return ResponseDto<bool>.Fail(new List<string> { "silme esnasında bir hata meydana geldi" }, HttpStatusCode.InternalServerError);
            }

            return ResponseDto<bool>.Success(true, HttpStatusCode.NoContent);
        }

    }
}
