using System;
using System.Linq;
using System.Threading.Tasks;
using eShop.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace eShop.Catalog.Infrastructure
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly string _connectionString;
        private readonly ILogger _logger;

        public CatalogRepository(string connectionString, ILogger logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        public async Task<CatalogResponse> GetItemsAsync(int pageIndex, int pageSize)
        {
            using (var dbContext = new CatalogContext())
            {
                try
                {
                    var totalItems = await dbContext.CatalogItems
                        .LongCountAsync();

                    var itemsOnPage = await dbContext.CatalogItems
                        .OrderBy(c => c.Name)
                        .Skip(pageSize * pageIndex)
                        .Take(pageSize)
                        .ToListAsync();
                    
                    return new CatalogResponse{ ItemsOnPage = itemsOnPage, TotalItems = totalItems };
                }
                catch (Exception ex)
                {
                    _logger.Debug(ex.Message);
                    throw;
                }
            }
        }

        public async Task<CatalogItem> GetItemAsync(int id)
        {
            using (var dbContext = new CatalogContext())
            {
                try
                {
                    return await dbContext.CatalogItems.FindAsync(id);
                }
                catch (Exception ex)
                {
                    _logger.Debug(ex.Message);
                    throw;
                }
            }
        }

        public async Task<bool> AddItemAsync(CatalogItem product)
        {
            using (var dbContext = new CatalogContext())
            using (var dbContextTransaction = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    const bool result = true;

                    await dbContext.CatalogItems.AddAsync(product);

                    await dbContext.SaveChangesAsync();

                    dbContextTransaction.Commit();

                    return result;
                }
                catch (Exception ex)
                {
                    _logger.Debug(ex.Message);
                    dbContextTransaction.Rollback();
                    throw;
                }
            }
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            using (var dbContext = new CatalogContext())
            using (var dbContextTransaction = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    const bool result = true;

                    var deleteProduct = await dbContext.CatalogItems.FindAsync(id);
                    dbContext.CatalogItems.Remove(deleteProduct);

                    await dbContext.SaveChangesAsync();

                    dbContextTransaction.Commit();

                    return result;
                }
                catch (Exception ex)
                {
                    _logger.Debug(ex.Message);
                    dbContextTransaction.Rollback();
                    throw;
                }
            }
        }

        public async Task<bool> UpdateItemAsync(CatalogItem product)
        {
            using (var dbContext = new CatalogContext())
            using (var dbContextTransaction = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    const bool result = true;

                    var updateProduct = await dbContext.CatalogItems.FindAsync(product.Id);
                    

                    await dbContext.SaveChangesAsync();

                    dbContextTransaction.Commit();

                    return result;
                }
                catch (Exception ex)
                {
                    _logger.Debug(ex.Message);
                    dbContextTransaction.Rollback();
                    throw;
                }
            }
        }
    }
}