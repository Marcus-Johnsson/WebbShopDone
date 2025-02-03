using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebbShop.Model
{
    [Keyless]
    internal class Stock
    {
        public int ProductID { get; set; }

        public int StockCount { get; set; }

        public int PurchMore { get; set; }
    }
}
