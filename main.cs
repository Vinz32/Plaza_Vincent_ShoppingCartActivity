using System;

class Product
{
    public int Id;
    public string Name;
    public double Price;
    public int Stock;

    public Product(int id, string name, double price, int stock)
    {
        Id = id;
        Name = name;
        Price = price;
        Stock = stock;
    }

    public void Display()
    {
        Console.WriteLine("[" + Id + "] " + Name + " - P" + Price + " | Stock: " + Stock);
    }

    public double GetTotal(int qty)
    {
        return Price * qty;
    }

    public bool HasStock(int qty)
    {
        return Stock >= qty;
    }

    public void Deduct(int qty)
    {
        Stock = Stock - qty;
    }
}

class Program
{
    static void Main()
    {
        Product[] menu = new Product[5];
        menu[0] = new Product(1, "Laptop",   45000, 5);
        menu[1] = new Product(2, "Mouse",      500, 20);
        menu[2] = new Product(3, "Keyboard",  1200, 15);
        menu[3] = new Product(4, "Monitor",  12000,  8);
        menu[4] = new Product(5, "USB Hub",    800, 10);

        int[] cartId  = new int[10];
        int[] cartQty = new int[10];
        double[] cartSub = new double[10];
        int count = 0;

        Console.WriteLine("=== WELCOME TO THE STORE ===");

        string answer = "Y";

        while (answer == "Y")
        {
            Console.WriteLine("\n--- MENU ---");
            for (int i = 0; i < menu.Length; i++)
            {
                menu[i].Display();
            }

            Console.Write("\nEnter product number (0 to checkout): ");
            string input = Console.ReadLine();
            int id;

            if (!int.TryParse(input, out id))
            {
                Console.WriteLine("Please enter a number only.");
                continue;
            }

            if (id == 0)
            {
                break;
            }

            // find the product
            Product selected = null;
            for (int i = 0; i < menu.Length; i++)
            {
                if (menu[i].Id == id)
                {
                    selected = menu[i];
                }
            }

            if (selected == null)
            {
                Console.WriteLine("Product not found.");
                continue;
            }

            if (selected.Stock == 0)
            {
                Console.WriteLine("Sorry, that item is out of stock.");
                continue;
            }

            Console.Write("Enter quantity: ");
            string qInput = Console.ReadLine();
            int qty;

            if (!int.TryParse(qInput, out qty) || qty <= 0)
            {
                Console.WriteLine("Please enter a valid quantity.");
                continue;
            }

            if (!selected.HasStock(qty))
            {
                Console.WriteLine("Not enough stock. Only " + selected.Stock + " left.");
                continue;
            }

            // check if product is already in cart
            bool found = false;
            for (int i = 0; i < count; i++)
            {
                if (cartId[i] == selected.Id)
                {
                    cartQty[i] = cartQty[i] + qty;
                    cartSub[i] = cartSub[i] + selected.GetTotal(qty);
                    Console.WriteLine("Updated cart: " + selected.Name + " x" + cartQty[i] + " = P" + cartSub[i]);
                    found = true;
                }
            }

            if (found == false)
            {
                if (count >= 10)
                {
                    Console.WriteLine("Cart is full!");
                    continue;
                }

                cartId[count]  = selected.Id;
                cartQty[count] = qty;
                cartSub[count] = selected.GetTotal(qty);
                Console.WriteLine("Added to cart: " + selected.Name + " x" + qty + " = P" + cartSub[count]);
                count = count + 1;
            }

            selected.Deduct(qty);

            Console.Write("Add another item? (Y/N): ");
            answer = Console.ReadLine().ToUpper();
        }

        // show receipt
        if (count == 0)
        {
            Console.WriteLine("Your cart is empty.");
            return;
        }

        Console.WriteLine("\n--- RECEIPT ---");
        double grand = 0;

        for (int i = 0; i < count; i++)
        {
            string name = "";
            for (int j = 0; j < menu.Length; j++)
            {
                if (menu[j].Id == cartId[i])
                {
                    name = menu[j].Name;
                }
            }

            Console.WriteLine(name + " x" + cartQty[i] + " = P" + cartSub[i]);
            grand = grand + cartSub[i];
        }

        Console.WriteLine("Grand Total: P" + grand);

        if (grand >= 5000)
        {
            double discount = grand * 0.10;
            double finalTotal = grand - discount;
            Console.WriteLine("Discount (10%): -P" + discount);
            Console.WriteLine("Final Total: P" + finalTotal);
        }
        else
        {
            Console.WriteLine("Final Total: P" + grand);
        }

        Console.WriteLine("\n--- UPDATED STOCK ---");
        for (int i = 0; i < menu.Length; i++)
        {
            menu[i].Display();
        }

        Console.WriteLine("\nThank you for shopping!");
        Console.ReadKey();
    }
}