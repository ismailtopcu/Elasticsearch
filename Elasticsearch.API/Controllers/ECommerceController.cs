﻿using Elasticsearch.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elasticsearch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ECommerceController : ControllerBase
    {
        private readonly ECommerceRepository _repository;

        public ECommerceController(ECommerceRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> TermQuery(string customerFirstName)
        {
            return Ok(await _repository.TermQuery(customerFirstName));
        }

        [HttpPost]
        public async Task<IActionResult> TermsQuery(List<string> customerFirstNameList)
        {
            return Ok(await _repository.TermsQuery(customerFirstNameList));
        }

        [HttpGet]
        public async Task<IActionResult> PrefixQuery(string customerFullName)
        {
            return Ok(await _repository.PrefixQueryAsync(customerFullName));
        }
        [HttpGet]
        public async Task<IActionResult> RangeQuery(double fromPrice,double toPrice)
        {
            return Ok(await _repository.RangeQueryAsync(fromPrice,toPrice));
        }
        [HttpGet]
        public async Task<IActionResult> MatchAllQuery()
        {
            return Ok(await _repository.MatchAllQueryAsync());
        }
        [HttpGet]
        public async Task<IActionResult> PaginationQuery(int page=1,int pageSize=3)
        {
            return Ok(await _repository.PaginationQueryAsync(page,pageSize));
        }
        [HttpGet]
        public async Task<IActionResult> WildCardQuery(string customerFullName)
        {
            return Ok(await _repository.WildCardQueryAsync(customerFullName));
        }
    }
}