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
    public class ProductsControllerTest
    {
        private Mock<IProductService> _productService = new Mock<IProductService>();
        List<ApiProduct> apiProducts = new List<ApiProduct>() {
                new ApiProduct { Description = "aaa", DescriptionCulture = "en", ProductID = 1, ProductName = "test", ProductNumber = "0", SellStartDate = DateTime.Now }
            };

        [TestMethod]
        public void TestProductsControllerShouldReturnAll()
        {
            //Arrange
            _productService.Setup(s => s.GetAllApiProducts()).Returns(Task.FromResult(apiProducts.AsQueryable()));
            ProductsController controller = new ProductsController(_productService.Object);

            //Act
            var result = (JsonResult<Response<IEnumerable<ApiProduct>>>)controller.GetAllProducts().Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Content.Data.Count());
            Assert.IsTrue(result.Content.Data.First().Description == "aaa");
        }

        [TestMethod]
        public void TestProductsControllerShouldReturnFiltered()
        {
            //Arrange string name, DateTime? date, string keyword, string culture, int pageIndex, int pageSize

            _productService.Setup(s => s.GetApiProductsFiltered("test", It.IsAny<DateTime?>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(apiProducts.AsQueryable()));
            ProductsController controller = new ProductsController(_productService.Object);

            //Act
            var result = (JsonResult<Response<IEnumerable<ApiProduct>>>)controller.GetProductsFiltered("test", DateTime.Now, "", 0, 50, "").Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Content.Data.Count());
            Assert.IsTrue(result.Content.Data.First().ProductName == "test");
        }
       
    }
}
