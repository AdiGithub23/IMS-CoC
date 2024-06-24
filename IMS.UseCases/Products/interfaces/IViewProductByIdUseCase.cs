using IMS.CoreBusiness;
using IMS.CoreBusiness.DTO;

namespace IMS.UseCases.Products.interfaces
{
    public interface IViewProductByIdUseCase
    {
        Task<Product?> ExecuteAsync(int productId);
    }
}