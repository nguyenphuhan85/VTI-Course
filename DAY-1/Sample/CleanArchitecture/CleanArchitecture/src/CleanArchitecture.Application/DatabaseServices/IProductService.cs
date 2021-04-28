using CleanArchitecture.Application.CQRS.Product.Command;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Application.Models.Product.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DatabaseServices
{
   public interface IProductService
    {
        Task<bool> CreateProduct(CreateProductCommand request);

        Task<bool> UpdateProduct(UpdateProductCommand request);
        Task<bool> DeleteProduct(DeleteProductCommand request);
        Task<IEnumerable<Models.Product.Response.ProductResponseModel>> FetchProduct();
        Task<Models.Product.Response.ProductResponseModel> GetProduct(Guid productId);
    }
}
