﻿CREATE DATABASE Nukutaka
GO
USE Nukutaka
GO
CREATE TABLE PRODUCTS(
ID INT PRIMARY KEY IDENTITY,
CODE VARCHAR(255),
NAME NVARCHAR(255),
PRICE INT,
IMAGEURL NVARCHAR(255),
CODECATEGORY VARCHAR(255),
CODEBRAND VARCHAR(255),
CODEKIND VARCHAR(255),
INSTOCK INT,
STATUS INT
)

CREATE TABLE CATEGORY(
ID INT PRIMARY KEY IDENTITY,
CODE VARCHAR(255),
NAME NVARCHAR(255)
)

CREATE TABLE BRANDS(
ID INT PRIMARY KEY IDENTITY,
CODE VARCHAR(255),
NAME NVARCHAR(255)
)

CREATE TABLE KIND(
ID INT PRIMARY KEY IDENTITY,
CODE VARCHAR(255),
NAME NVARCHAR(255),
CODECATEGORY VARCHAR(255)
)

CREATE TABLE ADMIN(
ID INT PRIMARY KEY IDENTITY,
CODE VARCHAR(255),
NAME NVARCHAR(255),
PHONE VARCHAR(255),
PASSWORD VARCHAR(255),
)

CREATE TABLE STAFF(
ID INT PRIMARY KEY IDENTITY,
CODE VARCHAR(255),
NAME NVARCHAR(255),
PHONE VARCHAR(255),
)

CREATE TABLE INVOICE(
ID INT PRIMARY KEY IDENTITY,
CODE VARCHAR(255),
NAMECUSTOMER NVARCHAR(255),
PHONECUSTOMER VARCHAR(255),
ADDRESS NVARCHAR(255),
ORDERDATE DATE,
SHIPPINGDATE DATE,
DELIVERYDATE DATE,
SHIPPER VARCHAR(255),
PRODUCT NVARCHAR(MAX),
STATUS INT
)

-- INSERT DATA INTO TABLE
INSERT INTO CATEGORY VALUES (1, N'NAM')
INSERT INTO CATEGORY VALUES (2, N'NỮ')
INSERT INTO CATEGORY VALUES (3, N'TRẺ EM')
INSERT INTO CATEGORY VALUES (4, N'THỂ THAO')

INSERT INTO KIND VALUES (1, N'Áo sơ mi tay ngắn', 1)
INSERT INTO KIND VALUES (2, N'Áo sơ mi tay dài', 1)
INSERT INTO KIND VALUES (3, N'Áo thun', 2)
INSERT INTO KIND VALUES (4, N'Áo khoác', 2)
INSERT INTO KIND VALUES (5, N'Quần tây', 1)
INSERT INTO KIND VALUES (6, N'Quần kaki', 3)
INSERT INTO KIND VALUES (7, N'Quần short', 3)

INSERT INTO KIND VALUES (1, N'Áo sơ mi tay ngắn', 1)
INSERT INTO KIND VALUES (2, N'Áo sơ mi tay dài', 1)
INSERT INTO KIND VALUES (3, N'Áo thun', 1)
INSERT INTO KIND VALUES (4, N'Áo khoác', 2)
INSERT INTO KIND VALUES (5, N'Quần tây', 1)
INSERT INTO KIND VALUES (6, N'Quần kaki', 1)
INSERT INTO KIND VALUES (7, N'Quần short', 3)
INSERT INTO KIND VALUES (8, N'Áo thun nữ', 2)
INSERT INTO KIND VALUES (9, N'Quần short nam', 1)
INSERT INTO KIND VALUES (10, N'Áo', 3)
INSERT INTO KIND VALUES (11, N'Quần Jean', 2)

INSERT INTO BRANDS VALUES (1, N'NIKE')
INSERT INTO BRANDS VALUES (2, N'ADIDAS')
INSERT INTO BRANDS VALUES (3, N'PUMA')
INSERT INTO BRANDS VALUES (4, N'GUCCI')
INSERT INTO BRANDS VALUES (5, N'CHANEL')

INSERT INTO PRODUCTS VALUES (N'SP01', N'ÁO KHOÁC ĐEN', 250000, N'aokhoacnam2.jpg', N'2', N'5', N'4', 1, 1)
INSERT INTO PRODUCTS VALUES (N'SP02', N'ÁO THUN SỌC', 100000, N'aothunnam5.jpg', N'1', N'1', N'3', 1, 1)
INSERT INTO PRODUCTS VALUES (N'SP03', N'ÁO THUN XANH', 40000, N'aothunnam4.jpg', N'1', N'4', N'3', 1, 1)
INSERT INTO PRODUCTS VALUES (N'SP04', N'ÁO SƠ MI XÁM', 200000, N'taydainam5.jpg', N'1', N'3', N'2', 1, 1)
INSERT INTO PRODUCTS VALUES (N'SP05', N'ÁO TÍM MẠNH MẼ', 15000, N'aothunnam3.jpg', N'1', N'5', N'3', 1, 1)
INSERT INTO PRODUCTS VALUES (N'SP07', N'ÁO SƠ MI SỌC', 300000, N'taydainam6.jpg', N'1', N'2', N'2', 1, 1)
INSERT INTO PRODUCTS VALUES (N'SP08', N'ÁO THUN SỌC', 60000, N'aothunnu.jpg', N'2', N'5', N'8', 1, 0)
INSERT INTO PRODUCTS VALUES (N'SP09', N'ÁO THUN TRẮNG', 900000, N'aothunnu1.jpg', N'2', N'1', N'8', 1, 0)
INSERT INTO PRODUCTS VALUES (N'SP10', N'ÁO THUN HỌA TIẾT', 100000, N'aothunnu2.jpg', N'2', N'4', N'8', 1, 0)
INSERT INTO PRODUCTS VALUES (N'SP11', N'ÁO SƠ MI DÀI', 550000, N'taydainam1.jpg', N'1', N'3', N'2', 1, 0)
INSERT INTO PRODUCTS VALUES (N'SP12', N'ÁO SƠ MI DÀI', 680000, N'taydainam.jpg', N'1', N'5', N'2', 1, 0)
INSERT INTO PRODUCTS VALUES (N'SP13', N'ÁO TAY NGẮN', 420000, N'tayngannam1.jpg', N'1', N'4', N'1', 0, 0)
INSERT INTO PRODUCTS VALUES (N'SP14', N'ÁO TAY NGẮN ĐEN', 580000, N'tayngannam2.jpg', N'1', N'3', N'1', 1, 0)
INSERT INTO PRODUCTS VALUES (N'SP15', N'QUẦN SHORT', 180000, N'quanshort.jpg', N'1', N'5', N'9', 0, 0)
INSERT INTO PRODUCTS VALUES (N'SP16', N'QUẦN TÂY', 400000, N'quantaynam.jpg', N'1', N'2', N'5', 0, 0)
INSERT INTO PRODUCTS VALUES (N'SP17', N'QUẦN TÂY ĐỎ', 350000, N'quantaynam2.jpg', N'1', N'5', N'5', 0, 0)
INSERT INTO PRODUCTS VALUES (N'SP18', N'QUẦN KAKI', 300000, N'quankakinam2.jpg', N'1', N'2', N'6', 1, 0)
INSERT INTO PRODUCTS VALUES (N'SP19', N'ÁO KHOÁC NỮ', 250000, N'aokhoacnu.jpg', N'2', N'4', N'4', 1, 0)
INSERT INTO PRODUCTS VALUES (N'SP21', N'QUẦN JEAN', 350000, N'quanjean.jpg', N'2', N'4', N'11', 1, 0)

INSERT INTO STAFF VALUES ('NV01', N'THIỆN CHÓ', '0898698890')
INSERT INTO STAFF VALUES ('NV02', N'MINH CỎ', '0123456789')
INSERT INTO STAFF VALUES ('NV03', N'HÃM', '0909123456')

-- STORE PROCEDURE PRODUCT
CREATE PROC SP_INSERT_UPDATE_PRODUCT(@P_ID INT,
									 @P_CODE VARCHAR(255),
									 @P_NAME NVARCHAR(255),
									 @P_PRICE INT,
									 @P_IMAGEURL NVARCHAR(255),
									 @P_CODECATEGORY VARCHAR(255),
									 @P_CODEKIND VARCHAR(255),
									 @P_CODEBRAND VARCHAR(255),
									 @P_STATUS INT,
									 @P_INSTOCK INT)
AS
BEGIN
IF(@P_ID = 0)
	INSERT INTO PRODUCTS (CODE,
						  NAME,
						  PRICE,
						  IMAGEURL,
						  CODECATEGORY,
						  CODEKIND,
						  CODEBRAND,
						  STATUS,
						  INSTOCK)
				  VALUES (@P_CODE,
						  @P_NAME,
						  @P_PRICE,
						  @P_IMAGEURL,
						  @P_CODECATEGORY,
						  @P_CODEKIND,
						  @P_CODEBRAND,
						  @P_STATUS,
						  @P_INSTOCK)
ELSE
	UPDATE PRODUCTS SET CODE = @P_CODE,
						NAME = @P_NAME,
						PRICE = @P_PRICE,
						IMAGEURL = @P_IMAGEURL,
						CODECATEGORY = @P_CODECATEGORY,
						CODEKIND = @P_CODEKIND,
						CODEBRAND = @P_CODEBRAND,
						STATUS = @P_STATUS,
						INSTOCK = @P_INSTOCK
			WHERE ID = @P_ID
END

-- EXEC SP_INSERT_UPDATE_PRODUCT 0, '1', '1N', 1, '234', '2', '3', '4', 1, 1


-- STORE PROCEDURE CATEGORY
CREATE PROC SP_INSERT_UPDATE_CATEGORY(@P_ID INT,
									  @P_CODE VARCHAR(255),
									  @P_NAME NVARCHAR(255))
AS
BEGIN
IF(@P_ID = 0)
	INSERT INTO CATEGORY (CODE,
						  NAME)
				  VALUES (@P_CODE,
						  @P_NAME)					 
ELSE
	UPDATE CATEGORY SET CODE = @P_CODE,
						NAME = @P_NAME					
			WHERE ID = @P_ID
END


-- STORE PROCEDURE KIND
CREATE PROC SP_INSERT_UPDATE_KIND(@P_ID INT,
									  @P_CODE VARCHAR(255),
									  @P_NAME NVARCHAR(255),
									  @P_CODECATE VARCHAR(255))
AS
BEGIN
IF(@P_ID = 0)
	INSERT INTO KIND (CODE,
					  NAME,
					  CODECATEGORY)
		      VALUES (@P_CODE,
					  @P_NAME,
					  @P_CODECATE)					 
ELSE
	UPDATE KIND SET CODE = @P_CODE,
					NAME = @P_NAME,
					CODECATEGORY = @P_CODECATE				
			WHERE ID = @P_ID
END


-- STORE PROCEDURE BRAND
CREATE PROC SP_INSERT_UPDATE_BRAND (@P_ID INT,
									@P_CODE VARCHAR(255),
									@P_NAME NVARCHAR(255))
AS
BEGIN
IF(@P_ID = 0)
	INSERT INTO BRANDS (CODE,
						NAME)
				VALUES (@P_CODE,
						@P_NAME)					 
ELSE
	UPDATE BRANDS SET CODE = @P_CODE,
					  NAME = @P_NAME					
			WHERE ID = @P_ID
END

--STORE PROCEDURE STAFF
CREATE PROC SP_INSERT_UPDATE_STAFF (@P_ID INT,
									@P_CODE VARCHAR(255),
									@P_NAME NVARCHAR(255),
									@P_PHONE VARCHAR(255))
AS
BEGIN
IF(@P_ID = 0)
	INSERT INTO STAFF (CODE,
					   NAME,
					   PHONE)
				VALUES (@P_CODE,
						@P_NAME,
						@P_PHONE)					 
ELSE
	UPDATE STAFF SET CODE = @P_CODE,
					 NAME = @P_NAME,
					 PHONE = @P_PHONE				
			WHERE ID = @P_ID
END