using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebbShop.Model
{
    internal class Exchange
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int OldProductId { get; set; }

        public int NewProductId { get; set; }

        public string Reason { get; set; }

        public string Color { get; set; }

        public DateTime ExchangeDate { get; set; }
    }
}
