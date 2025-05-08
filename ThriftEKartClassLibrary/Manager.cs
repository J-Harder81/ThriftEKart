using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThriftEKartClassLibrary
{
    public class Manager
    {
        public static void ValidateManager()
        {
            string username = "THRIFTEKART.ADMIN";
            string password = "Thr!ft3K@rt4Us";

            bool isValidated = false;
            while (!isValidated)
            {
                Console.WriteLine("Please enter login credentials to continue:");
                Console.Write("User Name: ");
                string inputUsername = Console.ReadLine().ToUpper();
                Console.Write("Password: ");
                string inputPassword = "";
                ConsoleKeyInfo key;
                do
                {
                    key = Console.ReadKey(true);
                    if (key.Key != ConsoleKey.Enter)
                    {
                        inputPassword += key.KeyChar;
                        Console.Write("*");
                    }
                } while (key.Key != ConsoleKey.Enter);
                Console.WriteLine();

                if (inputUsername == username && inputPassword == password)
                {
                    isValidated = true;
                }
                else
                {
                    Console.WriteLine("\nInvalid credentials! Please try again.\n");
                }
            }
        }
        public static List<Product> DonatedProducts { get; set; } = new List<Product>();
        public static void AddDonatedProduct()
        {
            bool addMoreProducts = true;

            while (addMoreProducts)
            {
                Console.Clear();
                Product newProduct = Product.CreateProduct();
                DonatedProducts.Add(newProduct);
                Console.Write("\nDo you want to add another product: (Y/N)");
                string addProductResponse = Console.ReadLine().ToUpper();

                if (addProductResponse == "Y")
                {
                    addMoreProducts = true;
                }
                else if (addProductResponse == "N")
                {
                    addMoreProducts = false;
                }
                else
                {
                    Console.WriteLine("\nInvalid response! Please enter Y for YES or N for NO.");
                }
            }
            bool addDonation = false;

            while (!addDonation)
            {
                Console.Clear();
                Console.WriteLine("--------------------------------------- Donated Products List --------------------------------------");
                Console.WriteLine("----------------------------------------------------------------------------------------------------");
                Console.WriteLine($"{"Category",-20} {"Brand",-15} {"Description",-40} {"Size",-10} {"Price",-10}");
                Console.WriteLine("----------------------------------------------------------------------------------------------------");

                foreach (Product product in DonatedProducts)
                {
                    Console.WriteLine($"{product.ProductCategory,-20} {product.Brand,-15} {product.Description,-40} {product.Size,-10} {product.Price,-10:C}");
                }
                Console.WriteLine("----------------------------------------------------------------------------------------------------");


                Console.Write("\nAre you sure you want to add these donated products to the inventory? (Y/N): ");
                string addInventoryResponse = Console.ReadLine().ToUpper();
                if (addInventoryResponse == "Y")
                {
                    DatabaseHelper dbHelper = new DatabaseHelper();
                    foreach (Product product in DonatedProducts)
                    {
                        dbHelper.InsertDonatedProduct(product);
                    }
                    Console.WriteLine("\nAll donated products added successfully!");
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    addDonation = true;
                }
                else if (addInventoryResponse == "N")
                {
                    Console.WriteLine("Add donation cancelled!\nPress any key to return to the menu...");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    Console.WriteLine("\nInvalid response! Please enter Y for YES or N for NO.");
                }
            }
            DonatedProducts.Clear();
        }
    }
}
