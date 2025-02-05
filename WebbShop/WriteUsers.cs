using Dapper;
using Microsoft.Data.SqlClient;
using WebbShop.Model;

namespace WebbShop
{
    internal class WriteUsers
    {

        public static void SearchUserOptions()
        {
            Console.Clear();
            using (var myDb = new MyDbContext())
            {
                List<string> box = new List<string>();
                box.Add("Search for a user or every list users");
                box.Add("    [S]earch      [L]ist   ");
                var searchOption = new Window("", 60, 8, box);
                searchOption.Draw();



                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.S)
                {
                    string connectionString = DataTracker.GetConnectionString();
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {

                        string text = "your search:                                 ";
                        string searchText = AdminTools.EnterValue(text);


                        string query = @"SELECT UserName, Id
                                        FROM users WHERE Id LIKE @SearchText OR UserName LIKE @SearchText, OR Name LIKE @SearchText";

                        var listUser = conn.Query<User>(query, new { SearchText = "%" + searchText + "%" }).ToList();

                        Console.ReadLine();
                        SearchEngine(listUser);
                    }
                }
                else if (key.Key == ConsoleKey.L)
                {
                    var everyUser = myDb.users.ToList();
                    DataTracker.SetRunPage(true);
                    SearchEngine(everyUser);
                }

            }
        }

        public static void SearchEngine(List<User>? users)
        {

            using (var myDb = new MyDbContext())
            {
                List<string> box = new List<string>();
                int pageSize = 6;   // Hur många produkter som ska visas

                // Få Max antal sidor
                int countedUsers = users.Count;
                int totalPages = (int)Math.Ceiling((double)countedUsers / pageSize);

                int start = 1;
                DataTracker.SetPageNumber(start);

                while (DataTracker.GetRunPage())
                {
                    Console.Clear();
                    int page = DataTracker.GetPageNumber();


                    // få enbart de produkter som ska vissa på sidan
                    var pageUsers = users
                        .Skip((page - 1) * pageSize)  //skip tar bort de första så vi inte bevarar info från tidigare sidor
                        .Take(pageSize)
                        .ToList();

                    //tömma listan så den blir tom mellan sidorna


                    int[,] positions =
                        {
                            {20, 8, 0},   // 1
                            {58, 8,0},   // 2
                            {95, 8,0},  // 3
                            {20, 16,0},  // 4
                            {58, 16,0},  // 5
                            {95, 16,0}  // 6
                        };
                    for (int i = 0; i < pageUsers.Count(); i++)
                    {
                        var user = pageUsers[i];
                        var selectedUser = myDb.users.Where(p => p.Id == user.Id).SingleOrDefault();



                        box.Add("Users Name: " + selectedUser.Name);
                        box.Add("Users UserName: " + selectedUser.UserName);
                        box.Add("Users Age: " + selectedUser.Age);

                        // position för lådor, 

                        positions[i, 2] = user.Id;

                        var productwindow = new Window("Product " + (i + 1), positions[i, 0], positions[i, 1], box);
                        productwindow.Draw();
                        Helpers.TopBarBox();
                        Helpers.UserBox();
                        box.Clear();

                        if (i == pageUsers.Count() - 1)
                        {
                            Console.WriteLine($"Page {page} of {totalPages}");
                            ConsoleKeyInfo key = Console.ReadKey();
                            DataTracker.SetRunPage(true);
                            switch (key.Key)
                            {


                                case ConsoleKey.LeftArrow:
                                    {
                                        int reduce = DataTracker.GetPageNumber() - 1;
                                        DataTracker.SetPageNumber(reduce);
                                        break;
                                    }
                                case ConsoleKey.RightArrow:
                                    {
                                        int increase = DataTracker.GetPageNumber() + 1;
                                        DataTracker.SetPageNumber(increase);
                                        break;
                                    }


                                case ConsoleKey.D1:
                                    {

                                        ChangeUserData(positions[0, 2]);
                                        break;
                                    }
                                case ConsoleKey.D2:
                                    {

                                        ChangeUserData(positions[1, 2]);
                                        break;
                                    }
                                case ConsoleKey.D3:
                                    {

                                        ChangeUserData(positions[2, 2]);
                                        break;
                                    }
                                case ConsoleKey.D4:
                                    {

                                        ChangeUserData(positions[3, 2]);
                                        break;
                                    }
                                case ConsoleKey.D5:
                                    {

                                        ChangeUserData(positions[4, 2]);
                                        break;
                                    }
                                case ConsoleKey.D6:
                                    {

                                        ChangeUserData(positions[5, 2]);
                                        break;
                                    }

                                case ConsoleKey.B:
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



        public static void ChangeUserData(int positions)
        {

            List<string> box = new List<string>();


            using (var myDb = new MyDbContext())
            {
                int pointer = 0;

                while (DataTracker.GetRunPage())
                {
                    Console.Clear();
                    var user = myDb.users.Where(x => x.Id == positions).FirstOrDefault();


                    box.Add("Users Name: " + user.Name + (pointer == 0 ? " <-" : ""));
                    box.Add("Users UserName: " + user.UserName + (pointer == 1 ? " <-" : ""));
                    box.Add("Users Age: " + user.Age + (pointer == 2 ? " <-" : " "));
                    box.Add("Users Mail: " + user.Mail + (pointer == 3 ? " <-" : ""));
                    box.Add("Users Addres: " + user.Addres + (pointer == 4 ? " <-" : ""));
                    box.Add("Users Security Number: " + user.SecurityNumber + (pointer == 5 ? " <-" : ""));

                    string[] infoTitle = { "User Name: ", "User UserName: ", "User Age: ", "User Mail: ", "User Addres", "User Sercurity Number: " };
                    var infoChange = new Dictionary<int, Action>
                                                 {
                                                    { 0, () => user.Name = AdminTools.EnterValue(infoTitle[0]) },
                                                    {  1, () => user.UserName = AdminTools.EnterValue(infoTitle[1]) },
                                                    {  2, () => user.Age = Helpers.GetBirthDateFromUser() },
                                                    {  3, () => user.Mail = AdminTools.EnterValue(infoTitle[2])},
                                                    {  4, () => user.Addres = AdminTools.EnterValue(infoTitle[3]) },
                                                    {  5, () => user.SecurityNumber = AdminTools.EnterValue(infoTitle[4]) },
                                                    {  6, () => user.City = Helpers.GetCityFromUser()  },


                                                 };

                    var productwindow = new Window("", 25, 8, box);
                    productwindow.Draw();
                    box.Clear();
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            {
                                if (pointer == 0)
                                {
                                    pointer = 6;
                                }
                                else
                                {
                                    pointer--;
                                }
                                break;

                            }
                        case ConsoleKey.DownArrow:
                            {
                                if (pointer == 6)
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
                                infoChange[pointer]();
                                myDb.SaveChanges();
                                break;
                            }
                        case ConsoleKey.B:
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
