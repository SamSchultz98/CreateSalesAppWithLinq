using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSalesAppWithLinq.Models
{
    public class Orderline
    {

        [Key]                                           //No need to place the key
        public int Id { get; set; }

            
        public int OrderId { get; set; }                //fk
        public virtual Order Order { get; set; }

        public int ProductId { get; set; }              //fk
        public virtual Product Product { get; set; }


        public int Quantity { get; set; } = 1;


    }
}
