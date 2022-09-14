using CreateSalesAppWithLinq.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSalesAppWithLinq.Controllers
{
    public class ProductsController
    {

        public readonly AppDbContext _context = null!;
        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product?>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByPk(int ProductId)
        {
            {
                return await _context.Products.FindAsync(ProductId);
            }
        }

        public async Task<Product> Insert(Product productId)
        {
            if (productId is null)
            {
                throw new ArgumentException("The product Id must be 0 in order to add new");
            }
            _context.Add(productId);
            await _context.SaveChangesAsync();
            return productId;
        }

        public async Task Update(int ProductId, Product product)
        {
            if (ProductId != product.Id)
            {
                throw new ArgumentException("The Product Id you entered does not match the given product id in database for product");
            }
            _context.Entry(ProductId).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int ProductId)
        {
            Product? _product = await GetByPk(ProductId);
            if (_product is null)
            {
                throw new ArgumentException("The Product Id does not match with an exisiting Product ID");
            }
            _context.Remove(ProductId);
            await _context.SaveChangesAsync();

        }
        public async Task<IEnumerable<Product?>> GetByProductId(int productId)
        {
            /* var _proId = from ol in _context.Orderlines
                        join p in _context.Products
                        on ol.ProductId equals p.Id
                        where p.Id == productId
                        select ol;
            */
            var _proId = from p in _context.Products
                         join ol in _context.Orderlines
                         on p.Id equals ol.ProductId
                         join o in _context.Orders
                         on ol.OrderId equals o.Id
                         where productId == p.Id
                         select p;

            return await _proId.ToListAsync();

        }
    }
}
