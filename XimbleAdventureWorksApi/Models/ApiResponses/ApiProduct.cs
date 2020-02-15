using System;

namespace XimbleAdventureWorksApi.Models.ApiResponses
{
    public class ApiProduct
    {
        public int ProductID { get; set; }
        public string ProductNumber { get; set; }
        public string ProductName { get; set; }
        public DateTime SellStartDate { get; set; }
        public string Description { get; set; }
        public string DescriptionCulture { get; set; }
    }
}