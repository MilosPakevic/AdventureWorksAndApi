using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using XimbleAdventureWorksApi.Models.ApiResponses;
using XimbleAdventureWorksApi.Services;

namespace XimbleAdventureWorksApi.Controllers
{
    public class PurchaseOrdersController : ApiController
    {
        private readonly IPurchaseOrderService _purchaseOrderService;
        public PurchaseOrdersController(IPurchaseOrderService service)
        {
            _purchaseOrderService = service;
        }

        // GET: api/PurchaseOrder
        [HttpGet]
        public async Task<IHttpActionResult> GetAllPurchaseDetails()
        {
            var model = new List<ApiPurchaseOrderSum>();
            return Json(new Response<IEnumerable<ApiPurchaseOrderSum>>(model));
        }

        [HttpGet]
        // GET: api/PurchaseOrder/start,end
        public async Task<IHttpActionResult> GetPurchaseDetailsSumFiltered(DateTime? start, DateTime? end, int? pageIndex, int? pageSize)
        {
            var model = await _purchaseOrderService.GetApiPurchaseOrderSumFiltered(start, end, pageIndex.GetValueOrDefault(0), pageSize.GetValueOrDefault(50));
            return Json(new Response<IEnumerable<ApiPurchaseOrderSum>>(model) { PageIndex = pageIndex, PageSize = pageSize });
        }
    }
}
