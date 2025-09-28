using System;
using System.Collections.Generic;
using System.Linq;


/// This class manages the inventory, transactions, and user balance.
public class VendingMachine
{
    /// A list of all products available in the machine.
    public List<Product> Products { get; private set; }


    public decimal UserBalance { get; private set; }

    public decimal TotalRevenue { get; private set; }


    public VendingMachine()
    {
        Products = new List<Product>();
        UserBalance = 0;
        TotalRevenue = 0;
        InitializeStock();
    }

    private void InitializeStock()
    {
        Products.Add(new Product { Name = "Cola", Price = 1.50m, Quantity = 10 });
        Products.Add(new Product { Name = "Chips", Price = 1.00m, Quantity = 15 });
        Products.Add(new Product { Name = "Chocolate Bar", Price = 1.25m, Quantity = 12 });
        Products.Add(new Product { Name = "Water", Price = 0.75m, Quantity = 20 });
        Products.Add(new Product { Name = "Gum", Price = 0.50m, Quantity = 30 });
    }

    /// Displays all available products, their prices, and current stock.
    public void DisplayProducts()
    {
        Console.WriteLine("\n--- Available Products ---");
        for (int i = 0; i < Products.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {Products[i].Name,-15} | Price: {Products[i].Price:C} | Stock: {(Products[i].Quantity > 0 ? Products[i].Quantity.ToString() : "Out of Stock")}");
        }
        Console.WriteLine("--------------------------\n");
    }


    public bool InsertCoin(decimal amount)
    {
        // We can define which coins/notes are valid
        decimal[] validDenominations = { 0.10m, 0.25m, 0.50m, 1.00m, 5.00m };
        if (validDenominations.Contains(amount))
        {
            UserBalance += amount;
            Console.WriteLine($"Added {amount:C}. Your new balance is {UserBalance:C}.");
            return true;
        }
        else
        {
            Console.WriteLine($"Invalid denomination: {amount:C}. Please insert a valid coin or bill.");
            return false;
        }
    }


    public void PurchaseProduct(int productNumber)
    {
        // Convert 1-based number to 0-based index
        int productIndex = productNumber - 1;

        if (productIndex < 0 || productIndex >= Products.Count)
        {
            Console.WriteLine("Invalid product selection. Please try again.");
            return;
        }

        Product selectedProduct = Products[productIndex];

        if (selectedProduct.Quantity == 0)
        {
            Console.WriteLine("Sorry, this item is out of stock.");
            return;
        }

        if (UserBalance < selectedProduct.Price)
        {
            Console.WriteLine($"Insufficient balance. You need {selectedProduct.Price:C} but only have {UserBalance:C}.");
            return;
        }

        // Transaction
        UserBalance -= selectedProduct.Price;
        selectedProduct.Quantity--;
        TotalRevenue += selectedProduct.Price;

        Console.WriteLine($"Thank you for purchasing {selectedProduct.Name}!");
        Console.WriteLine($"Your remaining balance is {UserBalance:C}.");

        
        ReturnChange();
    }

    /// Returns any remaining balance to the user.
    public void ReturnChange()
    {
        if (UserBalance > 0)
        {
            Console.WriteLine($"Returning {UserBalance:C} in change.");
            UserBalance = 0;
        }
        else
        {
            Console.WriteLine("You have no balance to return.");
        }
    }


    /// Admin: Restocks a specific product.
    public void RestockProduct(int productNumber, int amount)
    {
        int productIndex = productNumber - 1;
        if (productIndex >= 0 && productIndex < Products.Count && amount > 0)
        {
            Products[productIndex].Quantity += amount;
            Console.WriteLine($"Restocked {amount} units of {Products[productIndex].Name}. New quantity: {Products[productIndex].Quantity}.");
        }
        else
        {
            Console.WriteLine("Invalid product number or amount for restocking.");
        }
    }

    /// Admin: Collects all revenue from the machine.
    public void CollectRevenue()
    {
        Console.WriteLine($"Collected {TotalRevenue:C} from the machine.");
        TotalRevenue = 0;
    }
}