using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using XimbleAdventureWorksApi.Models;
using XimbleAdventureWorksApi.Models.ApiResponses;

namespace XimbleAdventureWorksApi.Services
{
    public class PurchaseOrderService: IPurchaseOrderService
    {
        private readonly AdventureWorks2012Entities _data;
        public PurchaseOrderService()
        {
            _data = new AdventureWorks2012Entities();
        }

        public async Task<IQueryable<ApiPurchaseOrderSum>> GetApiPurchaseOrderSumFiltered(DateTime? start, DateTime? end, int pageIndex, int pageSize)
        {
            var startDate = start.GetValueOrDefault(DateTime.MinValue);
            var endDate = end.GetValueOrDefault(DateTime.Now);
            var purch = _data.PurchaseOrderDetails.Where(x => x.DueDate >= startDate && x.DueDate <= endDate);
            var gpurch = purch.GroupBy(x => DbFunctions.TruncateTime(x.DueDate));
            var result = gpurch.Select(x => new ApiPurchaseOrderSum
            {
                DueDate = x.Key.Value,
                LineTotalSum = x.Sum(s => s.LineTotal),
                OrderQtySum = x.Sum(s => s.OrderQty)
            }).OrderBy(x => x.DueDate).Skip(pageIndex * pageSize).Take(pageSize);
            return result;
        }
    }
}