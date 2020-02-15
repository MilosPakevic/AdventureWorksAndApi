using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using XimbleAdventureWorksApi.Controllers;
using XimbleAdventureWorksApi.Models.ApiResponses;
using XimbleAdventureWorksApi.Services;

namespace XimbleAdventureWorksApi.Tests.Controllers
{
    [TestClass]
    public class PurchaseOrdersControllerTest
    {
        private Mock<IPurchaseOrderService> _service = new Mock<IPurchaseOrderService>();
        List<ApiPurchaseOrderSum> apiPurchaseOrders = new List<ApiPurchaseOrderSum>() {
                new ApiPurchaseOrderSum { DueDate = DateTime.Now, LineTotalSum = 100, OrderQtySum = 10 }
            };

        [TestMethod]
        public void TestPurchaseOrdersControllerShouldReturnAll()
        {
            //Arrange
            PurchaseOrdersController controller = new PurchaseOrdersController(_service.Object);

            //Act
            var result = (JsonResult<Response<IEnumerable<ApiPurchaseOrderSum>>>)controller.GetAllPurchaseDetails().Result;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestPurchaseOrdersControllerShouldReturnFiltered()
        {
            //Arrange -> DateTime? start, DateTime? end, int pageIndex, int pageSize

            _service.Setup(s => s.GetApiPurchaseOrderSumFiltered(It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(apiPurchaseOrders.AsQueryable()));
            PurchaseOrdersController controller = new PurchaseOrdersController(_service.Object);

            //Act
            var result = (JsonResult<Response<IEnumerable<ApiPurchaseOrderSum>>>)controller.GetPurchaseDetailsSumFiltered(DateTime.Now, DateTime.Now, 0, 50).Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Content.Data.Count());
            Assert.IsTrue(result.Content.Data.First().LineTotalSum == 100);
            Assert.IsTrue(result.Content.Data.First().OrderQtySum == 10);
        }       
    }
}
