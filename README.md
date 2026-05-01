Shopping cart Activity

Plaza, Jan Vincent Roi C. BSIT 1-2

Ai usage

I used AI to help build the Product class design, the cart logic using parallel arrays, and the duplicate item detection I used AI to get a working starting point and to understand Some parts like handling duplicate cart entries and validating multiple inputs at once were tricky to figure out from scratch, so I asked AI to explain the logic before writing the code.

What AI helped with for part 2:
- *CartManager* - AI helped structure the cart menu (view, update, remove, clear) into its own dedicated class after I had the base cart logic working
- *ReceiptPrinter* — AI helped format the receipt with the auto-incrementing receipt number and date/time output
- *OrderHistory* — AI suggested and helped implement storing completed transactions in an array for later viewing

Part 2 — Enhanced Features

1. Cart Management Menu
Users can now fully manage their cart before checking out:
- *View Cart* — see all items, quantities, and subtotals
- *Update Item Quantity* — change qty or set to 0 to remove
- *Remove Item* — remove a specific item and restore its stock
- *Clear Cart* — empty the entire cart and restore all stock
- *Checkout* — proceed to payment and receipt generation

2. Product Search
Users can search for products by name (case-insensitive, partial match):

4. Product Categories
A `Category` field was added to the `Product` class. Users can filter the product list by category:

6. Low Stock Alert
After every checkout, products with 5 or fewer units remaining are flagged:

8. Payment Validation & Change Computation
  The system validates payment before completing the transaction:
- Payment must be a valid number
- Payment must be greater than or equal to the final total
- Re-prompts until a sufficient amount is entered
- Computes and displays exact change

6. Receipt Number and Date/Time
Each completed order generates a unique, formatted receipt:
- Auto-incrementing receipt number (e.g. 0001, 0002)
- Exact checkout date and time
- Itemized list of purchased products
- Grand total, discount (if applicable), final total, payment, and change

7. Order History
Completed transactions are stored during the program run and viewable from the main menu:

9. Strict Y/N Input Validation
All yes/no prompts re-prompt until exactly Y or N is entered:
