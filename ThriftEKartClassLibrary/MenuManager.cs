using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThriftEKartClassLibrary
{
    public class MenuManager
    {
        public static void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine(" -------------------------- ");
                Console.WriteLine("| Welcome to Thrift-E-Kart |");
                Console.WriteLine(" -------------------------- ");
                Console.WriteLine();
                Console.WriteLine("Please select the number that corresponds with your role");
                Console.WriteLine();
                Console.WriteLine("1. Customer");
                Console.WriteLine("2. Consignor");
                Console.WriteLine("3. Manager");
                Console.WriteLine("4. Exit");
                Console.WriteLine();
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowCustomerStart();
                        break;
                    case "2":
                        ShowConsignorStart();
                        break;
                    case "3":
                        ShowManagerMenu();
                        break;
                    case "4":
                        ExitProgram();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        static void ShowCustomerStart()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(" --------------- ");
                Console.WriteLine("| Customer Menu |");
                Console.WriteLine(" --------------- ");
                Console.WriteLine();
                Console.WriteLine("1. New Customer");
                Console.WriteLine("2. Existing Customer");
                Console.WriteLine("3. Return to Main Menu");
                Console.WriteLine("4. Exit");
                Console.WriteLine();
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Customer.AddCustomer();
                        break;
                    case "2":
                        ShowCustomerMenu();
                        break;
                    case "3":
                        ShowMainMenu();
                        break;
                    case "4":
                        ExitProgram();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        public static void ShowCustomerMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(" ------------------ ");
                Console.WriteLine("| Welcome Customer |");
                Console.WriteLine(" ------------------ ");
                Console.WriteLine();
                Console.WriteLine("1. Browse Inventory");
                Console.WriteLine("2. View Cart");
                Console.WriteLine("3. Checkout");
                Console.WriteLine("4. Return to Previous Menu");
                Console.WriteLine("5. Return to Main Menu");
                Console.WriteLine("6. Exit");
                Console.WriteLine();
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        // Method call for viewing products
                        break;
                    case "2":
                        Console.Clear();
                        // Method call for viewing the customer cart
                        break;
                    case "3":
                        Console.Clear();
                        // Method call for checkout
                        break;
                    case "4":
                        ShowCustomerStart();
                        break;
                    case "5":
                        ShowMainMenu();
                        break;
                    case "6":
                        ExitProgram();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        static void ShowConsignorStart()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(" ---------------- ");
                Console.WriteLine("| Consignor Menu |");
                Console.WriteLine(" ---------------- ");
                Console.WriteLine();
                Console.WriteLine("1. New Consignor");
                Console.WriteLine("2. Existing Consignor");
                Console.WriteLine("3. Return to Main Menu");
                Console.WriteLine("4. Exit");
                Console.WriteLine();
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Consignor.AddConsignor();
                        break;
                    case "2":
                        ShowConsignorMenu();
                        break;
                    case "3":
                        ShowMainMenu();
                        break;
                    case "4":
                        ExitProgram();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        public static void ShowConsignorMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(" ------------------- ");
                Console.WriteLine("| Welcome Consignor |");
                Console.WriteLine(" ------------------- ");
                Console.WriteLine();
                Console.WriteLine("1. New Consignment");
                Console.WriteLine("2. View Consignment History");
                Console.WriteLine("3. View Sales");
                Console.WriteLine("4. Return to Previous Menu");
                Console.WriteLine("5. Return to Main Menu");
                Console.WriteLine("6. Exit");
                Console.WriteLine();
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Consignment.CreateConsignment();
                        break;
                    case "2":
                        Console.Clear();
                        // Method call for viewing previous consignments
                        break;
                    case "3":
                        Console.Clear();
                        // Method call for viewing sales
                        break;
                    case "4":
                        ShowConsignorStart();
                        break;
                    case "5":
                        ShowMainMenu();
                        break;
                    case "6":
                        ExitProgram();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        public static void ShowManagerMenu()
        {
            Console.Clear();
            Manager.ValidateManager();
            while (true)
            {
                Console.Clear();
                Console.WriteLine(" -------------- ");
                Console.WriteLine("| Manager Menu |");
                Console.WriteLine(" -------------- ");
                Console.WriteLine();
                Console.WriteLine("Inventory:");
                Console.WriteLine("    1. View Inventory");
                Console.WriteLine("    2. Add Donation");
                Console.WriteLine("    3. Add Consignment");
                Console.WriteLine("    4. Remove Product");
                Console.WriteLine("Customers:");
                Console.WriteLine("    5. View Customers");
                Console.WriteLine("    6. Add Customer");
                Console.WriteLine("    7. Remove Customer");
                Console.WriteLine("Consignments:");
                Console.WriteLine("    8. View Consignors");
                Console.WriteLine("    9. Add Consignor");
                Console.WriteLine("   10. Remove Consignor");
                Console.WriteLine("   11. View Consignment History");
                Console.WriteLine("Orders:");
                Console.WriteLine("   12. View Orders");
                Console.WriteLine("   13. Search Orders");
                Console.WriteLine("Sales:");
                Console.WriteLine("   14. View Sales");
                Console.WriteLine("   15. Search Sales");
                Console.WriteLine("Other Options:");
                Console.WriteLine("   16. Return to Main Menu");
                Console.WriteLine("   17. Exit");
                Console.WriteLine();
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        // Method call for viewing inventory
                        break;
                    case "2":
                        Console.Clear();
                        Manager.AddDonatedProduct();
                        break;
                    case "3":
                        Console.Clear();
                        // Add Consignment
                        break;
                    case "4": 
                        Console.Clear();
                        // Remove product
                        break;
                    case "5":
                        Console.Clear();
                        Customer.ViewCustomerList();
                        break;
                    case "6":
                        Console.Clear();
                        Customer.AddCustomer();
                        break;
                    case "7":
                        Console.Clear();
                        Customer.RemoveCustomer();
                        break;
                    case "8":
                        Console.Clear();
                        Consignor.ViewConsignorList();
                        break;
                    case "9":
                        Console.Clear();
                        Consignor.AddConsignor();
                        break;
                    case "10":
                        Console.Clear();
                        Consignor.RemoveConsignor();
                        break;
                    case "11":
                        Console.Clear();
                        // Method call for viewing consignment history
                        break;
                    case "12":
                        Console.Clear();
                        // Method call for viewing orders
                        break;
                    case "13":
                        Console.Clear();
                        // Method call for searching orders
                        break;
                    case "14":
                        Console.Clear();
                        // Method call for viewing sales
                        break;
                    case "15":
                        Console.Clear();
                        // Method call for searching sales
                        break;
                    case "16":
                        ShowMainMenu();
                        break;
                    case "17":
                        ExitProgram();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        static void ExitProgram()
        {
            Console.Write("Are you sure you want to exit the program? (Y/N): ");
            string programExit = Console.ReadLine().ToUpper();

            if (programExit == "Y")
            {
                Console.WriteLine("Exiting the program. Goodbye!");
                Environment.Exit(0);
            }
            else if (programExit == "N")
            {
                return;
            }
            else
            {
                Console.WriteLine("Invalid choice! Please try again.");
                Console.WriteLine("Type Y for YES or N for NO...");
                //Console.ReadKey();
                ExitProgram();
            }
        }
    }
}
