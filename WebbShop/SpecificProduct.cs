using WebbShop.Model;

namespace WebbShop
{
    internal class SpecificProduct
    {
        static public void WriteSpecificProduct(int positions)              // allt funkar i classen
        {
            Console.Clear();


            using (var myDb = new MyDbContext())
            {
                List<string> product = new List<string>();
                List<string> input = new List<string>();
                

                var selectedProduct = myDb.products.Where(p => p.Id == positions).SingleOrDefault();

                var productGroup = myDb.products.Where(p => p.ProductGroup == selectedProduct.ProductGroup).ToList();

              

                var colors = selectedProduct.ColorId.Select(pc => pc).ToList();

                var result = (from color in colors
                              join c in myDb.colors on color equals c.Id
                              select new
                              {
                                  c.Id,
                                  c.Name,
                                  RelatedColors = colors

                              }).ToList();

                //lista är viktigt för rotera!!
                //--------------------------------------------------

                // Få en produkts alla storlekar


                string[] sortSize = { "XS", "S", "M", "L", "XL" };

                List<string> sizes = new List<string>();
                for (int i = 0; i < productGroup.Count; i++)
                {
                    sizes.Add(productGroup[i].Size);
                }
                sizes.OrderBy(sizes => sortSize).ToList();
                //lista är viktigt för rotera!!



                //--------------------------------------------------

                // Få Brand Namn
                var brands = myDb.brands;
                var brandName = (from p in productGroup
                                 join b in brands
                                 on p.Brand equals b.Id
                                 where p.Id == DataTracker.GetProductId()
                                 select b.Name)
                                 .FirstOrDefault();

                //--------------------------------------------------
                string cash = selectedProduct.Price.ToString();

                string line = "";
                int j = 0;
                string[] words = selectedProduct.Description.Split(' ');
                int pointer = 0;
                int productCount = 1;
                bool productSpecific = true;
                while (productSpecific == true)
                {
                    string sizeText = "Available Sizes: ";
                    string lastSize = sizes.Last();


                    for (int i = 0; i < sizes.Count; i++)
                    {
                        var size = sizes[i];

                        sizeText += size;

                        if (i != sizes.Count - 1)
                        {
                            sizeText += ", ";
                        }

                        if (pointer == 0 && size == lastSize)
                        {
                            sizeText += " <-";
                        }
                    }

                        string colorText = "Available colors: ";
                    for (int i = 0; i < result.Count; i++)
                    {
                        var color = result[i].Name;

                        colorText += color;

                        if (i != result.Count - 1)
                        {
                            colorText += ", ";
                        }

                        if (pointer == 1 && i == result.Count-1)
                        {
                            colorText += " <-";
                        }
                    }
                    Console.Clear();
                    Helpers.TopBarBox();
                    Helpers.WriteCart();
                    Helpers.UserBox();
                    product.Add(selectedProduct.ProductName);
                    product.Add(brandName);
                    product.Add("Gender: " + selectedProduct.Gender);

                    product.Add("");
                    product.Add(sizeText);
                    product.Add("");
                    product.Add(colorText);
                    product.Add("");
                    product.Add("");
                    // Delar upp beskrivningen
                    for (int i = 0; i < words.Length; i++)
                    {

                        line += words[i];
                        line += " ";

                        if (j == 6 || i == words.Length - 1)
                        {
                            product.Add(line);
                            line = "";
                            j = 0;
                        }
                        j++;
                    }
                    product.Add("");
                    if (pointer == 2)
                    {
                        product.Add("How many products: " + productCount + " <-");
                    }
                    else
                    {
                        product.Add("How many products: " + productCount);
                    }
                    product.Add(cash + " Sek");
                    var productwindow = new Window("", 25, 8, product);
                    productwindow.Draw();
                    product.Clear();
                    Helpers.WriteCart();

                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.RightArrow: // rotera åt höger  1 2 3 4 5 = 5 1 2 3 4
                            {
                                if (pointer == 0)
                                {
                                    Helpers.RotateRight(sizes);
                                }
                                else if (pointer == 1)
                                {
                                    Helpers.RotateRight(result);
                                }
                                else if (pointer == 2)
                                {
                                    productCount++;
                                }


                                break;
                            }
                        case ConsoleKey.LeftArrow: //  rotera åt vänster   1 2 3 4 5 = 2 3 4 5 1
                            {
                                if (pointer == 0)
                                {
                                    Helpers.RotateLeft(sizes);
                                }
                                else if (pointer == 1)
                                {
                                    Helpers.RotateLeft(result);
                                }
                                else if (pointer == 2 && productCount > 1)
                                {
                                    productCount--;
                                }
                                break;
                            }
                        case ConsoleKey.UpArrow:    // Välja vad man ska rotera colors eller size
                            {
                                if (pointer == 0)
                                {
                                    pointer = 2;
                                }
                                else
                                {
                                    pointer = (pointer - 1) % 3;
                                }
                                break;
                            }

                        case ConsoleKey.DownArrow:  // Välja vad man ska rotera colors eller size
                            {
                                pointer = (pointer + 1) % 3;
                                break;
                            }
                        case ConsoleKey.E: // lägga in produkt i cart
                            {
                                if (DataTracker.GetIsAdmin() == false)
                                {
                                    int productGroupId = 0;
                                    var userId = DataTracker.GetUserId();

                                    var id = myDb.products.Where(p => p.
                                                ProductGroup == selectedProduct.
                                                ProductGroup && p.Size == sizes.First()).
                                                Select(p => p.Id).
                                                FirstOrDefault();


                                    // kontrollera om det finns en cart
                                    if (id != 0)
                                    {

                                        productGroupId = id;
                                    }
                                    else // ny cart max id++
                                    {

                                        int? maxCartGroupId = myDb.shopingCart.Max(p => (int?)p.CartGroupId);


                                        productGroupId = (maxCartGroupId.HasValue ? maxCartGroupId.Value + 1 : 1);
                                    }





                                    string productColor = result[0].Id.ToString();

                                    var cart = new ShopingCart()
                                    {
                                        ProductId = id,
                                        UserId = DataTracker.GetUserId(),
                                        color = productColor,
                                        Antal = productCount,
                                        CartGroupId = productGroupId,
                                        CompletedPurchase = false,

                                    };

                                    myDb.shopingCart.Add(cart);
                                    myDb.SaveChanges();

                                }
                                //size;


                                break;
                            }
                        case ConsoleKey.B:
                            {
                                productSpecific = false;
                                Console.Clear();
                                break;
                            }
                        case ConsoleKey.C:
                            {
                                BuyCart.ContinueOrCreateAcc();
                                break;
                            }
                    }
                }

            }
        }
    }
}
