namespace WebbShop
{
    internal class HomePage
    {

        public static async Task StartPage()
        {

            while (true)
            {
                Console.Clear();
                int[] firstPageId = DataTracker.GetFirstPageProducts();

                if (DataTracker.GetIsAdmin() == true)
                {
                    AdminPage.WriteAdminPage();
                }

                if (DataTracker.GetUserId() == 0) // Micke! Det här är enbart för det ska vara en "pop up".
                {
                    await Helpers.Product1();
                    await Helpers.Product2();
                    await Helpers.Product3();
                    await Helpers.Product4();
                    Helpers.TopBarBox();
                    Helpers.WriteCart();
                    Thread.Sleep(2000);
                    Helpers.LogginUser();
                }

                Console.Clear();
                Helpers.TopBarBox();
                await Helpers.Product1();
                await Helpers.Product2();
                await Helpers.Product3();
                await Helpers.Product4();
                Helpers.WriteCart();
                Helpers.UserBox();

                List<string> userInput = new List<string> { "" };
                userInput.Add("Choose products [1-4]");
                userInput.Add("[P]Products list");
                userInput.Add("[C}art");
                userInput.Add("[S]earch");
                userInput.Add("[A]ccount");

                var intopWindow = new Window("Inputs", 35, 25, userInput);
                intopWindow.Draw();

                ConsoleKeyInfo keyInfo = Console.ReadKey();


                switch (keyInfo.Key)
                {
                    case ConsoleKey.D1:
                        {
                            DataTracker.SetProductId(firstPageId[0]);
                            Console.Clear();
                            SpecificProduct.WriteSpecificProduct(DataTracker.GetProductId());
                            break;
                        }
                    case ConsoleKey.D2:
                        {
                            DataTracker.SetProductId(firstPageId[1]);
                            Console.Clear();
                            SpecificProduct.WriteSpecificProduct(DataTracker.GetProductId());
                            break;
                        }
                    case ConsoleKey.D3:
                        {
                            DataTracker.SetProductId(firstPageId[2]);
                            Console.Clear();
                            SpecificProduct.WriteSpecificProduct(DataTracker.GetProductId());
                            break;
                        }
                    case ConsoleKey.D4:
                        {
                            DataTracker.SetProductId(firstPageId[3]);
                            Console.Clear();
                            SpecificProduct.WriteSpecificProduct(DataTracker.GetProductId());
                            break;
                        }
                    case ConsoleKey.S:
                        {
                            DataTracker.SetRunPage(true);
                            Search.SearchEngine();
                            break;
                        }
                    case ConsoleKey.P:
                        {
                            DataTracker.SetRunPage(true);
                            WriteAllPages.WriteOutPages();
                            break;
                        }
                    case ConsoleKey.C:
                        {
                            BuyCart.ContinueOrCreateAcc();
                            break;
                        }
                    case ConsoleKey.A:
                        {
                            Helpers.LogginUser();
                            break;
                        }
                }

            }
        }

    }
}

