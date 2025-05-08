using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThriftEKartClassLibrary
{
    public class Customer
    {
        public int CustomerID { get; set; } = 0;
        public string CustomerNumber { get; set; } = string.Empty;
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerStreet { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerState { get; set; }
        public string CustomerPostalCode { get; set; }
        public Customer(string customerFirst, string customerLast, string customerEmail,
            string customerStreet, string customerCity, string customerState, string customerZip)
        {
            CustomerFirstName = customerFirst;
            CustomerLastName = customerLast;
            CustomerEmail = customerEmail;
            CustomerStreet = customerStreet;
            CustomerCity = customerCity;
            CustomerState = customerState;
            CustomerPostalCode = customerZip;
        }
        public static void AddCustomer()
        {
            Console.WriteLine(" ------------------ ");
            Console.WriteLine("| Add New Customer |");
            Console.WriteLine(" ------------------ ");
            Console.WriteLine();
            Console.WriteLine("Please enter the customer's details below:");

            string customerFirst = ValidationHelper.GetValidatedInput("First Name: ", input => !string.IsNullOrWhiteSpace(input),
                "First Name cannot be blank!");
            string customerLast = ValidationHelper.GetValidatedInput("Last Name: ", input => !string.IsNullOrWhiteSpace(input),
                "Last Name cannot be blank!");
            string customerEmail = ValidationHelper.GetValidatedInput("Email: ", ValidationHelper.IsValidEmail,
                "Invalid Email! Please enter a valid email address...");
            string customerStreet = ValidationHelper.GetValidatedInput("Street (Example: 123 Any Rd): ", input => !string.IsNullOrWhiteSpace(input),
                "Street cannot be blank!");
            string customerCity = ValidationHelper.GetValidatedInput("City: ", input => !string.IsNullOrWhiteSpace(input),
                "City cannot be blank!");
            string customerState = ValidationHelper.GetValidatedInput("State (Example: WA): ", ValidationHelper.IsValidState,
                "Invalid state! Please enter a valid 2 letter state code...");
            string customerZip = ValidationHelper.GetValidatedInput("Postal Code: ", input => ValidationHelper.IsValidPostalCode(input, 5),
                "Invalid postal code! Please enter a 5 digit postal code...");

            bool isConfirmed = false;
            while (!isConfirmed)
            {
                Console.WriteLine("\n-- Verify the information is correct! --");
                Console.WriteLine($"Name: {customerFirst} {customerLast}");
                Console.WriteLine($"Email: {customerEmail}");
                Console.WriteLine($"Mailing Address:\n   {customerStreet}\n   {customerCity}, {customerState}\n   {customerZip}");
                Console.Write("\nIs this information correct? (Y/N): ");
                string verifyResponse = Console.ReadLine().ToUpper();

                if (verifyResponse == "Y")
                {
                    Console.WriteLine("\nCustomer information saved successfully!");
                    isConfirmed = true;
                }
                else if (verifyResponse == "N")
                {
                    Console.WriteLine("\n-- Input the correct details --");
                    customerFirst = ValidationHelper.GetValidatedInput("First Name: ", input => !string.IsNullOrWhiteSpace(input),
                        "First Name cannot be blank!");
                    customerLast = ValidationHelper.GetValidatedInput("Last Name: ", input => !string.IsNullOrWhiteSpace(input),
                        "Last Name cannot be blank!");
                    customerEmail = ValidationHelper.GetValidatedInput("Email: ", ValidationHelper.IsValidEmail,
                        "Invalid Email! Please enter a valid email address...");
                    customerStreet = ValidationHelper.GetValidatedInput("Street (Example: 123 Any Rd): ", input => !string.IsNullOrWhiteSpace(input),
                        "Street cannot be blank!");
                    customerCity = ValidationHelper.GetValidatedInput("City: ", input => !string.IsNullOrWhiteSpace(input),
                        "City cannot be blank!");
                    customerState = ValidationHelper.GetValidatedInput("State (Example: WA): ", ValidationHelper.IsValidState,
                        "Invalid state! Please enter a valid 2 letter state code...");
                    customerZip = ValidationHelper.GetValidatedInput("Postal Code: ", input => ValidationHelper.IsValidPostalCode(input, 5),
                        "Invalid postal code! Please enter a 5 digit postal code...");
                }
                else
                {
                    Console.WriteLine("\nInvalid response! Please enter Y for YES or N for NO: ");
                }
            }
            
            bool addCustomer = false;
            while (!addCustomer)
            {
                Console.Write($"\nAre you sure you want to add {customerFirst} {customerLast}? (Y/N): ");
                string addResponse = Console.ReadLine().ToUpper();

                if (addResponse == "Y")
                {
                    Customer newCustomer = new Customer(customerFirst, customerLast, customerEmail, customerStreet, customerCity, customerState, customerZip);
                    
                    DatabaseHelper dbHelper = new DatabaseHelper();
                    string customerNumber = dbHelper.InsertCustomer(newCustomer);
                    Console.WriteLine($"\nCustomer {customerFirst} {customerLast} added successfully with Customer Number: {customerNumber}" +
                        $"\nPlease remember this number as you will need it to shop.");
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    addCustomer = true;
                }
                else if (addResponse == "N")
                {
                    Console.WriteLine("Add customer cancelled!\nPress any key to return to the menu...");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid response! Please enter Y for YES or N for NO.");
                }
            }
        }
        public static void RemoveCustomer()
        {
            Console.WriteLine(" ----------------- ");
            Console.WriteLine("| Remove Customer |");
            Console.WriteLine(" ----------------- ");
            Console.WriteLine();
            string customerToRemove = ValidationHelper.GetValidatedInput("Please enter the entire customer number to remove including the dash (Example: CUST-01): ", 
                ValidationHelper.IsValidCustomerNumber, "Invalid customer number! The customer number must be in the correct format: CUST-##.");
            
            DatabaseHelper dbHelper = new DatabaseHelper();
            string customerName = dbHelper.DoesCustomerExist(customerToRemove);

            if (!string.IsNullOrEmpty(customerName))
            {
                Console.WriteLine($"\nCustomer found: {customerName}");
                Console.Write("Are you sure you want to remove this customer? (Y/N): ");
                string deleteResponse = Console.ReadLine().ToUpper();

                if (deleteResponse == "Y")
                {
                    bool isDeleted = dbHelper.DeleteCustomer(customerToRemove);
                    Console.WriteLine(isDeleted
                        ? $"\nCustomer {customerName} (Customer Number: {customerToRemove}) deleted successfully!"
                        : $"Failed to delete Customer {customerName} (Customer Number: {customerToRemove})! Please try again.");
                    Console.Write("Press any key to return to the menu...");
                    Console.ReadKey();
                }
                else if (deleteResponse == "N")
                {
                    Console.WriteLine("\nRemove customer cancelled!\nPress any key to return to the menu...");
                    Console.ReadKey();
                    return;
                }
            }
            else
            {
                Console.WriteLine($"\nNo customer with customer number {customerToRemove} found!" +
                    $"\nPress any key to return to the menu...");
                Console.ReadKey();
            }
        }
        public static void ViewCustomerList()
        {
            DatabaseHelper dbHelper = new DatabaseHelper();
            List<Customer> customers = dbHelper.GetAllCustomers();

            if (customers.Count == 0)
            {
                Console.WriteLine("No customers found.");
                Console.WriteLine("Press any key to return to the menu...");
                Console.ReadKey();
                return;
            }

            // Calculate column widths dynamically
            int customerNumberWidth = Math.Max(customers.Max(c => c.CustomerNumber.Length), "Cust #".Length);
            int nameWidth = Math.Max(customers.Max(c => (c.CustomerFirstName + " " + c.CustomerLastName).Length), "Name".Length);
            int emailWidth = Math.Max(customers.Max(c => c.CustomerEmail.Length), "Email".Length);
            int addressWidth = Math.Max(customers.Max(c => (c.CustomerStreet + ", " + c.CustomerCity + ", " + c.CustomerState + ", " + c.CustomerPostalCode).Length), "Address".Length);

            // Print table title
            int totalWidth = customerNumberWidth + nameWidth + emailWidth + addressWidth + 13; // Account for column separators and spaces
            Console.WriteLine($"{"-- Thrift-E-Kart Customer List --".PadLeft((totalWidth + "-- Thrift-E-Kart Customer List --".Length) / 2)}");
            Console.WriteLine();

            // Print header row
            Console.WriteLine($"| {"Cust #".PadRight(customerNumberWidth)} | {"Name".PadRight(nameWidth)} | {"Email".PadRight(emailWidth)} | {"Address".PadRight(addressWidth)} |");
            Console.WriteLine(new string('-', customerNumberWidth + nameWidth + emailWidth + addressWidth + 13));

            // Print each customer row
            foreach (var customer in customers)
            {
                string fullName = $"{customer.CustomerFirstName} {customer.CustomerLastName}";
                string fullAddress = $"{customer.CustomerStreet}, {customer.CustomerCity}, {customer.CustomerState}, {customer.CustomerPostalCode}";

                Console.WriteLine($"| {customer.CustomerNumber.PadRight(customerNumberWidth)} | {fullName.PadRight(nameWidth)} | {customer.CustomerEmail.PadRight(emailWidth)} | {fullAddress.PadRight(addressWidth)} |");
                Console.WriteLine(new string('-', customerNumberWidth + nameWidth + emailWidth + addressWidth + 13));
            }
            
            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
        }
    }    
}
