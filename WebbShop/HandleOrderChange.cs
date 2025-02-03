using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbShop.Model;

namespace WebbShop
{
    internal class HandleOrderChange
    {


        public static void OrderChange(int id, int cartGroupId)
        {
            Console.Clear();

            List<string> list = new List<string>();

            using (var myDb = new MyDbContext())
            {
                var changeProduct = myDb.shopingCart.Where(p=>p.ProductId == id && p.CartGroupId == cartGroupId).FirstOrDefault();
                var productInfo = myDb.products.Where(p=>p.Id == id).SingleOrDefault();

                int pointer = 0;

                if (changeProduct != null)
                {
                    list.Add("Error 1011 Id or CartGroupId is wrong");
                }
                else
                {
                    list.Add($"Product: " + productInfo.ProductName + (pointer == 0 ? "<-" :""));
                    list.Add($"Size: " + productInfo.Size + (pointer == 1 ? "<-" : ""));
                    list.Add($"Color: " + productInfo.ColorId + (pointer == 2 ? "<-" : ""));
                    list.Add($"Quantity: " + changeProduct.Antal + (pointer == 3 ? "<-" : ""));

                    list.Add("[E]nter");
                    list.Add("[B]ack");

                    ConsoleKeyInfo key = Console.ReadKey(true);

                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            {
                                if (pointer == 0)
                                {
                                    pointer = 3;
                                }
                                else
                                {
                                    pointer--;
                                }
                                break;
                            }
                        case ConsoleKey.DownArrow:
                            {
                                if(pointer == 3)
                                {
                                    pointer = 0;
                                }
                                else
                                {
                                    pointer++;
                                }
                                break;
                            }
                        case ConsoleKey.E:
                            {
                                if (pointer == 0)
                                {

                                }
                                break;
                            }
                        case ConsoleKey.B:
                            {
                                break;
                            }
                    }
                    
                }
            }
        }

        public static void ChangeProduct(int id)
        {
            DataTracker.SetRunPage(true);
            WriteAllPages.WriteOutPages();

            using (var myDb = new MyDbContext())
            {

            }
        }
    }
}
