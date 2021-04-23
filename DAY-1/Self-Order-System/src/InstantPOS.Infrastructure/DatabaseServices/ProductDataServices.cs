using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InstantPOS.Application.CQRS.Product.Command;
using InstantPOS.Application.DatabaseServices.Interfaces;
using InstantPOS.Application.Models.Product;
using InstantPOS.Domain.Enums;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace InstantPOS.Infrastructure.DatabaseServices   
{
    public class ProductDataServices : IProductDataService
    {
        private readonly IDatabaseConnectionFactory _database;

        public ProductDataServices(IDatabaseConnectionFactory database)
        {
            _database = database;
        }

        public async Task<bool> CreateProduct(CreateProductCommand request)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            if (!await IsProductKeyUnique(db, request.ProductKey, Guid.Empty))
                return false;

            var affectedRecords = await db.Query("Product").InsertAsync(new
            {
                ProductId = Guid.NewGuid(),
                ProductKey = request.ProductKey,
                ProductName = request.ProductName,
                ProductImageUri = request.ProductImageUri,
                ProductTypeID = request.ProductTypeID,
                RecordStatus = request.RecordStatus,
                CreatedDate = DateTime.UtcNow,
                UpdatedUser = Guid.NewGuid()
            });

            return affectedRecords > 0;
        }

        public async Task<bool> DeleteProduct(Guid productId)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            var affectedRecords = await db.Query("Product").Where("ProductID", productId).UpdateAsync(new {RecordStatus=RecordStatus.InActive
            });

            return affectedRecords > 0;
        }

        public async Task<IEnumerable<ProductResponseModel>> FetchProduct()
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            var result = db.Query("Product")
                .Select(
                "ProductID",
                "ProductKey",
                "ProductName",
                "ProductImageUri",
                "ProductTypeName",
                "Product.RecordStatus")
                .Join("ProductType", "ProductType.ProductTypeID", "Product.ProductTypeID")
                .OrderByDesc("Product.UpdatedDate")
                .OrderByDesc("Product.CreatedDate")
                .ForPage(3,5); 

            return await result.GetAsync<ProductResponseModel>();
        }

        public async Task<bool> UpdateProduct(UpdateProductCommand request)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());
            var affectedRecords = await db.Query("Product").Where("ProductID", request.ProductID).UpdateAsync(new
            {
                ProductKey = request.ProductKey,
                ProductName = request.ProductName,
                ProductImageUri = request.ProductImageUri,
                ProductTypeID = request.ProductTypeID,
                RecordStatus = request.RecordStatus,
                UpdatedDate = DateTime.UtcNow,
                UpdatedUser = Guid.NewGuid()
            });

            return affectedRecords > 0;
        }

        private async Task<bool> IsProductKeyUnique(QueryFactory db, string productKey, Guid productID)
        {
            var result = await db.Query("Product").Where("ProductKey", "=", productKey)
                .FirstOrDefaultAsync<ProductResponseModel>();

            if (result == null)
                return true;

            return result.ProductID == productID;
        }

        public async Task<ProductResponseModel> GetProduct(Guid productId)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            var result = db.Query("Product")
                .Select(
                "ProductID",
                "ProductKey",
                "ProductName",
                "ProductImageUri",
                "ProductTypeName",
                "Product.RecordStatus")
                .Join("ProductType", "ProductType.ProductTypeID", "Product.ProductTypeID")
                .Where("ProductID",productId)
                .OrderByDesc("Product.UpdatedDate")
                .OrderByDesc("Product.CreatedDate")
                .Take(1);

            return await result.FirstOrDefaultAsync<ProductResponseModel>();
        }
    }
}
