namespace WebbShop
{
    internal class DataTracker
    {

        private static int[] firstPageProducts = { 1, 2, 3, 4 };
        private static int userId { get; set; }

        private static bool isAdmin = false;
        private static int productId { get; set; }
        private static int changeProduct { get; set; }

        private static bool addProduct = false;


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
            return changeProduct;
        }

        public static void SetChangeProduct(int value)
        {
            changeProduct = value;
        }

        public static bool GetAddProduct()
        {
            return addProduct;
        }

        public static void SetAddProduct(bool value)
        {
            addProduct = value;
        }

        public static int[] GetFirstPageProducts()
        {
            return firstPageProducts;
        }

        public static void SetFirstPageProducts(int[] products)
        {
            firstPageProducts = products;
        }

        public static int GetUserId()
        {
            return userId;
        }

        public static void SetUserId(int id)
        {
            userId = id;
        }

        public static bool GetIsAdmin()
        {
            return isAdmin;
        }

        public static void SetIsAdmin(bool value)
        {
            isAdmin = value;
        }

        public static int GetProductId()
        {
            return productId;
        }

        public static void SetProductId(int id)
        {
            productId = id;
        }


    }
}
