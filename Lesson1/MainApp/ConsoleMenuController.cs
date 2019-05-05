using System;
using System.Linq;
using Abstractions;
using System.Collections.Generic;
using Lesson1;

namespace Lesson3.UI
{
    public class ConsoleMenuController
    {

        Menu mainMenu = new Menu();
        Menu newOrderMenu = new Menu();
        Menu newCustomerMenu = new Menu();

        public CustomerOrderController orderController;
        public readonly DataRepository repository;
        public Customer LoggedInCustomer = new Customer();
        //BankPay payment = new BankPay();
        Menu paymentMenu = new Menu();
        private PaymentContainer container = new PaymentContainer();
        List<PaymentPlugin> PaymentPlugins = new List<PaymentPlugin>();    

        
        /* public void HandlePayment(){
            payment.InitTranzaction();
        }
        */
        public void HandleCustomerLogin(){
            int ok = 0;
            int correctUser = 0;
            string user;

            do{
            correctUser = 0;
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("---Login as customer---");
            Console.WriteLine();
            Console.Write("User: ");
            user = Console.ReadLine();
            for (int i=0; i<repository.Customers.Count && ok==0; i++)
            {   
                
                if(repository.Customers[i].Name == user)
                {   
                    correctUser=1;
                    Console.Write("Password: ");
                    string pass = Console.ReadLine();
                    if(repository.Customers[i].Password == pass) ok=1;
                }
            }
           
            if(correctUser==0){
                Console.WriteLine("User not found !"); 
                System.Threading.Thread.Sleep(2000);
    
            }
                else 
                    if(correctUser==1 && ok ==0){
                        Console.WriteLine("Password is incorrect !");
                         System.Threading.Thread.Sleep(2000);
                         
                    }

            LoggedInCustomer.Name = user;
            }while(ok == 0);
        }

        public void HandleSellerLogin(){
            int ok = 0;

            do{
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("---Login as customer---");
            Console.WriteLine();
            Console.Write("User: ");
            string user = Console.ReadLine();
            Console.Write("Password: ");
            string pass = Console.ReadLine();
            if (user == "user" && pass== "seller")ok=1;
            }while(ok==0);

        }

        public Customer getCustomer(){
            int id=0;
            Customer currentCustomer = new Customer();
            foreach (var customer in repository.Customers){
                if(LoggedInCustomer.Name == customer.Name)
                    {
                    
                        id = customer.Id;
                    }
                    
            }
            return repository.Customers.ElementAt(id);
      }

        public void DisplayCustomerName(){
            Console.WriteLine("Customer Logged in As: {0}", LoggedInCustomer.Name);
            Console.WriteLine();
            
        }

        public string GetCustomerName(){
            return LoggedInCustomer.Name;
        }
        
        private void ListProductsInStock()
        {
            Console.WriteLine("PRODUCTS ");
            Stock currentStock = repository.ProductsStock;
            Console.WriteLine("{0,4}|{1,60}|{2,10}", "Id", "Name", "Qty");
            foreach(var stockEntry in currentStock.StockEntries)
            {
                Console.WriteLine("{0,4}|{1,60}|{2,10}", stockEntry.Product.Id, stockEntry.Product.Name, stockEntry.Qty);
            }
        }

        private void ListProductsAvailable()
        {
             Console.WriteLine("PRODUCTS Available: ");
            Stock currentStock = repository.ProductsStock;
            Console.WriteLine("{0,4}|{1,60}|{2,10}", "Id", "Name", "Qty");
            foreach(var stockEntry in currentStock.StockEntries)
            {
                if(stockEntry.Qty>0)
                Console.WriteLine("{0,4}|{1,60}|{2,10}", stockEntry.Product.Id, stockEntry.Product.Name, stockEntry.Qty);
            }
        }

        private void ListProductsInOrder(Customer customer)
        {
            var orderEntries = orderController.ListOrderEntries(customer);
            if (orderEntries.Count() == 0)
            {
                Console.WriteLine("Empty order !!!");
            }
            else
            {
                Console.WriteLine("Order entries ");
                Console.WriteLine("{0,4}|{1,4}|{2,40}|{3,10}|{4,10}|{5, 10}", "#" ,"Id", "Name", "Qty", "P.P.U", "Total");
                int idx = 0;
                foreach(var orderEntry in orderEntries)
                {
                    Console.WriteLine("{0,4}|{1,4}|{2,40}|{3,10}|{4,10}|{5, 10}", 
                                        idx+1,
                                        orderEntry.ProductId, 
                                        orderEntry.ProductName, 
                                        orderEntry.Qty, 
                                        orderEntry.PricePerUnit, 
                                        orderEntry.TotalPrice);
                    idx++;
                }
            }

            var orderSummary = orderController.GetOrderSummary(customer);    
            Console.WriteLine("\n\nTOTALS\n--------------------------------------------------------------------------------------------");
            Console.WriteLine("{0, 72} {1, 10}","Total without VAT:", orderSummary.Price);
            Console.WriteLine("{0, 72} {1, 10}","VAT:", orderSummary.VAT);
            Console.WriteLine("{0, 72} {1, 10}","Total with VAT:", orderSummary.TotalValue);
            Console.WriteLine("{0, 72} {1, 10}","Customer:",GetCustomerName());

        }

        private int ReadProductId()
        {
            int productId = 0;
            var readId = "";
            do 
            {
                Console.Write("Product Id: ");
                readId = Console.ReadLine();     

            } while (!Int32.TryParse(readId, out productId));   

            return productId;
        }

        private Product GetProductToAdd()
        {            
            int productId = 0;
            productId = ReadProductId();
            var currentStock = repository.ProductsStock;
            var stockEntry = currentStock.StockEntries.Where(entry => entry.Product.Id == productId)
                                     .SingleOrDefault();
            Product retVal = null;
            if (stockEntry != null)
            {   
                retVal = stockEntry.Product;
            }

            return retVal;
        }

        private ulong ReadQty()
        {
            var readString = "";
            ulong qty = 0;
            do 
            {
                Console.Write("Qty (valid number): ");
                readString = Console.ReadLine();     

            } while (!ulong.TryParse(readString, out qty));

            return qty;
        }

        public void HandleFinalizeOrder(Customer customer)
        {
            orderController.FinalizeOrder(customer);
        }

        private void HandleRemoveProduct(Customer customer)
        {
            Console.Clear();
            ListProductsInOrder(customer);
            int productId = ReadProductId();
            ulong qty = (ulong)ReadQty();
            try
            {
                orderController.RemoveItemFromOrder(customer,productId, qty);
                Console.WriteLine($"Successfully removed {qty} units of product {productId} from order");
            }
            catch(ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(Exception)
            {
                Console.WriteLine("Unexpected error occured while trying to remove the product from the order.");
            }

            Console.ReadLine();
        }

        private void HandleAddNewProduct(Customer customer)
        {
           
            Console.Clear();
            ListProductsInStock();
            var productToAdd = GetProductToAdd();
            var qty = ReadQty();
            try
            {    
                orderController.AddItemToOrder(productToAdd, (ulong)qty, customer);            
                Console.WriteLine($"Product {productToAdd.Name} {qty} pcs added to the order");
            }
            catch(Exception e)
            {
                Console.WriteLine("Unable to add product to order. " + e.Message);
            }
            
            Console.ReadLine();
        } 
        

        private void HandleViewAllProducts()
        {
            Console.Clear();
            ListProductsInStock();
            Console.ReadLine();
        }

        private void HandleViewStock()
        {
            Console.Clear();
            ListProductsAvailable();
            Console.ReadLine();
        }

        public ConsoleMenuController(DataRepository repository)
        {
            this.repository = repository;
            orderController = new CustomerOrderController(this.repository.ProductsStock);
        }

         public void AddAvailableShape(PaymentPlugin PaymentPlugin)
        {
            paymentMenu.SetMenuItem(PaymentPlugins.Count + 1, PaymentPlugin.GetName(), () => container.Add(PaymentPlugin.Pay()));
            PaymentPlugins.Add(PaymentPlugin);
        }

        public void HandleFinalizePayment(){
            paymentMenu.continueMenu = false;
            newOrderMenu.continueMenu = false;
            
        }

        public void Initialize()
        {   

            Menu newCustomerMenu = new Menu();
            Menu newSellerMenu = new Menu();

 
            mainMenu.SetMenuItem(1, "Seller accout", newCustomerMenu, () => HandleCustomerLogin());
            mainMenu.SetMenuItem(2, "Customer accout",newSellerMenu, () => HandleSellerLogin());

            newCustomerMenu.OnPreRender = new Action( ()=> DisplayCustomerName());
            newCustomerMenu.SetMenuItem(1, "New Order", newOrderMenu, () => orderController.StartNewOrder(LoggedInCustomer));  
            newCustomerMenu.SetMenuItem(2, "Back", mainMenu);  

            newOrderMenu.SetMenuItem(1, "Add product", () => HandleAddNewProduct(LoggedInCustomer));
            newOrderMenu.SetMenuItem(2, "Remove product from order", () => HandleRemoveProduct(LoggedInCustomer));
            newOrderMenu.SetMenuItem(3, "Finalize Order",paymentMenu, () => HandleFinalizePayment()); 
            newOrderMenu.SetMenuItem(4, "Cancel Order",newCustomerMenu, () => orderController.CancelOrder(LoggedInCustomer)); 

            paymentMenu.SetMenuItem(0, "Back", newOrderMenu);

            newSellerMenu.SetMenuItem(1, "Accept Order",() => HandleRemoveProduct(LoggedInCustomer));
            newSellerMenu.SetMenuItem(2, "Modify Order", () => HandleRemoveProduct(LoggedInCustomer));
            newSellerMenu.SetMenuItem(3, "Decline Order", () => HandleFinalizeOrder(LoggedInCustomer)); 
            newSellerMenu.SetMenuItem(4, "Back", mainMenu); 

            /* 
            newOrderMenu.SetMenuItem(1, "Add product", () => HandleAddNewProduct(getCustomer()));
            newOrderMenu.SetMenuItem(2, "Remove product from order", () => HandleRemoveProduct(getCustomer()));
            newOrderMenu.SetMenuItem(3, "Finalize Order",mainMenu, () => HandleFinalizeOrder(getCustomer())); 
            newOrderMenu.OnPreRender = new Action( ()=> ListProductsInOrder(getCustomer()));
            mainMenu.SetMenuItem(1, "New Order", newOrderMenu, () => seller.StartNewOrder(getCustomer()));            
            mainMenu.SetMenuItem(2, "View All Products", ()=>HandleViewAllProducts());
            mainMenu.SetMenuItem(3, "View Products in stock", ()=>HandleViewStock());
            */
        }
        public void EnterMainMenu()
        {
            mainMenu.EnterMenu();            
        } 
        
    }
}