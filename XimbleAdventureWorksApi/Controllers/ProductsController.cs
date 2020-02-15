using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using XimbleAdventureWorksApi.Models.ApiResponses;
using XimbleAdventureWorksApi.Services;

namespace XimbleAdventureWorksApi.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService service)
        {           
            _productService = service;
        }        

        // GET: api/Products
        [HttpGet]
        public async Task<IHttpActionResult> GetAllProducts()
        {
            var model = await _productService.GetAllApiProducts();
            return Json(new Response<IEnumerable<ApiProduct>>(model));
        }

        // GET: api/Products/name,date,keyword
        [HttpGet]
        public async Task<IHttpActionResult> GetProductsFiltered(string name, DateTime? date, string keyword, int? pageIndex, int? pageSize, string culture = "en")
        {
            var model = await _productService.GetApiProductsFiltered(name, date, keyword, culture, pageIndex.GetValueOrDefault(0), pageSize.GetValueOrDefault(50));
            return Json(new Response<IEnumerable<ApiProduct>>(model) { PageIndex = pageIndex, PageSize = pageSize }) ;
        }        
    }
}
