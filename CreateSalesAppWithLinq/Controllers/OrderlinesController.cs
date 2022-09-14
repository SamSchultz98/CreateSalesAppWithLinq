using CreateSalesAppWithLinq.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreateSalesAppWithLinq.Controllers;

namespace CreateSalesAppWithLinq.Controllers
{
    public class OrderlinesController
    {
        AppDbContext _context = null!;

    private OrdersController _ordersController;

       public OrderlinesController(AppDbContext context)
        {
            _context = context;
            OrdersController _ordersController = new(context);
        }
        

        public async Task<IEnumerable<Orderline?>> GetAll()
        {
            return await _context.Orderlines.ToListAsync();
        }

        public async Task<Orderline?> GetByPk(int orderLineId)
        {
            return await _context.Orderlines.FindAsync(orderLineId);
        }

        public async Task<Orderline> Insert(Orderline orderline)
        {
            if (orderline.Id != 0)
            {
                throw new InvalidOperationException("The Id must be set to zero to add");
            }
            _context.Orderlines.Add(orderline);
            await _context.SaveChangesAsync();
            await OrderTotalUpdate(orderline.Id);
            return orderline;
        }
        public async Task Update(int Id, Orderline orderline) 
        { 
            if(Id != orderline.Id)
            {
                throw new Exception("The entered Id does not match with any of the data in the database");

            }
            _context.Entry(Id).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            await OrderTotalUpdate(orderline.OrderId);
        }
        public async Task Delete(int Id)
        {
            Orderline? _orderline = await GetByPk(Id);
            if(_orderline == null)
            {
                throw new ArgumentException("The Id entered does not have a matching orderline item");

            }
            _context.Orderlines.Remove(_orderline);
            await _context.SaveChangesAsync();
            await OrderTotalUpdate(_orderline.OrderId);

        }

        private async Task OrderTotalUpdate(int orderid)            //which order do you want to do this for
        {
            var order = await _ordersController.GetByPk(orderid);
            if (order is null)
            {
                throw new ArgumentException("Order not found");
            }
            order.Total = (from ol in _context.Orderlines
                             join p in _context.Products
                             on ol.ProductId equals p.Id
                             where ol.OrderId == orderid
                             select new {linetotal= ol.Quantity * p.MyProperty }).Sum(x=>x.linetotal);               //MyProperty is price

            await _ordersController.Update(order.Id,order);
            



        }



















        //public async Task<IEnumerable<Orderline?>> GetByProductId(int productId)
        //{
        //    /* var _proId = from ol in _context.Orderlines
        //                join p in _context.Products
        //                on ol.ProductId equals p.Id
        //                where p.Id == productId
        //                select ol;
        //    */
        //    var _proId = from p in _context.Products
        //                 join ol in _context.Orderlines
        //                 on p.Id equals ol.ProductId
        //                 join o in _context.Orders
        //                 on ol.OrderId equals o.Id
        //                 where productId == p.Id
        //                 select p;

        //    return _proId.ToListAsync();

        //}


    }
}
