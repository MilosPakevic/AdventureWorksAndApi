using System;

namespace XimbleAdventureWorksApi.Models.ApiResponses
{
    public class ApiPurchaseOrderSum
    {
        public DateTime DueDate { get; set; }
        public decimal LineTotalSum { get; set; }
        public decimal OrderQtySum { get; set; }
    }
}