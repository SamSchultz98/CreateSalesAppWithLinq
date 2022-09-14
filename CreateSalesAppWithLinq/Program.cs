using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using CreateSalesAppWithLinq.Controllers;
using CreateSalesAppWithLinq.Models;


Console.WriteLine("Hello, World!");

AppDbContext _context = new();
CustomersController cusCtrl = new(_context);
ProductsController proCtrl = new(_context);
OrdersController ordCtrl = new(_context);
OrderlinesController ordliCtrl = new(_context);


//(await ordCtrl.GetOrderByCus("TARG")).ToList().ForEach(o => Console.WriteLine($"{o.Description}"));



var answer = await proCtrl.GetByProductId(17);

foreach (var product in answer)
{
    Console.WriteLine(product);
}

Order order = new();
await ordCtrl.InProcessUpdate(13, order);

var orderline = await ordliCtrl.GetByPk(4);
orderline.Quantity = 2;
