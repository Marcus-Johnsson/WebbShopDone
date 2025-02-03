namespace WebbShop.Model
{
    internal class ShopingCart
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }

        public int Antal { get; set; }

        public int CartGroupId { get; set; }

        public string color { get; set; }

        public bool CompletedPurchase { get; set; } = false;

        public string? Frakt { get; set; }

        public DateTime? DateWhenBought { get; set; } = null;

    }
}
