using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ThriftEKartClassLibrary
{
    public class Consignment
    {
        public int ConsignmentID { get; set; } = 0;
        public string ConsignmentNumber { get; set; } = string.Empty;
        public int ConsignorID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Consignment(int consignorid, DateTime startDate, DateTime endDate)
        {
            ConsignorID = consignorid;
            StartDate = startDate;
            EndDate = endDate;
        }
        public static List<Product> ConsignmentList { get; set; } = new List<Product>();
        public static void CreateConsignment()
        {
            Console.WriteLine(" ------------------------ ");
            Console.WriteLine("| Create New Consignment |");
            Console.WriteLine(" ------------------------ ");
            Console.WriteLine();
            string consignorNumber = ValidationHelper.GetValidatedInput("Enter your Consignor Number (Example: CONS-11):",
                ValidationHelper.IsValidConsignorNumber, "Invalid consignor number! The consignor number must be in the correct format: CONS-##.");

            DatabaseHelper dbHelper = new DatabaseHelper();
            string consignorName = dbHelper.DoesConsignorExist(consignorNumber);

            if (!string.IsNullOrEmpty(consignorName))
            {
                bool nameConfirmed = false;

                while (!nameConfirmed)
                {
                    Console.WriteLine($"\nThe name associated with consignor number '{consignorNumber}' is {consignorName}.");
                    Console.Write($"Is this information correct? (Y/N): ");
                    string confirmName = Console.ReadLine().ToUpper();

                    if (confirmName == "Y")
                    {
                        Console.WriteLine($"\nWelcome {consignorName}");
                        Console.WriteLine("Please press any key to continue...");
                        Console.ReadKey();
                        nameConfirmed = true;
                    }
                    else if (confirmName == "N")
                    {
                        Console.WriteLine("\nIf you feel there has been an error, please contact customer service.");
                        Console.WriteLine("Press any key to return to the menu...");
                        Console.ReadKey();
                        return;
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid response! Please enter Y for YES or N for NO.");
                    }
                }
            }
            else
            {
                Console.WriteLine($"\nNo consignor with consignor number {consignorNumber} found!" +
                    $"\nPress any key to return to the menu...");
                Console.ReadKey();
                return;
            }
            DisplayAgreement();
            
            bool addItems = true;

            while (addItems)
            {
                Console.Clear();
                Product newProduct = Product.CreateProduct();
                ConsignmentList.Add(newProduct);
                Console.Write("\nDo you want to add another item? (Y/N): ");
                string addItemResponse = Console.ReadLine().ToUpper();

                if (addItemResponse == "Y")
                {
                    addItems = true;
                }
                else if (addItemResponse == "N")
                {
                    addItems = false;
                }
                else
                {
                    Console.WriteLine("\nInvalid response!Please enter Y for YES or N for NO.");
                }
            }
            bool addConsignment = false;

            while (!addConsignment)
            {
                Console.Clear();
                Console.WriteLine("--------------------------------------- Consignment Item List --------------------------------------");
                Console.WriteLine("----------------------------------------------------------------------------------------------------");
                Console.WriteLine($"{"Category",-20} {"Brand",-15} {"Description",-40} {"Size",-10} {"Price",-10}");
                Console.WriteLine("----------------------------------------------------------------------------------------------------");

                foreach (Product product in ConsignmentList)
                {
                    Console.WriteLine($"{product.ProductCategory,-20} {product.Brand,-15} {product.Description,-40} {product.Size,-10} {product.Price,-10:C}");
                }
                Console.WriteLine("----------------------------------------------------------------------------------------------------");


                Console.Write("\nAre you sure you want to consign the above listed items? (Y/N): ");
                string consignItemsResponse = Console.ReadLine().ToUpper();
                if (consignItemsResponse == "Y")
                {
                    int consignorID = dbHelper.GetConsignorID(consignorNumber);
                    DateTime startDate = DateTime.Now;
                    DateTime endDate = startDate.AddDays(90);
                    Consignment newConsignment = new Consignment(consignorID, startDate, endDate);
                    (int consignmentID, string consignmentNumber) = dbHelper.InsertConsignment(newConsignment);
                    foreach (Product product in ConsignmentList)
                    {
                        product.ConsignmentID = consignmentID;
                        dbHelper.InsertConsignedProduct(product);
                    }
                    Console.WriteLine($"\nConsignment created successfully with the following details:");
                    Console.WriteLine($"    Consignment Number: {consignmentNumber}");
                    Console.WriteLine($"    Start Date: {startDate}");
                    Console.WriteLine($"    End Date: {endDate}");
                    Console.WriteLine("Please save this information for your reference!");
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    addConsignment = true;
                }
                else if (consignItemsResponse == "N")
                {
                    Console.WriteLine("Consignment cancelled!\nPress any key to return to the menu...");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    Console.WriteLine("\nInvalid response! Please enter Y for YES or N for NO.");
                }
            }
            ConsignmentList.Clear();
        }
        public static void DisplayAgreement()
        {
            Console.Clear();
            Console.WriteLine(" ----------------------- ");
            Console.WriteLine("| Consignment Agreement |");
            Console.WriteLine(" ----------------------- ");
            Console.WriteLine();
            Console.WriteLine("I acknowledge that by entering into this agreement, I hereby");
            Console.WriteLine("commission Thrift-E-Kart to sell the items listed under this consignment.");
            Console.WriteLine("The consignment period shall last for a duration of 90 days, during which");
            Console.WriteLine("Thrift-E-Kart will make reasonable efforts to sell the consigned items.");
            Console.WriteLine("Thrift-E-Kart will retain a commission rate of 40% of the final sale price");
            Console.WriteLine("for each item sold as compensation for its services");
            Console.WriteLine("\nI further acknowledge that upon the expiration of the consignment");
            Console.WriteLine("period, I will have a grace period of 14 days to retrieve any unsold items.");
            Console.WriteLine("Failure to collect these items within the specified grace period will result");
            Console.WriteLine("in the items becoming property of Thrift-E-Kart, where they may be");
            Console.WriteLine("donated, sold, or otherwise disposed of at Thrift-E-Kart's discretion.");
            Console.WriteLine("\nI affirm that all items consigned are legally owned by me, are free from");
            Console.WriteLine("liens, and comply with all applicable laws. I release Thrift-E-Kart from");
            Console.WriteLine("liability in the event of loss, damage, or theft of consigned items during");
            Console.WriteLine("the consignment period.");
            Console.WriteLine("\n-- Consent --");
            
            bool isAgreed = false;
            while (!isAgreed)
            {
                Console.Write("Do you agree to the above terms and conditions? (Y/N): ");
                string agreedResponse = Console.ReadLine().ToUpper();

                if (agreedResponse == "Y")
                {
                    Console.WriteLine("\nThank you for agreeing to the terms and conditions!");
                    Console.WriteLine("Press any key to begin adding your items...");
                    Console.ReadKey();
                    isAgreed = true;
                }
                else if (agreedResponse == "N")
                {
                    Console.WriteLine("\nYou must agree to the terms and conditions in order to continue!");
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    MenuManager.ShowConsignorMenu();
                    return;
                }
                else
                {
                    Console.WriteLine("\nInvalid response! Please enter Y for YES or N for NO.");
                }
            }
        }
    }    
}
