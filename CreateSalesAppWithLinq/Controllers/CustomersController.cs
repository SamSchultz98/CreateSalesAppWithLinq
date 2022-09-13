using CreateSalesAppWithLinq.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSalesAppWithLinq.Controllers
{
    public class CustomersController
    {
        public AppDbContext _context = null!;
        public CustomersController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer?>> GetAll()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetByPk(int CustomerId)
        {
            return await _context.Customers.FindAsync(CustomerId);
        }

        public async Task Update(int Id, Customer customer)
        {
            if(Id != customer.Id)
            {
                throw new ArgumentException("The Id does not match a customer in database");
            }
            _context.Entry(Id).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<Customer> Insert(Customer customerId)
        {
            if(customerId == null)
            {
                throw new ArgumentException("The Id must be set to zero to add");
            }
            _context.Customers.Add(customerId);
            await _context.SaveChangesAsync();
            return customerId;
        }

        public async Task Delte(int customerId)
        {
           Customer? cust = await GetByPk(customerId);
            if(cust is null)
            {
                throw new ArgumentException("The ID entered did not have a match in the database");
            }
            _context.Remove(cust);
            await _context.SaveChangesAsync();
        }


    }
}
