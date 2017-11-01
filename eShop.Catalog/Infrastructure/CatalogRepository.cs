using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShop.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace eShop.Catalog.Infrastructure
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly CatalogContext _dbContext;
        private readonly ILogger _logger;

        public CatalogRepository(CatalogContext context, ILogger logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        public async Task<CatalogResponse> GetItemsAsync(int pageIndex, int pageSize)
        {
            try
            {
                var totalItems = await _dbContext.CatalogItems
                    .LongCountAsync();

                var itemsOnPage = await _dbContext.CatalogItems
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

        public async Task<CatalogResponse> GetItemsAsync(string name, int pageIndex, int pageSize)
        {
            try
            {
                var totalItems = await _dbContext.CatalogItems.Where(c => c.Name.Contains(name))
                    .LongCountAsync();

                var itemsOnPage = await _dbContext.CatalogItems
                    .Where(c => c.Name.Contains(name))
                    .OrderBy(c => c.Name)
                    .Skip(pageSize * pageIndex)
                    .Take(pageSize)
                    .ToListAsync();

                return new CatalogResponse { ItemsOnPage = itemsOnPage, TotalItems = totalItems };
            }
            catch (Exception ex)
            {
                _logger.Debug(ex.Message);
                throw;
            }
        }

        public async Task<CatalogResponse> GetItemsAsync(int? catalogTypeId, int? catalogBrandId, int pageIndex, int pageSize)
        {
            try
            {
                var query = (IQueryable<CatalogItem>)_dbContext.CatalogItems;

                if (catalogTypeId.HasValue)
                {
                    query = query.Where(ci => ci.CatalogTypeId == catalogTypeId);
                }

                if (catalogBrandId.HasValue)
                {
                    query = query.Where(ci => ci.CatalogBrandId == catalogBrandId);
                }

                var totalItems = await query
                    .LongCountAsync();

                var itemsOnPage = await query
                    .OrderBy(c => c.Name)
                    .Skip(pageSize * pageIndex)
                    .Take(pageSize)
                    .ToListAsync();

                return new CatalogResponse { ItemsOnPage = itemsOnPage, TotalItems = totalItems };
            }
            catch (Exception ex)
            {
                _logger.Debug(ex.Message);
                throw;
            }
        }

        public async Task<CatalogItem> GetItemAsync(int id)
        {
            try
            {
                return await _dbContext.CatalogItems.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.Debug(ex.Message);
                throw;
            }
        }

        public async Task<List<CatalogBrand>> GetCatalogBrandsAsync()
        {
            try
            {
                return await _dbContext.CatalogBrands
                    .OrderBy(cb => cb.Brand)
                    .ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.Debug(ex.Message);
                throw;
            }
        }

        public async Task<List<CatalogType>> GetCatalogTypesAsync()
        {
            try
            {
                return await _dbContext.CatalogTypes
                    .OrderBy(cb => cb.Type)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Debug(ex.Message);
                throw;
            }
        }

        public async Task<CatalogItem> AddItemAsync(CatalogItem product)
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

                await _dbContext.CatalogItems.AddAsync(item);

                await _dbContext.SaveChangesAsync();
                
                return _dbContext.CatalogItems.Last();
            }
            catch (Exception ex)
            {
                _logger.Debug(ex.Message);
                throw;
            }
        }

        public async Task<CatalogItem> DeleteItemAsync(int id)
        {
            try
            {
                var deleteProduct = await _dbContext.CatalogItems.FindAsync(id);

                if (deleteProduct == null) return null;

                _dbContext.CatalogItems.Remove(deleteProduct);

                await _dbContext.SaveChangesAsync();

                return deleteProduct;
            }
            catch (Exception ex)
            {
                _logger.Debug(ex.Message);
                throw;
            }
        }

        public async Task<CatalogItem> UpdateItemAsync(CatalogItem product)
        {
            try
            {
                var updateProduct = await _dbContext.CatalogItems.FindAsync(product.Id);

                if (updateProduct == null) return null;

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
                
                
                _dbContext.CatalogItems.Update(updateProduct);

                await _dbContext.SaveChangesAsync();

                //dbContextTransaction.Commit(); TODO: Will need some kind of eventual consistency here to update price

                return updateProduct;
            }
            catch (Exception ex)
            {
                _logger.Debug(ex.Message);
                throw;
            }
        }
    }
}