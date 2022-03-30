-- 
CREATE DATABASE test_db; -- RUN FIRST
--

USE test_db

CREATE TABLE Categories (
	Id BIGINT PRIMARY KEY IDENTITY(1,1),
	Title NVARCHAR(30) UNIQUE,
)

CREATE TABLE Products (
	Id BIGINT PRIMARY KEY IDENTITY(1,1),
	Title NVARCHAR(30) UNIQUE
)

CREATE TABLE ProductsToCategories(
	ProductId BIGINT,
	CategoryId BIGINT,
	CONSTRAINT PK_ProductsToCategories PRIMARY KEY CLUSTERED (ProductId, CategoryId),	
	FOREIGN KEY (ProductId) REFERENCES Products (Id) ON DELETE CASCADE,
	FOREIGN KEY (CategoryId) REFERENCES Categories (Id) ON DELETE CASCADE
)

INSERT INTO Products VALUES 
(N'Product1'),
(N'Product2'),
(N'Product3'),
(N'Product4')

INSERT INTO Categories VALUES 
(N'Category1'),
(N'Category2')

INSERT INTO ProductsToCategories VALUES 
(1, 1),
(1, 2),
(3, 1),
(4, 2)

SELECT p.Title, c.Title FROM Products p
LEFT JOIN ProductsToCategories pc ON p.Id = pc.ProductId
LEFT JOIN Categories c ON pc.CategoryId = c.Id
ORDER BY p.Title

USE another_db -- Switch to another db to be able to drop test_db

DROP DATABASE test_db