using System;

class Program
{
    
    static VendingMachine vendingMachine = new VendingMachine();

    static void Main(string[] args)
    {

        while (true)
        {
            DisplayMainMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    vendingMachine.DisplayProducts();
                    break;
                case "2":
                    InsertCoinMenu();
                    break;
                case "3":
                    PurchaseProductMenu();
                    break;
                case "4":
                    vendingMachine.ReturnChange();
                    break;
                case "5":
                    AdminMenu();
                    break;
                case "6":
                    Console.WriteLine("Thank you for using the vending machine. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    static void DisplayMainMenu()
    {
        Console.WriteLine("=====================================");
        Console.WriteLine(" C# Vending Machine - Main Menu");
        Console.WriteLine("=====================================");
        Console.WriteLine($"Current Balance: {vendingMachine.UserBalance:C}");
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("1. Display Products");
        Console.WriteLine("2. Insert Coin");
        Console.WriteLine("3. Purchase Product");
        Console.WriteLine("4. Return Change & End Session");
        Console.WriteLine("5. Administrator Menu");
        Console.WriteLine("6. Exit");
        Console.Write("\nPlease select an option: ");
    }


static void InsertCoinMenu()
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("--- Insert Money ---");
        Console.WriteLine($"Your current balance is: {vendingMachine.UserBalance:C}");
        Console.WriteLine("We accept the following denominations: 0.10, 0.25, 0.50, 1.00, 5.00");
        Console.Write("Please insert a coin/bill, or type 'done' to return to the main menu: ");

        string input = Console.ReadLine();

        if (input.Equals("done", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Returning to main menu...");
            break;
        }

        if (decimal.TryParse(input, out decimal amount))
        {

            if (!vendingMachine.InsertCoin(amount))
                {
                    Console.WriteLine("Press any key to try again...");
                    Console.ReadKey();
                }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid number or type 'done'.");
            Console.WriteLine("Press any key to try again...");
            Console.ReadKey();
        }
    }
}

    static void PurchaseProductMenu()
    {
        vendingMachine.DisplayProducts();
        Console.Write("Enter the number of the product you wish to buy: ");
        if (int.TryParse(Console.ReadLine(), out int productNumber))
        {
            vendingMachine.PurchaseProduct(productNumber);
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid product number.");
        }
    }
    
    static void AdminMenu()
    {
        Console.Write("Enter administrator password: ");
        string password = Console.ReadLine();

        if (password != "admin123")
        {
            Console.WriteLine("Incorrect password.");
            return;
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine("\n--- Administrator Menu ---");
            Console.WriteLine("1. Restock Product");
            Console.WriteLine("2. Collect Revenue");
            Console.WriteLine("3. View Full Inventory Report");
            Console.WriteLine("4. Return to Main Menu");
            Console.Write("\nSelect an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    RestockMenu();
                    break;
                case "2":
                    vendingMachine.CollectRevenue();
                    break;
                case "3":
                    vendingMachine.DisplayProducts();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    static void RestockMenu()
    {
        Console.Write("Enter the product number to restock: ");
        if (int.TryParse(Console.ReadLine(), out int productNumber))
        {
            Console.Write("Enter the quantity to add: ");
            if (int.TryParse(Console.ReadLine(), out int amount))
            {
                vendingMachine.RestockProduct(productNumber, amount);
            }
            else
            {
                Console.WriteLine("Invalid quantity.");
            }
        }
        else
        {
            Console.WriteLine("Invalid product number.");
        }
    }
}