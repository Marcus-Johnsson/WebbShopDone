using Dapper;
using Microsoft.Data.SqlClient;
using System.Drawing;
using WebbShop.Model;

namespace WebbShop
{
    internal class WriteUsers
    {

        public static void SearchUserOptions()
        {
            using (var myDb = new MyDbContext())
            {
                List<string> box = new List<string>();
                box.Add("Search for a user or every list users");
                box.Add("    [S]earch      [L]ist   ");

                var listUser = new List<User>();

                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.S)
                {
                    string connectionString = DataTracker.GetConnectionString();
                    string seachText = "your search: ";
                    string searchText = AdminTools.EnterValue(seachText);

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = @"SELECT Id FROM Users WHERE Id LIKE @SearchText OR UserName LIKE @SearchText, OR Name LIKE @SearchText";

                        listUser = conn.Query<User>(query, new { SearchText = "%" + searchText + "%" }).ToList();
                    }
                }
                else if (key.Key == ConsoleKey.L)
                {
                    listUser = myDb.users.ToList();
                    SearchEngine(listUser);
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
                    for (int i = 0; i < users.Count(); i++)
                    {
                        var user = users[i];
                        var selectedUser = myDb.users.Where(p => p.Id == user.Id).SingleOrDefault();



                        box.Add("Users Name: " + selectedUser.Name);
                        box.Add("Users UserName: " + selectedUser.UserName);
                        box.Add("Users Age: " + selectedUser.Age);

                        // position för lådor, 

                        positions[i, 2] = user.Id;

                        var productwindow = new Window("Product " + (i + 1), positions[i, 0], positions[i, 1], box);
                        productwindow.Draw();
                        Helpers.TopBarBox();
                        Helpers.WriteCart();
                        Helpers.UserBox();
                        box.Clear();

                        if (i == users.Count() - 1)
                        {
                            Console.WriteLine($"Page {page} of {totalPages}");
                            ConsoleKeyInfo key = Console.ReadKey();
                            switch (key.Key)
                            {

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

            int pointer = 0;
            using (var myDb = new MyDbContext())
            {
                var user = myDb.users.Where(x => x.Id == positions).FirstOrDefault();


                box.Add("Users Name: " + user.Name + (pointer == 0 ? "" : " <-"));
                box.Add("Users UserName: " + user.UserName + (pointer == 1 ? "" : " <-"));
                box.Add("Users Age: " + user.Age + (pointer == 2 ? "" : " <-"));
                box.Add("Users Mail: " + user.Mail + (pointer == 3 ? "" : " <-"));
                box.Add("Users Addres: " + user.Addres + (pointer == 4 ? "" : " <-"));
                box.Add("Users Security Number: " + user.SecurityNumber + (pointer == 5 ? "" : " <-"));

                string[] infoTitle = { "User Name: ", "User UserName: ", "User Age: ", "User Mail: ", "User Addres", "User Sercurity Number: " };
                var infoChange = new Dictionary<bool, Action>
                                                 {
                                                    { pointer == 0, () => user.Name = AdminTools.EnterValue(infoTitle[0]) },
                                                    { pointer == 1, () => user.UserName = AdminTools.EnterValue(infoTitle[1]) },
                                                    { pointer == 2, () => user.Age = Helpers.GetBirthDateFromUser() },
                                                    { pointer == 3, () => user.Mail = AdminTools.EnterValue(infoTitle[2])},
                                                    { pointer == 4, () => user.Addres = AdminTools.EnterValue(infoTitle[3]) },
                                                    { pointer == 5, () => user.SecurityNumber = AdminTools.EnterValue(infoTitle[4]) },
                                                    { pointer == 6, () => user.City = Helpers.GetCityFromUser()  },
                                
                                                };


            }

        }
    }
}
