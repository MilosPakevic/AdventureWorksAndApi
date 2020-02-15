using System;
using System.Linq;
using System.Threading.Tasks;
using XimbleAdventureWorksApi.Models.ApiResponses;

namespace XimbleAdventureWorksApi.Services
{
    public interface IPurchaseOrderService
    {
        Task<IQueryable<ApiPurchaseOrderSum>> GetApiPurchaseOrderSumFiltered(DateTime? start, DateTime? end, int pageIndex, int pageSize);
    }
}