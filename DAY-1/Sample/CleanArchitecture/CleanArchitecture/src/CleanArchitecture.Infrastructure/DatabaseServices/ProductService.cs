using CleanArchitecture.Application.DatabaseServices;
using CleanArchitecture.Application.Models;
using System;
using System.Collections.Generic;
using Dapper;
using System.Threading.Tasks;
using System.Linq;
using SqlKata.Execution;
using SqlKata.Compilers;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Infrastructure.DatabaseServices
{
    public class ProductService : IProductService
    {
        private readonly IDatabaseConnectionFactory _database;

        public ProductService(IDatabaseConnectionFactory database)
        {
            _database = database;
        }

        public async Task<bool> CreateProduct(Product request)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            if (!await IsProductKeyUnique(db, request.ProductKey, Guid.Empty))
               return false;

            var affectedRecords = await db.Query("Product").InsertAsync(new
            {
                ProductID = Guid.NewGuid(),
                ProductKey = request.ProductKey,
                ProductName = request.ProductName,
                ProductImageUri = request.ProductImageUri,
                ProductTypeID=request.ProductType,
                RecordStatus = request.RecordStatus,
                CreatedDate = DateTime.UtcNow,
                UpdatedUser = Guid.NewGuid()
            });
           
            return affectedRecords > 0;
        }


        public async Task<bool> UpdateProduct(Product request)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            var affectedRecords = await db.Query("Product").Where("ProductID",request.ProductID).UpdateAsync(new
            {
                ProductKey = request.ProductKey,
                ProductName = request.ProductName,
                ProductImageUri = request.ProductImageUri,
                ProductTypeID = request.ProductType,
                RecordStatus = request.RecordStatus,
                UpdatedDate = DateTime.UtcNow,
                UpdatedUser = Guid.NewGuid()
            });

            return affectedRecords > 0;
        }

        public async Task<bool> DeleteProduct(Guid productId)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());
            var affectedRecords = await db.Query("Product").Where("ProductID", productId).UpdateAsync(new
            {
                RecordStatus = RecordStatus.InActive,
            });
            return affectedRecords > 0;
        }

        public async Task<IEnumerable<Product>> FetchProduct()
        {
            using var conn = await _database.CreateConnectionAsync();
            var result = conn.Query<Product>("Select * from Product").ToList();
            return result;
        }

        private async Task<bool> IsProductKeyUnique(QueryFactory db, string productKey, Guid productID)
        {
            var result = await db.Query("Product").Where("ProductKey", "=", productKey)
                .FirstOrDefaultAsync<ProductType>();

            if (result == null)
                return true;

            return result.ProductTypeID == productID;
        }
    }
}
