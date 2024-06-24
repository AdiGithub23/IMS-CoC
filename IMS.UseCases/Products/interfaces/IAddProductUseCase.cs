using IMS.CoreBusiness;
using IMS.CoreBusiness.DTO;

namespace IMS.UseCases.Products.interfaces
{
    public interface IAddProductUseCase
    {
        Task ExecuteAsync(Product product);
    }
}