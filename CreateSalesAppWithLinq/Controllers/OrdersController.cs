using CreateSalesAppWithLinq.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSalesAppWithLinq.Controllers
{
    public class OrdersController
    {
       public AppDbContext _context = null!;
        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order?>> GetAll()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order?> GetByPk(Order Id)
        {
            return await _context.Orders.FindAsync(Id);
        }

        public async Task<Order?> Insert(Order orderId)
        {
            if(orderId is null)
            {
                throw new ArgumentException("The Id must be set to zero to add");
            }
            _context.Orders.Add(orderId);
            await _context.SaveChangesAsync();
            return orderId;
        }
        public async Task Update(int OrderId, Order order)
        {
            if(OrderId != order.Id)
            {
                throw new ArgumentException("The OrderId entered does not match the correct Order");
            }
            _context.Entry(OrderId).State= EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Order OrderId)
        {
            Order? _order = await GetByPk(OrderId);
            if (_order is null)
            {
                throw new ArgumentException("The OrderId does not match an Order");
            }
            _context.Remove(OrderId);
            await _context.SaveChangesAsync();
        }

    }
}
