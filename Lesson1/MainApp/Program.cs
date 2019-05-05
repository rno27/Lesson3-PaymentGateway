using System;
using System.Linq;
using Abstractions;
using Common;
using Lesson1;
using PaymentMethod;


namespace Lesson3.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            DataRepository repository = new DataRepository();
            repository.Initialize();

            ConsoleMenuController menuController = new ConsoleMenuController(repository);
            menuController.Initialize();
            //BankPayPlugin bk = new BankPayPlugin();
            PayPallPlugin pl = new PayPallPlugin();
            //menuController.AddAvailableShape(bk);
            menuController.AddAvailableShape(pl);
            menuController.EnterMainMenu();
            
        }
    }
}
