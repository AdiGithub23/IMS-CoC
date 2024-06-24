using IMS.CoreBusiness;
using IMS.CoreBusiness.DTO;

namespace IMS.UseCases.Products.interfaces
{
    public interface IViewProductsByNameUseCase
    {
        Task<IEnumerable<Product>> ExecuteAsync(string name = "");
    }
}