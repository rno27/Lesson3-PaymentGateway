using System;
using System.IO;
using System.Linq;
using Abstractions;
using Common;
using Lesson1;
using PaymentMethod;
using System.Reflection;

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
            PaymentPluginManager pluginManager = new PaymentPluginManager();
            try
            {
                pluginManager.LoadPlugins();       
                foreach(var plugin in pluginManager.Plugins)
                {
                    menuController.AddAvailableShape(plugin);
                }
            }
            catch(DirectoryNotFoundException e)
            {
                Console.WriteLine($"WARNING: The plugins directory: {e.Message} does not exists. Press any key to continue ...");
                Console.ReadLine();
            }
            catch(ReflectionTypeLoadException e)
            {
                Console.WriteLine("TypeLoad error " + e.Message);
                return;
            }
            catch(Exception e)
            {
            
                Console.WriteLine("Unexpected error occured while loading the plugins. " + e.Message);
                return;
            }

            menuController.EnterMainMenu();

        }
        

        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        /* 
        static void Main(string[] args)
        {
            DataRepository repository = new DataRepository();
            repository.Initialize();

            ConsoleMenuController menuController = new ConsoleMenuController(repository);
            menuController.Initialize();
            BankPayPlugin bk = new BankPayPlugin();
            PayPallPlugin pl = new PayPallPlugin();
            menuController.AddAvailableShape(bk);
            menuController.AddAvailableShape(pl);
            menuController.EnterMainMenu();
        }
        */
    }
}
