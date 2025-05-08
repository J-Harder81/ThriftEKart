using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ThriftEKartClassLibrary
{
    public class DatabaseHelper
    {
        string connectionString = "Data Source=Hyper_Z-FURY;Initial Catalog=ThriftEKart;Integrated Security=True;Trust Server Certificate=True";
     // Customer Helper
        public string InsertCustomer(Customer customer)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Customers (FirstName, LastName, Email, Street, City, State, PostalCode) 
                        OUTPUT INSERTED.CustomerNumber
                        VALUES (@FirstName, @LastName, @Email, @Street, @City, @State, @PostalCode);";

                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@FirstName", customer.CustomerFirstName);
                    command.Parameters.AddWithValue("@LastName", customer.CustomerLastName);
                    command.Parameters.AddWithValue("@Email", customer.CustomerEmail);
                    command.Parameters.AddWithValue("@Street", customer.CustomerStreet);
                    command.Parameters.AddWithValue("@City", customer.CustomerCity);
                    command.Parameters.AddWithValue("@State", customer.CustomerState);
                    command.Parameters.AddWithValue("@PostalCode", customer.CustomerPostalCode);

                    connection.Open();
                    string customerNumber = command.ExecuteScalar().ToString();

                    customer.CustomerNumber = customerNumber;

                    return customerNumber;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                throw; // Optionally rethrow the exception or handle it gracefully
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Optionally rethrow the exception or handle it gracefully
            }
        }
        public string DoesCustomerExist(string customerNumber)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT FirstName + ' ' + LastName AS FullName FROM Customers WHERE CustomerNumber = @CustomerNumber";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CustomerNumber", customerNumber);

                    connection.Open();

                    object result = command.ExecuteScalar();
                    return result != null ? result.ToString() : null; // Return the name or null if not found
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL error: {ex.Message}");
                return null; // Gracefully handle the error
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                return null; // Gracefully handle the error
            }
        }
        public bool DeleteCustomer (string customerNumber)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Customers WHERE CustomerNumber = @CustomerNumber";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CustomerNumber", customerNumber);

                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Returns true if at least one row was deleted
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL error: {ex.Message}");
                return false; // Gracefully handle the error
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                return false; // Gracefully handle the error
            }
        }
        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT CustomerNumber, FirstName, LastName, Email, Street, City, State, PostalCode FROM Customers";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customer customer = new Customer(
                                reader["FirstName"].ToString(),
                                reader["LastName"].ToString(),
                                reader["Email"].ToString(),
                                reader["Street"].ToString(),
                                reader["City"].ToString(),
                                reader["State"].ToString(),
                                reader["PostalCode"].ToString()
                            )
                            {
                                CustomerNumber = reader["CustomerNumber"].ToString()
                            };

                            customers.Add(customer);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL error occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }

            return customers;
        }
     // Consignor helper
        public string InsertConsignor(Consignor consignor)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Consignors (FirstName, LastName, Email, Street, City, State, PostalCode, PhoneNumber) 
                        OUTPUT INSERTED.ConsignorNumber
                        VALUES (@FirstName, @LastName, @Email, @Street, @City, @State, @PostalCode, @PhoneNumber);";

                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@FirstName", consignor.ConsignorFirstName);
                    command.Parameters.AddWithValue("@LastName", consignor.ConsignorLastName);
                    command.Parameters.AddWithValue("@Email", consignor.ConsignorEmail);
                    command.Parameters.AddWithValue("@Street", consignor.ConsignorStreet);
                    command.Parameters.AddWithValue("@City", consignor.ConsignorCity);
                    command.Parameters.AddWithValue("@State", consignor.ConsignorState);
                    command.Parameters.AddWithValue("@PostalCode", consignor.ConsignorPostalCode);
                    command.Parameters.AddWithValue("@PhoneNumber", consignor.ConsignorPhoneNumber);

                    connection.Open();
                    string consignorNumber = command.ExecuteScalar().ToString();

                    consignor.ConsignorNumber = consignorNumber;

                    return consignorNumber;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                throw; // Optionally rethrow the exception or handle it gracefully
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Optionally rethrow the exception or handle it gracefully
            }
        }
        public int GetConsignorID(string consignorNumber)
        {
            int consignorID = 0; // Default value for consignor ID if not found

            string query = "SELECT ConsignorID FROM Consignors WHERE ConsignorNumber = @ConsignorNumber";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ConsignorNumber", consignorNumber);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out consignorID))
                    {
                        return consignorID; // Return the fetched consignor ID
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while fetching the consignor ID: {ex.Message}");
                }
            }
            return consignorID; // Return 0 if no consignor ID was found
        }
        public string DoesConsignorExist(string consignorNumber)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT FirstName + ' ' + LastName AS FullName FROM Consignors WHERE ConsignorNumber = @ConsignorNumber";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ConsignorNumber", consignorNumber);

                    connection.Open();

                    object result = command.ExecuteScalar();
                    return result != null ? result.ToString() : null; // Return the name or null if not found
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL error: {ex.Message}");
                return null; // Gracefully handle the error
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                return null; // Gracefully handle the error
            }
        }
        public bool DeleteConsignor(string consignorNumber)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Consignors WHERE ConsignorNumber = @ConsignorNumber";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ConsignorNumber", consignorNumber);

                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Returns true if at least one row was deleted
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL error: {ex.Message}");
                return false; // Gracefully handle the error
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                return false; // Gracefully handle the error
            }
        }
        public List<Consignor> GetAllConsignors()
        {
            List<Consignor> consignors = new List<Consignor>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT ConsignorNumber, FirstName, LastName, Email, Street, City, State, PostalCode, PhoneNumber FROM Consignors";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Consignor consignor = new Consignor(
                                reader["FirstName"].ToString(),
                                reader["LastName"].ToString(),
                                reader["Email"].ToString(),
                                reader["Street"].ToString(),
                                reader["City"].ToString(),
                                reader["State"].ToString(),
                                reader["PostalCode"].ToString(),
                                reader["PhoneNumber"].ToString()
                            )
                            {
                                ConsignorNumber = reader["ConsignorNumber"].ToString()
                            };

                            consignors.Add(consignor);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL error occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }

            return consignors;
        }
        // Product Helper
        public void InsertDonatedProduct(Product product)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Products(Category, CatAbbrev, Brand, Description, Size, Price)
                        VALUES(@Category, @CatAbbrev, @Brand, @Description, @Size, @Price)";

                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@Category", product.ProductCategory);
                    command.Parameters.AddWithValue("@CatAbbrev", product.CatAbbrev);
                    command.Parameters.AddWithValue("@Brand", product.Brand);
                    command.Parameters.AddWithValue("@Description", product.Description);
                    command.Parameters.AddWithValue("@Size", product.Size);
                    command.Parameters.AddWithValue("@Price", product.Price);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL error occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
     // Consignment Helper
        public void InsertConsignedProduct (Product product)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Products(ConsignmentID, Category, CatAbbrev, Brand, Description, Size, Price)
                        VALUES(@ConsignmentID, @Category, @CatAbbrev, @Brand, @Description, @Size, @Price)";

                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@ConsignmentID", product.ConsignmentID);
                    command.Parameters.AddWithValue("@Category", product.ProductCategory);
                    command.Parameters.AddWithValue("@CatAbbrev", product.CatAbbrev);
                    command.Parameters.AddWithValue("@Brand", product.Brand);
                    command.Parameters.AddWithValue("@Description", product.Description);
                    command.Parameters.AddWithValue("@Size", product.Size);
                    command.Parameters.AddWithValue("@Price", product.Price);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL error occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
        public (int ConsignmentID, string ConsignmentNumber) InsertConsignment(Consignment consignment)
        {
            int consignmentID = 0;
            string consignmentNumber = string.Empty;

            string query = @"
            INSERT INTO Consignments (ConsignorID, StartDate, EndDate)
            OUTPUT INSERTED.ConsignmentID
            VALUES (@ConsignorID, @StartDate, @EndDate);";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters
                    command.Parameters.AddWithValue("@ConsignorID", consignment.ConsignorID);
                    command.Parameters.AddWithValue("@StartDate", consignment.StartDate);
                    command.Parameters.AddWithValue("@EndDate", consignment.EndDate);

                    connection.Open();

                    // Execute the query and get the ConsignmentID
                    consignmentID = (int)command.ExecuteScalar();

                    // Retrieve the ConsignmentNumber based on ConsignmentID
                    string consignmentNumberQuery = "SELECT ConsignmentNumber FROM Consignments WHERE ConsignmentID = @ConsignmentID";
                    using (SqlCommand numberCommand = new SqlCommand(consignmentNumberQuery, connection))
                    {
                        numberCommand.Parameters.AddWithValue("@ConsignmentID", consignmentID);
                        object result = numberCommand.ExecuteScalar();
                        if (result != null)
                        {
                            consignmentNumber = result.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return (consignmentID, consignmentNumber);
        }
    }
}
