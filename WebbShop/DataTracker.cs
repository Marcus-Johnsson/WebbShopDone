namespace WebbShop
{
    internal class DataTracker
    {

        private static int[] FirstPageProducts = [1, 6, 11, 17];

        private static int UserId { get; set; }

        private static bool IsAdmin = false;

        private static int ProductId { get; set; }
        private static int ChangeProduct { get; set; }

        private static bool AddProduct = false;

        private static string ConnectionString = "Server=tcp:marcusdatabas.database.windows.net,1433;Initial Catalog=Webbshop;Persist Security Info=False;User ID=MarcusAdmin;Password=Kanelbulle96;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        private static bool UserIsAGuest { get; set; }

        private static bool RunPage { get; set; }

        private static int PageNumber { get; set; }

        public static int GetPageNumber()
        {
            return PageNumber;
        }

        public static void SetPageNumber(int value)
        {
            PageNumber = value;
        }

        public static bool GetUserIsAGuest()
        {
            return UserIsAGuest;
        }

        public static void SetUserIsAGuest(bool value)
        {
            UserIsAGuest = value;
        }
        public static bool GetRunPage()
        {
            return RunPage;
        }

        public static void SetRunPage(bool value)
        {
            RunPage = value;
        }

        public static string GetConnectionString()
        {
            return ConnectionString;
        }
        public static int GetChangeProduct()
        {
            return ChangeProduct;
        }

        public static void SetChangeProduct(int value)
        {
            ChangeProduct = value;
        }

        public static bool GetAddProduct()
        {
            return AddProduct;
        }

        public static void SetAddProduct(bool value)
        {
            AddProduct = value;
        }

        public static int[] GetFirstPageProducts()
        {
            return FirstPageProducts;
        }

        public static void SetFirstPageProducts(int[] products)
        {
            FirstPageProducts = products;
        }

        public static int GetUserId()
        {
            return UserId;
        }

        public static void SetUserId(int id)
        {
            UserId = id;
        }

        public static bool GetIsAdmin()
        {
            return IsAdmin;
        }

        public static void SetIsAdmin(bool value)
        {
            IsAdmin = value;
        }

        public static int GetProductId()
        {
            return ProductId;
        }

        public static void SetProductId(int id)
        {
            ProductId = id;
        }


    }
}
