using System;
using System.Linq;
using System.Threading.Tasks;
using XimbleAdventureWorksApi.Models.ApiResponses;

namespace XimbleAdventureWorksApi.Services
{
    public interface IProductService
    {
        Task<IQueryable<ApiProduct>> GetAllApiProducts();
        Task<IQueryable<ApiProduct>> GetApiProductsFiltered(string name, DateTime? date, string keyword, string culture, int pageIndex, int pageSize);
    }
}