Shopping cart Activity

Plaza, Jan Vincent Roi C. BSIT 1-2

Ai usage

I used AI to help build the Product class design, the cart logic using parallel arrays, and the duplicate item detection I used AI to get a working starting point and to understand Some parts like handling duplicate cart entries and validating multiple inputs at once were tricky to figure out from scratch, so I asked AI to explain the logic before writing the code.

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

  Enter product name to search: key
  Result: [3] Keyboard (Electronics) - P1200 | Stock: 15

4. Product Categories
A `Category` field was added to the `Product` class. Users can filter the product list by category:

  --- CATEGORIES ---
  [1] Electronics
  Choose a category number: 1

6. Low Stock Alert
After every checkout, products with 5 or fewer units remaining are flagged:

  --- LOW STOCK ALERT ---
  ⚠  Laptop has only 2 stock(s) left!
  ⚠  Mouse has only 4 stock(s) left!

8. Payment Validation & Change Computation
  The system validates payment before completing the transaction:
- Payment must be a valid number
- Payment must be greater than or equal to the final total
- Re-prompts until a sufficient amount is entered
- Computes and displays exact change

  Final Total: P5200
  Enter payment: P5000
  Insufficient payment. Need P200.00 more.
  Enter payment: P6000
  Change: P800

6. Receipt Number and Date/Time
Each completed order generates a unique, formatted receipt:
- Auto-incrementing receipt number (e.g. 0001, 0002)
- Exact checkout date and time
- Itemized list of purchased products
- Grand total, discount (if applicable), final total, payment, and change

7. Order History
Completed transactions are stored during the program run and viewable from the main menu:

  ========== ORDER HISTORY ==========
  Receipt #0001 | May 01, 2026 9:00 AM | Final Total: P40500
  Receipt #0002 | May 01, 2026 9:15 AM | Final Total: P1800

9. Strict Y/N Input Validation
All yes/no prompts re-prompt until exactly Y or N is entered:

  Add another item? (Y/N): maybe
  Invalid input. Please enter Y or N only.
  Add another item? (Y/N):
