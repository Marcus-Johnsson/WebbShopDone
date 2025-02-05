namespace WebbShop
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            // Micke Frågor! 'using' statement can be simplified

            // visual klagar (blåt medelande) den föredrar >>>  var myDb = new MyDbContext()

            // istället för using (var myDb = MyDbContext()) { bla bla bla.....}  

            //------

            // Kan/ska man använda var istället för typ T och andra kommandon?

            //-------------

            // createUserBox.Add("   Addres:                    "); eller  createUserBox.Add("   Addres: "); Spelar det någon större roll i sin hel het?

            //AddToDataBase.Run();
            

            await HomePage.StartPage();



        }


    }

}
