﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_TheTravelingSalesperson
{
    /// <summary>
    /// MVC View class
    /// </summary>
    public class ConsoleView
    {
        #region FIELDS

        private const int MAXIMUM_ATTEMPTS = 3;
        private const int MINIMUM_BUYSELL_AMOUNT = 0;
        private const int MAXIMUM_BUYSELL_AMOUNT = 100;

        #endregion

        #region PROPERTIES

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// default constructor to create the console view objects
        /// </summary>
        public ConsoleView()
        {
            InitializeConsole();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// initialize all console settings
        /// </summary>
        private void InitializeConsole()
        {
            ConsoleUtil.WindowTitle = "New Ocean LLC.";
            ConsoleUtil.HeaderText = "The Traveling Salesperson Application";
        }

        /// <summary>
        /// display the Continue prompt
        /// </summary>
        public void DisplayContinuePrompt()
        {
            Console.CursorVisible = false;

            ConsoleUtil.DisplayMessage("");

            ConsoleUtil.DisplayMessage("Press any key to continue.");
            ConsoleKeyInfo response = Console.ReadKey();

            ConsoleUtil.DisplayMessage("");

            Console.CursorVisible = true;
        }

        /// <summary>
        /// display the Exit prompt on a clean screen
        /// </summary>
        public void DisplayExitPrompt()
        {
            ConsoleUtil.DisplayReset();

            Console.CursorVisible = false;

            ConsoleUtil.DisplayMessage("");
            ConsoleUtil.DisplayMessage("Thank you for using the application. Press any key to Exit.");

            Console.ReadKey();

            System.Environment.Exit(1);
        }


        /// <summary>
        /// display the welcome screen
        /// </summary>
        public void DisplayWelcomeScreen()
        {
            StringBuilder sb = new StringBuilder();

            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Written by Jacob Barsheff");
            ConsoleUtil.DisplayMessage("Northwestern Michigan College");
            ConsoleUtil.DisplayMessage("");

            sb.Clear();
            sb.AppendFormat("Welcome! As a traveling Salesperson you will travel around the country");
            sb.AppendFormat("selling products. As you travel, you'll be asked to log your cites");
            sb.AppendFormat("and the products you sell or buy. Good luck!");
            ConsoleUtil.DisplayMessage(sb.ToString());
            ConsoleUtil.DisplayMessage("");

            sb.Clear();
            sb.AppendFormat("Task 1: Setup Account");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// setup the new salesperson object with the initial data
        /// Note: To maintain the pattern of only the Controller changing the data this method should
        ///       return a Salesperson object with the initial data to the controller. For simplicity in 
        ///       this demo, the ConsoleView object is allowed to access the Salesperson object's properties.
        /// </summary>
        public Salesperson DisplaySetupAccount(SalesLog salesLog, Product product)
        {
            Salesperson salesperson = new Salesperson();

            ConsoleUtil.HeaderText = "Account Setup";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Setup your account now.");
            ConsoleUtil.DisplayMessage("");

            ConsoleUtil.DisplayPromptMessage("Enter your first name: ");
            salesperson.FirstName = Console.ReadLine();
            ConsoleUtil.DisplayMessage("");

            ConsoleUtil.DisplayPromptMessage("Enter your last name: ");
            salesperson.LastName = Console.ReadLine();
            ConsoleUtil.DisplayMessage("");

            ConsoleUtil.DisplayPromptMessage("Enter your Age: ");
            int.TryParse(Console.ReadLine(), out int age);
            salesperson.Age = age;
            ConsoleUtil.DisplayMessage("");

            ConsoleUtil.DisplayPromptMessage("Enter your account ID: ");
            salesperson.AccountID = Console.ReadLine();
            ConsoleUtil.DisplayMessage("");

            ConsoleUtil.DisplayPromptMessage("Please Enter your Current City: ");
            salesperson.CurrentCity = Console.ReadLine();
            ConsoleUtil.DisplayMessage("");

            ConsoleUtil.DisplayPromptMessage("Do you have any prior sales experience, please type TRUE for yes: ");
            bool.TryParse(Console.ReadLine(), out bool experience);
            salesperson.HasPriorSalesExperience = experience;
            ConsoleUtil.DisplayMessage("");

            ConsoleUtil.DisplayMessage("Available Product Type For Sale: ");
            ConsoleUtil.DisplayMessage("");

            //
            // list all product types
            //
            foreach (string productTypeName in Enum.GetNames(typeof(Product.ProductType)))
            {
                //
                // do not display the "NONE" enum value
                //
                if (productTypeName != Product.ProductType.None.ToString())
                {
                    ConsoleUtil.DisplayMessage(productTypeName);
                }

            }

            //
            // get product type, if invalid entry, set type to "None"
            //
            ConsoleUtil.DisplayMessage("");
            ConsoleUtil.DisplayPromptMessage("Please choose a product to sell: ");
            Product.ProductType productType;
            if (Enum.TryParse<Product.ProductType>(UppercaseFirst(Console.ReadLine()), out productType))
            {
                salesperson.CurrentStock.Type = productType;
            }
            else
            {
                salesperson.CurrentStock.Type = Product.ProductType.None;
            }
            //
            // get number of products in inventory
            //
            if (ConsoleValidator.TryGetIntegerFromUser(0, 100, 3, "products", out int numberOfUnits))
            {
                salesperson.CurrentStock.AddProducts(numberOfUnits);
                salesLog._totalPurchase = salesLog._totalPurchase + numberOfUnits;
                salesLog.UnitsBought = numberOfUnits;
            }
            else
            {
                ConsoleUtil.DisplayMessage($"The Max Number of attempts ({MAXIMUM_ATTEMPTS}) to setting stock count has been exceeded.");
                ConsoleUtil.DisplayMessage("By default, the number of products in your inventory are now set to zero.");
                salesperson.CurrentStock.AddProducts(0);
                DisplayContinuePrompt();
            }

            ConsoleUtil.DisplayMessage("");

            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Your account is setup :)");

            DisplayContinuePrompt();

            return salesperson;
        }

        /// <summary>
        /// display a closing screen when the user quits the application
        /// </summary>
        public void DisplayClosingScreen()
        {
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Thank you for using The Traveling Salesperson Application.");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// get the menu choice from the user
        /// </summary>
        public MenuOption DisplayGetUserMenuChoice()
        {
            MenuOption userMenuChoice = MenuOption.None;
            bool usingMenu = true;

            while (usingMenu)
            {
                //
                // set up display area
                //
                
                ConsoleUtil.HeaderText = "Main Menu";
                ConsoleUtil.DisplayReset();
                Console.CursorVisible = false;

                //
                // display the menu
                //
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                ConsoleUtil.DisplayMessage("Please select a menu choice.");
                ConsoleUtil.DisplayMessage("");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(
                    "\t" + "A. Account Setup" + Environment.NewLine);
                    Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("\t" + "--------------------" + Environment.NewLine +
                    "\t" + "B. Travel" + Environment.NewLine +
                    "\t" + "C. Buy" + Environment.NewLine +
                    "\t" + "D. Sell" + Environment.NewLine);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\t" + "--------------------" + Environment.NewLine +
                    "\t" + "E. Display Inventory" + Environment.NewLine +
                    "\t" + "F. Display Cities" + Environment.NewLine +
                    "\t" + "G. Display Account Info" + Environment.NewLine);
                    Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("\t" + "--------------------" + Environment.NewLine +
                    "\t" + "H. Display Sales Log" + Environment.NewLine +
                    "\t" + "I. Display Purchase Log" + Environment.NewLine +
                    "\t" + "J. Change Product" + Environment.NewLine);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\t" + "--------------------" + Environment.NewLine +
                    "\t" + "K. Edit Account" + Environment.NewLine +
                    "\t" + "L. Save Account" + Environment.NewLine +
                    "\t" + "M. Load Account" + Environment.NewLine);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\t" + "--------------------" + Environment.NewLine +
                    "\t" + "Z. Exit" + Environment.NewLine);
                
                //
                // get and process the user's response
                // note: ReadKey argument set to "true" disables the echoing of the key press
                //
                ConsoleKeyInfo userResponse = Console.ReadKey(true);
                switch (userResponse.KeyChar)
                {
                    case 'A':
                    case 'a':
                        userMenuChoice = MenuOption.SetUpAccount;
                        usingMenu = false;
                        break;
                    case 'B':
                    case 'b':
                        userMenuChoice = MenuOption.Travel;
                        usingMenu = false;
                        break;
                    case 'C':
                    case 'c':
                        userMenuChoice = MenuOption.Buy;
                        usingMenu = false;
                        break;
                    case 'D':
                    case 'd':
                        userMenuChoice = MenuOption.Sell;
                        usingMenu = false;
                        break;
                    case 'E':
                    case 'e':
                        userMenuChoice = MenuOption.DisplayInventory;
                        usingMenu = false;
                        break;
                    case 'F':
                    case 'f':
                        userMenuChoice = MenuOption.DisplayCities;
                        usingMenu = false;
                        break;
                    case 'G':
                    case 'g':
                        userMenuChoice = MenuOption.DisplayAccountInfo;
                        usingMenu = false;
                        break;
                    case 'H':
                    case 'h':
                        userMenuChoice = MenuOption.DisplayStats;
                        usingMenu = false;
                        break;
                    case 'I':
                    case 'i':
                        userMenuChoice = MenuOption.DisplayPurchaseLog;
                        usingMenu = false;
                        break;
                    case 'J':
                    case 'j':
                        userMenuChoice = MenuOption.ChangeItem;
                        usingMenu = false;
                        break;
                    case 'K':
                    case 'k':
                        userMenuChoice = MenuOption.AccountEdit;
                        usingMenu = false;
                    break;
                    case 'L':
                    case 'l':
                        userMenuChoice = MenuOption.Save;
                        usingMenu = false;
                        break;
                    case 'M':
                    case 'm':
                        userMenuChoice = MenuOption.Load;
                        usingMenu = false;
                        break;
                    case 'Z':
                    case 'z':
                        userMenuChoice = MenuOption.Exit;
                        usingMenu = false;
                        break;
                    default:
                        ConsoleUtil.DisplayMessage(
                            "Invalid Selection." + Environment.NewLine +
                            "Press any key to continue or the ESC key to quit the application.");

                        userResponse = Console.ReadKey(true);
                        if (userResponse.Key == ConsoleKey.Escape)
                        {
                            usingMenu = false;
                        }
                        break;
                }
            }
            Console.CursorVisible = true;

            return userMenuChoice;
        }

        public string DisplayGetNextCity()
        {
            string nextCity = "";

            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayPromptMessage("Please enter the name of the next city:");
            nextCity = Console.ReadLine();

            return nextCity;
        }

        /// <summary>
        /// get the number of product units to buy from the user
        /// </summary>
        /// <returns>int number of units to buy</returns>
        public int DisplayGetNumberOfUnitsToBuy(Product product)
        {
            ConsoleUtil.HeaderText = "Buy Inventory";
            ConsoleUtil.DisplayReset();

            //
            // get number of products to buy
            //
            ConsoleUtil.DisplayMessage("Buying " + product.Type.ToString() + " products.");
            ConsoleUtil.DisplayMessage("");
            ConsoleUtil.DisplayMessage("Current Stock: " + product.NumberOfUnits.ToString() + " units.");
            ConsoleUtil.DisplayMessage("");
            if (!ConsoleValidator.TryGetIntegerFromUser(MINIMUM_BUYSELL_AMOUNT, MAXIMUM_BUYSELL_AMOUNT, MAXIMUM_ATTEMPTS, "products", out int numberOfUnitsToBuy))
            {
                ConsoleUtil.DisplayMessage("It appears you are having difficulty setting the number of products to buy.");
                ConsoleUtil.DisplayMessage("By default, the number of products to sell will be set to zero.");
                numberOfUnitsToBuy = 0;
                DisplayContinuePrompt();
            }
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage(numberOfUnitsToBuy + " " + product.Type.ToString() + " products have been added to the inventory.");

            DisplayContinuePrompt();

            return numberOfUnitsToBuy;
        }

        /// <summary>
        /// get the number of product units to sell from the user
        /// </summary>
        /// <returns>int number of units to buy</returns>
        public int DisplayGetNumberOfUnitsToSell(Product product)
        {
            ConsoleUtil.HeaderText = "Sell Inventory";
            ConsoleUtil.DisplayReset();

            //
            // get number of products to sell
            //
            
            ConsoleUtil.DisplayMessage("Selling " + product.Type.ToString() + " products.");
            ConsoleUtil.DisplayMessage("");
            ConsoleUtil.DisplayMessage("Current Stock: " + product.NumberOfUnits.ToString() + " units.");
            ConsoleUtil.DisplayMessage("");
            if (!ConsoleValidator.TryGetIntegerFromUser(MINIMUM_BUYSELL_AMOUNT, MAXIMUM_BUYSELL_AMOUNT, MAXIMUM_ATTEMPTS, "products", out int numberOfUnitsToSell))
            {
                ConsoleUtil.DisplayMessage("It appears you are having difficulty setting the number of products to sell.");
                ConsoleUtil.DisplayMessage("By default, the number of products to sell will be set to zero.");
                numberOfUnitsToSell = 0;
                DisplayContinuePrompt();
            }
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage(numberOfUnitsToSell + " " + product.Type.ToString() + " products have been subtracted from the inventory.");

            DisplayContinuePrompt();

            return numberOfUnitsToSell;
        }

        /// <summary>
        /// display backorder notification
        /// </summary>
        /// <param name="product"></param>
        public void DisplayBackorderNotification(Product product, int numberOfUnitsSold)
        {
            ConsoleUtil.HeaderText = "Inventory Backorder Notification";
            ConsoleUtil.HeaderBackgroundColor = ConsoleColor.Red;
            ConsoleUtil.DisplayReset();

            int numberOfUnitsBackordered = Math.Abs(product.NumberOfUnits);
            int numberOfUnitsShipped = numberOfUnitsSold - numberOfUnitsBackordered;

            Console.ForegroundColor = ConsoleColor.Red;

            ConsoleUtil.DisplayMessage("Products Sold: " + numberOfUnitsSold);
            ConsoleUtil.DisplayMessage("Products Shipped: " + numberOfUnitsShipped);
            ConsoleUtil.DisplayMessage("Products on Backorder: " + numberOfUnitsBackordered);

            DisplayContinuePrompt();
            ConsoleUtil.HeaderBackgroundColor = ConsoleColor.Gray;
        }

        /// <summary>
        /// display the current inventory
        /// </summary>
        public void DisplayInventory(Product product)
        {
            ConsoleUtil.HeaderText = "Current Inventory";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Product type: " + product.Type.ToString());
            ConsoleUtil.DisplayMessage("Number of units: " + product.NumberOfUnits.ToString());
            switch (product.Type)
            {
                case Product.ProductType.None:
                    break;
                case Product.ProductType.Samsung32:
                    product.Price = 149.99;
                    break;
                case Product.ProductType.Samsung48:
                    product.Price = 299.99;
                    break;
                case Product.ProductType.Samsung60:
                    product.Price = 399.99;
                    break;
                default:
                    break;
            }
            ConsoleUtil.DisplayMessage("Total Value: $" + product.Price * product.NumberOfUnits);
            ConsoleUtil.DisplayMessage("");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display a list of the cities traveled
        /// </summary>
        public void DisplayCitiesTraveled(Salesperson salesperson)
        {
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("You have traveled to the following cities.");
            ConsoleUtil.DisplayMessage("");

            foreach (string city in salesperson.CitiesVisited)
            {
                ConsoleUtil.DisplayMessage(city);
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display the current account information
        /// </summary>
        public void DisplayAccountInfo(Salesperson salesperson)
        {
            ConsoleUtil.HeaderText = "Account Info";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("First Name: " + salesperson.FirstName);
            ConsoleUtil.DisplayMessage("Last Name: " + salesperson.LastName);
            ConsoleUtil.DisplayMessage("Account ID: " + salesperson.AccountID);
            ConsoleUtil.DisplayMessage("Product Type: " + salesperson.CurrentStock.Type);
            if (!salesperson.CurrentStock.OnBackorder)
            {
                ConsoleUtil.DisplayMessage("Units of Products in Inventory: " + salesperson.CurrentStock.NumberOfUnits);
            }
            else
            {
                ConsoleUtil.DisplayMessage("Units of Products on Backorder: " + Math.Abs(salesperson.CurrentStock.NumberOfUnits));
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// changes string to lowercase with first letter uppercase
        /// adapted from: https://www.dotnetperls.com/uppercase-first-letter
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concatenation substring.
            return char.ToUpper(s[0]) + s.Substring(1).ToLower();
        }

        public void DisplayLog (List<string> _salesLogList, SalesLog salesLog)
        {
            ConsoleUtil.HeaderText = "Sales Log";
            ConsoleUtil.DisplayReset();
            Console.WriteLine("Product Type" + "   " + "Qty Sold" + "   " + "Time Sold" +"                  " + "City Sold");
            Console.WriteLine();
            int counter = 1;
            for (int index = 0; index < _salesLogList.Count; index++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(counter + _salesLogList[index]);
                counter++;
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("Total Sold....." + salesLog._totalSales.ToString());

            DisplayContinuePrompt();

        }
        public void DisplayPurchaseLog(List<string> _purchaseLogList, SalesLog salesLog)
        {
            ConsoleUtil.HeaderText = "Purchase Log";
            ConsoleUtil.DisplayReset();
            Console.WriteLine("Product Type" + "   " + "Qty Bought" + "    " + "Time Bought" + "                " + "City Bought");
            Console.WriteLine();
            int counter = 1;
            for (int index = 0; index < _purchaseLogList.Count; index++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(counter + _purchaseLogList[index]);
                counter++;
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("Total Bought....." + salesLog._totalPurchase.ToString());

            DisplayContinuePrompt();

        }

        public Salesperson ChangeItem(Salesperson salesperson, SalesLog salesLog)
        {
            ConsoleUtil.DisplayReset();
            ConsoleUtil.HeaderText = "Change Product";
            ConsoleUtil.DisplayReset();
            ConsoleUtil.DisplayMessage("Available Product Type For Sale:");
            ConsoleUtil.DisplayMessage("");

            //
            // list all product types
            //
            foreach (string productTypeName in Enum.GetNames(typeof(Product.ProductType)))
            {
                //
                // do not display the "NONE" enum value
                //
                if (productTypeName != Product.ProductType.None.ToString())
                {
                    ConsoleUtil.DisplayMessage(productTypeName);
                }

            }

            //
            // get product type, if invalid entry, set type to "None"
            //
            ConsoleUtil.DisplayMessage("");
            ConsoleUtil.DisplayPromptMessage("Please choose a product to sell: ");
            Product.ProductType productType;
            if (Enum.TryParse<Product.ProductType>(UppercaseFirst(Console.ReadLine()), out productType))
            {
                salesperson.CurrentStock.Type = productType;
            }
            else
            {
                salesperson.CurrentStock.Type = Product.ProductType.None;
            }

            //
            // get number of products in inventory
            //
            if (ConsoleValidator.TryGetIntegerFromUser(0, 100, 3, "products", out int numberOfUnits))
            {
                salesperson.CurrentStock.SubtractProducts(salesperson.CurrentStock.NumberOfUnits);
                salesperson.CurrentStock.AddProducts(numberOfUnits);
                salesLog._totalPurchase = salesLog._totalPurchase + numberOfUnits;
                salesLog.UnitsBought = numberOfUnits;
                
                
            }
            else
            {
                ConsoleUtil.DisplayMessage($"The Max Number of attempts ({MAXIMUM_ATTEMPTS}) to setting stock count has been exceeded.");
                ConsoleUtil.DisplayMessage("By default, the number of products in your inventory are now set to zero.");
                salesperson.CurrentStock.AddProducts(0);
                DisplayContinuePrompt();
            }

            ConsoleUtil.DisplayMessage("");

            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Product Change Successful");

            DisplayContinuePrompt();

            return salesperson;
        }

        public Salesperson DisplayEditAccount(Salesperson salesperson)
        {
            ConsoleUtil.HeaderText = "Edit Account";
            ConsoleUtil.DisplayReset();
            int menuChoiceNum = 1;
            bool exiting = false;

            do
            {
                Console.CursorVisible = false;

                ConsoleUtil.DisplayReset();
                
                switch (menuChoiceNum.ToString())
                {
                    case "1":
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("> 1. First Name (" + salesperson.FirstName + ")");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine("2. Last Name");
                        Console.WriteLine("3. AccountID");
                        Console.WriteLine();
                        Console.WriteLine("Change First Name?");
                        break;
                    case "2":
                        Console.WriteLine("1. First Name");
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("> 2. Last Name (" + salesperson.LastName + ")");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine("3. AccountID");
                        Console.WriteLine();
                        Console.WriteLine("Change Last Name?");
                        break;
                    case "3":
                        Console.WriteLine("1. First Name");
                        Console.WriteLine("2. Last Name");
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("> 3. AccountID (" + salesperson.AccountID + ")");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine();
                        Console.WriteLine("Change AccountID?");
                        break;
                    default:
                        break;
                }
                ConsoleUtil.DisplayConfirmPropt();
                ConsoleKeyInfo keyinfo = Console.ReadKey();
                if (keyinfo.Key == ConsoleKey.DownArrow)
                {
                    if (menuChoiceNum < 3)
                    {
                        menuChoiceNum = menuChoiceNum + 1;
                    }
                    else
                    {
                        menuChoiceNum = 1;
                    }

                }
                else if (keyinfo.Key == ConsoleKey.Enter)
                {
                    exiting = true;
                }
                else if (keyinfo.Key == ConsoleKey.UpArrow)
                {
                    if (menuChoiceNum == 1)
                    {
                        menuChoiceNum = 3;
                    }
                    else
                    {
                        menuChoiceNum = menuChoiceNum - 1;
                    }

                }
                else if (keyinfo.Key == ConsoleKey.D1)
                {
                    menuChoiceNum = 1;
                }
                else if (keyinfo.Key == ConsoleKey.D2)
                {
                    menuChoiceNum = 2;
                }
                else if (keyinfo.Key == ConsoleKey.D3)
                {
                    menuChoiceNum = 3;
                }

            } while (!exiting);
            Console.CursorVisible = true;

            switch (menuChoiceNum)
            {
                case 1:
                    ConsoleUtil.DisplayReset();
                    ConsoleUtil.DisplayPromptMessage("What would you like to change your first name to?");
                    string firstName = Console.ReadLine();
                    if (firstName == "")
                    {
                        Console.WriteLine("No input detected");
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                    }
                    else
                    {
                        salesperson.FirstName = firstName;
                    }  
                    
                    break;
                case 2:
                    ConsoleUtil.DisplayReset();
                    ConsoleUtil.DisplayPromptMessage("What would you like to change your last name to?");
                    string lastName = Console.ReadLine();
                    if (lastName == "")
                    {
                        Console.WriteLine("No input detected");
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                    }
                    else
                    {
                        salesperson.LastName = lastName;
                    }
                    break;
                case 3:
                    ConsoleUtil.DisplayReset();
                    ConsoleUtil.DisplayPromptMessage("What would you like to change your AccountID to?");
                    string accountID = Console.ReadLine();
                    if (accountID == "")
                    {
                        Console.WriteLine("No input detected");
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                    }
                    else
                    {
                        salesperson.AccountID = accountID;
                    }
                    break;

                default:
                    break;
            }
            return salesperson;

        }
        public bool DisplayConfirmSave()
        {   
            ConsoleUtil.DisplayReset();
            ConsoleUtil.DisplayPromptMessage("Are you sure you would like to save?" +
                " Press ENTER to Confirm, or any other key to go back.");
            bool yesToSave = false;
            ConsoleKeyInfo keyinfo = Console.ReadKey();
            if (keyinfo.Key == ConsoleKey.Enter)
            {
                yesToSave = true;
            }
            else
            {
                yesToSave = false;
            }
            return yesToSave;
        }
        public bool DisplayConfirmLoad()
        {
            ConsoleUtil.DisplayReset();
            ConsoleUtil.DisplayPromptMessage("Are you sure you would like to Load?" +
                " Press ENTER to Confirm, or any other key to go back.");
            bool yesToSave = false;
            ConsoleKeyInfo keyinfo = Console.ReadKey();
            if (keyinfo.Key == ConsoleKey.Enter)
            {
                yesToSave = true;
            }
            else
            {
                yesToSave = false;
            }
            return yesToSave;
        }
        public void DisplayConfirmNo()
        {
            ConsoleUtil.DisplayReset();
            ConsoleUtil.DisplayMessage("Process not executed");
            ConsoleUtil.DisplayMessage("Press any key to continue");
            Console.ReadKey();
        }
        #endregion
    }

}

