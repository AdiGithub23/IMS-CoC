using IMS.CoreBusiness;
using IMS.CoreBusiness.DTO;

namespace IMS.UseCases.Products.interfaces
{
    public interface IEditProductUseCase
    {
        Task ExecuteAsync(Product product);
    }
}