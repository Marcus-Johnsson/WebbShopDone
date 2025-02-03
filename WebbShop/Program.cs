using WebbShop.Model;

namespace WebbShop
{
    internal class Program
    {
        
        static  async Task Main(string[] args)
        {
            // Om tiden fanns så skulle jag uppdatera hur jag fick fram data på vissa klasser, som products1-4 i helpers,
            // samt andra som jag gjorde i början av uppgiften. Jag lärde mig bättre sätt vilket man kan se i saker som man gjorde
            // närmare i slutet. AdminTools case har det som jag gjorde mot slutet, vissa av gamla som i specificProducts fick lite uppdaterat.

            // Det som sabbade mest med strukturen av kod var att man kom på saker och la till mitt i som borde ha varit med från början.
            // Blev lite att det man kom på gjorde saker lättare från början med ändra lite där så fick man skriva om större delen av koden.

           

            //AddToDataBase.Run();


            await HomePage.StartPage();

            

        }


    }

}
