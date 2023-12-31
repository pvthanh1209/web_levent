USE [master]
GO
/****** Object:  Database [Levent]    Script Date: 11/10/2023 1:36:35 AM ******/
CREATE DATABASE [Levent]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Levent', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Levent.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Levent_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Levent_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Levent] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Levent].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Levent] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Levent] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Levent] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Levent] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Levent] SET ARITHABORT OFF 
GO
ALTER DATABASE [Levent] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Levent] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Levent] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Levent] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Levent] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Levent] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Levent] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Levent] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Levent] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Levent] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Levent] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Levent] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Levent] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Levent] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Levent] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Levent] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Levent] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Levent] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Levent] SET  MULTI_USER 
GO
ALTER DATABASE [Levent] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Levent] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Levent] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Levent] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Levent] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Levent] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Levent] SET QUERY_STORE = OFF
GO
USE [Levent]
GO
/****** Object:  Table [dbo].[AdminUser]    Script Date: 11/10/2023 1:36:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdminUser](
	[ID_User] [int] IDENTITY(1,1) NOT NULL,
	[User_Name] [nvarchar](50) NULL,
	[Full_Name] [nvarchar](500) NULL,
	[Email_User] [nvarchar](50) NULL,
	[Phone_Number] [nvarchar](50) NULL,
	[Address] [nvarchar](max) NULL,
	[Password_User] [nchar](50) NULL,
	[Role] [int] NULL,
 CONSTRAINT [PK_AdminUser] PRIMARY KEY CLUSTERED 
(
	[ID_User] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 11/10/2023 1:36:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[ID_Cate] [int] IDENTITY(1,1) NOT NULL,
	[Name_Cate] [nvarchar](50) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[ID_Cate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Color_De]    Script Date: 11/10/2023 1:36:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Color_De](
	[IdColor] [int] IDENTITY(1,1) NOT NULL,
	[ID_Details] [int] NOT NULL,
	[ColorPr_Name] [nvarchar](50) NOT NULL,
	[Img_Pro] [nvarchar](max) NULL,
 CONSTRAINT [PK_Color_De] PRIMARY KEY CLUSTERED 
(
	[IdColor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Details]    Script Date: 11/10/2023 1:36:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Details](
	[ID_Detail] [int] IDENTITY(1,1) NOT NULL,
	[ID_Pro] [int] NULL,
	[Name_Pro] [nvarchar](50) NULL,
	[Price_Pro] [float] NULL,
	[Img_Pro] [nvarchar](max) NULL,
	[Quantity_Pro] [int] NULL,
 CONSTRAINT [PK_Details] PRIMARY KEY CLUSTERED 
(
	[ID_Detail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 11/10/2023 1:36:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[OrderCode] [nvarchar](500) NULL,
	[UserId] [int] NOT NULL,
	[TotalPrice] [decimal](18, 0) NULL,
	[Status] [int] NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 11/10/2023 1:36:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[Order_Detail_Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NULL,
	[ProductId] [int] NULL,
	[DetailId] [int] NULL,
	[Quantity] [int] NULL,
	[Price] [decimal](18, 0) NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[Order_Detail_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 11/10/2023 1:36:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID_Pro] [int] IDENTITY(1,1) NOT NULL,
	[Name_Pro] [nvarchar](50) NULL,
	[ID_Cate] [int] NULL,
	[Img_pro] [nvarchar](max) NULL,
	[Price_Pro] [float] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ID_Pro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Size_De]    Script Date: 11/10/2023 1:36:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Size_De](
	[Size_Pro] [int] IDENTITY(1,1) NOT NULL,
	[ID_Cate] [int] NOT NULL,
	[ID_Details] [int] NULL,
	[Size_Name] [nvarchar](500) NULL,
 CONSTRAINT [PK_Size_De] PRIMARY KEY CLUSTERED 
(
	[Size_Pro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AdminUser] ON 

INSERT [dbo].[AdminUser] ([ID_User], [User_Name], [Full_Name], [Email_User], [Phone_Number], [Address], [Password_User], [Role]) VALUES (1, N'admin', NULL, N'admin@gmail.com', N'0995571282', N'Hà Nội', N'E10ADC3949BA59ABBE56E057F20F883E                  ', 1)
INSERT [dbo].[AdminUser] ([ID_User], [User_Name], [Full_Name], [Email_User], [Phone_Number], [Address], [Password_User], [Role]) VALUES (2, N'thanhpham', NULL, N'thanh123@gmail.com', N'0997752127', N'30 Đặng Thùy Trâm', N'FCEA920F7412B5DA7BE0CF42B8C93759                  ', 2)
SET IDENTITY_INSERT [dbo].[AdminUser] OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([ID_Cate], [Name_Cate]) VALUES (1, N'Áo d2')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Color_De] ON 

INSERT [dbo].[Color_De] ([IdColor], [ID_Details], [ColorPr_Name], [Img_Pro]) VALUES (1, 1, N'Xanh', N'avx')
INSERT [dbo].[Color_De] ([IdColor], [ID_Details], [ColorPr_Name], [Img_Pro]) VALUES (2, 2, N'xanh', N'/Uploads/Product/color231109021806751.png')
INSERT [dbo].[Color_De] ([IdColor], [ID_Details], [ColorPr_Name], [Img_Pro]) VALUES (3, 3, N'xanh', N'/Uploads/Product/color231110010257345.png')
SET IDENTITY_INSERT [dbo].[Color_De] OFF
GO
SET IDENTITY_INSERT [dbo].[Details] ON 

INSERT [dbo].[Details] ([ID_Detail], [ID_Pro], [Name_Pro], [Price_Pro], [Img_Pro], [Quantity_Pro]) VALUES (1, 1, N'áo lông 2', 600000, N'avcx', 1)
INSERT [dbo].[Details] ([ID_Detail], [ID_Pro], [Name_Pro], [Price_Pro], [Img_Pro], [Quantity_Pro]) VALUES (2, 1, N'Áo len', 59999, N'/Uploads/Product/dt231109021759604.jpg', 20)
INSERT [dbo].[Details] ([ID_Detail], [ID_Pro], [Name_Pro], [Price_Pro], [Img_Pro], [Quantity_Pro]) VALUES (3, 3, N'Áo len đài xxx', 60000, N'/Uploads/Product/dt231110010257344.png', 1)
SET IDENTITY_INSERT [dbo].[Details] OFF
GO
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([OrderId], [OrderCode], [UserId], [TotalPrice], [Status], [CreatedDate]) VALUES (1, N'09304540', 1, CAST(1 AS Decimal(18, 0)), 1, CAST(N'2023-11-10T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderDetail] ON 

INSERT [dbo].[OrderDetail] ([Order_Detail_Id], [OrderId], [ProductId], [DetailId], [Quantity], [Price]) VALUES (1, 1, 1, 1, 2, CAST(180000 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[OrderDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ID_Pro], [Name_Pro], [ID_Cate], [Img_pro], [Price_Pro]) VALUES (1, N'Áo dài kaka', 1, N'/Uploads/Product/231108235350840.jpg', 90000)
INSERT [dbo].[Product] ([ID_Pro], [Name_Pro], [ID_Cate], [Img_pro], [Price_Pro]) VALUES (3, N'Áo len đài', 1, N'/Uploads/Product/231110010229181.png', 100000)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[Size_De] ON 

INSERT [dbo].[Size_De] ([Size_Pro], [ID_Cate], [ID_Details], [Size_Name]) VALUES (2, 1, 1, N'xxl')
INSERT [dbo].[Size_De] ([Size_Pro], [ID_Cate], [ID_Details], [Size_Name]) VALUES (3, 1, 3, N'11111')
SET IDENTITY_INSERT [dbo].[Size_De] OFF
GO
ALTER TABLE [dbo].[Color_De]  WITH CHECK ADD  CONSTRAINT [FK_Color_De_Details] FOREIGN KEY([ID_Details])
REFERENCES [dbo].[Details] ([ID_Detail])
GO
ALTER TABLE [dbo].[Color_De] CHECK CONSTRAINT [FK_Color_De_Details]
GO
ALTER TABLE [dbo].[Details]  WITH CHECK ADD  CONSTRAINT [FK_Details_Product] FOREIGN KEY([ID_Pro])
REFERENCES [dbo].[Product] ([ID_Pro])
GO
ALTER TABLE [dbo].[Details] CHECK CONSTRAINT [FK_Details_Product]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Details] FOREIGN KEY([UserId])
REFERENCES [dbo].[Details] ([ID_Detail])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Details]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([ID_Cate])
REFERENCES [dbo].[Category] ([ID_Cate])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
ALTER TABLE [dbo].[Size_De]  WITH CHECK ADD  CONSTRAINT [FK_Size_De_Category] FOREIGN KEY([ID_Cate])
REFERENCES [dbo].[Category] ([ID_Cate])
GO
ALTER TABLE [dbo].[Size_De] CHECK CONSTRAINT [FK_Size_De_Category]
GO
ALTER TABLE [dbo].[Size_De]  WITH CHECK ADD  CONSTRAINT [FK_Size_De_Details] FOREIGN KEY([ID_Details])
REFERENCES [dbo].[Details] ([ID_Detail])
GO
ALTER TABLE [dbo].[Size_De] CHECK CONSTRAINT [FK_Size_De_Details]
GO
USE [master]
GO
ALTER DATABASE [Levent] SET  READ_WRITE 
GO
