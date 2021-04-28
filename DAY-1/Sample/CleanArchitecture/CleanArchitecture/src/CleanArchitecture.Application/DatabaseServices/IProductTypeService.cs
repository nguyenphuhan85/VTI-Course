using CleanArchitecture.Application.CQRS.ProductType.Command;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Application.Models.ProductType.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DatabaseServices
{
    public interface IProductTypeService
    {
        Task<bool> CreateProductType(CreateProductTypeCommand request);
        Task<bool> UpdateProductType(UpdateProductTypeCommand request);
        Task<bool> DeleteProductType(DeleteProductTypeCommand request);
        Task<IEnumerable<ProductTypeResponseModel>> FetchProductType();
        Task<Models.ProductType.Response.ProductTypeDetailsResponseModel> GetProductTypeDetails(Guid productTypeId);
    }
}
