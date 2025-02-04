﻿using WebbShop.Model;

namespace WebbShop
{
    internal class ReceiptPages
    {
        public static void WriteAllReceipt()
        {
            using (var myDb = new MyDbContext())
            {
                List<string> receipt = new List<string>();
                var everyReceipt = myDb.ShopingCart.Where(p => p.CompletedPurchase == true).GroupBy(p => p.CartGroupId).ToList();

                
                int pageSize = 6;   // Hur många produkter som ska vissas

                // Få Max antal sidor
                int totalReceipt = everyReceipt.Count;
                int totalPages = (int)Math.Ceiling((double)totalReceipt / pageSize);

                
                int start = 1;
                DataTracker.SetPageNumber(start);
                while (DataTracker.GetRunPage())
                {
                    Console.Clear();
                    int page = DataTracker.GetPageNumber();
                    // få enbart de kvitton som ska vissa på sidan
                    var pageReceipt = everyReceipt
                        .Skip((page - 1) * pageSize)  //skip tar bort de första så vi inte får fram info från tidigare sidor
                        .Take(pageSize)
                        .ToList();


                    int[,] positions =
{
                            {5, 8, 0},   // 1
                            {55, 8,0},   // 2
                            {115, 8,0},  // 3
                            {5, 16,0},  // 4
                            {55, 16,0},  // 5
                            {115, 16,0}  // 6
                        };
                    var brands = myDb.brands.ToList();
                    for (int i = 0; i < pageReceipt.Count; i++)
                    {
                        var groupId = pageReceipt[i];
                        var id = groupId.FirstOrDefault();

                        if (id != null)
                        {
                            var user = myDb.users.Where(p => p.Id == id.UserId).SingleOrDefault();

                            var products = myDb.products.Where(p => p.Id == id.ProductId).ToList();

                            var brandName = (from p in products
                                             join b in brands
                                             on p.Id equals b.Id
                                             where p.Id == id.Id
                                             select b.Name)
                                    .FirstOrDefault();

                            receipt.Add(user.Name);
                            receipt.Add("Addres: " + user.Addres);
                            receipt.Add("Social Number" + user.Age);

                            foreach (var product in products)
                            {
                                receipt.Add(product.ProductName);
                            }




                            receipt.Add("Time when purchaed: " + id.DateWhenBought.ToString()); //("yyyy-MM-dd"));

                            // position för lådor, 

                            positions[i, 2] = id.CartGroupId;

                            var productwindow = new Window("Receipt " + (i + 1), positions[i, 0], positions[i, 1], receipt);

                            productwindow.Draw();
                            Helpers.TopBarBox();
                            Helpers.WriteCart();
                            receipt.Clear();


                            if (i == pageReceipt.Count() - 1)
                            {
                                Console.WriteLine($"Page {page} of {totalPages}");
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
                                    WriteSpecificReceipt.WriteReceipt(positions[0, 2]);
                                }
                                else if (key.Key == ConsoleKey.D2)
                                {
                                    WriteSpecificReceipt.WriteReceipt(positions[1, 2]);
                                }
                                else if (key.Key == ConsoleKey.D3)
                                {
                                    WriteSpecificReceipt.WriteReceipt(positions[2, 2]);
                                }
                                else if (key.Key == ConsoleKey.D4)
                                {
                                    WriteSpecificReceipt.WriteReceipt(positions[3, 2]);
                                }
                                else if (key.Key == ConsoleKey.D5)
                                {
                                    WriteSpecificReceipt.WriteReceipt(positions[4, 2]);
                                }
                                else if (key.Key == ConsoleKey.D6)
                                {
                                    WriteSpecificReceipt.WriteReceipt(positions[5, 2]);
                                }
                                else if (key.Key == ConsoleKey.B)
                                {
                                    DataTracker.SetRunPage(false);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
