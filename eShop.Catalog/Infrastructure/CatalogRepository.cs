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
        private readonly ILogger _logger;

        public CatalogRepository(ILogger logger)
        {
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
                    var item = new CatalogItem
                    {
                        CatalogBrandId = product.CatalogBrandId,
                        CatalogTypeId = product.CatalogTypeId,
                        AvailableStock = product.AvailableStock,
                        DateTimeAdded = DateTime.Now,
                        DateTimeModified = DateTime.Now,
                        Description = product.Description,
                        Name = product.Name,
                        OnReorder = product.OnReorder,
                        PictureFilename = product.PictureFilename,
                        Price = product.Price,
                        RestockThreshold = product.RestockThreshold
                    };

                    await dbContext.CatalogItems.AddAsync(item);

                    await dbContext.SaveChangesAsync();

                    dbContextTransaction.Commit();

                    return true;
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
                    var deleteProduct = await dbContext.CatalogItems.FindAsync(id);

                    if (deleteProduct == null) return false;

                    dbContext.CatalogItems.Remove(deleteProduct);

                    await dbContext.SaveChangesAsync();

                    dbContextTransaction.Commit();

                    return true;
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
                    var updateProduct = await dbContext.CatalogItems.FindAsync(product.Id);

                    if (updateProduct == null) return false;

                    updateProduct.CatalogBrandId = product.CatalogBrandId;
                    updateProduct.CatalogTypeId = product.CatalogTypeId;
                    updateProduct.AvailableStock = product.AvailableStock;
                    updateProduct.DateTimeAdded = product.DateTimeAdded;
                    updateProduct.Description = product.Description;
                    updateProduct.Name = product.Name;
                    updateProduct.OnReorder = product.OnReorder;
                    updateProduct.PictureFilename = product.PictureFilename;
                    updateProduct.Price = product.Price;
                    updateProduct.RestockThreshold = product.RestockThreshold;
                    
                    
                    dbContext.CatalogItems.Update(updateProduct);

                    await dbContext.SaveChangesAsync();

                    dbContextTransaction.Commit();

                    return true;
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