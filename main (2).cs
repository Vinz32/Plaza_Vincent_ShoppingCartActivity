using System;

class Product
{
    private int id;
    private string name;
    private string category;
    private double price;
    private int stock;

    public int GetId()
    {
        return id;
    }

    public string GetName()
    {
        return name;
    }

    public string GetCategory()
    {
        return category;
    }

    public double GetPrice()
    {
        return price;
    }

    public int GetStock()
    {
        return stock;
    }

    public Product(int id, string name, string category, double price, int stock)
    {
        this.id = id;
        this.name = name;
        this.category = category;
        this.price = price;
        this.stock = stock;
    }

    public void Display()
    {
        Console.WriteLine("[" + id + "] " + name + " (" + category + ") - P" + price + " | Stock: " + stock);
    }

    public double GetTotal(int qty)
    {
        return price * qty;
    }

    public bool HasStock(int qty)
    {
        return stock >= qty;
    }

    public void Deduct(int qty)
    {
        stock = stock - qty;
    }

    public void Restore(int qty)
    {
        stock = stock + qty;
    }
}

class OrderRecord
{
    private int receiptNumber;
    private string dateTime;
    private double finalTotal;

    public int GetReceiptNumber()
    {
        return receiptNumber;
    }

    public string GetDateTime()
    {
        return dateTime;
    }

    public double GetFinalTotal()
    {
        return finalTotal;
    }

    public OrderRecord(int receiptNumber, string dateTime, double finalTotal)
    {
        this.receiptNumber = receiptNumber;
        this.dateTime = dateTime;
        this.finalTotal = finalTotal;
    }
}

class Program
{
    static Product[] menu = new Product[5];

    static int[] cartId = new int[10];
    static int[] cartQty = new int[10];
    static double[] cartSub = new double[10];
    static int cartCount = 0;

    static OrderRecord[] orderHistory = new OrderRecord[100];
    static int orderCount = 0;

    static int receiptCounter = 0;

    static void Main()
    {
        menu[0] = new Product(1, "Laptop",   "Electronics", 45000, 5);
        menu[1] = new Product(2, "Mouse",    "Electronics",   500, 20);
        menu[2] = new Product(3, "Keyboard", "Electronics",  1200, 15);
        menu[3] = new Product(4, "Monitor",  "Electronics", 12000,  8);
        menu[4] = new Product(5, "USB Hub",  "Electronics",   800, 10);

        Console.WriteLine("=== WELCOME TO THE STORE ===");

        bool running = true;
        while (running)
        {
            Console.WriteLine("\n========== MAIN MENU ==========");
            Console.WriteLine("[1] Browse & Add Items");
            Console.WriteLine("[2] Search Product by Name");
            Console.WriteLine("[3] Filter by Category");
            Console.WriteLine("[4] Manage Cart");
            Console.WriteLine("[5] View Order History");
            Console.WriteLine("[6] Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    BrowseAndAdd();
                    break;
                case "2":
                    SearchProduct();
                    break;
                case "3":
                    FilterByCategory();
                    break;
                case "4":
                    CartMenu();
                    break;
                case "5":
                    ViewOrderHistory();
                    break;
                case "6":
                    running = false;
                    Console.WriteLine("\nThank you for visiting! Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid option. Please enter 1-6.");
                    break;
            }
        }

        Console.ReadKey();
    }

    static void BrowseAndAdd()
    {
        string answer = "Y";

        while (answer == "Y")
        {
            Console.WriteLine("\n--- PRODUCT LIST ---");
            for (int i = 0; i < menu.Length; i++)
            {
                menu[i].Display();
            }

            Console.Write("\nEnter product ID (0 to go back): ");
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

            Product selected = FindProduct(id);
            if (selected == null)
            {
                Console.WriteLine("Product not found.");
                continue;
            }

            if (selected.GetStock() == 0)
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
                Console.WriteLine("Not enough stock. Only " + selected.GetStock() + " left.");
                continue;
            }

            AddToCart(selected, qty);

            answer = AskYesNo("Add another item? (Y/N): ");
        }
    }

    static void AddToCart(Product selected, int qty)
    {
        // Check if the product is already in the cart
        for (int i = 0; i < cartCount; i++)
        {
            if (cartId[i] == selected.GetId())
            {
                cartQty[i] = cartQty[i] + qty;
                cartSub[i] = cartSub[i] + selected.GetTotal(qty);
                selected.Deduct(qty);
                Console.WriteLine("Updated cart: " + selected.GetName() + " x" + cartQty[i] + " = P" + cartSub[i]);
                return;
            }
        }

        if (cartCount >= 10)
        {
            Console.WriteLine("Cart is full (max 10 items)!");
            return;
        }

        // Add as a new item in the cart
        cartId[cartCount] = selected.GetId();
        cartQty[cartCount] = qty;
        cartSub[cartCount] = selected.GetTotal(qty);
        selected.Deduct(qty);
        Console.WriteLine("Added to cart: " + selected.GetName() + " x" + qty + " = P" + cartSub[cartCount]);
        cartCount = cartCount + 1;
    }

    static void SearchProduct()
    {
        Console.Write("\nEnter product name to search: ");
        string keyword = Console.ReadLine().ToLower();

        bool found = false;
        Console.WriteLine("\n--- SEARCH RESULTS ---");

        for (int i = 0; i < menu.Length; i++)
        {
            if (menu[i].GetName().ToLower().Contains(keyword))
            {
                menu[i].Display();
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("No products found matching \"" + keyword + "\".");
        }
    }

    static void FilterByCategory()
    {
        string[] cats = new string[10];
        int catCount = 0;

        for (int i = 0; i < menu.Length; i++)
        {
            bool exists = false;
            for (int j = 0; j < catCount; j++)
            {
                if (cats[j] == menu[i].GetCategory())
                {
                    exists = true;
                    break;
                }
            }
            if (!exists)
            {
                cats[catCount] = menu[i].GetCategory();
                catCount = catCount + 1;
            }
        }

        Console.WriteLine("\n--- CATEGORIES ---");
        for (int i = 0; i < catCount; i++)
        {
            Console.WriteLine("[" + (i + 1) + "] " + cats[i]);
        }

        Console.Write("Choose a category: ");
        string input = Console.ReadLine();
        int choice;

        if (!int.TryParse(input, out choice) || choice < 1 || choice > catCount)
        {
            Console.WriteLine("Invalid choice.");
            return;
        }

        string selectedCat = cats[choice - 1];
        Console.WriteLine("\n--- PRODUCTS IN: " + selectedCat + " ---");

        for (int i = 0; i < menu.Length; i++)
        {
            if (menu[i].GetCategory() == selectedCat)
            {
                menu[i].Display();
            }
        }
    }

    static void CartMenu()
    {
        bool inCart = true;

        while (inCart)
        {
            Console.WriteLine("\n========== CART MENU ==========");
            Console.WriteLine("[1] View Cart");
            Console.WriteLine("[2] Update Item Quantity");
            Console.WriteLine("[3] Remove Item");
            Console.WriteLine("[4] Clear Cart");
            Console.WriteLine("[5] Checkout");
            Console.WriteLine("[6] Back to Main Menu");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ViewCart();
                    break;
                case "2":
                    UpdateCartItem();
                    break;
                case "3":
                    RemoveCartItem();
                    break;
                case "4":
                    ClearCart();
                    break;
                case "5":
                    if (Checkout())
                    {
                        inCart = false;
                    }
                    break;
                case "6":
                    inCart = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please enter 1-6.");
                    break;
            }
        }
    }

    static void ViewCart()
    {
        if (cartCount == 0)
        {
            Console.WriteLine("\nYour cart is empty.");
            return;
        }

        Console.WriteLine("\n--- YOUR CART ---");
        double total = 0;

        for (int i = 0; i < cartCount; i++)
        {
            Product p = FindProduct(cartId[i]);
            Console.WriteLine("[" + (i + 1) + "] " + p.GetName() + " x" + cartQty[i] + " = P" + cartSub[i]);
            total = total + cartSub[i];
        }

        Console.WriteLine("─────────────────");
        Console.WriteLine("Subtotal: P" + total);
    }

    static void UpdateCartItem()
    {
        ViewCart();
        if (cartCount == 0)
        {
            return;
        }

        Console.Write("\nEnter cart item number to update: ");
        string input = Console.ReadLine();
        int index;

        if (!int.TryParse(input, out index) || index < 1 || index > cartCount)
        {
            Console.WriteLine("Invalid cart item.");
            return;
        }

        index = index - 1;

        Product p = FindProduct(cartId[index]);
        Console.WriteLine("Current quantity: " + cartQty[index]);
        Console.Write("Enter new quantity (0 to remove): ");

        string qInput = Console.ReadLine();
        int newQty;

        if (!int.TryParse(qInput, out newQty) || newQty < 0)
        {
            Console.WriteLine("Invalid quantity.");
            return;
        }

        if (newQty == 0)
        {
            RemoveAtIndex(index);
            return;
        }

        int diff = newQty - cartQty[index];

        if (diff > 0 && !p.HasStock(diff))
        {
            Console.WriteLine("Not enough stock. Only " + p.GetStock() + " available.");
            return;
        }

        if (diff > 0)
        {
            p.Deduct(diff);
        }
        else
        {
            p.Restore(-diff);
        }

        cartQty[index] = newQty;
        cartSub[index] = p.GetPrice() * newQty;
        Console.WriteLine("Updated: " + p.GetName() + " x" + cartQty[index] + " = P" + cartSub[index]);
    }

    static void RemoveCartItem()
    {
        ViewCart();
        if (cartCount == 0)
        {
            return;
        }

        Console.Write("\nEnter cart item number to remove: ");
        string input = Console.ReadLine();
        int index;

        if (!int.TryParse(input, out index) || index < 1 || index > cartCount)
        {
            Console.WriteLine("Invalid cart item.");
            return;
        }

        index = index - 1;
        RemoveAtIndex(index);
    }

    static void RemoveAtIndex(int index)
    {
        Product p = FindProduct(cartId[index]);
        p.Restore(cartQty[index]);
        Console.WriteLine("Removed: " + p.GetName() + " from cart.");

        for (int i = index; i < cartCount - 1; i++)
        {
            cartId[i] = cartId[i + 1];
            cartQty[i] = cartQty[i + 1];
            cartSub[i] = cartSub[i + 1];
        }

        cartCount = cartCount - 1;
    }

    static void ClearCart()
    {
        if (cartCount == 0)
        {
            Console.WriteLine("\nCart is already empty.");
            return;
        }

        string confirm = AskYesNo("Are you sure you want to clear the cart? (Y/N): ");

        if (confirm == "Y")
        {
            for (int i = 0; i < cartCount; i++)
            {
                Product p = FindProduct(cartId[i]);
                p.Restore(cartQty[i]);
            }

            cartCount = 0;
            Console.WriteLine("Cart cleared.");
        }
    }

    static bool Checkout()
    {
        if (cartCount == 0)
        {
            Console.WriteLine("\nYour cart is empty. Nothing to checkout.");
            return false;
        }

        double grand = 0;
        for (int i = 0; i < cartCount; i++)
        {
            grand = grand + cartSub[i];
        }

        double discount = 0;
        double finalTotal = grand;

        if (grand >= 5000)
        {
            discount = grand * 0.10;
            finalTotal = grand - discount;
        }

        receiptCounter = receiptCounter + 1;
        string receiptNo = receiptCounter.ToString("D4");
        string now = DateTime.Now.ToString("MMMM dd, yyyy h:mm tt");

        Console.WriteLine("\n========== RECEIPT ==========");
        Console.WriteLine("Receipt No : " + receiptNo);
        Console.WriteLine("Date/Time  : " + now);
        Console.WriteLine("─────────────────────────────");

        for (int i = 0; i < cartCount; i++)
        {
            Product p = FindProduct(cartId[i]);
            Console.WriteLine(p.GetName() + " x" + cartQty[i] + " = P" + cartSub[i]);
        }

        Console.WriteLine("─────────────────────────────");
        Console.WriteLine("Grand Total : P" + grand);

        if (discount > 0)
        {
            Console.WriteLine("Discount (10%): -P" + discount);
            Console.WriteLine("Final Total : P" + finalTotal);
        }
        else
        {
            Console.WriteLine("Final Total : P" + finalTotal);
        }

        double payment = 0;
        while (true)
        {
            Console.Write("\nEnter payment amount: P");
            string pInput = Console.ReadLine();

            if (!double.TryParse(pInput, out payment) || payment <= 0)
            {
                Console.WriteLine("Invalid input. Please enter a valid amount.");
                continue;
            }

            if (payment < finalTotal)
            {
                Console.WriteLine("Insufficient payment. You still need P" + (finalTotal - payment) + " more.");
                continue;
            }

            break;
        }

        double change = payment - finalTotal;
        Console.WriteLine("Payment     : P" + payment);
        Console.WriteLine("Change      : P" + change);
        Console.WriteLine("==============================");
        Console.WriteLine("       THANK YOU!             ");
        Console.WriteLine("==============================");

        orderHistory[orderCount] = new OrderRecord(receiptCounter, now, finalTotal);
        orderCount = orderCount + 1;

        ShowLowStockAlert();

        cartCount = 0;
        return true;
    }

    static void ShowLowStockAlert()
    {
        bool hasLow = false;
        Console.WriteLine("\n--- LOW STOCK ALERT ---");

        for (int i = 0; i < menu.Length; i++)
        {
            if (menu[i].GetStock() <= 5)
            {
                Console.WriteLine("⚠  " + menu[i].GetName() + " has only " + menu[i].GetStock() + " stock(s) left!");
                hasLow = true;
            }
        }

        if (!hasLow)
        {
            Console.WriteLine("All products have sufficient stock.");
        }
    }

    static void ViewOrderHistory()
    {
        if (orderCount == 0)
        {
            Console.WriteLine("\nNo orders yet.");
            return;
        }

        Console.WriteLine("\n========== ORDER HISTORY ==========");

        for (int i = 0; i < orderCount; i++)
        {
            OrderRecord o = orderHistory[i];
            Console.WriteLine("Receipt #" + o.GetReceiptNumber().ToString("D4") +
                              " | " + o.GetDateTime() +
                              " | Final Total: P" + o.GetFinalTotal());
        }
    }

    static Product FindProduct(int id)
    {
        for (int i = 0; i < menu.Length; i++)
        {
            if (menu[i].GetId() == id)
            {
                return menu[i];
            }
        }
        return null;
    }

    static string AskYesNo(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine().ToUpper().Trim();

            if (input == "Y" || input == "N")
            {
                return input;
            }

            Console.WriteLine("Invalid input. Please enter Y or N only.");
        }
    }
}