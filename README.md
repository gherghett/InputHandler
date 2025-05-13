
# InputHandler

This is a small utility I wrote to help manage command-line interfaces in school assignments. Making CLI menus can get messy, especially when trying to avoid spaghetti code. This helper class made it easier to separate functionality from navigation and keep things readable.

---

## What It Does


The core idea is tree-like navigation, where each menu is a node, and screens or submenus are children. When you call Enter(), the user is prompted to choose from the available options. The system handles the flow, including "back" navigation, without you writing repetitive logic.


The `MenuBuilder` class lets you define nested menus and screens in a structured way. It's useful for CLI programs that need multiple options and submenus.

Each menu can contain:

* Submenus (`AddMenu(...)`)
* Screens that run actions (`AddScreen(...)`)
* A back navigation via `Done()`
* A quit option (`AddQuit(...)`)

When you call `menu.Enter()`, it displays the options and handles user input automatically.

---

## Example

Here’s an example of how I used it in one of my assignments:

```csharp
// Example actions
void AddCustomer() => Console.WriteLine("Adding a new customer...");
void ViewCustomers() => Console.WriteLine("Showing all customers...");
void AddOrder() => Console.WriteLine("Adding a new order...");
void ViewOrders() => Console.WriteLine("Showing all orders...");

// Build the menu
var menu = MenuBuilder.CreateMenu("Main Menu")

    // First submenu: Customer Menu
    .AddMenu("Customers")
        .AddScreen("Add Customer", AddCustomer)
        .AddScreen("View Customers", ViewCustomers)
        .Done()  // Go back to Main Menu

    // Second submenu: Orders Menu (this is the nested one)
    .AddMenu("Orders")
        .AddScreen("Add Order", AddOrder)
        .AddScreen("View Orders", ViewOrders)
        .Done()  // Go back to Main Menu

    // Quit option
    .AddQuit("Exit");

// Start the menu loop
menu.Enter();
```

### 🔸 What It Looks Like at Runtime

```text

Main Menu
>Customers
 Orders
 Exit

Customers
>Back: Main Menu
 Add Customer
 View Customers

Main Menu
 Customers
>Orders
 Exit

Orders
 Back: Main Menu
 Add Order
>View Orders

Showing all orders...
```
User, using the arrow buttons and enter, selected first Customers, the went back to the Main Menu, then selected ORders, then View Orders.

---

## How It Works (Briefly)

* **MenuBuilder**: Represents a menu. Can have child menus or screens. Calling `.Enter()` on a menu displays its options and lets the user choose.
* **Screen**: A leaf node in the menu that runs one or more actions. Can optionally return to the previous menu afterward.
* **Done()**: Moves back up to the parent menu in the chain.

The menu is recursive and builds a tree of options. The user navigates it interactively at runtime.

---

## Why I Made It

I was working on a couple of CLI-heavy assignments and found that writing manual menu logic made the code hard to follow and test. This structure helped keep things organized while allowing me to focus on the actual features of the program.

