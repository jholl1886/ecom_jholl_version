

--CREATE SCHEMA product

CREATE PROCEDURE Product.InsertProduct
@Name nvarchar(255)
, @Description nvarchar(max)
, @Quantity int
, @Price numeric(10,2)
, @Id int output
, @MarkDown numeric(10,2)
, @IsBogo BIT
AS
BEGIN
	INSERT INTO PRODUCT ([Name],[Description],[Price],[Quantity],[MarkDown],[IsBogo]) 
	VALUES(@Name, @Description, @Price,@Quantity,@MarkDown,@IsBogo)

	SET @Id = SCOPE_IDENTITY()
END

declare @newId int
exec Product.InsertProduct @Name = 'SP Product'
, @Description = 'Product inserted from SP'
, @Quantity = 10
, @Price = 1.23
, @Id = @newId out
, @MarkDown = 0.00
, @IsBogo = 0

select @newId

select * from Product

CREATE PROCEDURE Product.UpdateProduct
@Name nvarchar(255)
, @Description nvarchar(max)
, @Quantity int
, @Price numeric(10,2)
, @Id int output
, @MarkDown numeric(10,2)
, @IsBogo BIT
AS
BEGIN
	UPDATE PRODUCT 
	SET
		Name = @Name
		, Description = @Description
		, Quantity = @Quantity
		, Price = @Price
		, MarkDown = @MarkDown
		, IsBogo = @IsBogo
	WHERE
		Id = @Id
END

CREATE PROCEDURE Product.DeleteProduct
@Id int
AS
BEGIN
    DELETE FROM PRODUCT
    WHERE Id = @Id
END

exec Product.DeleteProduct @Id = 7