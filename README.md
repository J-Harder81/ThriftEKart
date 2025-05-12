ThriftEKart is a console application designed to manage customers, consignors, and products
for a school ecommerce project.

The application contains a SQL DataBase query file to create the database to store customer,
consignor, and product information. In order for the application to work with SQL, you
must install the Microsoft.Data.SqlClient NuGet package and update the connectionString located
on line 12. To do this change <Server Name> with your SQL server name, and change <Database Name>
to the database name.

Currently the project is still under construction, but contains the following functionality:
Create and view customers, consignors, consignments, and products.

Still to come: functionality to track and manage sales, orders, and customer shopping carts.

