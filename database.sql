USE [master]
GO
/****** Object:  Database [TranDungShop]    Script Date: 06/04/2021 9:43:37 CH ******/
CREATE DATABASE [TranDungShop]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TranDungShop', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\TranDungShop.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TranDungShop_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\TranDungShop_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [TranDungShop] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TranDungShop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TranDungShop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TranDungShop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TranDungShop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TranDungShop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TranDungShop] SET ARITHABORT OFF 
GO
ALTER DATABASE [TranDungShop] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [TranDungShop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TranDungShop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TranDungShop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TranDungShop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TranDungShop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TranDungShop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TranDungShop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TranDungShop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TranDungShop] SET  ENABLE_BROKER 
GO
ALTER DATABASE [TranDungShop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TranDungShop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TranDungShop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TranDungShop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TranDungShop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TranDungShop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TranDungShop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TranDungShop] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TranDungShop] SET  MULTI_USER 
GO
ALTER DATABASE [TranDungShop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TranDungShop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TranDungShop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TranDungShop] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [TranDungShop] SET DELAYED_DURABILITY = DISABLED 
GO
USE [TranDungShop]
GO
/****** Object:  Table [dbo].[cart]    Script Date: 06/04/2021 9:43:37 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cart](
	[id] [int] NOT NULL,
	[status] [int] NULL,
	[total_price] [float] NULL,
	[customer_id] [int] NOT NULL,
	[date_buy] [datetime] NULL,
	[note] [nvarchar](255) NULL,
	[is_active] [int] NOT NULL,
	[created_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[cart_detail]    Script Date: 06/04/2021 9:43:37 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cart_detail](
	[id] [int] NOT NULL,
	[product_id] [int] NOT NULL,
	[date_buy] [datetime] NULL,
	[sum_price] [float] NULL,
	[quanity] [int] NULL,
	[is_active] [int] NOT NULL,
	[created_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[categories]    Script Date: 06/04/2021 9:43:37 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[categories](
	[id] [int] NOT NULL,
	[name_category] [nvarchar](255) NULL,
	[is_active] [int] NOT NULL,
	[created_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[colors]    Script Date: 06/04/2021 9:43:37 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[colors](
	[id] [int] NOT NULL,
	[code] [int] NULL,
	[color_name] [nvarchar](100) NULL,
	[image_url] [varchar](255) NOT NULL,
	[product_id] [int] NOT NULL,
 CONSTRAINT [pk_color] PRIMARY KEY CLUSTERED 
(
	[id] ASC,
	[image_url] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[customers]    Script Date: 06/04/2021 9:43:37 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[customers](
	[id] [int] NOT NULL,
	[name_customer] [nvarchar](50) NULL,
	[phone] [varchar](15) NULL,
	[pass] [varchar](50) NULL,
	[address_customer] [nvarchar](255) NULL,
	[gender] [int] NULL,
	[DOB] [datetime] NULL,
	[email] [varchar](100) NULL,
	[avatar_url] [varchar](255) NULL,
	[is_active] [int] NOT NULL,
	[created_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[news]    Script Date: 06/04/2021 9:43:37 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[news](
	[id] [int] NOT NULL,
	[summary] [nvarchar](255) NULL,
	[detail] [text] NULL,
	[imageUrl] [varchar](255) NULL,
	[type_new] [int] NULL,
	[is_active] [int] NOT NULL,
	[created_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[order_detail]    Script Date: 06/04/2021 9:43:37 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_detail](
	[id] [int] NOT NULL,
	[product_id] [int] NOT NULL,
	[date_buy] [datetime] NULL,
	[sum_price] [float] NULL,
	[quanity] [int] NULL,
	[is_active] [int] NOT NULL,
	[created_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[orders]    Script Date: 06/04/2021 9:43:37 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orders](
	[id] [int] NOT NULL,
	[status] [int] NULL,
	[total_price] [float] NULL,
	[customer_id] [int] NOT NULL,
	[date_buy] [datetime] NULL,
	[note] [nvarchar](255) NULL,
	[is_active] [int] NOT NULL,
	[created_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[product_categories]    Script Date: 06/04/2021 9:43:37 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product_categories](
	[id] [int] NOT NULL,
	[name_product_category] [nvarchar](255) NULL,
	[category_id] [int] NULL,
	[is_active] [int] NOT NULL,
	[created_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[products]    Script Date: 06/04/2021 9:43:37 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[products](
	[id] [int] NOT NULL,
	[product_name] [nvarchar](255) NULL,
	[price_start] [float] NULL,
	[price_sale] [float] NULL,
	[summary] [nvarchar](255) NULL,
	[description] [nvarchar](255) NULL,
	[detail] [text] NULL,
	[slug] [varchar](255) NULL,
	[size] [char](5) NULL,
	[active] [int] NULL,
	[image_url] [varchar](255) NULL,
	[product_category_id] [int] NOT NULL,
	[is_hot] [int] NULL,
	[is_new] [int] NULL,
	[is_sale] [int] NULL,
	[is_active] [int] NOT NULL,
	[created_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[slides]    Script Date: 06/04/2021 9:43:37 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[slides](
	[id] [int] NOT NULL,
	[summary] [nvarchar](255) NULL,
	[image_url] [varchar](255) NULL,
	[slug] [varchar](255) NULL,
	[is_active] [int] NOT NULL,
	[created_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[users]    Script Date: 06/04/2021 9:43:37 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[users](
	[id] [int] NOT NULL,
	[role] [int] NULL,
	[username] [varchar](100) NULL,
	[pass] [varchar](max) NULL,
	[phone] [varchar](15) NULL,
	[is_active] [int] NOT NULL,
	[created_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[categories] ([id], [name_category], [is_active], [created_at]) VALUES (0, N'Trẻ con', 1, CAST(N'2021-03-16 13:55:10.067' AS DateTime))
INSERT [dbo].[categories] ([id], [name_category], [is_active], [created_at]) VALUES (1, N'Phụ nữ', 0, CAST(N'2021-03-16 13:56:23.163' AS DateTime))
INSERT [dbo].[categories] ([id], [name_category], [is_active], [created_at]) VALUES (2, N'Con trai 2', 1, CAST(N'2021-03-16 14:07:41.140' AS DateTime))
INSERT [dbo].[customers] ([id], [name_customer], [phone], [pass], [address_customer], [gender], [DOB], [email], [avatar_url], [is_active], [created_at]) VALUES (0, N'Nguyễn Văn nam', N'0964443370', N'123456', N'Ha noi', 1, CAST(N'1999-03-23 00:00:00.000' AS DateTime), N'nguyenvannam230399@gmail.com', NULL, 1, NULL)
INSERT [dbo].[customers] ([id], [name_customer], [phone], [pass], [address_customer], [gender], [DOB], [email], [avatar_url], [is_active], [created_at]) VALUES (1, N'Nguyễn Hoàng Phú', N'0985004547', N'234567', N'Ha noi', 1, CAST(N'2008-08-15 00:00:00.000' AS DateTime), N'hoangphu@gmail.com', NULL, 1, NULL)
INSERT [dbo].[news] ([id], [summary], [detail], [imageUrl], [type_new], [is_active], [created_at]) VALUES (8, N'tttttttttttttttt', N'<p>uuuu</p>', N'http://localhost:11111/Uploads/files/12932596_1006237379411951_6532306100321186769_n.png', 1, 1, CAST(N'2021-03-17 10:57:41.590' AS DateTime))
INSERT [dbo].[news] ([id], [summary], [detail], [imageUrl], [type_new], [is_active], [created_at]) VALUES (9, N'tiêu đề', N'mô ta', N'http://localhost:11111/Uploads/files/promo-banner-1.jpg', 1, 1, CAST(N'2021-03-22 19:07:21.650' AS DateTime))
INSERT [dbo].[news] ([id], [summary], [detail], [imageUrl], [type_new], [is_active], [created_at]) VALUES (10, N'tiêu đề men', N'mô ta 1', N'http://localhost:11111/Uploads/files/banner-3.jpg', 1, 1, CAST(N'2021-03-22 19:08:44.893' AS DateTime))
INSERT [dbo].[news] ([id], [summary], [detail], [imageUrl], [type_new], [is_active], [created_at]) VALUES (11, N'tiêu đề', N'mô ta 2', N'http://localhost:11111/Uploads/files/product-2.jpg', 1, 1, CAST(N'2021-03-22 19:10:16.537' AS DateTime))
INSERT [dbo].[product_categories] ([id], [name_product_category], [category_id], [is_active], [created_at]) VALUES (0, N'Áo thun cho bé', 2, 1, CAST(N'2021-03-16 14:56:36.770' AS DateTime))
INSERT [dbo].[product_categories] ([id], [name_product_category], [category_id], [is_active], [created_at]) VALUES (1, N'Áo sơ mi nam', 1, 1, CAST(N'2021-03-16 16:22:58.213' AS DateTime))
INSERT [dbo].[product_categories] ([id], [name_product_category], [category_id], [is_active], [created_at]) VALUES (2, N'Áo thun nam', 1, 1, CAST(N'2021-03-16 16:23:04.677' AS DateTime))
INSERT [dbo].[product_categories] ([id], [name_product_category], [category_id], [is_active], [created_at]) VALUES (3, N'Đầm váy nữ', 0, 1, CAST(N'2021-03-23 13:24:26.140' AS DateTime))
INSERT [dbo].[product_categories] ([id], [name_product_category], [category_id], [is_active], [created_at]) VALUES (4, N'Quần jean nam', 1, 1, CAST(N'2021-03-23 13:24:57.633' AS DateTime))
INSERT [dbo].[product_categories] ([id], [name_product_category], [category_id], [is_active], [created_at]) VALUES (5, N'Áo thun nữ', 0, 1, CAST(N'2021-03-23 13:25:17.510' AS DateTime))
INSERT [dbo].[product_categories] ([id], [name_product_category], [category_id], [is_active], [created_at]) VALUES (6, N'Bộ đồ cho bé', 2, 1, CAST(N'2021-03-23 13:26:07.437' AS DateTime))
INSERT [dbo].[products] ([id], [product_name], [price_start], [price_sale], [summary], [description], [detail], [slug], [size], [active], [image_url], [product_category_id], [is_hot], [is_new], [is_sale], [is_active], [created_at]) VALUES (0, N'áo nữ 1', 213, NULL, NULL, N'adsda', N'<p>13213</p>', NULL, NULL, NULL, N'http://localhost:11111/Uploads/files/12932596_1006237379411951_6532306100321186769_n.png', 3, 0, 0, 0, 0, CAST(N'2021-03-16 15:01:02.367' AS DateTime))
INSERT [dbo].[products] ([id], [product_name], [price_start], [price_sale], [summary], [description], [detail], [slug], [size], [active], [image_url], [product_category_id], [is_hot], [is_new], [is_sale], [is_active], [created_at]) VALUES (1, N'quần áo', 2132, NULL, NULL, N'adsđ', N'<p>s?n ph?m m?i</p>', NULL, NULL, NULL, N'http://localhost:11111/Uploads/files/aonam.jpg', 1, 0, 1, 0, 1, CAST(N'2021-03-16 15:08:06.593' AS DateTime))
INSERT [dbo].[products] ([id], [product_name], [price_start], [price_sale], [summary], [description], [detail], [slug], [size], [active], [image_url], [product_category_id], [is_hot], [is_new], [is_sale], [is_active], [created_at]) VALUES (2, N'áo sơ mi', 200000, NULL, NULL, N'mô tả', N'Ch?t kaki dày d?n, m?n m?m d?m b?o cho các cô gái luôn tho?i mái ho?t d?ng mà không c?m th?y bí bách hay khó ch?u. Ð?c bi?t là thi?t k? don gi?n mang d?n s? tr? trung, nang d?ng v?i màu s?c tuoi tr? và d?c bi?t là k?t h?p màu n?i b?t giúp các nàng luôn th?t n?i b?t gi?a dám dông', NULL, NULL, NULL, N'http://localhost:11111/Uploads/files/polo-shirt-1.png', 1, 1, 0, 1, 1, CAST(N'2021-03-17 10:10:53.583' AS DateTime))
INSERT [dbo].[products] ([id], [product_name], [price_start], [price_sale], [summary], [description], [detail], [slug], [size], [active], [image_url], [product_category_id], [is_hot], [is_new], [is_sale], [is_active], [created_at]) VALUES (3, N'AO NAM', 20000, NULL, NULL, N'FFFFFFFFFFFFF', N'FFFFFFFFFFFF', NULL, NULL, NULL, N'http://localhost:11111/Uploads/files/polo-shirt-5.png', 1, 0, 1, 0, 1, CAST(N'2021-03-18 22:05:58.917' AS DateTime))
INSERT [dbo].[products] ([id], [product_name], [price_start], [price_sale], [summary], [description], [detail], [slug], [size], [active], [image_url], [product_category_id], [is_hot], [is_new], [is_sale], [is_active], [created_at]) VALUES (4, N'áo sơ mi 1', 44444, NULL, NULL, N'GGGGGGGG', N'Ch?t kaki dày d?n, m?n m?m d?m b?o cho các cô gái luôn tho?i mái ho?t d?ng mà không c?m th?y bí bách hay khó ch?u. Ð?c bi?t là thi?t k? don gi?n mang d?n s? tr? trung, nang d?ng v?i màu s?c tuoi tr? và d?c bi?t là k?t h?p màu n?i b?t giúp các nàng luôn th?t n?i b?t gi?a dám dông', NULL, NULL, NULL, N'http://localhost:11111/Uploads/files/aonam.jpg', 1, 1, 0, 1, 1, CAST(N'2021-03-18 22:06:48.793' AS DateTime))
INSERT [dbo].[products] ([id], [product_name], [price_start], [price_sale], [summary], [description], [detail], [slug], [size], [active], [image_url], [product_category_id], [is_hot], [is_new], [is_sale], [is_active], [created_at]) VALUES (5, N'AO NAM 2', 23231, NULL, NULL, N'DDDD', N'Ch?t kaki dày d?n, m?n m?m d?m b?o cho các cô gái luôn tho?i mái ho?t d?ng mà không c?m th?y bí bách hay khó ch?u. Ð?c bi?t là thi?t k? don gi?n mang d?n s? tr? trung, nang d?ng v?i màu s?c tuoi tr? và d?c bi?t là k?t h?p màu n?i b?t giúp các nàng luôn th?t n?i b?t gi?a dám dông', NULL, NULL, NULL, N'http://localhost:11111/Uploads/files/aonam.jpg', 1, 0, 1, 0, 1, CAST(N'2021-03-18 22:09:13.087' AS DateTime))
INSERT [dbo].[products] ([id], [product_name], [price_start], [price_sale], [summary], [description], [detail], [slug], [size], [active], [image_url], [product_category_id], [is_hot], [is_new], [is_sale], [is_active], [created_at]) VALUES (6, N'áo nữ', 1221212, NULL, NULL, N'aaa', N'<p>adsdad</p>', NULL, NULL, NULL, N'http://localhost:11111/Uploads/files/girl-3.png', 3, 1, 0, 0, 1, CAST(N'2021-03-20 09:44:51.163' AS DateTime))
INSERT [dbo].[products] ([id], [product_name], [price_start], [price_sale], [summary], [description], [detail], [slug], [size], [active], [image_url], [product_category_id], [is_hot], [is_new], [is_sale], [is_active], [created_at]) VALUES (7, N'tre con 1', 423434, NULL, NULL, N'adsad', N'<p>ruqeqe</p>', NULL, NULL, NULL, N'http://localhost:11111/Uploads/files/3669507a.jpg', 0, 1, 0, 0, 1, CAST(N'2021-03-20 09:55:58.350' AS DateTime))
INSERT [dbo].[products] ([id], [product_name], [price_start], [price_sale], [summary], [description], [detail], [slug], [size], [active], [image_url], [product_category_id], [is_hot], [is_new], [is_sale], [is_active], [created_at]) VALUES (8, N'tre con 2', 222222, NULL, NULL, N'mota', N'<p>hghghgh</p>', NULL, NULL, NULL, N'http://localhost:11111/Uploads/files/trecon1.jpg', 0, 0, 0, 1, 1, CAST(N'2021-03-20 09:56:32.737' AS DateTime))
INSERT [dbo].[products] ([id], [product_name], [price_start], [price_sale], [summary], [description], [detail], [slug], [size], [active], [image_url], [product_category_id], [is_hot], [is_new], [is_sale], [is_active], [created_at]) VALUES (9, N'áo tre con 3', 222222222, NULL, NULL, N'adasd', N'<p>áddas</p>', NULL, NULL, NULL, N'http://localhost:11111/Uploads/files/images%20(1).jpg', 0, 1, 0, 0, 1, CAST(N'2021-03-20 09:56:56.617' AS DateTime))
INSERT [dbo].[products] ([id], [product_name], [price_start], [price_sale], [summary], [description], [detail], [slug], [size], [active], [image_url], [product_category_id], [is_hot], [is_new], [is_sale], [is_active], [created_at]) VALUES (10, N'áo trẻ con', 3333, NULL, NULL, N'adsa', N'<p>adsda</p>', NULL, NULL, NULL, N'http://localhost:11111/Uploads/files/3669507a(1).jpg', 0, 0, 1, 0, 1, CAST(N'2021-03-20 09:57:56.220' AS DateTime))
INSERT [dbo].[products] ([id], [product_name], [price_start], [price_sale], [summary], [description], [detail], [slug], [size], [active], [image_url], [product_category_id], [is_hot], [is_new], [is_sale], [is_active], [created_at]) VALUES (11, N'AO nữ', 111111, NULL, NULL, N'sdfsf', N'<p>adasdad</p>', NULL, NULL, NULL, N'http://localhost:11111/Uploads/files/aonu1.jpg', 3, 0, 1, 1, 1, CAST(N'2021-03-20 09:59:32.323' AS DateTime))
INSERT [dbo].[products] ([id], [product_name], [price_start], [price_sale], [summary], [description], [detail], [slug], [size], [active], [image_url], [product_category_id], [is_hot], [is_new], [is_sale], [is_active], [created_at]) VALUES (12, N'áo sơ mi nữ', 44444444, NULL, NULL, N'sad', N'Ch?t kaki dày d?n, m?n m?m d?m b?o cho các cô gái luôn tho?i mái ho?t d?ng mà không c?m th?y bí bách hay khó ch?u. Ð?c bi?t là thi?t k? don gi?n mang d?n s? tr? trung, nang d?ng v?i màu s?c tuoi tr? và d?c bi?t là k?t h?p màu n?i b?t giúp các nàng luôn th?t n?i b?t gi?a dám dông', NULL, NULL, NULL, N'http://localhost:11111/Uploads/files/aonu2.jpg', 3, 1, 0, 0, 1, CAST(N'2021-03-20 09:59:50.337' AS DateTime))
INSERT [dbo].[products] ([id], [product_name], [price_start], [price_sale], [summary], [description], [detail], [slug], [size], [active], [image_url], [product_category_id], [is_hot], [is_new], [is_sale], [is_active], [created_at]) VALUES (13, N'áo nữ 1', 555, NULL, NULL, N'ad', N'Ch?t kaki dày d?n, m?n m?m d?m b?o cho các cô gái luôn tho?i mái ho?t d?ng mà không c?m th?y bí bách hay khó ch?u. Ð?c bi?t là thi?t k? don gi?n mang d?n s? tr? trung, nang d?ng v?i màu s?c tuoi tr? và d?c bi?t là k?t h?p màu n?i b?t giúp các nàng luôn th?t n?i b?t gi?a dám dông', NULL, NULL, NULL, N'http://localhost:11111/Uploads/files/f3d55f9907130067b588d47db67fb88d_jpg_720x720q80.jpg', 3, 0, 0, 0, 1, CAST(N'2021-03-20 10:00:12.610' AS DateTime))
INSERT [dbo].[products] ([id], [product_name], [price_start], [price_sale], [summary], [description], [detail], [slug], [size], [active], [image_url], [product_category_id], [is_hot], [is_new], [is_sale], [is_active], [created_at]) VALUES (14, N'áo nữ 23', 4333, NULL, NULL, N'sdfsdfdsfds', N'Ch?t kaki dày d?n, m?n m?m d?m b?o cho các cô gái luôn tho?i mái ho?t d?ng mà không c?m th?y bí bách hay khó ch?u. Ð?c bi?t là thi?t k? don gi?n mang d?n s? tr? trung, nang d?ng v?i màu s?c tuoi tr? và d?c bi?t là k?t h?p màu n?i b?t giúp các nàng luôn th?t n?i b?t gi?a dám dông', NULL, NULL, NULL, N'http://localhost:11111/Uploads/files/aonu1.jpg', 3, 1, 0, 1, 1, CAST(N'2021-03-20 11:13:17.303' AS DateTime))
INSERT [dbo].[products] ([id], [product_name], [price_start], [price_sale], [summary], [description], [detail], [slug], [size], [active], [image_url], [product_category_id], [is_hot], [is_new], [is_sale], [is_active], [created_at]) VALUES (15, N'áo tre con 8', 99900, NULL, NULL, N'hf', N'Ch?t kaki dày d?n, m?n m?m d?m b?o cho các cô gái luôn tho?i mái ho?t d?ng mà không c?m th?y bí bách hay khó ch?u. Ð?c bi?t là thi?t k? don gi?n mang d?n s? tr? trung, nang d?ng v?i màu s?c tuoi tr? và d?c bi?t là k?t h?p màu n?i b?t giúp các nàng luôn th?t n?i b?t gi?a dám dông', NULL, NULL, NULL, N'http://localhost:11111/Uploads/files/images.jpg', 0, 0, 1, 0, 1, CAST(N'2021-03-20 11:14:36.260' AS DateTime))
INSERT [dbo].[products] ([id], [product_name], [price_start], [price_sale], [summary], [description], [detail], [slug], [size], [active], [image_url], [product_category_id], [is_hot], [is_new], [is_sale], [is_active], [created_at]) VALUES (16, N'AO NAM', 120000, NULL, NULL, N'ffff', N'<p>ewewwewewe</p>', NULL, NULL, NULL, N'http://localhost:11111/Uploads/files/product-2(1).jpg', 2, 0, 1, 0, 0, CAST(N'2021-03-23 14:15:46.110' AS DateTime))
INSERT [dbo].[products] ([id], [product_name], [price_start], [price_sale], [summary], [description], [detail], [slug], [size], [active], [image_url], [product_category_id], [is_hot], [is_new], [is_sale], [is_active], [created_at]) VALUES (17, N'bộ quần áo trẻ con vàng', 80000, NULL, NULL, N'vvv', N'vvvv', NULL, NULL, NULL, N'http://localhost:11111/Uploads/files/banner-3.jpg', 0, 0, 1, 1, 0, CAST(N'2021-03-23 14:17:38.097' AS DateTime))
INSERT [dbo].[products] ([id], [product_name], [price_start], [price_sale], [summary], [description], [detail], [slug], [size], [active], [image_url], [product_category_id], [is_hot], [is_new], [is_sale], [is_active], [created_at]) VALUES (18, N'AO NAM 3', 150000, NULL, NULL, N'abc', N'hhhhhh', NULL, NULL, NULL, N'http://localhost:11111/Uploads/files/man-3.jpg', 1, 1, 1, 1, 1, CAST(N'2021-03-24 08:43:10.880' AS DateTime))
INSERT [dbo].[products] ([id], [product_name], [price_start], [price_sale], [summary], [description], [detail], [slug], [size], [active], [image_url], [product_category_id], [is_hot], [is_new], [is_sale], [is_active], [created_at]) VALUES (19, N'áo thô nam', 200000, NULL, NULL, N'áo thô nam', N'áo thô nam', NULL, NULL, NULL, N'http://localhost:11111/Uploads/files/man-4.jpg', 2, 1, 1, 1, 1, CAST(N'2021-03-24 08:45:15.087' AS DateTime))
INSERT [dbo].[products] ([id], [product_name], [price_start], [price_sale], [summary], [description], [detail], [slug], [size], [active], [image_url], [product_category_id], [is_hot], [is_new], [is_sale], [is_active], [created_at]) VALUES (20, N'áo sơ mi', 1231, NULL, NULL, N'fgd', N'?ewr', NULL, NULL, NULL, N'http://localhost:11111/Uploads/files/aonu1.jpg', 1, 0, 0, 1, 1, CAST(N'2021-03-25 08:50:46.513' AS DateTime))
INSERT [dbo].[slides] ([id], [summary], [image_url], [slug], [is_active], [created_at]) VALUES (0, NULL, N'http://localhost:11111/Uploads/files/18222010_1661011120601237_4773318581788694666_n.png', NULL, 1, CAST(N'2021-03-12 22:20:33.517' AS DateTime))
INSERT [dbo].[slides] ([id], [summary], [image_url], [slug], [is_active], [created_at]) VALUES (1, NULL, N'http://localhost:11111/Uploads/files/12932596_1006237379411951_6532306100321186769_n.png', NULL, 1, CAST(N'2021-03-12 22:23:06.670' AS DateTime))
INSERT [dbo].[slides] ([id], [summary], [image_url], [slug], [is_active], [created_at]) VALUES (2, NULL, N'http://localhost:11111/Uploads/files/37873739_1811556552213359_3884005721104187392_n.png', NULL, 0, CAST(N'2021-03-12 22:53:56.433' AS DateTime))
INSERT [dbo].[slides] ([id], [summary], [image_url], [slug], [is_active], [created_at]) VALUES (3, NULL, N'http://localhost:11111/Uploads/files/120365990_3376233409078991_6358463720552846414_o.jpg', NULL, 0, CAST(N'2021-03-12 22:54:08.560' AS DateTime))
INSERT [dbo].[slides] ([id], [summary], [image_url], [slug], [is_active], [created_at]) VALUES (4, NULL, N'http://localhost:11111/Uploads/files/33780712_1723367331032282_7420225301478637568_n.png', NULL, 0, CAST(N'2021-03-13 08:22:48.813' AS DateTime))
INSERT [dbo].[users] ([id], [role], [username], [pass], [phone], [is_active], [created_at]) VALUES (0, 1, N'phu', N'$2a$10$Dlfy87gxaKHnEWb8GQqJruwdalCWJ62Bs6BEeCIzGoVTiNygfk2y.', N'0964443370', 0, CAST(N'2021-03-12 16:29:03.507' AS DateTime))
INSERT [dbo].[users] ([id], [role], [username], [pass], [phone], [is_active], [created_at]) VALUES (1, 1, N'nam', N'$2a$10$QP1JOqCkI1A5QaPjV/6/HuYzDv2n5q3ni.HDpie8VeQDqtgRS2QRG', N'123456', 1, NULL)
INSERT [dbo].[users] ([id], [role], [username], [pass], [phone], [is_active], [created_at]) VALUES (2, 1, N'hello', N'$2a$10$Kl10wJCzIDDNkgkjXPqmbOXc6Dg0KmgibiFrsSt1jXNgMPfKGKoLy', N'0964443390', 1, CAST(N'2021-03-12 17:07:45.313' AS DateTime))
INSERT [dbo].[users] ([id], [role], [username], [pass], [phone], [is_active], [created_at]) VALUES (3, 1, N'nhungnth', N'$2a$10$83HDhsccIfnHAA4FhzF9c.kzRVTZmju1TFfgiYUG/sD3O22Frj9F.', N'0985298565', 1, CAST(N'2021-03-13 08:24:53.870' AS DateTime))
INSERT [dbo].[users] ([id], [role], [username], [pass], [phone], [is_active], [created_at]) VALUES (4, 1, N'hahaa', N'$2a$10$x/wkWckJc0cRzK7ZaH9wYen8YNyGoUcicqGeg.8FYxdHyTDJ1PALm', N'0964443370', 0, CAST(N'2021-03-13 15:49:31.347' AS DateTime))
ALTER TABLE [dbo].[cart]  WITH CHECK ADD  CONSTRAINT [fk_cus] FOREIGN KEY([customer_id])
REFERENCES [dbo].[customers] ([id])
GO
ALTER TABLE [dbo].[cart] CHECK CONSTRAINT [fk_cus]
GO
ALTER TABLE [dbo].[cart_detail]  WITH CHECK ADD  CONSTRAINT [fk_pro_cart] FOREIGN KEY([product_id])
REFERENCES [dbo].[products] ([id])
GO
ALTER TABLE [dbo].[cart_detail] CHECK CONSTRAINT [fk_pro_cart]
GO
ALTER TABLE [dbo].[colors]  WITH CHECK ADD  CONSTRAINT [fk_clor] FOREIGN KEY([product_id])
REFERENCES [dbo].[products] ([id])
GO
ALTER TABLE [dbo].[colors] CHECK CONSTRAINT [fk_clor]
GO
ALTER TABLE [dbo].[order_detail]  WITH CHECK ADD  CONSTRAINT [fk_pro] FOREIGN KEY([product_id])
REFERENCES [dbo].[products] ([id])
GO
ALTER TABLE [dbo].[order_detail] CHECK CONSTRAINT [fk_pro]
GO
ALTER TABLE [dbo].[orders]  WITH CHECK ADD  CONSTRAINT [fk_cus_order] FOREIGN KEY([customer_id])
REFERENCES [dbo].[customers] ([id])
GO
ALTER TABLE [dbo].[orders] CHECK CONSTRAINT [fk_cus_order]
GO
ALTER TABLE [dbo].[product_categories]  WITH CHECK ADD  CONSTRAINT [fk_cate] FOREIGN KEY([category_id])
REFERENCES [dbo].[categories] ([id])
GO
ALTER TABLE [dbo].[product_categories] CHECK CONSTRAINT [fk_cate]
GO
ALTER TABLE [dbo].[products]  WITH CHECK ADD  CONSTRAINT [fk_proCate] FOREIGN KEY([product_category_id])
REFERENCES [dbo].[product_categories] ([id])
GO
ALTER TABLE [dbo].[products] CHECK CONSTRAINT [fk_proCate]
GO
USE [master]
GO
ALTER DATABASE [TranDungShop] SET  READ_WRITE 
GO
