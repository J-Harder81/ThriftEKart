USE master;
GO

CREATE DATABASE ThriftEKart;
GO

USE ThriftEKart;
GO

CREATE TABLE Customers (
	CustomerID int IDENTITY(1,1) PRIMARY KEY,
	CustomerNumber AS ('CUST-0' + CAST(CustomerID AS varchar)),
	FirstName varchar(25),
	LastName varchar(25),
	Email nvarchar(50),
	Street varchar(50),
	City varchar(25),
	State char(2),
	PostalCode varchar(10),
);

CREATE TABLE Consignors (
	ConsignorID int IDENTITY(1,1) PRIMARY KEY,
	ConsignorNumber AS ('CONS-1' + CAST(ConsignorID AS varchar)),
	FirstName varchar(25),
	LastName varchar(25),
	Email nvarchar(50),
	Street varchar(50),
	City varchar(25),
	State char(2),
	PostalCode varchar(10),
	PhoneNumber varchar(20),
);

CREATE TABLE Consignments (
	ConsignmentID int IDENTITY(1,1) PRIMARY KEY,
	ConsignmentNumber AS ('CMNT-2' + CAST(ConsignmentID AS varchar)),
	ConsignorID int,
	StartDate date,
	EndDate date,
	FOREIGN KEY (ConsignorID) REFERENCES Consignors(ConsignorID),
);

CREATE TABLE Products (
	ProductID int IDENTITY(1,1) PRIMARY KEY,
	ProductNumber AS (CatAbbrev + '-' + CAST(ProductID AS varchar)),
	ConsignmentID int,
	Category varchar(20),
	CatAbbrev varchar(5),
	Brand varchar(50),
	Description varchar(250),
	Size varchar(50),
	Price decimal(10, 2),
	FOREIGN KEY (ConsignmentID) REFERENCES Consignments(ConsignmentID),
);

CREATE TABLE ConsignmentDetails (
	ConsignmentDetailID int IDENTITY(1,1) PRIMARY KEY,
	ConsignmentID int,
	ProductID int,
	FOREIGN KEY (ConsignmentID) REFERENCES Consignments(ConsignmentID),
	FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
);

CREATE TABLE Orders (
	OrderID int IDENTITY(1,1) PRIMARY KEY,
	OrderNumber varchar(100),
	CustomerID int,
	OrderDate date,
	TotalAmount decimal(10, 2),
	OrderStatus varchar(50),
	FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),
);

CREATE TABLE OrderDetails (
	OrderDetailID int IDENTITY(1,1) PRIMARY KEY,
	OrderID int,
	ProductID int,
	FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
	FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
);

CREATE TABLE Sales (
	SaleID int IDENTITY(1,1) PRIMARY KEY,
	ConsignorID int,
	ProductID int,
	SaleDate date,
	SalePrice decimal(10, 2),
	CommissionRate decimal(5, 2),
	CommissionAmount AS (SalePrice * CommissionRate),
	DisbursementAmount AS (SalePrice - (SalePrice * CommissionRate)),
	FOREIGN KEY (ConsignorID) REFERENCES Consignors(ConsignorID),
	FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
);