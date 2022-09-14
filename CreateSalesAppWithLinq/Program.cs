using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using CreateSalesAppWithLinq.Controllers;
using CreateSalesAppWithLinq.Models;


Console.WriteLine("Hello, World!");

CustomersController cusCtrl = new(_context);


