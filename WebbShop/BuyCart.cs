using WebbShop.Model;

namespace WebbShop
{
    internal class BuyCart
    {
        // Classen funkar, lite mer visual show men allt funkar
        public static void CartMenu()
        {
            Console.Clear();

            List<string> cart = new List<string>();
            List<string> cartWindow = new List<string>();
            List<string> popUpWindow = new List<string>();


            var popUpWindowBox = new Window("FraktVal", 35, 8, popUpWindow);
            var cartBox = new Window("FraktVal", 35, 8, cartWindow);

            using var myDb = new MyDbContext();

            var userCart = myDb.ShopingCart.Where(p => p.UserId == DataTracker.GetUserId() && p.CompletedPurchase == false).ToList();
            int[] shippingCost = { 0, 80, 30 };

            bool section = true;

            var products = myDb.products.ToList();

            int pointer = 0;
            bool shipping = false;

            while (section)
            {
                Console.Clear();
                var cartDetails = (from p in userCart
                                   join b in products on p.Id equals b.Id
                                   select new
                                   {
                                       productName = b.ProductName,
                                       price = b.Price,
                                       quantity = p.Antal,
                                       size = b.Size,
                                       b.Id
                                   }).ToList();

                float productPrices = 0;

                for (int i = 0; i < userCart.Count * 3; i++)
                {
                    cartWindow.Add("                                              ");
                }

                for (int i = 0; i < cartDetails.Count; i++)
                {
                    var productDetails = cartDetails[i];

                    if (i == pointer)
                    {
                        cart.Add(productDetails.productName + "  Size: " + productDetails.size);
                        cart.Add("Price: " + productDetails.price + "   Quantity: " + productDetails.quantity + "<-");
                        cart.Add("---------------------------");
                        productPrices += productDetails.price * productDetails.quantity;
                    }
                    else
                    {
                        cart.Add(productDetails.productName + "  Size: " + productDetails.size);
                        cart.Add("Price: " + productDetails.price + "   Quantity: " + productDetails.quantity);
                        productPrices += productDetails.price * productDetails.quantity;
                        cart.Add("---------------------------");
                    }
                }


                cart.Add("Total Price with taxes: " + productPrices);
                var cartWindowBox = new Window("FraktVal", 35, 8, cart);
                cartWindowBox.Draw();
                cart.Clear();


                ConsoleKeyInfo key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        {
                            if (pointer == 0)
                            {
                                pointer = cartDetails.Count - 1;
                            }
                            else
                            {
                                pointer = (pointer - 1) % cartDetails.Count;
                            }

                            break;
                        }

                    case ConsoleKey.DownArrow:
                        {


                            pointer = (pointer + 1) % cartDetails.Count;


                            break;
                        }
                    case ConsoleKey.LeftArrow:
                        {
                            var findProduct = myDb.ShopingCart.FirstOrDefault(p => p.Id == cartDetails[pointer].Id);
                            findProduct.Antal--;


                            myDb.SaveChanges();
                            if (findProduct.Antal < 0) // safty net =)
                            {
                                findProduct.Antal = 0;
                            }
                            if (cartDetails[pointer].quantity == 0)
                            {
                                Console.Clear();
                                popUpWindow.Add("Would you like to remove " + cartDetails[pointer].productName + "  " + cartDetails[pointer].size + "from the cart?");
                                popUpWindow.Add("      [Y]es     [No]    ");
                                popUpWindowBox.Draw();
                                popUpWindow.Clear();
                                key = Console.ReadKey();


                                if (key.Key == ConsoleKey.Y)
                                {
                                    var itemToRemove = myDb.ShopingCart.FirstOrDefault(p => p.UserId == DataTracker.GetUserId() && p.Antal == cartDetails[pointer].quantity);




                                    if (itemToRemove != null)
                                    {

                                        myDb.ShopingCart.Remove(itemToRemove);


                                        myDb.SaveChanges();
                                    }
                                }
                            }


                            break;



                        }
                    case ConsoleKey.RightArrow:
                        {
                            var findProduct = myDb.ShopingCart.FirstOrDefault(p => p.Id == cartDetails[pointer].Id);
                            findProduct.Antal++;

                            myDb.SaveChanges();
                            break;
                        }

                    case ConsoleKey.E:
                        {
                            section = false;
                            Shipping();

                            break;
                        }
                }
            }
        }
        public static void Shipping()
        {
            List<string> FraktBox = new List<string>();
            var cheeckOutBox = new Window("FraktVal", 35, 8, FraktBox);

            int pointer = 0;


           
            bool section = true;
            while (section)
            {
                Console.Clear();
                string[] shipping = { "PostNord leverans 2 - 5 arbetsdagar pris: 0 Sek", "DHL Express 1-2 arbetsdagar pris: 80 Sek", "Dhl leverans 2 - 3 arbetsdagar pris 30 Sek" };
                FraktBox.Add("Ange vilket sätt du vill fraktuera.");
                FraktBox.Add("");
                for (int i = 0; i < shipping.Count(); i++)
                {
                    if (pointer == i)
                    {
                        FraktBox.Add(shipping[i] += "<-");
                    }
                    else
                    {
                        FraktBox.Add(shipping[i]);
                    }
                }
                cheeckOutBox.Draw();
                FraktBox.Clear();
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.DownArrow)
                {
                    pointer = (pointer + 1) % 3;
                }
                if (key.Key == ConsoleKey.UpArrow)
                {
                    if (pointer == 0)
                    {
                        pointer = 2;
                    }
                    else
                    {
                        pointer = (pointer - 1) % 3;
                    }
                }
                else if (key.Key == ConsoleKey.E)
                {
                    section = false;
                    CompletePurchase(shipping[pointer], pointer);
                }
                else if (key.Key == ConsoleKey.B)
                {

                }
            }
        }

        public static void CompletePurchase(string shipping, int pointer)
        {

            List<string> FraktBox = new List<string>();
            List<string> cart = new List<string>();


            var cheeckOutBox = new Window("FraktVal", 30, 8, FraktBox);
            var cartBox = new Window("Cart", 90, 8, cart);

            using (var myDb = new MyDbContext())
            {
                bool section = true;

                int userId = DataTracker.GetUserId();


                var userCart = myDb.ShopingCart.Where(p => p.UserId == userId && p.CompletedPurchase == false).ToList();

                int[] shippingCost = { 0, 80, 30 };

                var userInfo = myDb.users.FirstOrDefault(p => p.Id == userId);

                var products = myDb.products.ToList();


                var cartDetails = (from p in userCart
                                   join b in products on p.CartGroupId equals b.Id
                                   select new
                                   {
                                       productName = b.ProductName,
                                       price = b.Price,
                                       quantity = p.Antal,
                                       size = b.Size,
                                   }).ToList();
                while (section)
                {
                    float productPrices = 0;
                    Console.Clear();

                    for (int i = 0; i < cartDetails.Count; i++)
                    {
                        var productDetails = cartDetails[i];


                        cart.Add(productDetails.productName + "  Size: " + productDetails.size);
                        cart.Add("Price: " + productDetails.price + "   Quantity: " + productDetails.quantity);
                        cart.Add("---------------------------");
                        productPrices += productDetails.price * productDetails.quantity;

                    }
                    productPrices += shippingCost[pointer];
                    float taxRate = 25f; // f är singel decimal glöm inte!
                    float taxes = productPrices * (taxRate / (100 + taxRate));
                    float withOutTaxes = productPrices - taxes;


                    FraktBox.Add("Customer name: " + userInfo.Name);
                    FraktBox.Add("Ship to: " + userInfo.Addres);
                    FraktBox.Add(shipping);
                    FraktBox.Add("");
                    FraktBox.Add("");
                    FraktBox.Add("Without taxes: " + withOutTaxes + "Taxes: " + taxes);
                    FraktBox.Add("");
                    FraktBox.Add("");
                    FraktBox.Add("Total cost: " + productPrices);
                    cheeckOutBox.Draw();
                    cartBox.Draw();
                    FraktBox.Clear();
                    cart.Clear();

                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Enter)
                    {

                        var productQuantities = userCart.Select(p => new { p.ProductId, p.Antal }).ToList();

                        var changeStock = myDb.stocks
                                .Where(p => productQuantities.Select(q => q.ProductId)
                                .Contains(p.ProductID))
                                .ToList();
                        string[] words = shipping.Split(' ');
                        string result;
                        result = $"{words[0]} {words[1]} -- {words[^2]} {words[^1]}";
                        for (int i = 0; i < changeStock.Count; i++)
                        {
                            var stockItem = changeStock[i];
                            var cartItem = productQuantities.FirstOrDefault(p => p.ProductId == stockItem.ProductID);

                        }


                        foreach (var product in userCart)
                        {
                            product.Frakt = result;
                            product.CompletedPurchase = true;
                            product.DateWhenBought = DateTime.Now;
                        }
                        myDb.SaveChanges();
                        section = false;
                    }
                    else if (key.Key == ConsoleKey.B)
                    {

                    }
                }
            }
        }




        public static void ContinueOrCreateAcc()
        {
            List<string> checkOutWindow = new List<string>();
            var cheeckOutBox = new Window("", 60, 8, checkOutWindow);
            Console.Clear();

            using (var myDb = new MyDbContext())
            {
                if (DataTracker.GetUserIsAGuest())
                {
                    checkOutWindow.Add("Would you like to create an account?");
                    checkOutWindow.Add("     [Y]es    [N]o    [B]ack   ");
                    cheeckOutBox.Draw();

                    ConsoleKeyInfo key = Console.ReadKey();

                    if (key.Key == ConsoleKey.Y)
                    {
                        int oldId = DataTracker.GetUserId();
                        Helpers.CreateUser();

                        var cartToUpdate = myDb.ShopingCart.Where(p => p.UserId == oldId && p.CompletedPurchase == false).ToList();
                        foreach (var product in cartToUpdate)
                        {
                            product.UserId = DataTracker.GetUserId();
                        }
                        myDb.SaveChanges();
                        DataTracker.SetUserIsAGuest(false);
                        CartMenu();
                    }
                    if (key.Key == ConsoleKey.N)
                    {

                        checkOutWindow.Clear();
                        Console.Clear();
                        var cart = myDb.ShopingCart.Where(p => p.UserId == DataTracker.GetUserId() && p.CompletedPurchase == false).ToList();


                        //  v 60,8
                        checkOutWindow.Add("                                               ");
                        checkOutWindow.Add("   Address:                                    ");
                        checkOutWindow.Add("                                               ");
                        checkOutWindow.Add("   Email:                                      ");
                        checkOutWindow.Add("                                               ");
                        checkOutWindow.Add("   Personnummer:                               ");
                        checkOutWindow.Add("                                               ");
                        checkOutWindow.Add("   City:  Awating                              ");
                        cheeckOutBox.Draw();

                        Console.SetCursorPosition(75, 10);
                        string address = Console.ReadLine();

                        Console.SetCursorPosition(72, 12);
                        string mail = Console.ReadLine();

                        Console.SetCursorPosition(79, 14);
                        string personnummer = Console.ReadLine();

                        int city = Helpers.GetCityFromUser();



                        var updateUser = myDb.users.FirstOrDefault(p => p.Id == DataTracker.GetUserId());

                        updateUser.SecurityNumber = personnummer;
                        updateUser.Addres = address;
                        updateUser.Mail = mail;
                        updateUser.City = city;
                        myDb.SaveChanges();
                        CartMenu();
                    }
                    else if (key.Key == ConsoleKey.B)
                    {
                        
                    }
                }




            }
        }


    }
}

