using Microsoft.EntityFrameworkCore;

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
