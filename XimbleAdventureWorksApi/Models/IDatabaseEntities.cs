using System.Data.Entity;

namespace XimbleAdventureWorksApi.Models
{
    public interface IDatabaseEntities
    {
        DbSet<Culture> Cultures { get; set; }
        DbSet<ProductDescription> ProductDescriptions { get; set; }
        DbSet<ProductModelProductDescriptionCulture> ProductModelProductDescriptionCultures { get; set; }
        DbSet<ProductModel> ProductModels { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        DbSet<PurchaseOrderHeader> PurchaseOrderHeaders { get; set; }
    }
}