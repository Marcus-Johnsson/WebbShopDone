﻿using Microsoft.EntityFrameworkCore;
using WebbShop.Model;

namespace WebbShop
{
    internal class Helpers              //Allt funkar här
    {
        static public void TopBarBox()
        {
            List<string> topText = new List<string> {
                "                                                                                     ",
                "                                              Mr.Scam                        ",
                "                                       Clothes Worth Paying For          ",
                "                                       Low Quality & High Cost                   " };
            var windowTop = new Window("", 30, 1, topText);
            windowTop.Draw();
        }

        static public void UserBox()
        {
            List<string> topText = new List<string>();
            if (DataTracker.GetUserId() == 0)
            {

                topText.Add("LoggIn");
            }
            else
            {
                using (var myDb = new MyDbContext())
                {
                    if (DataTracker.GetIsAdmin() == false)
                    {
                        var username = myDb.users.Where(p => p.Id == DataTracker.GetUserId()).ToList().FirstOrDefault();
                        topText.Add("User: " + username.Name);
                    }
                    else
                    {
                        var admin = myDb.admins.Where(p => p.Id == DataTracker.GetUserId()).ToList().FirstOrDefault();
                        topText.Add("User: " + admin.Id);
                    }
                }

            }
            var windowTop = new Window("", 5, 1, topText);
            windowTop.Draw();
        }

        static public async Task Product1()
        {
            UserBox();
            using (var myDb = new MyDbContext())
            {
                List<string> product = new List<string>();

                int[] productsId = DataTracker.GetFirstPageProducts();

                var selectedProduct = await myDb.products.Where(p => p.Id == productsId[0]).SingleOrDefaultAsync();

                string cash = selectedProduct.Price.ToString();
                var products = await myDb.products.Where(p => p.ProductName == selectedProduct.ProductName).ToListAsync();

                var brands = myDb.brands;
                var brandName = (from p in products
                                 join b in brands
                                 on p.Brand equals b.Id
                                 where p.Id == selectedProduct.Id
                                 select b.Name)
                                 .FirstOrDefault();

                product.Add(selectedProduct.ProductName);
                product.Add(brandName);
                product.Add($"{cash} Sek");

                int[,] positions = { {25, 8 },
                                         {25, 14},
                                         {65, 8},
                                         {65, 14}

                    };
                var productwindow = new Window("Product 1", 35, 8, product);
                productwindow.Draw();
                product.Clear();

            }
        }
        static public async Task Product2()
        {

            using (var myDb = new MyDbContext())
            {
                List<string> product = new List<string>();



                int[] productsId = DataTracker.GetFirstPageProducts();
                var selectedProduct = await myDb.products.Where(p => p.Id == productsId[1]).SingleOrDefaultAsync();

                string cash = selectedProduct.Price.ToString();
                var products = await myDb.products.Where(p => p.ProductName == selectedProduct.ProductName).ToListAsync();

                var brands = myDb.brands;
                var brandName = (from p in products
                                 join b in brands
                                 on p.Brand equals b.Id
                                 where p.Id == selectedProduct.Id
                                 select b.Name)
                                 .FirstOrDefault();

                product.Add(selectedProduct.ProductName);
                product.Add(brandName);
                product.Add($"{cash} Sek");


                var productwindow = new Window("Product 2", 75, 8, product);
                productwindow.Draw();
                product.Clear();


            }
        }
        static public async Task Product3()
        {

            using (var myDb = new MyDbContext())
            {
                List<string> product = new List<string>();



                int[] productsId = DataTracker.GetFirstPageProducts();
                var selectedProduct = await myDb.products
                        .Where(p => p.Id == productsId[2])
                        .SingleOrDefaultAsync();

                //if (selectedProduct == null) return;
                string cash = selectedProduct.Price.ToString();

                var products = await myDb.products.Where(p => p.ProductName == selectedProduct.ProductName).ToListAsync();

                var brands = myDb.brands;

                var brandName = (from p in products
                                 join b in brands
                                 on p.Brand equals b.Id
                                 where p.Id == selectedProduct.Id
                                 select b.Name)
                                 .FirstOrDefault();

                product.Add(selectedProduct.ProductName);
                product.Add(brandName);
                product.Add($"{cash} Sek");


                var productwindow = new Window("Product 3", 35, 14, product);
                productwindow.Draw();
                product.Clear();

            }
        }
        static public async Task Product4()
        {

            using (var myDb = new MyDbContext())
            {
                List<string> product = new List<string>();

                int[] productsId = DataTracker.GetFirstPageProducts();
                var selectedProduct = await myDb.products.Where(p => p.Id == productsId[3]).SingleOrDefaultAsync();

                string cash = selectedProduct.Price.ToString();
                var products = await myDb.products.Where(p => p.ProductName == selectedProduct.ProductName).ToListAsync();

                var brands = myDb.brands;

                var brandName = (from p in products
                                 join b in brands
                                 on p.Brand equals b.Id
                                 where p.Id == selectedProduct.Id
                                 select b.Name)
                                 .FirstOrDefault();

                product.Add(selectedProduct.ProductName);
                product.Add(brandName);
                product.Add($"{cash} Sek");


                var productwindow = new Window("Product 4", 75, 14, product);
                productwindow.Draw();
                product.Clear();


            }
        }

        static public void WriteCart()
        {
            List<string> ShopingCart = new List<string> { "" };

            using (var myDb = new MyDbContext())
            {

                int productPrices = 0;
                var UserId = DataTracker.GetUserId();


                if (DataTracker.GetIsAdmin() == false)
                {
                    var cartDetails = (from p in myDb.ShopingCart
                                       join b in myDb.products on p.ProductId equals b.Id
                                       where p.UserId == UserId && p.CompletedPurchase == false
                                       select new
                                       {
                                           productName = b.ProductName,
                                           price = b.Price,
                                           quantity = p.Antal,
                                           size = b.Size,
                                           b.Id
                                       }).ToList();



                    if (!cartDetails.Any())
                    {
                        ShopingCart.Add("ShopingCart is empty.");
                    }
                    else
                    {

                        foreach (var product in cartDetails)
                        {
                            ShopingCart.Add(product.productName + "  Size: " + product.size);
                            ShopingCart.Add("Quantity: " + product.quantity + "    Price: " + product.price);
                            ShopingCart.Add("---------------------------");
                            productPrices += product.price * product.quantity;
                        }
                        ShopingCart.Add("");
                        ShopingCart.Add("Total Price: " + productPrices.ToString());
                    }

                    var CartTop = new Window("ShopingCart", 120, 1, ShopingCart);
                    CartTop.Draw();
                }
            }
        }

        public static void RotateRight<T>(List<T> list)
        {
            if (list.Count > 1) // särkerhets ställer att en lista finns
            {
                // Remove the last element and insert it at the beginning
                var lastItem = list[^1];
                list.RemoveAt(list.Count - 1);
                list.Insert(0, lastItem);
            }
        }
        public static void RotateLeft<T>(List<T> list)
        {
            if (list.Count > 1) // särkerhets ställer att en lista finns
            {

                var firstItem = list[0];
                list.RemoveAt(0);
                list.Add(firstItem);
            }
        }
        static public void OptionsForPages(int[,] positions, int totalPages)
        {

            int page = DataTracker.GetPageNumber();

            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.LeftArrow && page != 1)
            {
                int reduce = DataTracker.GetPageNumber() - 1;
                DataTracker.SetPageNumber(reduce);
            }
            else if (key.Key == ConsoleKey.RightArrow && page != totalPages)
            {
                int increase = DataTracker.GetPageNumber() + 1;
                DataTracker.SetPageNumber(increase);
            }
            else if (key.Key == ConsoleKey.D1)
            {
                SpecificProduct.WriteSpecificProduct(positions[0, 2]);

            }
            else if (key.Key == ConsoleKey.D2)
            {
                SpecificProduct.WriteSpecificProduct(positions[1, 2]);

            }
            else if (key.Key == ConsoleKey.D3)
            {
                SpecificProduct.WriteSpecificProduct(positions[2, 2]);

            }
            else if (key.Key == ConsoleKey.D4)
            {
                SpecificProduct.WriteSpecificProduct(positions[3, 2]);

            }
            else if (key.Key == ConsoleKey.D5)
            {
                SpecificProduct.WriteSpecificProduct(positions[4, 2]);

            }
            else if (key.Key == ConsoleKey.D6)
            {
                SpecificProduct.WriteSpecificProduct(positions[5, 2]);

            }
            else if (key.Key == ConsoleKey.B)
            {
                DataTracker.SetRunPage(false);
            }

        }

        static public void OptionsForAdminPages(int[,] positions, int totalPages)
        {
            int page = DataTracker.GetPageNumber();
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.LeftArrow && page != 1)
            {
                int reduce = DataTracker.GetPageNumber() - 1;
                DataTracker.SetPageNumber(reduce);
            }
            else if (key.Key == ConsoleKey.RightArrow && page != totalPages)
            {
                int increase = DataTracker.GetPageNumber() + 1;
                DataTracker.SetPageNumber(increase);
            }
            else if (key.Key == ConsoleKey.D1)
            {
                int[] currentFirstPageProducts = DataTracker.GetFirstPageProducts();
                currentFirstPageProducts[DataTracker.GetChangeProduct()] = positions[0, 2];
                DataTracker.SetFirstPageProducts(currentFirstPageProducts);
                DataTracker.SetRunPage(false);

            }
            else if (key.Key == ConsoleKey.D2)
            {
                int[] currentFirstPageProducts = DataTracker.GetFirstPageProducts();
                currentFirstPageProducts[DataTracker.GetChangeProduct()] = positions[1, 2];
                DataTracker.SetFirstPageProducts(currentFirstPageProducts);
                DataTracker.SetRunPage(false);
            }
            else if (key.Key == ConsoleKey.D3)
            {
                int[] currentFirstPageProducts = DataTracker.GetFirstPageProducts();
                currentFirstPageProducts[DataTracker.GetChangeProduct()] = positions[2, 2];
                DataTracker.SetFirstPageProducts(currentFirstPageProducts);
                DataTracker.SetRunPage(false);

            }
            else if (key.Key == ConsoleKey.D4)
            {
                int[] currentFirstPageProducts = DataTracker.GetFirstPageProducts();
                currentFirstPageProducts[DataTracker.GetChangeProduct()] = positions[3, 2];
                DataTracker.SetRunPage(false);
            }
            else if (key.Key == ConsoleKey.D5)
            {
                int[] currentFirstPageProducts = DataTracker.GetFirstPageProducts();
                currentFirstPageProducts[DataTracker.GetChangeProduct()] = positions[4, 2];
                DataTracker.SetFirstPageProducts(currentFirstPageProducts);
                DataTracker.SetRunPage(false);
            }
            else if (key.Key == ConsoleKey.D6)
            {
                int[] currentFirstPageProducts = DataTracker.GetFirstPageProducts();
                currentFirstPageProducts[DataTracker.GetChangeProduct()] = positions[5, 2];
                DataTracker.SetFirstPageProducts(currentFirstPageProducts);
                DataTracker.SetRunPage(false);
            }
            else if (key.Key == ConsoleKey.B)
            {
                DataTracker.SetRunPage(false);
            }

        }

        static public void OptionsAddProductAdmin(int[,] positions, int totalPages)
        {

            int page = DataTracker.GetPageNumber();

            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.LeftArrow && page != 1)
            {
                int reduce = DataTracker.GetPageNumber() - 1;
                DataTracker.SetPageNumber(reduce);
            }
            else if (key.Key == ConsoleKey.RightArrow && page != totalPages)
            {
                int increase = DataTracker.GetPageNumber() + 1;
                DataTracker.SetPageNumber(increase);
            }
            else if (key.Key == ConsoleKey.D1)
            {

                DataTracker.SetProductId(positions[0, 3]);
                DataTracker.SetRunPage(false);
            }
            else if (key.Key == ConsoleKey.D2)
            {
                DataTracker.SetProductId(positions[1, 3]);
                DataTracker.SetRunPage(false);
            }
            else if (key.Key == ConsoleKey.D3)
            {
                DataTracker.SetProductId(positions[2, 3]);
                DataTracker.SetRunPage(false);
            }
            else if (key.Key == ConsoleKey.D4)
            {
                DataTracker.SetProductId(positions[3, 3]);
                DataTracker.SetRunPage(false);
            }
            else if (key.Key == ConsoleKey.D5)
            {
                DataTracker.SetProductId(positions[4, 3]);
                DataTracker.SetRunPage(false);
            }
            else if (key.Key == ConsoleKey.D6)
            {
                DataTracker.SetProductId(positions[5, 3]);
                DataTracker.SetRunPage(false);
            }
            else if (key.Key == ConsoleKey.B)
            {
                DataTracker.SetRunPage(false);
            }

            ;
        }
        static public void CreateUser()
        {

            Console.Clear();
            List<string> createUserBox = new List<string>();

            createUserBox.Add("                                    ");
            createUserBox.Add("   Username:                        ");
            createUserBox.Add("                                    ");
            createUserBox.Add("   Password:                        ");
            createUserBox.Add("                                    ");
            createUserBox.Add("   Full Name:                       ");
            createUserBox.Add("                                    ");
            createUserBox.Add("   Email:                           ");
            createUserBox.Add("                                    ");
            createUserBox.Add("   Personnummer:                    ");
            createUserBox.Add("                                    ");
            createUserBox.Add("   Enter Year (YYYY):        ");
            createUserBox.Add("                                    ");
            createUserBox.Add("   Enter Month (1-12):    ");
            createUserBox.Add("                                    ");
            createUserBox.Add("   Enter Day:         ");
            createUserBox.Add("");
            createUserBox.Add("   Addres:                  ");
            createUserBox.Add("                                    ");
            createUserBox.Add("   City: awating                   ");

            using (var myDb = new MyDbContext())
            {
                string personNummer = string.Empty;
                string email = string.Empty;
                string username = string.Empty;

                var createUser = new Window("", 60, 8, createUserBox);
                createUser.Draw();
                //--------------------------------------------------------------------------------------------------------
                while (true)
                {

                    Console.SetCursorPosition(75, 10);
                    while (string.IsNullOrEmpty(username = Console.ReadLine())) 
                    {
                        Console.SetCursorPosition(72, 11);
                        Console.WriteLine("Invalid Input. Please enter a valid username:");
                        Console.SetCursorPosition(75, 10);

                    }

                    var CheckUserName = myDb.users.Where(i => i.
                                   UserName == username).
                                   Select(p => p).FirstOrDefault();

                    if (CheckUserName != null)
                    {
                        Console.SetCursorPosition(80, 11);
                        Console.WriteLine("Username already in use");
                        Console.SetCursorPosition(75, 10);
                        Console.WriteLine("                  ");
                    }
                    else
                    {
                        break;

                    }
                }

                    Console.SetCursorPosition(75, 12);
                    string password = string.Empty;
                    while (string.IsNullOrEmpty(password = Console.ReadLine()))
                    {
                        Console.SetCursorPosition(72, 13);
                        Console.WriteLine("Invalid Input. Please enter a valid password:");
                        Console.SetCursorPosition(75, 12);

                }


                Console.SetCursorPosition(76, 14);
                    string fullname = string.Empty;
                    while (string.IsNullOrEmpty(fullname = Console.ReadLine()))
                    {
                        Console.SetCursorPosition(72, 15);
                        Console.WriteLine("Invalid Input.");
                        Console.SetCursorPosition(76, 14);

                }


                //--------------------------------------------------------------------------------------------------------
                while (true)
                    {

                        Console.SetCursorPosition(72, 16);
                        while (string.IsNullOrEmpty(email = Console.ReadLine()))
                        {
                            Console.SetCursorPosition(72, 17);
                            Console.WriteLine("Invalid Input. Please enter a valid Mail:");
                            Console.SetCursorPosition(72, 16);

                        }   


                    var CheckUserMail = myDb.users.Where(i => i.
                                       Mail == email).
                                       Select(p => p).FirstOrDefault();

                        if (CheckUserMail != null)
                        {
                            Console.SetCursorPosition(72, 17);
                            Console.WriteLine("Email already in use");
                            Console.SetCursorPosition(72, 16);
                            Console.WriteLine("                  ");
                        }
                        else
                        {
                            break;
                        }

                    }
                    //--------------------------------------------------------------------------------------------------------

                    while (true)
                    {


                        Console.SetCursorPosition(79, 18);
                        while (string.IsNullOrEmpty(personNummer = Console.ReadLine()))
                        {
                            Console.SetCursorPosition(72, 19);
                            Console.WriteLine("Invalid Input. Please enter a valid person nummer:");
                            Console.SetCursorPosition(79, 18);
                            
                        }


                    var CheckPersonNummer = myDb.users.Where(i => i.
                                       SecurityNumber == personNummer).
                                       Select(p => p).FirstOrDefault();

                        if (CheckPersonNummer != null)
                        {
                            Console.SetCursorPosition(72, 19);
                            Console.WriteLine("Personnummer already in use");
                            Console.SetCursorPosition(79, 18);
                            Console.WriteLine("                  ");
                            Console.SetCursorPosition(79, 18);

                        }
                    else
                        {
                            break;

                        }

                    }


                //--------------------------------------------------------------------------------------------------------

                int year, month, day;

                    Console.SetCursorPosition(84, 20);
                    while (!int.TryParse(Console.ReadLine(), out year) || year < 1900 || year > DateTime.Now.Year)
                    {
                        Console.SetCursorPosition(72, 21);
                        Console.WriteLine("Invalid input! Enter a valid Year (YYYY): ");
                        Console.SetCursorPosition(84, 20);
                    }

                Console.SetCursorPosition(85, 22);
                    while (!int.TryParse(Console.ReadLine(), out month) || month < 1 || month > 12)
                    {
                        Console.SetCursorPosition(72, 23);
                        Console.WriteLine("Invalid input! Enter a valid Month (1-12): ");
                        Console.SetCursorPosition(85, 22);
                    }

                    Console.SetCursorPosition(76, 24);
                    while (!int.TryParse(Console.ReadLine(), out day) || day < 1 || day > DateTime.DaysInMonth(year, month))
                    {
                        Console.SetCursorPosition(72, 25);
                        Console.WriteLine("Invalid input! Enter a valid Day (1-" + DateTime.DaysInMonth(year, month));
                        Console.SetCursorPosition(76, 24);
                    }
                //--------------------------------------------------------------------------------------------------------

                Console.SetCursorPosition(73, 26);

                string addres = string.Empty;
                while (string.IsNullOrEmpty(addres = Console.ReadLine()))
                {
                    Console.SetCursorPosition(72, 27);
                    Console.WriteLine("Invalid Input. Please enter a valid addres:");
                    Console.SetCursorPosition(73, 26);
                }

                int city = GetCityFromUser();

                    myDb.users.AddRange(
                    new User
                    {
                        Name = fullname,
                        UserName = username,
                        Password = password,
                        Mail = email,
                        Age = new DateTime(year, month, day),
                        SecurityNumber = personNummer,
                        userCreated = DateTime.Now,
                        Addres = addres,
                        City = city
                    }
                    );




                    myDb.SaveChanges();

                    var userId = myDb.users.Where(i =>
                      i.UserName == username &&
                              i.Mail == email &&
                              i.SecurityNumber == personNummer).
                              Select(p => p).FirstOrDefault();

                    DataTracker.SetUserId(userId.Id);
                }
            }
        


        static public void LogginUser()
        {
            List<string> loggInScreen = new List<string>();
            List<string> usernameBox = new List<string>();
            List<string> passwordBox = new List<string>();

            // sätter upp lådan och alternativ för user
            if (DataTracker.GetUserId() != 0)
            {
                Console.Clear();

                for (int i = 0; i < 12; i++)
                {
                    loggInScreen.Add("                                    ");
                }

                using (var myDb = new MyDbContext())
                {
                    var user = myDb.users.Where(p => p.Id == DataTracker.GetUserId()).FirstOrDefault();

                    usernameBox.Add("Name" + user.Name);
                    usernameBox.Add("UserName " + user.UserName);
                    usernameBox.Add("");
                    usernameBox.Add("Mail: " + user.Mail);
                    usernameBox.Add("Adress: " + user.Addres);
                    usernameBox.Add("");
                    usernameBox.Add("Security Number" + user.SecurityNumber);
                    usernameBox.Add("UserId: " + DataTracker.GetUserId());
                    usernameBox.Add("");
                    usernameBox.Add("[L]ogg out");
                    usernameBox.Add("[B]ack");

                    var logginScreen = new Window("", 60, 8, loggInScreen);
                    logginScreen.Draw();

                    var usernameWrite = new Window("", 65, 9, usernameBox);
                    usernameWrite.Draw();

                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.B:
                            {
                                break;
                            }
                        case ConsoleKey.L:
                            {
                                DataTracker.SetUserId(0);
                                DataTracker.SetUserIsAGuest(false);
                                break;
                            }
                    }
                }
            }
            else
            {


                for (int i = 0; i < 13; i++)
                {
                    loggInScreen.Add("                                    ");
                }
                usernameBox.Add("");
                usernameBox.Add("Username:                 ");
                usernameBox.Add("");

                passwordBox.Add("");
                passwordBox.Add("Password:                 ");
                passwordBox.Add("");

                loggInScreen.Add("[1] Loggin as a User.");
                loggInScreen.Add("[2] Create User.");
                loggInScreen.Add("[3] Continue as a guest.");
                loggInScreen.Add("");


                var logginScreen = new Window("", 60, 8, loggInScreen);
                logginScreen.Draw();

                var usernameWrite = new Window("", 65, 10, usernameBox);
                usernameWrite.Draw();

                var passwordWrite = new Window("", 65, 15, passwordBox);
                passwordWrite.Draw();

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.D1:
                        {
                            bool loggin = true;

                            while (loggin == true)
                            {
                                Console.SetCursorPosition(77, 12);
                                string username = string.Empty;

                                while (string.IsNullOrEmpty(username = Console.ReadLine()))
                                {
                                    Console.SetCursorPosition(77, 13);
                                    Console.WriteLine("Invalid Input");
                                }

                                Console.SetCursorPosition(77, 17);
                                string password = string.Empty;

                                while (string.IsNullOrEmpty(password = Console.ReadLine()))
                                {
                                    Console.SetCursorPosition(77, 18);
                                    Console.WriteLine("Invalid Input");
                                }

                                using (var myDb = new MyDbContext())
                                {

                                    var checkUser = myDb.users.Where(i =>
                                                i.UserName == username &&
                                                i.Password == password).
                                                Select(p => p).FirstOrDefault();

                                    if (checkUser != null)
                                    {
                                        DataTracker.SetUserId(checkUser.Id);
                                        loggin = false;
                                    }
                                    else if (checkUser == null)
                                    {
                                        var checkAdmin = myDb.admins.Where(i =>
                                        i.Username == username &&
                                        i.Password == password).
                                        Select(p => p).FirstOrDefault();

                                        if (checkAdmin != null)
                                        {
                                            DataTracker.SetUserId(checkAdmin.Id);
                                            DataTracker.SetIsAdmin(true);
                                            loggin = false;
                                        }
                                        if (checkAdmin == null)
                                        {
                                            Console.SetCursorPosition(68, 20);
                                            Console.WriteLine("Invalid information.");
                                        }
                                    }
                                }
                            }
                            break;
                        }
                    case ConsoleKey.D2:
                        {
                            CreateUser();
                            break;
                        }
                    case ConsoleKey.D3: //Skapar ett gäst konto
                        {
                            using (var myDb = new MyDbContext())
                            {
                                var highestId = myDb.users.Max(i => i.Id);
                                var users1 = new User()
                                {
                                    UserName = "GuestUser" + (highestId + 1),
                                    Name = "GuestUser",
                                    SecurityNumber = null,
                                    Mail = null,
                                    Password = "GuestUser" + (highestId + 1),
                                    Addres = null,
                                    Age = DateTime.Now
                                };
                                DataTracker.SetUserId(highestId + 1);
                                DataTracker.SetUserIsAGuest(true);
                                myDb.users.Add(users1);
                                myDb.SaveChanges();
                            }

                            break;
                        }
                }

            }
        }


        public static DateTime GetBirthDateFromUser()
        {
            int year, month, day;
            List<string> box = new List<string>();

            Console.Clear();
            box.Add("Enter Year 1900 - " + DateTime.Now.Year + ":      Enter Month (1-12):    Enter Day:        ");
            box.Add("");
            var AdminWindow = new Window("", 50, 7, box);
            AdminWindow.Draw();

            Console.SetCursorPosition(76, 8);
            while (!int.TryParse(Console.ReadLine(), out year) || year < 1900 || year > DateTime.Now.Year)
            {
                Console.SetCursorPosition(55, 9);
                Console.WriteLine("Invalid input! Enter a valid Year (<1900): " + DateTime.Now.Year);
            }

            Console.SetCursorPosition(101, 8);
            while (!int.TryParse(Console.ReadLine(), out month) || month < 1 || month > 12)
            {
                Console.SetCursorPosition(55, 9);
                Console.WriteLine("Invalid input! Enter a valid Month (1-12): ");
            }

            Console.SetCursorPosition(115, 8);
            while (!int.TryParse(Console.ReadLine(), out day) || day < 1 || day > DateTime.DaysInMonth(year, month))
            {
                Console.SetCursorPosition(55, 9);
                Console.WriteLine("Invalid input! Enter a valid Day (1-" + DateTime.DaysInMonth(year, month) + ":");
            }
            

            return new DateTime(year, month, day);
        }



        public static int GetCityFromUser()
        {
            using var myDb = new MyDbContext();

            var cities = myDb.citys.ToList();

            List<string> cites = new List<string>();
            while (true)
            {
                Console.Clear();

                string cityText = "Available cities: ";

                for (int i = 0; i < cities.Count; i++)
                {
                    var city = cities[i];

                    cityText += city.Name + (city == cities.Last() ? "" : ", ");
                }
                cites.Add("                   vv");
                cites.Add(cityText);
                cites.Add("                   ^^");
                cites.Add("Change City with Arrow Keys");
                cites.Add("[Left Arrow]");
                cites.Add("[Right Arrow]");
                cites.Add("[E] Choose city");


                var productwindow = new Window("City Menu", 50, 7 , cites);
                productwindow.Draw();
                cites.Clear();

                ConsoleKeyInfo key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.RightArrow:
                        {
                            RotateRight(cities);
                            break;
                        }
                    case ConsoleKey.LeftArrow:
                        {
                            RotateLeft(cities);
                            break;
                        }
                    case ConsoleKey.E:
                        {

                            return cities.First().Id;
                        }
                }
            }
        }
    }
}

