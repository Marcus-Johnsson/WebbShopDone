using WebbShop.Model;

namespace WebbShop
{
    internal class AddToDataBase
    {
        private static string ProductName { get; set; }
        private static string Category { get; set; }
        private static int Price { get; set; }
        private static string Gender { get; set; }
        private static int[] ColorId { get; set; }
        private static string[] Size { get; set; }
        private static string Description { get; set; }
        private static int Brand { get; set; }
        private static bool EnviromentFriendly { get; set; }
        private static int ProductGroup { get; set; }
        private static bool CanBeBought { get; set; } = true;
        private static int CompanyBuyInPrice { get; set; }

        // Get info
        public static string GetProductName() => ProductName;
        public static string GetCategory() => Category;
        public static int GetPrice() => Price;
        public static string GetGender() => Gender;
        public static int[] GetColorId() => ColorId;
        public static string[] GetSize() => Size;
        public static string GetDescription() => Description;
        public static int GetBrand() => Brand;
        public static bool GetEnviromentFriendly() => EnviromentFriendly;
        public static int GetProductGroup() => ProductGroup;
        public static bool GetCanBeBought() => CanBeBought;
        public static int GetCompanyBuyInPrice() => CompanyBuyInPrice;

        // sett info
        public static void SetProductName(string value) => ProductName = value;
        public static void SetCategory(string value) => Category = value;
        public static void SetPrice(int value) => Price = value;
        public static void SetGender(string value) => Gender = value;
        public static void SetColorId(int[] value) => ColorId = value;
        public static void SetSize(string[] value) => Size = value;
        public static void SetDescription(string value) => Description = value;
        public static void SetBrand(int value) => Brand = value;
        public static void SetEnviromentFriendly(bool value) => EnviromentFriendly = value;
        public static void SetProductGroup(int value) => ProductGroup = value;
        public static void SetCanBeBought(bool value) => CanBeBought = value;
        public static void SetCompanyBuyInPrice(int value) => CompanyBuyInPrice = value;
        public static void Run()
        {
            using (var myDb = new MyDbContext())
            {
                myDb.AddRange(
                    // Brand | Nike = 1 | Adidas = 2 | GEMJ = 3 | H&M = 4 |
                    // Size | XS = 1 | S = 2 | M = 3 | L = 4 | XL = 5 |
                    // Color | RED = 1 | BLUE = 2 | Yellow = 3 | Black = 4 | Grey = 5 | Green = 6 |







                    //new Product
                    //{
                    //    ProductName = "Trainings Comfort",
                    //    Category = "Pants",
                    //    Price = 200,
                    //    Gender = "Unisex",
                    //    Size = "L",
                    //    Description = "A desing made from Nike, made for either use at home to relax in or while training. Available in multiepul colors.",
                    //    EnviromentFriendly = false,
                    //    Brand = 1,
                    //    ColorId = [2, 4, 5]
                    //},
                                       

                    //new Product
                    //{
                    //    ProductName = "Adidas All-Weather Jacket",
                    //    Category = "Jackets",
                    //    Price = 350,
                    //    Gender = "Unisex",
                    //    Size = "L", // M S L
                    //    Description = "A versatile jacket designed for all weather conditions, keeping you dry and comfortable. Available in classic colors.",
                    //    EnviromentFriendly = true,
                    //    Brand = 2, // Adidas
                    //    ColorId = [1, 4, 6] // Colors: Red, Black, Green
                    //},

                    //new Product
                    //{
                    //    ProductName = "H&M Everyday Casual Tee",
                    //    Category = "T-Shirts",
                    //    Price = 50,
                    //    Gender = "Women",
                    //    Size = "L", // S M L
                    //    Description = "A simple yet stylish t-shirt, perfect for everyday wear. Comes in vibrant shades to suit your style.",
                    //    EnviromentFriendly = true,
                    //    Brand = 4, // H&M
                    //    ColorId = [1, 3, 5] // Colors: Red, Yellow, Grey
                    //},

                    //new Product
                    //{
                    //    ProductName = "GEMJ Flex Sneakers",
                    //    Category = "Footwear",
                    //    Price = 400,
                    //    Gender = "Men",
                    //    Size = "M", // L XL M
                    //    Description = "Premium sneakers designed for ultimate comfort and flexibility, suitable for both sports and casual outings.",
                    //    EnviromentFriendly = false,
                    //    Brand = 3, // GEMJ
                    //    ColorId = [2, 4, 5] // Colors: Blue, Black, Grey
                    //},

                    //new Product
                    //{
                    //    ProductName = "Nike Pro Training Shorts",
                    //    Category = "Shorts",
                    //    Price = 150,
                    //    Gender = "Unisex",
                    //    Size = "M", // XL L M
                    //    Description = "High-performance shorts ideal for intense training sessions, designed to enhance your athletic abilities.",
                    //    EnviromentFriendly = false,
                    //    Brand = 1, // Nike
                    //    ColorId = [3, 4, 6] // Colors: Yellow, Black, Green
                    //},

                    //new Product
                    //{
                    //    ProductName = "H&M Comfort Hoodie",
                    //    Category = "Hoodies",
                    //    Price = 300,
                    //    Gender = "Unisex",
                    //    Size = "S", // L M S
                    //    Description = "A cozy hoodie crafted for maximum comfort, great for lounging or casual outings. Available in modern tones.",
                    //    EnviromentFriendly = true,
                    //    Brand = 4, // H&M
                    //    ColorId = [2, 5, 6] // Colors: Blue, Grey, Green
                    //}



                    //new Color
                    //{
                    //    Name = "red",
                    //},
                    //new Color
                    //{
                    //    Name = "Blue"
                    //},
                    //new Color
                    //{
                    //    Name = "Yellow"
                    //},
                    //new Color
                    //{
                    //    Name = "Black"
                    //},
                    //new Color
                    //{
                    //    Name = "Grey"
                    //},
                    //new Color
                    //{
                    //    Name = "Green"
                    //},
                    //new Brand
                    //{
                    //    Name = "Nike"
                    //},
                    //new Brand
                    //{
                    //    Name = "Adidas"
                    //},
                    //new Brand
                    //{
                    //    Name = "GEMJ"
                    //},
                    //new Brand
                    //{
                    //    Name = "H&M"
                    //},
                    //new Admin
                    //{
                    //    Username = "MarcusAdmin",
                    //    Password = "password",
                    //},
                    //new User
                    //{
                    //    Name = "Carl XVI Gustaf",
                    //    UserName = "KnugenDenStore",
                    //    Password = "Knugen1337",
                    //    Mail = "SverigesKnug@Outlook.com",
                    //    SecurityNumber = "30/04/1946/1337"
                    //}


                    );
                myDb.SaveChanges();
            }
        }
    }
}
