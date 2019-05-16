using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using ChinesePoetry.Database;
using ChinesePoetry.Database.Models;
using ChinesePoetry.Database.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace ChinesePoetry.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly PoetryDbContext _dbContext;
        private readonly HttpClient _http;
        private readonly IMapper _mapper;
        public ValuesController(PoetryDbContext dbContext,
            IMapper mapper,
            IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<object> InsertMingJu()
        {
            var name = "mingju";
            var url = "http://app.gushiwen.org/api/mingju/Default2.aspx?id=&c=&token=gswapi&pwd=&t=&page={0}";
            var home = await _httpClientFactory.CreateClient(name).GetStringAsync(string.Format(url, 0));
            var first = JsonConvert.DeserializeObject<MingJuResult>(home);
            var data = new List<MingJuResult>();
            var @lock = new object();

            var result = Parallel.For(1, first.sumPage + 1, index =>
            {
                var http = _httpClientFactory.CreateClient(name);
                var json = http.GetStringAsync(string.Format(url, index)).Result;
                var mingju = JsonConvert.DeserializeObject<MingJuResult>(json);

                var mingjus = mingju.mingjus.Select(m => _mapper.Map<MingJu>(m));

                _dbContext.MingJu.InsertMany(mingjus);
                //_dbContext.MingJu.BulkWrite();
                lock (@lock)
                {
                    data.Add(mingju);
                }
            });

            return _mapper.Map<MingJu>(data.First().mingjus.First());
        
        }

    }
}
