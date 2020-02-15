using System;
using System.Linq;
using System.Threading.Tasks;
using XimbleAdventureWorksApi.Models;
using XimbleAdventureWorksApi.Models.ApiResponses;

namespace XimbleAdventureWorksApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IDatabaseEntities _data;
        public ProductService(IDatabaseEntities db)
        {
            _data = db;
        }
        public async Task<IQueryable<ApiProduct>> GetAllApiProducts()
        {
            var result = from prod in _data.Products
                         join pmpdc in _data.ProductModelProductDescriptionCultures
                         on prod.ProductModelID equals pmpdc.ProductModelID into pmpdcs
                         from pmpdc in pmpdcs.DefaultIfEmpty()
                         join pdesc in _data.ProductDescriptions
                         on pmpdc.ProductDescriptionID equals pdesc.ProductDescriptionID into pdescs
                         from pdesc in pdescs.DefaultIfEmpty()
                         select new ApiProduct
                         {
                             ProductID = prod.ProductID,
                             ProductNumber = prod.ProductNumber,
                             ProductName = prod.Name,
                             SellStartDate = prod.SellStartDate,
                             Description = pdesc.Description == null ? "" : pdesc.Description,
                             DescriptionCulture = pmpdc.CultureID == null ? "" : pmpdc.CultureID.Trim()
                         };
            return result;
        }
        public async Task<IQueryable<ApiProduct>> GetApiProductsFiltered(string name, DateTime? date, string keyword, string culture, int pageIndex, int pageSize)
        {
            var allProducts = await GetAllApiProducts();
            return allProducts.Where(p =>
                (String.IsNullOrEmpty(name) ? true : p.ProductName.Contains(name)) &&
                (date.HasValue ? p.SellStartDate == date.Value : true) &&
                (String.IsNullOrEmpty(keyword) ? true : p.Description.Contains(keyword)) &&
                (String.IsNullOrEmpty(culture) ? true : p.DescriptionCulture == culture)).OrderBy(x => x.ProductName).Skip(pageIndex * pageSize).Take(pageSize);
        }
    }
}