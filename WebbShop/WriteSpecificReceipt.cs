using WebbShop.Model;

namespace WebbShop
{
    internal class WriteSpecificReceipt
    {
        public static void WriteReceipt(int cartGroupId)
        {
            using (var myDb = new MyDbContext())
            {
                Console.Clear();
                List<string> receipt = new List<string>();

                var everyReceipt = myDb.shopingCart.Where(p => p.CartGroupId == cartGroupId).ToList();
                var groupId = everyReceipt.FirstOrDefault();
                var id = groupId;

                if (id != null)
                {
                    int pointer = 0;

                    float price = 0;

                    var brands = myDb.brands.ToList();
                    var user = myDb.users.Where(p => p.Id == groupId.Id).SingleOrDefault();

                    var products = myDb.products.Where(p => p.Id == id.ProductId).ToList();

                    var brandName = (from p in products
                                     join b in brands
                                     on p.Id equals b.Id
                                     where p.Id == id.Id
                                     select b.Name)
                            .FirstOrDefault();


                    receipt.Add(user.Name);
                    receipt.Add("Addres " + user.Addres);
                    receipt.Add("Social Number " + user.SecurityNumber);

                    if (id.Frakt != null)
                    {
                        string[] words = id.Frakt.Split(' ');
                        string result;
                        result = $"{words[0]} {words[1]} -- {words[^2]} {words[^1]}";
                        receipt.Add(result);
                    }
                    else
                    {
                        receipt.Add("Frakt: Unknown/Error -- Call support for more infomation");
                    }
                    for (int i = 0; i < products.Count; i++)
                    {
                        var product = products[i];

                        if(pointer == i)
                        {
                            receipt.Add(product.ProductName + "<-");
                        }
                        else
                        {
                            receipt.Add(product.ProductName);
                        }
                        receipt.Add("Brand: " + brandName + "   Price: " + product.Price + " Sek");
                        receipt.Add("Size: " + product.Size + "   color: " + id.color);
                        receipt.Add("Amount: " + id.Antal.ToString());
                        receipt.Add("");

                        price += product.Price;
                    }
                    receipt.Add("Total price: " + price);

                    var productwindow = new Window("Receipt", 65, 8, receipt);
                    receipt.Add("Time when purchaed: " + id.DateWhenBought.ToString());
                    productwindow.Draw();
                    receipt.Clear();


                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            {
                                if (pointer == 0)
                                {
                                    pointer = products.Count - 1;
                                }
                                else
                                {
                                    pointer--;
                                }
                                break;
                            }
                        case ConsoleKey.DownArrow:
                            {
                                if(pointer == products.Count -1)
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

                                HandleOrderChange.OrderChange(products[pointer].Id, cartGroupId);

                                break;
                            }
                        case ConsoleKey.Q:
                            {


                                break;
                            }

                    }
                }
            }
        }
    }
}
