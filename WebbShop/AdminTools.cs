using Dapper;
using Microsoft.Data.SqlClient;
using WebbShop.Model;

namespace WebbShop
{
    internal class AdminTools 

        //they work fine and looks fine
    {

        public static int[] ChooseColor()
        {


            List<string> options = new List<string>();
            List<int> selectedColorIds = new List<int>();

            using (var myDb = new MyDbContext())
            {
                var color = myDb.colors
                     .Select(c => new { c.Id, c.Name })
                     .ToList();
                while (true)
                {
                    string chosenColors = "Selected color IDs: ";
                    Console.Clear();


                    string colorText = "Available colors: ";
                    var lastColor = color.Last();
                    foreach (var colors in color)
                    {
                        if (colors != lastColor)
                        {
                            colorText += colors.Name + ", ";
                        }

                        else if (colors == lastColor)
                        {
                            colorText += colors.Name;
                        }
                    }

                    foreach (var chosen in selectedColorIds)
                    {
                        chosenColors += chosen;
                    }
                    options.Add("                   vv");
                    options.Add(colorText);
                    options.Add("                   ^^");
                    options.Add("         ");
                    options.Add(chosenColors);
                    options.Add("         ");
                    options.Add("[E] Pick color");
                    options.Add("[Q] Done");
                    var box = new Window("", 50, 7, options);
                    box.Draw();
                    options.Clear();

                    ConsoleKeyInfo keyInfo = Console.ReadKey();

                    if (keyInfo.Key == ConsoleKey.RightArrow) // rotera åt höger  1 2 3 4 5 = 5 1 2 3 4
                    {
                        Helpers.RotateRight(color);

                    }
                    else if (keyInfo.Key == ConsoleKey.LeftArrow) //  rotera åt vänster   1 2 3 4 5 = 2 3 4 5 1
                    {
                        Helpers.RotateLeft(color);

                    }
                    else if (keyInfo.Key == ConsoleKey.E)
                    {
                        selectedColorIds.Add(color.First().Id);
                        color.RemoveAt(0);


                    }
                    else if (keyInfo.Key == ConsoleKey.Q)
                    {
                        int[] colorId = new int[selectedColorIds.Count];
                        for (int i = 0; i < selectedColorIds.Count; i++)
                        {
                            colorId[i] = selectedColorIds[i];
                        }
                        return colorId;
                    }
                }
            }

        }


        public static int ChooseBrand()
        {

            List<string> options = new List<string>();

            using (var myDb = new MyDbContext())
            {
                var brands = myDb.brands
                     .Select(c => new { c.Id, c.Name })
                     .ToList();
                while (true)
                {
                    Console.Clear();

                    string brandText = "Available Brands: ";
                    var lastColor = brands.Last();
                    foreach (var brand in brands)
                    {
                        if (brand != lastColor)
                        {
                            brandText += brand.Name + ", ";
                        }

                        else if (brand == lastColor)
                        {
                            brandText += brand.Name;
                        }
                    }
                    options.Add("                   vv");
                    options.Add(brandText);
                    options.Add("                   ^^");
                    options.Add("");
                    options.Add("[E] Pick color");
                    options.Add("[Q] Done");
                    var box = new Window("", 50, 7, options);
                    box.Draw();
                    options.Clear();

                    ConsoleKeyInfo keyInfo = Console.ReadKey();

                    if (keyInfo.Key == ConsoleKey.RightArrow) // rotera åt höger  1 2 3 4 5 = 5 1 2 3 4
                    {
                        Helpers.RotateRight(brands);

                    }
                    else if (keyInfo.Key == ConsoleKey.LeftArrow) //  rotera åt vänster   1 2 3 4 5 = 2 3 4 5 1
                    {
                        Helpers.RotateLeft(brands);

                    }
                    else if (keyInfo.Key == ConsoleKey.E)
                    {
                        return brands.First().Id;

                    }



                }

            }

        }

        public static int ChooseCategory()
        {
            Console.Clear();
            List<string> options = new List<string>();
            List<int> selected = new List<int>();
            using (var myDb = new MyDbContext())
            {
                var categories = myDb.categories.ToList();
                while (true)
                {
                    Console.Clear();

                    string categoryText = "Available Category: ";
                    var lastcategory = categories.Last();

                    for (int i = 0; i < categories.Count; i++)
                    {
                        var category = categories[i];
                       categoryText += category.Name + (lastcategory == category ? "" : ", ");
                    }

                    options.Add("                     vv");
                    options.Add(categoryText);
                    options.Add("                     ^^");
                    options.Add("");
                    options.Add("[E] Pick Category");
                    options.Add("[Q] Done");

                    var box = new Window("", 50, 7, options);
                    box.Draw();
                    options.Clear();




                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                    if (keyInfo.Key == ConsoleKey.RightArrow) // rotera åt höger  1 2 3 4 5 = 5 1 2 3 4
                    {
                        Helpers.RotateRight(categories);

                    }
                    else if (keyInfo.Key == ConsoleKey.LeftArrow) //  rotera åt vänster   1 2 3 4 5 = 2 3 4 5 1
                    {
                        Helpers.RotateLeft(categories);

                    }
                    else if (keyInfo.Key == ConsoleKey.E)
                    {

                        return categories.First().Id;


                    }
                }

            }
        }

    
        public static void AddNewProduct()
        {

            Console.Clear();

            List<string> newGroupId = new List<string>();


            var groupIdCheckBox = new Window("", 50, 7, newGroupId);




            using (var myDb = new MyDbContext())
            {

                AddToDataBase.SetProductGroup(myDb.products.Max(x => x.ProductGroup) + 1);


                var product1 = new Product { ProductGroup = DataTracker.GetProductId() };
                string[] infoTitle = { "Product Name", "Gender", "Size", "Description", "Brand", "Environment Friendly", "Product Price" };

                AddToDataBase.SetProductName(EnterValue(infoTitle[0]));
                AddToDataBase.SetCategory(AdminTools.ChooseCategory());
                AddToDataBase.SetPrice(EnterIntValue(infoTitle[6]));
                AddToDataBase.SetGender(EnterValue(infoTitle[1]));
                AddToDataBase.SetColorId(AdminTools.ChooseColor());
                AddToDataBase.SetSize(AdminTools.ChooseSize(product1.ProductGroup));
                AddToDataBase.SetDescription(EnterValue(infoTitle[3]));
                AddToDataBase.SetBrand(AdminTools.ChooseBrand());
                AddToDataBase.SetEnviromentFriendly(EnterBoolValue(infoTitle[5]));
                AddToDataBase.SetPrice(EnterIntValue(infoTitle[6]));



                string[] sizes = AddToDataBase.GetSize();
                foreach (string size in sizes)
                {
                    myDb.products.AddRange(

                            new Product
                            {
                                ProductName = AddToDataBase.GetProductName(),
                                CategoryId = AddToDataBase.GetCategory(),
                                Price = AddToDataBase.GetPrice(),
                                Gender = AddToDataBase.GetGender(),
                                Size = size,
                                Description = AddToDataBase.GetDescription(),
                                EnviromentFriendly = AddToDataBase.GetEnviromentFriendly(),
                                Brand = AddToDataBase.GetBrand(),
                                ColorId = AddToDataBase.GetColorId(),
                                CompanyBuyInPrice = AddToDataBase.GetCompanyBuyInPrice(),
                                ProductGroup = AddToDataBase.GetProductGroup(),

                            }

                            );

                    myDb.SaveChanges();
                }

            }
        }

        public static string[] ChooseSize(int product1)
        {

            List<string> options = new List<string>();
            List<string> selectedSize = new List<string>();

            using (var myDb = new MyDbContext())
            {
                if (product1 == null)
                {
                    string[] availeableSizes = { "XS", "S", "M", "L", "XL" };
                    options.AddRange(availeableSizes);
                }
                else if (product1 != null)
                {
                    var products = myDb.products.Where(p => p.ProductGroup == product1);

                    string[] sortSize = { "XS", "S", "M", "L", "XL" };
                    var existingSizes = products
                        .Where(p => sortSize.Contains(p.Size))
                        .Select(p => p.Size)
                        .ToList();

                    var availableSizes = sortSize
                            .Where(size => !existingSizes.Contains(size))
                            .ToList();
                    DataTracker.SetRunPage(true);

                    while (DataTracker.GetRunPage())
                    {


                        Console.Clear();

                        string sizeText = "Available sizes: ";
                        var lastSize = availableSizes.Last();


                        for (int i = 0; i < availableSizes.Count; i++)
                        {
                            var size = availableSizes[i];
                            sizeText += size + (size == lastSize ? "" : ", ");
                        }
                        options.Add("                 vv");
                        options.Add(sizeText);
                        options.Add("                 ^^");
                        options.Add("");
                        options.Add("Selected size: " + string.Join(", ", selectedSize));
                        options.Add("");
                        options.Add("[E] Pick size");
                        options.Add("[Q] Done");

                        var productwindow = new Window("", 50, 7, options);
                        productwindow.Draw();
                        options.Clear();

                        ConsoleKeyInfo keyInfo = Console.ReadKey(true);



                        if (keyInfo.Key == ConsoleKey.RightArrow) // rotera åt höger  1 2 3 4 5 = 5 1 2 3 4
                        {
                            Helpers.RotateRight(availableSizes);

                        }
                        else if (keyInfo.Key == ConsoleKey.LeftArrow) //  rotera åt vänster   1 2 3 4 5 = 2 3 4 5 1
                        {
                            Helpers.RotateLeft(availableSizes);

                        }
                        else if (keyInfo.Key == ConsoleKey.E)
                        {
                            selectedSize.Add(availableSizes.First());
                            availableSizes.RemoveAt(0);

                            if(availableSizes.Count <= 0)
                            {
                                string[] sizes = new string[selectedSize.Count];
                                for (int i = 0; i < selectedSize.Count; i++)
                                {
                                    sizes[i] = selectedSize[i];
                                }
                                DataTracker.SetRunPage(false);
                                return sizes.ToArray();
                            }
                        }

                        
                        else if (keyInfo.Key == ConsoleKey.Q)
                        {
                            string[] sizes = new string[selectedSize.Count];
                            for (int i = 0; i < selectedSize.Count; i++)
                            {
                                sizes[i] = selectedSize[i];
                            }
                            DataTracker.SetRunPage(false);
                            return sizes.ToArray();
                        }

                    }
                }

            }
            return null;
        }

        public static void AddNewCopy()
        {

            List<string> AdminBox = new List<string>();
            var AdminWindow = new Window("", 5, 2, AdminBox);

            for (int i = 0; i < 10; i++)
            {
                AdminBox.Add("");
            }
            Console.Clear();
            AdminWindow.Draw();

            DataTracker.SetRunPage(true);
            DataTracker.SetAddProduct(true);
            WriteAllPages.WriteOutPages();


            using (var myDb = new MyDbContext())
            {
                var selectedProduct = myDb.products.FirstOrDefault(p => p.ProductGroup == DataTracker.GetProductId());


                string[] sizes = AdminTools.ChooseSize(selectedProduct.ProductGroup);

                if (sizes != null)
                {

                    foreach (string size in sizes)
                    {
                        var product1 = new Product();

                        var product = new Dictionary<string, Action>
                                                 {
                                                    { "ProductName", () => product1.ProductName = selectedProduct.ProductName },
                                                    { "Category", () => product1.CategoryId = selectedProduct.CategoryId },
                                                    { "Price", () => product1.Price = selectedProduct.Price },
                                                    { "Gender", () => product1.Gender = selectedProduct.Gender },
                                                    { "ColorId", () => product1.ColorId = selectedProduct.ColorId },
                                                    { "Size", () => product1.Size = size },
                                                    { "Description", () => product1.Description = selectedProduct.Description },
                                                    { "Brand", () => product1.Brand = selectedProduct.Brand },
                                                    { "EnvironmentFriendly", () => product1.EnviromentFriendly = selectedProduct.EnviromentFriendly },
                                                    { "ProductId", () => product1.ProductGroup = selectedProduct.ProductGroup },
                                                    { "CanBeBought", () => product1.CanBeBought = selectedProduct.CanBeBought},
                                                    { "CompanyBuyInPrice", () => product1.CompanyBuyInPrice = selectedProduct.CompanyBuyInPrice}
                                                };

                        foreach (var key in product)
                        {
                            key.Value();
                        }
                        myDb.products.Add(product1);
                        myDb.SaveChanges();

                    }

                }
                else
                {
                    Console.WriteLine("Product not found.");
                }


                DataTracker.SetAddProduct(false);
            }
        }

        public static int[] NewColors()
        {
            int pointer = 0;
            List<string> options = new List<string>();
            List<int> selectedColorIds = new List<int>();
            using (var mydb = new MyDbContext())
            {
                var everyColors = mydb.colors.ToList();

                while (true)
                {
                    string chosenColors = "Selected color IDs: ";
                    Console.Clear();
                    foreach (var chosen in selectedColorIds)
                    {
                        chosenColors += chosen;
                    }

                    string colorText = "Available colors: ";
                    var lastColor = everyColors.Last();

                    for (int i = 0; i < everyColors.Count; i++)
                    {
                        colorText += chosenColors[i] + (everyColors[i].Name == lastColor.Name ? ", " : "");
                    }


                    options.Add("                   vv");
                    options.Add(colorText);
                    options.Add("                   ^^");
                    options.Add("         ");
                    options.Add(chosenColors);
                    options.Add("         ");
                    options.Add("[E] Pick color");
                    options.Add("[Q] Done");
                    var box = new Window("", 50, 7, options);
                    box.Draw();
                    options.Clear();

                    ConsoleKeyInfo keyInfo = Console.ReadKey();

                    if (keyInfo.Key == ConsoleKey.RightArrow) // rotera åt höger  1 2 3 4 5 = 5 1 2 3 4
                    {
                        Helpers.RotateRight(everyColors);

                    }
                    else if (keyInfo.Key == ConsoleKey.LeftArrow) //  rotera åt vänster   1 2 3 4 5 = 2 3 4 5 1
                    {
                        Helpers.RotateLeft(everyColors);

                    }
                    else if (keyInfo.Key == ConsoleKey.E)
                    {
                        selectedColorIds.Add(everyColors[pointer].Id);
                        everyColors.RemoveAt(pointer);
                    }
                    else if (keyInfo.Key == ConsoleKey.Q)
                    {
                        int[] colorId = new int[selectedColorIds.Count];
                        for (int i = 0; i < selectedColorIds.Count; i++)
                        {
                            colorId[i] = selectedColorIds[i];
                        }
                        return colorId;
                    }
                }
            }
        }

        public static void RemoveSize(int productGroup)
        {
            List<string> size = new List<string>();
            var sizeWindow = new Window("", 5, 2, size);

            size.Add("Are you sure you want to delete a size. ONLY DO IF WRONG SIZE HAS BEEN MADE. DO NOT REMOVE A SIZE THAT BEEN SOLD!");
            size.Add("[Y]ES    [Q] NO");
            int pointer = 0;
            ConsoleKeyInfo key = Console.ReadKey();

            if (key.Key == ConsoleKey.Y)
            {
                size.Clear();
                Console.Clear();
                using (var myDb = new MyDbContext())
                {
                    var allSizes = myDb.products.Where(p => p.ProductGroup == productGroup).ToList();

                    for (int i = 0; i < allSizes.Count; i++)
                    {
                        var sizeText = "Size: " + allSizes[i];


                        size.Add(sizeText + (i == allSizes.Count - 1 ? " <-" : ""));

                    }

                    size.Add("");
                    sizeWindow.Draw();
                    key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.RightArrow)
                    {
                        if (pointer == allSizes.Count)
                        {
                            pointer = 0;
                        }
                        else
                        {
                            pointer++;
                        }
                    }
                    else if (key.Key == ConsoleKey.LeftArrow)
                    {
                        if (pointer == 0)
                        {
                            pointer = allSizes.Count;
                        }
                        else
                        {
                            pointer--;
                        }
                    }
                    else if (key.Key == ConsoleKey.E)
                    {
                        while (true)
                        {
                            Console.Clear();
                            size.Clear();
                            sizeWindow.Draw();
                            size.Add("Is this the size you want to remove?");
                            size.Add("    Size: " + allSizes[pointer].Size);
                            size.Add("       [Y]ES    [Q] NO");

                            key = Console.ReadKey();

                            if (key.Key == ConsoleKey.Y)
                            {
                                string connectionString = DataTracker.GetConnectionString();
                                using (var connection = new SqlConnection(connectionString))
                                {
                                    connection.Open();

                                    // DELETE query to remove product by ID
                                    string query = $"DELETE FROM products WHERE ProductGroup = @{productGroup} && Size = @{allSizes[pointer].Size}";

                                    // Execute the query
                                    int rowsAffected = connection.Execute(query, new { productId = productGroup, Size = allSizes[pointer].Size });

                                }
                            }
                            else if (key.Key == ConsoleKey.Q)
                            {
                                break;
                            }
                            else
                            {
                                size.Add("Invalid input");
                                sizeWindow.Draw();
                                Thread.Sleep(1000);

                            }
                        }
                    }
                }
            }
            else if (key.Key == ConsoleKey.Q)
            {

            }
        }


        public static bool EnterBoolValue(string infoTitle)
        {
            Console.Clear();
            List<string> info = new List<string>();
            bool answer = true;
            info.Add("What would you like to pick for " + infoTitle);
            info.Add("      [Y]es    [N]o");
            var groupIdCheckBox = new Window("", 50, 7, info);
            groupIdCheckBox.Draw();

            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.N)
            {
                answer = false;
            }
            else if (key.Key == ConsoleKey.Y)
            {
                //  ;..;
            }

            return answer;
        }
        public static int EnterIntValue(string infoTitle)
        {
            Console.Clear();
            List<string> info = new List<string>();
            int price;

            info.Add("What would you like to pick for " + infoTitle);
            info.Add("Value: ");
            var valueBox = new Window("", 50, 7, info);
            valueBox.Draw();
            Console.SetCursorPosition(59, 9);
            while (!int.TryParse(Console.ReadLine(), out price))
            {
                info.Add("");
                info.Add("Invalid input. Please enter a valid number.");
                Console.SetCursorPosition(60, 8);

                valueBox.Draw();
            }
            return price;
        }
        public static string EnterValue(string infoTitle)
        {
            Console.Clear();
            List<string> info = new List<string>();

            info.Add("What would you like to write for " + infoTitle);
            info.Add("                                                                                                 ");
            info.Add("Type: ");
            var groupIdCheckBox = new Window("", 50, 7, info);
            groupIdCheckBox.Draw();
            Console.SetCursorPosition(58, 10);
            string answer = Console.ReadLine();
            return answer;
        } 

    }
}
