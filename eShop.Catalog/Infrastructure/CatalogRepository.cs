using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShop.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace eShop.Catalog.Infrastructure
{
    public class CatalogRepository
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

            public async Task<List<CatalogItem>> GetCatalogAsync(string count, string offset, DateTime? fromDate, DateTime? toDate)
            {
                using (var dbContext = new CatalogContext())
                {
                    try
                    {
                        return await dbContext.CatalogItems.ToListAsync();
                    }
                    catch (Exception ex)
                    {
                        _logger.Debug(ex.Message);
                        throw;
                    }
                }
            }

            public async Task<CatalogItem> GetProductAsync(int id)
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

            public async Task<bool> AddProductAsync(CatalogItem product)
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

            public async Task<bool> DeleteProductAsync(int id)
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

            public async Task<bool> UpdateProductAsync(CatalogItem product)
            {
                using (var dbContext = new CatalogContext())
                using (var dbContextTransaction = await dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        const bool result = true;

                        var updateProduct = await dbContext.CatalogItems.FindAsync(product.Id);

                        updateProduct.DetailId = product.DetailId;
                        updateProduct.Category = product.Category;
                        updateProduct.CreatedDate = product.CreatedDate;
                        updateProduct.ModifiedDate = product.ModifiedDate;
                        updateProduct.Price = product.Price;
                        updateProduct.Summary = product.Summary;

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
