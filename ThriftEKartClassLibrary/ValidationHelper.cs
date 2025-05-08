using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ThriftEKartClassLibrary
{
    public static class ValidationHelper
    {
        public static bool IsValidEmail(string email)
        {
            var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            var regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
        public static bool IsValidState(string state)
        {
            HashSet<string> validStates = new HashSet<string>
            {
                "AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "FL", "GA",
                "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD",
                "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ",
                "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC",
                "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY"
            };
            return validStates.Contains(state);
        }
        public static bool IsValidPostalCode(string postalCode, int requiredLength)
        {
            return postalCode.Length == requiredLength && postalCode.All(char.IsDigit);
        }
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            return phoneNumber.Length == 10 && long.TryParse(phoneNumber, out _);
        }
        public static bool IsValidCustomerNumber(string customerNumber)
        {
            string pattern = @"^CUST-\d+$"; // Regex for "CUST-" followed by digits
            return Regex.IsMatch(customerNumber, pattern);
        }
        public static bool IsValidConsignorNumber(string consignorNumber)
        {
            string pattern = @"^CONS-\d+$"; // Regex for "CONS-" followed by digits
            return Regex.IsMatch(consignorNumber, pattern);
        }
        public static bool IsValidPrice(string price)
        {
            string pattern = @"^\d+\.+\d{2}?$"; 

            if (!Regex.IsMatch(price, pattern))
            {
                return false;
            }

            return decimal.TryParse(price, out decimal result) && result >= 0; 
        }

        public static string GetValidatedInput(string prompt, Func<string, bool> validation, string errorMessage)
        {
            string input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine().ToUpper();
                if (!validation(input))
                {
                    Console.WriteLine(errorMessage);
                }
            } while (!validation(input));

            return input;
        }
    }
}
