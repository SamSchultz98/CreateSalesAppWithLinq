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

        public async Task<Order?> GetByPk(int orderId)
        {
            return await _context.Orders.FindAsync(orderId);
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
        public async Task<IEnumerable<Order?>> GetOrderByCus(int CustomerId)
        {
            return await _context.Orders.Where(o => o.CustomerId == CustomerId).ToListAsync();
            //How to do it with query syntax
            //var orders = from o in _context.Orders
            //where o.CustomerId == CustomerId
            //select o;
            //return await orders.ToListAsync();
        }
        public async Task<IEnumerable<Order?>> GetOrderByCus(string CustomerCode)
        {
            /*var _codePro = _context.Orders.Where(x => x.Customer. == CustomerCode);

            var _CustomerID = from i in _codePro
                              where i.Customer.Id == 
            */

            var order = from x in _context.Orders
                        join c in _context.Customers
                        on x.CustomerId equals c.Id
                        where c.Code == CustomerCode
                        select x;
            return await order.ToListAsync();
           
        }
        public async Task<IEnumerable<Order?>> GetOrdersByProductId(int productid)
        {
            /* This god damn one brought back the wrong shit
             var _proId = from p in _context.Products
                         join ol in _context.Orderlines
                         on p.Id equals ol.ProductId
                         join o in _context.Orders
                         on ol.OrderId equals o.Id
                         where productid == p.Id
                         select p;
            */

            var orders = from o in _context.Orders
                         join ol in _context.Orderlines
                         on o.Id equals ol.OrderId
                         join p in _context.Products
                         on ol.ProductId equals p.Id
                         select o;
            return await orders.ToListAsync();

        }
        public async Task UpdateByOrder(int Id, Order order)
        {
            order.Status = "CLOSED";
            await Update(Id, order);
        }
        public async Task InProcessUpdate(int Id, Order order)
        {
            if (order.Total == 0) 
            {
                return;
            }
            order.Status = "InProcess";
            await Update(Id, order);
        }

        
    }
}
