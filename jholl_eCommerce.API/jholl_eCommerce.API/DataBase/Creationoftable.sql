CREATE DATABASE AMAZON

CREATE TABLE PRODUCT (
	[Name] nvarchar(255) NULL,
	[Description] nvarchar(max) NULL,
	Price numeric(10,2) NOT NULL,
	Id int IDENTITY(1,1) NOT NULL,
	MarkDown numeric(10,2) NOT NULL,
	IsBogo BIT,
	Quantity int NULL

)

select * from PRODUCT ORDER BY Id

INSERT INTO PRODUCT ([Name],[Description],[Price],[Quantity],[MarkDown],[IsBogo]) 
VALUES
('Product A', '', 19.99, 50, 0.00, 0),
('Product B', '', 29.99, 30, 5.00, 0),
('Product C', '', 39.99, 20, 10.00, 1), 
('Product D', '', 49.99, 10, 0.00, 0),
('Product E', '', 59.99, 5, 15.00, 0);

begin tran 
delete PRODUCT
where Id = 6
commit

TRUNCATE TABLE PRODUCT;
