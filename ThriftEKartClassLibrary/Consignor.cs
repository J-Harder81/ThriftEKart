using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThriftEKartClassLibrary
{
    public class Consignor
    {
        public int ConsignorID { get; set; } = 0;
        public string ConsignorNumber { get; set; } = string.Empty;
        public string ConsignorFirstName { get; set; }
        public string ConsignorLastName { get; set; }
        public string ConsignorEmail { get; set; }
        public string ConsignorStreet { get; set; }
        public string ConsignorCity { get; set; }
        public string ConsignorState { get; set; }
        public string ConsignorPostalCode { get; set; }
        public string ConsignorPhoneNumber { get; set; }
        public Consignor (string consignorFirst, string consignorLast, string consignorEmail, string consignorStreet,
            string consignorCity, string consignorState, string consignorZip, string consignorPhone)
        {
            ConsignorFirstName = consignorFirst;
            ConsignorLastName = consignorLast;
            ConsignorEmail = consignorEmail;
            ConsignorStreet = consignorStreet;
            ConsignorCity = consignorCity;
            ConsignorState = consignorState;
            ConsignorPostalCode = consignorZip;
            ConsignorPhoneNumber = consignorPhone;
        }
        public static void AddConsignor()
        {
            Console.WriteLine(" ------------------- ");
            Console.WriteLine("| Add New Consignor |");
            Console.WriteLine(" ------------------- ");
            Console.WriteLine();
            Console.WriteLine("Please enter the consignor's details below:");

            string consignorFirst = ValidationHelper.GetValidatedInput("First Name: ", input => !string.IsNullOrWhiteSpace(input),
                "First Name cannot be blank!");
            string consignorLast = ValidationHelper.GetValidatedInput("Last Name: ", input => !string.IsNullOrWhiteSpace(input),
                "Last Name cannot be blank!");
            string consignorEmail = ValidationHelper.GetValidatedInput("Email: ", ValidationHelper.IsValidEmail,
                "Invalid Email! Please enter a valid email address...");
            string consignorStreet = ValidationHelper.GetValidatedInput("Street (Example: 123 Any Rd): ", input => !string.IsNullOrWhiteSpace(input),
                "Street cannot be blank!");
            string consignorCity = ValidationHelper.GetValidatedInput("City: ", input => !string.IsNullOrWhiteSpace(input),
                "City cannot be blank!");
            string consignorState = ValidationHelper.GetValidatedInput("State (Example: WA): ", ValidationHelper.IsValidState,
                "Invalid state! Please enter a valid 2 letter state code...");
            string consignorZip = ValidationHelper.GetValidatedInput("Postal Code: ", input => ValidationHelper.IsValidPostalCode(input, 5),
                "Invalid postal code! Please enter a 5 digit postal code...");
            string phoneNumber = ValidationHelper.GetValidatedInput("Phone Number (10 digits): ", ValidationHelper.IsValidPhoneNumber,
                "Invalid phone number! Please enter 10 digits only...");
            string consignorPhone = $"({phoneNumber.Substring(0, 3)}){phoneNumber.Substring(3, 3)}-{phoneNumber.Substring(6, 4)}";

            bool isConfirmed = false;
            while (!isConfirmed)
            {
                Console.WriteLine("\n-- Verify the information is correct! --");
                Console.WriteLine($"Name: {consignorFirst} {consignorLast}");
                Console.WriteLine($"Email: {consignorEmail}");
                Console.WriteLine($"Mailing Address:\n   {consignorStreet}\n   {consignorCity}, {consignorState}\n   {consignorZip}");
                Console.WriteLine($"Phone Number: {consignorPhone}");
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
                    consignorFirst = ValidationHelper.GetValidatedInput("First Name: ", input => !string.IsNullOrWhiteSpace(input),
                        "First Name cannot be blank!");
                    consignorLast = ValidationHelper.GetValidatedInput("Last Name: ", input => !string.IsNullOrWhiteSpace(input),
                        "Last Name cannot be blank!");
                    consignorEmail = ValidationHelper.GetValidatedInput("Email: ", ValidationHelper.IsValidEmail,
                        "Invalid Email! Please enter a valid email address...");
                    consignorStreet = ValidationHelper.GetValidatedInput("Street (Example: 123 Any Rd): ", input => !string.IsNullOrWhiteSpace(input),
                        "Street cannot be blank!");
                    consignorCity = ValidationHelper.GetValidatedInput("City: ", input => !string.IsNullOrWhiteSpace(input),
                        "City cannot be blank!");
                    consignorState = ValidationHelper.GetValidatedInput("State (Example: WA): ", ValidationHelper.IsValidState,
                        "Invalid state! Please enter a valid 2 letter state code...");
                    consignorZip = ValidationHelper.GetValidatedInput("Postal Code: ", input => ValidationHelper.IsValidPostalCode(input, 5),
                        "Invalid postal code! Please enter a 5 digit postal code...");
                    phoneNumber = ValidationHelper.GetValidatedInput("Phone Number (10 digits): ", ValidationHelper.IsValidPhoneNumber,
                        "Invalid phone number! Please enter 10 digits only...");
                    consignorPhone = $"({phoneNumber.Substring(0, 3)}){phoneNumber.Substring(3, 3)}-{phoneNumber.Substring(6, 4)}";
                }
                else
                {
                    Console.WriteLine("\nInvalid response! Please enter Y for YES or N for NO: ");
                }
            }

            bool addConsignor = false;
            while (!addConsignor)
            {
                Console.Write($"\nAre you sure you want to add {consignorFirst} {consignorLast}? (Y/N): ");
                string addResponse = Console.ReadLine().ToUpper();

                if (addResponse == "Y")
                {
                    Consignor newConsignor = new Consignor(consignorFirst, consignorLast, consignorEmail, consignorStreet, consignorCity, consignorState, consignorZip, consignorPhone);

                    DatabaseHelper dbHelper = new DatabaseHelper();
                    string consignorNumber = dbHelper.InsertConsignor(newConsignor);
                    Console.WriteLine($"\nCustomer {consignorFirst} {consignorLast} added successfully with Consignor Number: {consignorNumber}" +
                        $"\nPlease remember this number as you will need it to conduct business.");
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    addConsignor = true;
                }
                else if (addResponse == "N")
                {
                    Console.WriteLine("Add consignor cancelled!\nPress any key to return to the menu...");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid response! Please enter Y for YES or N for NO.");
                }
            }
        }
        public static void RemoveConsignor()
        {
            Console.WriteLine(" ------------------ ");
            Console.WriteLine("| Remove Consignor |");
            Console.WriteLine(" ------------------ ");
            Console.WriteLine();
            string consignorToRemove = ValidationHelper.GetValidatedInput("Please enter the entire consignor number to remove including the dash (Example: CONS-11): ", 
                ValidationHelper.IsValidConsignorNumber, "Invalid consignor number! The consignor number must be in the correct format: CONS-##.");

            DatabaseHelper dbHelper = new DatabaseHelper();
            string consignorName = dbHelper.DoesConsignorExist(consignorToRemove);

            if (!string.IsNullOrEmpty(consignorName))
            {
                Console.WriteLine($"\nConsignor found: {consignorName}");
                Console.Write("Are you sure you want to remove this consignor? (Y/N): ");
                string deleteResponse = Console.ReadLine().ToUpper();

                if (deleteResponse == "Y")
                {
                    bool isDeleted = dbHelper.DeleteConsignor(consignorToRemove);
                    Console.WriteLine(isDeleted
                        ? $"\nCustomer {consignorName} (Customer Number: {consignorToRemove}) deleted successfully!"
                        : $"Failed to delete Consignor {consignorName} (Consignor Number: {consignorToRemove})! Please try again.");
                    Console.Write("Press any key to return to the menu...");
                    Console.ReadKey();
                }
                else if (deleteResponse == "N")
                {
                    Console.WriteLine("\nRemove consignor cancelled!\nPress any key to return to the menu...");
                    Console.ReadKey();
                    return;
                }
            }
            else
            {
                Console.WriteLine($"\nNo consignor with consignor number {consignorToRemove} found!" +
                    $"\nPress any key to return to the menu...");
                Console.ReadKey();
            }
        }
        public static void ViewConsignorList()
        {
            DatabaseHelper dbHelper = new DatabaseHelper();
            List<Consignor> consignors = dbHelper.GetAllConsignors();

            if (consignors.Count == 0)
            {
                Console.WriteLine("No consignors found.");
                Console.WriteLine("Press any key to return to the menu...");
                Console.ReadKey();
                return;
            }

            // Calculate column widths dynamically
            int consignorNumberWidth = Math.Max(consignors.Max(c => c.ConsignorNumber.Length), "Cons #".Length);
            int nameWidth = Math.Max(consignors.Max(c => (c.ConsignorFirstName + " " + c.ConsignorLastName).Length), "Name".Length);
            int emailWidth = Math.Max(consignors.Max(c => c.ConsignorEmail.Length), "Email".Length);
            int addressWidth = Math.Max(consignors.Max(c => (c.ConsignorStreet + ", " + c.ConsignorCity + ", " + c.ConsignorState + ", " + c.ConsignorPostalCode).Length), "Address".Length);
            int phoneWidth = Math.Max(consignors.Max(c => c.ConsignorPhoneNumber.Length), "Phone".Length);

            // Print table title
            int totalWidth = consignorNumberWidth + nameWidth + emailWidth + addressWidth + phoneWidth + 16; // Account for column separators and spaces
            Console.WriteLine($"{"-- Thrift-E-Kart Consignor List --".PadLeft((totalWidth + "-- Thrift-E-Kart Customer List --".Length) / 2)}");
            Console.WriteLine();

            // Print header row
            Console.WriteLine($"| {"Cons #".PadRight(consignorNumberWidth)} | {"Name".PadRight(nameWidth)} | {"Email".PadRight(emailWidth)} | {"Address".PadRight(addressWidth)} | {"Phone".PadRight(phoneWidth)} |");
            Console.WriteLine(new string('-', consignorNumberWidth + nameWidth + emailWidth + addressWidth + phoneWidth + 16));

            // Print each customer row
            foreach (var consignor in consignors)
            {
                string fullName = $"{consignor.ConsignorFirstName} {consignor.ConsignorLastName}";
                string fullAddress = $"{consignor.ConsignorStreet}, {consignor.ConsignorCity}, {consignor.ConsignorState}, {consignor.ConsignorPostalCode}";

                Console.WriteLine($"| {consignor.ConsignorNumber.PadRight(consignorNumberWidth)} | {fullName.PadRight(nameWidth)} | {consignor.ConsignorEmail.PadRight(emailWidth)} | {fullAddress.PadRight(addressWidth)} | {consignor.ConsignorPhoneNumber.PadRight(phoneWidth)} |");
                Console.WriteLine(new string('-', consignorNumberWidth + nameWidth + emailWidth + addressWidth + phoneWidth + 13));
            }

            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
        }
    }    
}
