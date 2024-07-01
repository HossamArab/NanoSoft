namespace NanoSoftCode.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product? GetbyId(int? id);
        Task Create(ProductFormViewModel model);
        Task<Product?> Update(ProductFormViewModel model);
        Task Delete(ProductFormViewModel model);
        IEnumerable<Product> GetByCategoryID(int? ID);
    }
}
