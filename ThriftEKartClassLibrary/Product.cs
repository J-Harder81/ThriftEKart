using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThriftEKartClassLibrary
{
    public class Product
    {
        public int ProductID { get; set; } = 0;
        public string ProductNumber { get; set; } = string.Empty;
        public int ConsignmentID { get; set; }
        public string ProductCategory { get; set; }
        public string CatAbbrev { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public Product (int consignmentID, string productCategory, string catAbbrev, string brand,
            string description, string size, decimal price)
        {
            ConsignmentID = consignmentID;
            ProductCategory = productCategory;
            CatAbbrev = catAbbrev;
            Brand = brand;
            Description = description;
            Size = size;
            Price = price;
        }
        public static Product CreateProduct()
        {
            Console.WriteLine(" ----------------- ");
            Console.WriteLine("| Add New Product |");
            Console.WriteLine(" ----------------- ");
            Console.WriteLine();
            Console.WriteLine("Please select the number that corresponds product's category:");
            Console.WriteLine("   1. Mens Apparel      2. Womens Apparel");
            Console.WriteLine("   3. Boys Apparel      4. Girls Apparel");
            Console.WriteLine("   5. Toys/Games        6. Electronics");
            Console.WriteLine("   7. Household Goods   8. Sporting Goods");
            Console.Write("Selection: ");

            int consignmentID = 0;
            string productCategory = "";
            string catAbbrev = "";
            while (string.IsNullOrEmpty(productCategory))
            {
                string selection = Console.ReadLine();
                switch (selection)
                {
                    case "1": productCategory = "MENS APPAREL"; catAbbrev = "MENS"; break;
                    case "2": productCategory = "WOMENS APPAREL"; catAbbrev = "WMNS"; break;
                    case "3": productCategory = "BOYS APPAREL"; catAbbrev = "BOYS"; break;
                    case "4": productCategory = "GIRLS APPAREL"; catAbbrev = "GRLS"; break;
                    case "5": productCategory = "TOYS/GAMES"; catAbbrev = "TOGA"; break;
                    case "6": productCategory = "ELECTRONICS"; catAbbrev = "ELEC"; break;
                    case "7": productCategory = "HOUSEHOLD GOODS"; catAbbrev = "HHGS"; break;
                    case "8": productCategory = "SPORTING GOODS"; catAbbrev = "SPTG"; break;
                    default: Console.Write("Invalid selection! Please enter a number between 1 and 8: "); break;
                }
            }
            string productBrand = ValidationHelper.GetValidatedInput("Brand Name (Example: Nike, Christian Dior, or custom/handmade): ", 
                input => !string.IsNullOrWhiteSpace(input), "Brand Name cannot be blank!");
            string color = ValidationHelper.GetValidatedInput("Color/Pattern (Example: Solid Black, Floral Print, or Wood Grain): ",
                input => !string.IsNullOrWhiteSpace(input), "Color/Pattern cannot be blank!");
            string name = ValidationHelper.GetValidatedInput("Product Name (Example: Laptop or Running Shoes): ",
                input => !string.IsNullOrWhiteSpace(input), "Product Name cannot be blank!");
            Console.WriteLine("Please select the number that corresponds with the product's condition:");
            Console.WriteLine("   1. New   2. Like New   3. Gently Used   4. Heavily Used");
            Console.Write("Selection: ");
            string condition = "";

            while (string.IsNullOrEmpty(condition))
            {
                string selection = Console.ReadLine();
                switch (selection)
                {
                    case "1": condition = "NEW"; break;
                    case "2": condition = "LIKE NEW"; break;
                    case "3": condition = "GENTLY USED"; break;
                    case "4": condition = "HEAVILY USED"; break;
                    default: Console.Write("Invalid Selection! Please enter a number between 1 and 4: "); break;
                }
            }
            string productDescription = $"{color}, {name}, {condition}";
            string productSize = ValidationHelper.GetValidatedInput("Size (dimensions or weight for non-clothing items): ",
                input => !string.IsNullOrWhiteSpace(input), "Size cannot be blank!");
            string price = ValidationHelper.GetValidatedInput("Price (Example: 20.00 or 1.50): ",
                input => ValidationHelper.IsValidPrice(input), "Invalid price! Please enter a valid decimal number in the format: 20.00...");
            decimal productPrice = decimal.Parse(price);

            bool isConfirmed = false;
            while (!isConfirmed)
            {
                Console.WriteLine("\n-- Verify the information is correct! --");
                Console.WriteLine($"Category: {productCategory}");
                Console.WriteLine($"Brand: {productBrand}");
                Console.WriteLine($"Description: {productDescription}");
                Console.WriteLine($"Size: {productSize}");
                Console.WriteLine($"Price: {productPrice}");
                Console.Write("\nIs this information correct? (Y/N): ");
                string verifyResponse = Console.ReadLine().ToUpper();

                if (verifyResponse == "Y")
                {
                    Console.WriteLine("\nProduct information saved successfully!");
                    isConfirmed = true;
                }
                else if (verifyResponse == "N")
                {
                    Console.WriteLine("\n-- Input the correct details --");
                    Console.WriteLine("Please select the number that corresponds product's category:");
                    Console.WriteLine("   1. Mens Apparel      2. Womens Apparel");
                    Console.WriteLine("   3. Boys Apparel      4. Girls Apparel");
                    Console.WriteLine("   5. Toys/Games        6. Electronics");
                    Console.WriteLine("   7. Household Goods   8. Sporting Goods");
                    Console.Write("Selection: ");

                    productCategory = "";
                    catAbbrev = "";
                    while (string.IsNullOrEmpty(productCategory))
                    {
                        string selection = Console.ReadLine();
                        switch (selection)
                        {
                            case "1": productCategory = "MENS APPAREL"; catAbbrev = "MEN"; break;
                            case "2": productCategory = "WOMENS APPAREL"; catAbbrev = "WMN"; break;
                            case "3": productCategory = "BOYS APPAREL"; catAbbrev = "BOY"; break;
                            case "4": productCategory = "GIRLS APPAREL"; catAbbrev = "GRL"; break;
                            case "5": productCategory = "TOYS/GAMES"; catAbbrev = "TOGA"; break;
                            case "6": productCategory = "ELECTRONICS"; catAbbrev = "ELEC"; break;
                            case "7": productCategory = "HOUSEHOLE GOODS"; catAbbrev = "HHG"; break;
                            case "8": productCategory = "SPORTING GOODS"; catAbbrev = "SPT"; break;
                            default: Console.Write("Invalid selection! Please enter a number between 1 and 8: "); break;
                        }
                    }
                    productBrand = ValidationHelper.GetValidatedInput("Brand Name (Example: Nike, Christian Dior, or custom/handmade): ",
                        input => !string.IsNullOrWhiteSpace(input), "Brand Name cannot be blank!");
                    color = ValidationHelper.GetValidatedInput("Color/Pattern (Example: Solid Black, Floral Print, or Wood Grain): ",
                        input => !string.IsNullOrWhiteSpace(input), "Color/Pattern cannot be blank!");
                    name = ValidationHelper.GetValidatedInput("Product Name (Example: Laptop or Running Shoes): ",
                        input => !string.IsNullOrWhiteSpace(input), "Product Name cannot be blank!");
                    Console.WriteLine("Please select the number that corresponds with the product's condition:");
                    Console.WriteLine("   1. New   2. Like New   3. Gently Used   4. Heavily Used");
                    condition = "";

                    while (string.IsNullOrEmpty(condition))
                    {
                        string selection = Console.ReadLine();
                        switch (selection)
                        {
                            case "1": condition = "NEW"; break;
                            case "2": condition = "LIKE NEW"; break;
                            case "3": condition = "GENTLY USED"; break;
                            case "4": condition = "HEAVILY USED"; break;
                            default: Console.Write("Invalid Selection! Please enter a number between 1 and 4: "); break;
                        }
                    }
                    productDescription = $"{color}, {name}, {condition}";
                    productSize = ValidationHelper.GetValidatedInput("Size (dimensions or weight for non-clothing items): ",
                        input => !string.IsNullOrWhiteSpace(input), "Size cannot be blank!");
                    price = ValidationHelper.GetValidatedInput("Price (Example: 20.00 or 1.50): ",
                        input => ValidationHelper.IsValidPrice(input), "Invalid price! Please enter a valid decimal number in the format: 20.00...");
                    productPrice = decimal.Parse(price);                    
                }
                else
                {
                    Console.WriteLine("\nInvalid response! Please enter Y for YES or N for NO: ");
                }
            }
            Product newProduct = new Product(consignmentID, productCategory, catAbbrev, productBrand, productDescription, productSize, productPrice);
            return newProduct;
        }
    }
}
