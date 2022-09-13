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

        OrderlinesController(AppDbContext context)
        {
            _context = context;
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

        }

       
    }
}
