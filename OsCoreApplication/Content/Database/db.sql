USE [master]
GO
/****** Object:  Database [HoangLienSonDB]    Script Date: 7/4/2020 12:42:01 PM ******/
CREATE DATABASE [HoangLienSonDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HoangLienSonDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\HoangLienSonDB.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'HoangLienSonDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\HoangLienSonDB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [HoangLienSonDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HoangLienSonDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HoangLienSonDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HoangLienSonDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HoangLienSonDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HoangLienSonDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HoangLienSonDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [HoangLienSonDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [HoangLienSonDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HoangLienSonDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HoangLienSonDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HoangLienSonDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HoangLienSonDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HoangLienSonDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HoangLienSonDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HoangLienSonDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HoangLienSonDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [HoangLienSonDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HoangLienSonDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HoangLienSonDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HoangLienSonDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HoangLienSonDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HoangLienSonDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [HoangLienSonDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HoangLienSonDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [HoangLienSonDB] SET  MULTI_USER 
GO
ALTER DATABASE [HoangLienSonDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HoangLienSonDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HoangLienSonDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HoangLienSonDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [HoangLienSonDB] SET DELAYED_DURABILITY = DISABLED 
GO
USE [HoangLienSonDB]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 7/4/2020 12:42:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](500) NULL,
	[Password] [nvarchar](500) NULL,
	[Email] [nvarchar](500) NULL,
	[DateCreated] [datetime] NULL CONSTRAINT [DF_Account_DateCreated]  DEFAULT (getdate()),
	[DateUpdated] [datetime] NULL,
	[UserCreated] [bigint] NULL,
	[UserUpdated] [bigint] NULL,
	[IsAdmin] [bit] NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Banner]    Script Date: 7/4/2020 12:42:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Banner](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[ImageUrl] [nvarchar](500) NULL,
	[IsShow] [bit] NULL,
	[DateCreated] [datetime] NULL,
	[DateUpdated] [datetime] NULL,
	[UserCreated] [bigint] NULL,
	[UserUpdated] [bigint] NULL,
 CONSTRAINT [PK_Banner] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Category]    Script Date: 7/4/2020 12:42:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[idCategory] [bigint] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](500) NULL,
	[DateCreated] [datetime] NULL,
	[DateUpdated] [datetime] NULL,
	[UserCreated] [bigint] NULL,
	[UserUpdated] [bigint] NULL,
 CONSTRAINT [PK_HLS_Category] PRIMARY KEY CLUSTERED 
(
	[idCategory] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ConfigWeb]    Script Date: 7/4/2020 12:42:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConfigWeb](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[KeyConfig] [nvarchar](50) NULL,
	[Type] [nvarchar](50) NULL,
	[ValueConfig] [nvarchar](500) NULL,
	[NameConfig] [nvarchar](500) NULL,
	[DateCreated] [datetime] NULL CONSTRAINT [DF_ConfigWeb_DateCreated]  DEFAULT (getdate()),
	[DateUpdated] [datetime] NULL,
	[UserCreated] [bigint] NULL,
	[UserUpdated] [bigint] NULL,
 CONSTRAINT [PK_ConfigWeb] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Contact]    Script Date: 7/4/2020 12:42:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Email] [nvarchar](500) NULL,
	[Subject] [nvarchar](500) NULL,
	[ContentMsg] [nvarchar](2000) NULL,
	[DateContact] [datetime] NULL CONSTRAINT [DF_Contact_DateContact]  DEFAULT (getdate()),
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[News]    Script Date: 7/4/2020 12:42:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[News](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[NewsTitle] [nvarchar](500) NULL,
	[ShortDescription] [nvarchar](500) NULL,
	[NewsContent] [ntext] NULL,
	[Thumb] [nvarchar](500) NULL,
	[DateCreated] [datetime] NULL CONSTRAINT [DF_News_DateCreated]  DEFAULT (getdate()),
	[DateUpdate] [datetime] NULL,
	[UserCreated] [bigint] NULL,
	[UserUpdate] [bigint] NULL,
	[IsShow] [bit] NULL,
	[idNewsCategory] [bigint] NULL,
 CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NewsCategory]    Script Date: 7/4/2020 12:42:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NewsCategory](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[NewsCategoryName] [nvarchar](500) NULL,
	[DateCreated] [datetime] NULL CONSTRAINT [DF_NewsCategory_DateCreated]  DEFAULT (getdate()),
	[DateUpdated] [datetime] NULL,
	[UserCreated] [bigint] NULL,
	[UserUpdated] [bigint] NULL,
 CONSTRAINT [PK_NewsCategory] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Project]    Script Date: 7/4/2020 12:42:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[ProjectName] [nvarchar](500) NULL,
	[Address] [nvarchar](500) NULL,
	[Acreage] [nvarchar](500) NULL,
	[Price] [nvarchar](500) NULL,
	[Direction] [nvarchar](500) NULL,
	[Width] [nvarchar](500) NULL,
	[Height] [nvarchar](500) NULL,
	[Juridical] [nvarchar](500) NULL,
	[Thumbnail] [nvarchar](500) NULL,
	[Description] [ntext] NULL,
	[DateCreated] [datetime] NULL CONSTRAINT [DF_Project_DateCreated]  DEFAULT (getdate()),
	[DateUpdated] [datetime] NULL,
	[UserCreated] [bigint] NULL,
	[UserUpdated] [bigint] NULL,
	[IsShow] [bit] NULL CONSTRAINT [DF_Project_IsShow]  DEFAULT ((1)),
	[IsHighlights] [bit] NULL,
	[idType] [bigint] NULL,
	[idCategory] [bigint] NULL,
	[NumerOfBuilding] [nvarchar](50) NULL,
	[NumberOfFloors] [nvarchar](50) NULL,
	[Type] [nvarchar](200) NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([id], [Username], [Password], [Email], [DateCreated], [DateUpdated], [UserCreated], [UserUpdated], [IsAdmin]) VALUES (2, N'admins', N'123456789', NULL, CAST(N'2020-07-02 09:01:32.920' AS DateTime), NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Account] OFF
SET IDENTITY_INSERT [dbo].[Banner] ON 

INSERT [dbo].[Banner] ([id], [ImageUrl], [IsShow], [DateCreated], [DateUpdated], [UserCreated], [UserUpdated]) VALUES (2, N'/Content/Images/Banner/Banner2.jpg', 1, CAST(N'2020-06-24 00:00:00.000' AS DateTime), NULL, 1, NULL)
INSERT [dbo].[Banner] ([id], [ImageUrl], [IsShow], [DateCreated], [DateUpdated], [UserCreated], [UserUpdated]) VALUES (3, N'/Content/Images/Banner/Banner1.jpg', 1, CAST(N'2020-06-24 00:00:00.000' AS DateTime), NULL, 1, NULL)
INSERT [dbo].[Banner] ([id], [ImageUrl], [IsShow], [DateCreated], [DateUpdated], [UserCreated], [UserUpdated]) VALUES (4, N'/Content/Images/Banner/Banner2.jpg', 1, CAST(N'2020-06-24 00:00:00.000' AS DateTime), NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Banner] OFF
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([idCategory], [CategoryName], [DateCreated], [DateUpdated], [UserCreated], [UserUpdated]) VALUES (1, N'Nhà đất cần bán', CAST(N'2020-06-24 00:00:00.000' AS DateTime), NULL, 1, NULL)
INSERT [dbo].[Category] ([idCategory], [CategoryName], [DateCreated], [DateUpdated], [UserCreated], [UserUpdated]) VALUES (2, N'Nhà đất cho thuê', CAST(N'2020-06-24 00:00:00.000' AS DateTime), NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[Category] OFF
SET IDENTITY_INSERT [dbo].[ConfigWeb] ON 

INSERT [dbo].[ConfigWeb] ([id], [KeyConfig], [Type], [ValueConfig], [NameConfig], [DateCreated], [DateUpdated], [UserCreated], [UserUpdated]) VALUES (1, N'SLG_ABOUT', N'HOME', N'Bất động sản Hoàng Liên Sơn sẽ hỗ trợ mọi nhu cầu của bạn', N'Slogon', CAST(N'2020-06-24 15:09:36.723' AS DateTime), CAST(N'2020-07-02 16:35:25.387' AS DateTime), NULL, NULL)
INSERT [dbo].[ConfigWeb] ([id], [KeyConfig], [Type], [ValueConfig], [NameConfig], [DateCreated], [DateUpdated], [UserCreated], [UserUpdated]) VALUES (2, N'CONTENT_ABOUT', N'HOME', N'Với nhiều năm kinh nghiệm trong lĩnh vực bất động sản cùng đội ngũ nhân lực chuyên nghiệp, bất động sản Hoàng Liên Sơn sẽ hỗ trợ mua bán, tư vấn, tìm hiểu về bất động sản mọi lúc mọi nơi. Hãy liên hệ ngay với chúng tôi nếu bạn có nhu cầu.', N'Nội Dung Giới Thiệu', CAST(N'2020-06-24 15:09:36.723' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ConfigWeb] ([id], [KeyConfig], [Type], [ValueConfig], [NameConfig], [DateCreated], [DateUpdated], [UserCreated], [UserUpdated]) VALUES (3, N'CONTACT_INFO_PHONE', N'LAYOUT', N'0343324610', N'Điện thoại liên hệ', CAST(N'2020-06-25 14:34:17.200' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ConfigWeb] ([id], [KeyConfig], [Type], [ValueConfig], [NameConfig], [DateCreated], [DateUpdated], [UserCreated], [UserUpdated]) VALUES (4, N'CONTACT_INFO_MAIL', N'LAYOUT', N'bdsHoangLienSon@gmail.com', N'Email liên lệ', CAST(N'2020-06-25 14:35:29.023' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ConfigWeb] ([id], [KeyConfig], [Type], [ValueConfig], [NameConfig], [DateCreated], [DateUpdated], [UserCreated], [UserUpdated]) VALUES (5, N'CONTACT_TIMEWORK1', N'LAYOUT', N'7 9.00 am - 8.00 pm', N'Giờ làm việc 1 (Thứ 2 - Thứ 6)', CAST(N'2020-06-25 14:36:13.230' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ConfigWeb] ([id], [KeyConfig], [Type], [ValueConfig], [NameConfig], [DateCreated], [DateUpdated], [UserCreated], [UserUpdated]) VALUES (6, N'CONTACT_TIMEWORK2', N'LAYOUT', N'9.00 am - 12.00 pm', N'Giờ làm việc 2 (Chủ nhật)', CAST(N'2020-06-25 14:36:14.370' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ConfigWeb] ([id], [KeyConfig], [Type], [ValueConfig], [NameConfig], [DateCreated], [DateUpdated], [UserCreated], [UserUpdated]) VALUES (7, N'CONTACT_ADDRESS', N'LAYOUT', N'123 Phạm Văn Đồng, Thủ Đức, HCM', N'Địa chỉ', CAST(N'2020-06-25 14:37:10.213' AS DateTime), NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ConfigWeb] OFF
SET IDENTITY_INSERT [dbo].[Contact] ON 

INSERT [dbo].[Contact] ([id], [Name], [Email], [Subject], [ContentMsg], [DateContact]) VALUES (3, N'Phạm Thị Thu Hà', N'pvdao.utc2@gmail.com', N'Daocoiutc2', N'Excvef', CAST(N'2020-07-01 13:31:09.270' AS DateTime))
INSERT [dbo].[Contact] ([id], [Name], [Email], [Subject], [ContentMsg], [DateContact]) VALUES (4, N'Trần Van Huy', N'0343324610', N'Pham Van Dao', N'123456', CAST(N'2020-07-01 13:46:46.127' AS DateTime))
INSERT [dbo].[Contact] ([id], [Name], [Email], [Subject], [ContentMsg], [DateContact]) VALUES (5, N'Trần Thah Hóa', N'03433246215', N'Hỏi Giá Đất 1', N'Giá đất nhiêu???', CAST(N'2020-07-03 09:30:37.377' AS DateTime))
INSERT [dbo].[Contact] ([id], [Name], [Email], [Subject], [ContentMsg], [DateContact]) VALUES (6, N'Lê Thu Hà', N'03433246215', N'Hỏi Giá Đất 2', N'eyrurtyrtgrgre', CAST(N'2020-07-03 09:30:54.467' AS DateTime))
INSERT [dbo].[Contact] ([id], [Name], [Email], [Subject], [ContentMsg], [DateContact]) VALUES (7, N'Trần Văn Tâm', N'03433246215', N'Hỏi Giá Đất 3', N'bfngngnc', CAST(N'2020-07-03 09:30:59.887' AS DateTime))
INSERT [dbo].[Contact] ([id], [Name], [Email], [Subject], [ContentMsg], [DateContact]) VALUES (8, N'Phạm Quốc Huy', N'03433246215', N'Hỏi Giá Đất 5', N'vcncvnvn', CAST(N'2020-07-03 09:31:04.467' AS DateTime))
INSERT [dbo].[Contact] ([id], [Name], [Email], [Subject], [ContentMsg], [DateContact]) VALUES (9, N'Lê Thanh Nghĩa', N'03433246215', N'fdbdbfd', N'bcbcv', CAST(N'2020-07-03 09:31:08.320' AS DateTime))
SET IDENTITY_INSERT [dbo].[Contact] OFF
SET IDENTITY_INSERT [dbo].[News] ON 

INSERT [dbo].[News] ([id], [NewsTitle], [ShortDescription], [NewsContent], [Thumb], [DateCreated], [DateUpdate], [UserCreated], [UserUpdate], [IsShow], [idNewsCategory]) VALUES (1, N'Thời điểm vàng sở hữu biệt thự liền kề The Mansions ParkCity Hanoi', N'Theo thông tin từ chủ đầu tư, đợt mở bán cuối cùng chỉ còn 18 căn biệt thự, nhà vườn liền kề 4 tầng cao cấp nhất phân khu phân khu The Mansions, được trang bị thang máy kính với nhiều cải tiến tinh tế, đẳng cấp.', N'<h3 class="centeredText"><span style="text-decoration: underline; color: #e34f26;">Click here</span> to edit this demo</h3>
<!-- This comment is visible only in the source editor -->
<p><img class="imageRight" style="width: 53px; height: 60px;" src="https://htmleditor.tools/img/html-editor-tools.png" alt="HTML editor tools" />This is a demo text where you can experiment with the <strong class="orangeText">HTML Editor&nbsp;</strong>features. See how the visual and source editors are instantly changing each other.</p>
<p>Test the cleaning options and the toolbars of the two editors.</p>
<p>&nbsp;</p>
<table style="height: 164px;" width="378">
<tbody>
<tr>
<td colspan="2"><strong>This is a table.</strong></td>
</tr>
<tr>
<td>HTML <span style="text-decoration: underline;">image</span>:</td>
<td style="text-align: center; width: 266px;"><img src="https://htmleditor.tools/img/smiley.png" alt="smiley" />&nbsp;<em>Hi, I am &Ouml;d&ouml;nke!</em></td>
</tr>
<tr>
<td>A text link:</td>
<td>&nbsp; &nbsp; <a href="https://htmlg.com/">Click here</a> for more features!&nbsp;</td>
</tr>
</tbody>
</table>
<p>Copy-paste any document in the WYSIWYG editor to <span class="orangeText">convert</span> it to HTML code.</p>', N'/Content/Images/ThumbProject/DatNen2.jpg', CAST(N'2020-06-28 00:00:00.000' AS DateTime), NULL, 1, NULL, 1, 1)
INSERT [dbo].[News] ([id], [NewsTitle], [ShortDescription], [NewsContent], [Thumb], [DateCreated], [DateUpdate], [UserCreated], [UserUpdate], [IsShow], [idNewsCategory]) VALUES (2, N'Andochine Phú Quốc - giải cơn khát biệt thự biển cao cấp ở Phú Quốc', N'Andochine là dự án biệt thự hiếm hoi tại đất vàng Bãi Trường - một quỹ đất xa xỉ với mô hình biệt thự.', NULL, N'/Content/Images/ThumbProject/DuAn4.jpg', CAST(N'2020-06-28 00:00:00.000' AS DateTime), NULL, 1, NULL, 1, 2)
INSERT [dbo].[News] ([id], [NewsTitle], [ShortDescription], [NewsContent], [Thumb], [DateCreated], [DateUpdate], [UserCreated], [UserUpdate], [IsShow], [idNewsCategory]) VALUES (3, N'Chất lượng không khí ở Ecopark tương đương với New Zealand', N'Ecopark cho biết, theo kết quả đo lường chất lượng không khí của ứng dụng PamAir, chất lượng không khí ở khu đô thị Ecopark tương đương với với New Zealand - một trong những nơi có không khí trong lành hàng đầu thế giới.', NULL, N'/Content/Images/ThumbProject/DuAn8.jpg', CAST(N'2020-06-28 00:00:00.000' AS DateTime), NULL, 1, NULL, 1, 1)
INSERT [dbo].[News] ([id], [NewsTitle], [ShortDescription], [NewsContent], [Thumb], [DateCreated], [DateUpdate], [UserCreated], [UserUpdate], [IsShow], [idNewsCategory]) VALUES (4, N'Giữa Thủ đô, tổ hợp gần 5.000 tỷ chờ hợp thức sai phạm cho dân về ở', N'Dù đang trong quá trình xin điều chỉnh phương án kiến trúc và khắc phục vi phạm xây dựng nhưng tại dự án Hinode City (201 Minh Khai) hàng trăm căn hộ có cư dân về ở, bất chấp an toàn và quy định pháp luật.', NULL, N'/Content/Images/ThumbProject/DuAn4.jpg', CAST(N'2020-06-28 00:00:00.000' AS DateTime), NULL, 1, NULL, 1, 2)
INSERT [dbo].[News] ([id], [NewsTitle], [ShortDescription], [NewsContent], [Thumb], [DateCreated], [DateUpdate], [UserCreated], [UserUpdate], [IsShow], [idNewsCategory]) VALUES (5, N'BĐS quận 2 tăng tốc, đón đầu đà tăng trưởng', N'Theo các chuyên gia, khu vực “hạt nhân” quận 2 sẽ dẫn đầu làn sóng tăng trưởng BĐS mới của “thành phố phía Đông”.', NULL, N'/Content/Images/ThumbProject/DuAn9.jpg', CAST(N'2020-06-28 00:00:00.000' AS DateTime), NULL, 1, NULL, 1, 3)
INSERT [dbo].[News] ([id], [NewsTitle], [ShortDescription], [NewsContent], [Thumb], [DateCreated], [DateUpdate], [UserCreated], [UserUpdate], [IsShow], [idNewsCategory]) VALUES (6, N'Tư vấn thiết kế cải tạo căn hộ chung cư diện tích 79m² với tổng chi phí 140 triệu đồng', N'Gia đình gia chủ gồm 3 người và muốn kiến trúc sư tư vấn thiết kế phòng khách và bếp gian liên thông với nhau để tạo không gian thoáng đãng, rộng rãi, trẻ trung.', NULL, N'/Content/Images/ThumbProject/DatNen2.jpg', CAST(N'2020-06-28 00:00:00.000' AS DateTime), NULL, 1, NULL, 1, 2)
INSERT [dbo].[News] ([id], [NewsTitle], [ShortDescription], [NewsContent], [Thumb], [DateCreated], [DateUpdate], [UserCreated], [UserUpdate], [IsShow], [idNewsCategory]) VALUES (7, N'
Bộ Xây dựng đề xuất cho người nước ngoài mua bất động sản du lịch', N'Bộ Xây dựng đề xuất sửa đổi, bổ sung Luật nhà ở, Luật kinh doanh bất động sản (BĐS) 2014 theo hướng cho phép tổ chức, cá nhân nước ngoài được mua BĐS du lịch.', NULL, N'/Content/Images/ThumbProject/DuAn4.jpg', CAST(N'2020-06-28 00:00:00.000' AS DateTime), NULL, 1, NULL, 1, 3)
INSERT [dbo].[News] ([id], [NewsTitle], [ShortDescription], [NewsContent], [Thumb], [DateCreated], [DateUpdate], [UserCreated], [UserUpdate], [IsShow], [idNewsCategory]) VALUES (8, N'Những lý do để đầu tư shop thương mại dịch vụ', N'Trên thế giới, shop thương mại dịch vụ là một khái niệm phổ biến đối với giới đầu tư. Ở Hong Kong hay Singapore, các BĐS này có thời hạn sở hữu dao động từ 50 hoặc 70 năm nhưng giới đầu tư vẫn săn đón.', NULL, N'/Content/Images/ThumbProject/DatNen2.jpg', CAST(N'2020-06-28 00:00:00.000' AS DateTime), NULL, 1, NULL, 1, 2)
INSERT [dbo].[News] ([id], [NewsTitle], [ShortDescription], [NewsContent], [Thumb], [DateCreated], [DateUpdate], [UserCreated], [UserUpdate], [IsShow], [idNewsCategory]) VALUES (9, N'Tổ hợp đa tiện ích phong cách Singapore ở Thái Nguyên', N'Ngày 19/6/2020 Tecco Elite City - dự án tổ hợp đa tiện ích phong cách Singapore ra mắt thị trường BĐS Thái Nguyên, hứa hẹn mang lại phong cách sống đẳng cấp hoàn toàn mớ', NULL, N'/Content/Images/ThumbProject/DatNen2.jpg', CAST(N'2020-06-28 00:00:00.000' AS DateTime), NULL, 1, NULL, 1, 1)
INSERT [dbo].[News] ([id], [NewsTitle], [ShortDescription], [NewsContent], [Thumb], [DateCreated], [DateUpdate], [UserCreated], [UserUpdate], [IsShow], [idNewsCategory]) VALUES (10, N'Hơn 3.000 tỷ đồng làm 3 sân golf tại Bắc Giang, Hòa Bình', N'3 dự án sân golf gồm sân golf nghỉ dưỡng Bắc Giang 740 tỷ đồng, sân golf  Việt Yên 1.214 tỷ đồng và sân golf  Phúc Tiến (Hòa Bình) có tổng mức đầu tư 1.137 tỷ đồng.', NULL, N'/Content/Images/ThumbProject/DatNen2.jpg', CAST(N'2020-06-28 00:00:00.000' AS DateTime), NULL, 1, NULL, 1, 2)
INSERT [dbo].[News] ([id], [NewsTitle], [ShortDescription], [NewsContent], [Thumb], [DateCreated], [DateUpdate], [UserCreated], [UserUpdate], [IsShow], [idNewsCategory]) VALUES (11, N'Cặp vợ chồng trẻ tạo bất ngờ khi sửa nhà phố xập xệ tối tăm thành không gian hiện đại, tiện nghi', N'Chỉ trong vòng vài tháng, cặp vợ chồng người Thái đã lên kế hoạch sửa chữa lại căn nhà phố cũ vốn tồi tàn, dột nát thành không gian đẹp hiện đại với từng góc nhỏ tiện nghi ai ngắm cũng mê mẩn.', NULL, N'/Content/Images/ThumbProject/DuAn5.jpg', CAST(N'2020-06-28 00:00:00.000' AS DateTime), NULL, 1, NULL, 1, 2)
INSERT [dbo].[News] ([id], [NewsTitle], [ShortDescription], [NewsContent], [Thumb], [DateCreated], [DateUpdate], [UserCreated], [UserUpdate], [IsShow], [idNewsCategory]) VALUES (12, N'Decor phòng lung linh nhờ các món đồ xinh xắn vừa rẻ vừa đẹp', N'Những ý tưởng siêu tiết kiệm nhưng mang đến hiệu quả bất ngờ cho nhà bạn.', NULL, N'/Content/Images/ThumbProject/DuAn6.jpg', CAST(N'2020-06-28 00:00:00.000' AS DateTime), NULL, 1, NULL, 1, 3)
INSERT [dbo].[News] ([id], [NewsTitle], [ShortDescription], [NewsContent], [Thumb], [DateCreated], [DateUpdate], [UserCreated], [UserUpdate], [IsShow], [idNewsCategory]) VALUES (13, N'TP.HCM chỉ đạo thanh tra làm rõ sai phạm tại dự án Khu dân cư Tân Hải Minh', N'Từ đơn tố cáo của các hộ dân và sau loạt bài phản ánh của VietNamNet, UBND TP.HCM đã chỉ đạo Thanh tra Thành phố chủ trì, phối hợp cùng các cơ quan liên quan kiểm tra dấu hiệu sai phạm tại dự án Khu dân cư Tân Hải Minh. ', NULL, N'/Content/Images/ThumbProject/DuAn4.jpg', CAST(N'2020-06-28 00:00:00.000' AS DateTime), NULL, 1, NULL, 1, 1)
INSERT [dbo].[News] ([id], [NewsTitle], [ShortDescription], [NewsContent], [Thumb], [DateCreated], [DateUpdate], [UserCreated], [UserUpdate], [IsShow], [idNewsCategory]) VALUES (14, N'Chuyện lạ: Quận cho điều chỉnh quy hoạch theo bản đồ giả của chủ đầu tư', N'Chủ đầu tư Tân Hải Minh cung cấp bản đồ quy hoạch giả để lấy đất công viên làm trụ sở Ban điều hành khu phố. Điều lạ, dù có các phòng chuyên môn tham mưu nhưng UBND quận Thủ Đức vẫn không phát hiện vụ việc gian dối này? ', NULL, N'/Content/Images/ThumbProject/DatNen2.jpg', CAST(N'2020-06-28 00:00:00.000' AS DateTime), NULL, 1, NULL, 1, 2)
INSERT [dbo].[News] ([id], [NewsTitle], [ShortDescription], [NewsContent], [Thumb], [DateCreated], [DateUpdate], [UserCreated], [UserUpdate], [IsShow], [idNewsCategory]) VALUES (16, NULL, NULL, NULL, NULL, CAST(N'2020-07-04 11:46:07.790' AS DateTime), NULL, 1, NULL, 1, 0)
INSERT [dbo].[News] ([id], [NewsTitle], [ShortDescription], [NewsContent], [Thumb], [DateCreated], [DateUpdate], [UserCreated], [UserUpdate], [IsShow], [idNewsCategory]) VALUES (17, NULL, NULL, NULL, NULL, CAST(N'2020-07-04 12:23:44.730' AS DateTime), NULL, 1, NULL, 1, 0)
INSERT [dbo].[News] ([id], [NewsTitle], [ShortDescription], [NewsContent], [Thumb], [DateCreated], [DateUpdate], [UserCreated], [UserUpdate], [IsShow], [idNewsCategory]) VALUES (18, NULL, NULL, NULL, NULL, CAST(N'2020-07-04 12:28:40.907' AS DateTime), NULL, 1, NULL, 1, 0)
INSERT [dbo].[News] ([id], [NewsTitle], [ShortDescription], [NewsContent], [Thumb], [DateCreated], [DateUpdate], [UserCreated], [UserUpdate], [IsShow], [idNewsCategory]) VALUES (19, N'Lật tẩy chiêu thức phân lô bán nền đất nông nghiệp của nữ đại gia ở Bình Dương', N'sfsfsfsdf gsdfsdf fsdfsd s', N'<p>dsfsdfsd sdfsdf sfsfsdf sfsfsdf sfsdfsdf sdfsfsdf sfsfsdf sdfsfsdf sdfsfsdf sfsfsdfsd sfsdfdsf sfsdfsdf sdfsfs</p>
', N'/Content/Images/ThumbNews/xuong-pho-1593832079320585804180-crop-15938321010212105882546.jpg', CAST(N'2020-07-04 12:35:21.910' AS DateTime), NULL, 1, NULL, 1, 2)
INSERT [dbo].[News] ([id], [NewsTitle], [ShortDescription], [NewsContent], [Thumb], [DateCreated], [DateUpdate], [UserCreated], [UserUpdate], [IsShow], [idNewsCategory]) VALUES (20, N'CapitalHouse muốn miễn nhiệm Chủ tịch Nguyễn Thành Trung', N'CTCP Đầu tư và Thương mại Thủ Đô đang xin ý kiến cổ đông về việc miễn nhiệm 2 thành viên HĐQT, 2 thành viên BKS. Động thái này diễn ra chỉ vài tháng sau phiên họp ĐHĐCĐ thường niên năm 2020.', N'<p><strong><span style="background-color:#1abc9c">Theo t&igrave;m hiểu của PV, &ocirc;ng Nguyễn Th&agrave;nh Trung đ&atilde; gắn b&oacute; với CapitalHouse từ năm 2008 tới nay. Tới đầu năm 2018, &ocirc;ng Trung đ&atilde; tr&uacute;ng cử v&agrave;o HĐQT nhiệm kỳ 2018 - 2023 của CapitalHouse, sau đ&oacute; trở th&agrave;nh Chủ tịch HĐQT c&ocirc;ng ty thay thế cho &ocirc;ng Nguyễn Huy Anh (SN 1952).</span></strong></p>

<p>Trong khi đ&oacute;, &ocirc;ng&nbsp;Nguyễn Trung Th&agrave;nh c&oacute; kinh nghiệm l&agrave;m việc tại nhiều đơn vị kh&aacute;c nhau, như: C&ocirc;ng ty In C&ocirc;ng đo&agrave;n Việt Nam, X&iacute; nghiệp sản xuất kinh doanh vật liệu x&acirc;y dựng thuộc CTCP Đầu tư X&acirc;y dựng H&agrave; Nội (Hancic - M&atilde; CK: HCI).</p>

<p><span style="color:#1abc9c">C&ograve;n &ocirc;ng L&ecirc; Ngọc Ho&agrave;ng tự giới thiệu c&oacute; hơn 18 năm kinh nghiệm trong lĩnh vực c&ocirc;ng nghệ viễn th&ocirc;ng, c&ocirc;ng nghệ th&ocirc;ng tin. &Ocirc;ng Ho&agrave;ng từng đảm nhiệm c&aacute;c vị tr&iacute; Ph&oacute; Tổng Gi&aacute;m đốc CTCP Tư vấn chuyển giao c&ocirc;ng nghệ ITC, hiện l&agrave; Chủ tịch HĐQT ki&ecirc;m Tổng Gi&aacute;m đốc CTCP NGCorp (th&agrave;nh lập ng&agrave;y 31/7/2019, vốn điều lệ 10 tỷ đồng).</span></p>

<p><span style="color:#8e44ad">Ngo&agrave;i ra, HĐQT CapitalHouse c&ograve;n xin &yacute; kiến cổ đ&ocirc;ng về việc miễn nhiệm 2 th&agrave;nh vi&ecirc;n Ban kiểm so&aacute;t (BKS) l&agrave; b&agrave; Phạm Thị Hồng Li&ecirc;n (SN 1975) v&agrave; &ocirc;ng Tạ Huy Đăng (SN 1974). Ở chiều hướng ngược lại, HĐQT CapitalHouse đề xuất bầu bổ sung b&agrave; Nguyễn Thị Hồng Linh (SN 1981) v&agrave; &ocirc;ng Ho&agrave;ng Phụng Hiệp (SN 1980).</span></p>

<p>B&agrave; Nguyễn Thị Hồng Linh từng đảm nhiệm vị tr&iacute; Gi&aacute;m đốc ban ph&aacute;p chế BIDGroup; Trưởng ph&ograve;ng luật sư, Luật sư điều h&agrave;nh C&ocirc;ng ty TNHH Luật Gia Phạm; Chuy&ecirc;n vi&ecirc;n cao cấp ch&iacute;nh s&aacute;ch t&agrave;i sản bảo đảm (khối quản trị rủi ro) của ng&acirc;n h&agrave;ng Techcombank.</p>

<p>Đ&aacute;ng ch&uacute; &yacute;, b&agrave; Phạm Thị Hồng Li&ecirc;n (người bị đề xuất miễn nhiệm khỏi BKS CapitalHouse) c&ugrave;ng từng đảm nhiệm vai tr&ograve; Trưởng ph&ograve;ng Tư vấn t&agrave;i ch&iacute;nh v&agrave; đầu tư tại C&ocirc;ng ty TNHH Luật Gia Phạm.</p>

<p>Từ th&aacute;ng 10/2016 đến th&aacute;ng 6/2017, b&agrave; Li&ecirc;n l&agrave;m Gi&aacute;m đốc t&agrave;i ch&iacute;nh, trưởng ban t&agrave;i ch&iacute;nh - kế to&aacute;n của CapitalHouse. Từ th&aacute;ng 7/2017, b&agrave; Li&ecirc;n c&ograve;n đảm nhiệm chức Gi&aacute;m đốc t&agrave;i ch&iacute;nh, sau đ&oacute; l&agrave; Gi&aacute;m đốc Ban quản l&yacute; rủi ro v&agrave; kiểm so&aacute;t nội bộ của CTCP Đầu tư Capital House (CapitalHouse Invest).</p>

<p>CapitalHouse Invest được th&agrave;nh lập từ th&aacute;ng 9/2016 bởi vợ chồng doanh nh&acirc;n Đỗ Đức Đạt - Đỗ Thị Th&ugrave;y Chi c&ugrave;ng một người quen l&agrave; b&agrave; Đỗ Thị Th&uacute;y (SN 1989). Trong đ&oacute;, vợ chồng &ocirc;ng Đạt giữ quyền chi phối to&agrave;n diện c&ocirc;ng ty, với 90% cổ phần trực tiếp đứng t&ecirc;n.</p>
', N'/Content/Images/ThumbNews/24b7e343ebd5108b49c4.jpg', CAST(N'2020-07-04 12:37:09.130' AS DateTime), NULL, 1, NULL, 1, 2)
SET IDENTITY_INSERT [dbo].[News] OFF
SET IDENTITY_INSERT [dbo].[NewsCategory] ON 

INSERT [dbo].[NewsCategory] ([id], [NewsCategoryName], [DateCreated], [DateUpdated], [UserCreated], [UserUpdated]) VALUES (1, N'Dự Án', CAST(N'2020-06-27 09:47:25.690' AS DateTime), NULL, 1, NULL)
INSERT [dbo].[NewsCategory] ([id], [NewsCategoryName], [DateCreated], [DateUpdated], [UserCreated], [UserUpdated]) VALUES (2, N'Thị Trường', CAST(N'2020-06-27 09:47:41.137' AS DateTime), NULL, 1, NULL)
INSERT [dbo].[NewsCategory] ([id], [NewsCategoryName], [DateCreated], [DateUpdated], [UserCreated], [UserUpdated]) VALUES (3, N'Bất Động Sản', CAST(N'2020-06-27 09:47:57.450' AS DateTime), NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[NewsCategory] OFF
SET IDENTITY_INSERT [dbo].[Project] ON 

INSERT [dbo].[Project] ([id], [ProjectName], [Address], [Acreage], [Price], [Direction], [Width], [Height], [Juridical], [Thumbnail], [Description], [DateCreated], [DateUpdated], [UserCreated], [UserUpdated], [IsShow], [IsHighlights], [idType], [idCategory], [NumerOfBuilding], [NumberOfFloors], [Type]) VALUES (1, N'ĐẤT THỔ CƯ TẠI DĨ AN', N'Đông Hòa - Dĩ An - Bình Dương', N'350 M2', N'1,2 Tỷ', N'Đông Bắc', N'40 m', N'9 m', N'Sổ hồng', N'/Content/Images/ThumbProject/DatNen1.jpg', N'Dự án mới', CAST(N'2020-06-24 12:02:25.977' AS DateTime), NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, N'Đất nền')
INSERT [dbo].[Project] ([id], [ProjectName], [Address], [Acreage], [Price], [Direction], [Width], [Height], [Juridical], [Thumbnail], [Description], [DateCreated], [DateUpdated], [UserCreated], [UserUpdated], [IsShow], [IsHighlights], [idType], [idCategory], [NumerOfBuilding], [NumberOfFloors], [Type]) VALUES (2, N'ĐẤT THỔ CƯ TẠI BÌNH CHUẨN', N'Bình Hòa - Bình Chuẩn - Bình Dương', N'450 M2', N'1,6 Tỷ - 1,8 Tỷ', N'Tây Nam', N'50 m', N'12 m', N'Sổ đỏ', N'/Content/Images/ThumbProject/DatNen2.jpg', NULL, CAST(N'2020-06-25 10:03:44.220' AS DateTime), NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, N'Đất nền')
INSERT [dbo].[Project] ([id], [ProjectName], [Address], [Acreage], [Price], [Direction], [Width], [Height], [Juridical], [Thumbnail], [Description], [DateCreated], [DateUpdated], [UserCreated], [UserUpdated], [IsShow], [IsHighlights], [idType], [idCategory], [NumerOfBuilding], [NumberOfFloors], [Type]) VALUES (3, N'SUN GRAND CITY 31 LÁNG HẠ HÀ NỘI', N'Láng Hạ, Đống Đa, Hà Nội', N'469 M3', N'6 Tỷ', N'Đông', NULL, NULL, N'Sổ đỏ', N'/Content/Images/ThumbProject/DuAn4.jpg', NULL, CAST(N'2020-06-25 16:25:35.587' AS DateTime), NULL, NULL, NULL, 1, NULL, 2, NULL, N'3', N'12', N'Căn Hộ')
INSERT [dbo].[Project] ([id], [ProjectName], [Address], [Acreage], [Price], [Direction], [Width], [Height], [Juridical], [Thumbnail], [Description], [DateCreated], [DateUpdated], [UserCreated], [UserUpdated], [IsShow], [IsHighlights], [idType], [idCategory], [NumerOfBuilding], [NumberOfFloors], [Type]) VALUES (4, N'DỰ ÁN MIPEC RUBIK 360 XUÂN THỦY', N'Hà Nội', N'469 M3', N'5 Tỷ', N'Đông Bắc', NULL, NULL, N'Sổ đỏ', N'/Content/Images/ThumbProject/DuAn5.jpg', NULL, CAST(N'2020-06-25 16:27:35.540' AS DateTime), NULL, NULL, NULL, 1, NULL, 1, NULL, NULL, NULL, N'Đất nền')
INSERT [dbo].[Project] ([id], [ProjectName], [Address], [Acreage], [Price], [Direction], [Width], [Height], [Juridical], [Thumbnail], [Description], [DateCreated], [DateUpdated], [UserCreated], [UserUpdated], [IsShow], [IsHighlights], [idType], [idCategory], [NumerOfBuilding], [NumberOfFloors], [Type]) VALUES (5, N'DỰ ÁN ECO GREEN SAIGON - QUẬN 7', N'Đồng Nai', N'469 M3', N'560 Triệu', N'Tây Nam', NULL, NULL, N'Sổ đỏ', N'/Content/Images/ThumbProject/DuAn6.jpg', NULL, CAST(N'2020-06-25 16:27:40.597' AS DateTime), NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, N'Đất nền')
INSERT [dbo].[Project] ([id], [ProjectName], [Address], [Acreage], [Price], [Direction], [Width], [Height], [Juridical], [Thumbnail], [Description], [DateCreated], [DateUpdated], [UserCreated], [UserUpdated], [IsShow], [IsHighlights], [idType], [idCategory], [NumerOfBuilding], [NumberOfFloors], [Type]) VALUES (6, N'E.CITY TÂN ĐỨC', N'Long An', N'469 M3', N'Liên hệ', N'Đông', NULL, NULL, N'Sổ đỏ', N'/Content/Images/ThumbProject/DuAn7.jpg', NULL, CAST(N'2020-06-25 16:27:45.297' AS DateTime), NULL, NULL, NULL, 1, NULL, 2, NULL, N'5', N'43', N'Căn Hộ')
INSERT [dbo].[Project] ([id], [ProjectName], [Address], [Acreage], [Price], [Direction], [Width], [Height], [Juridical], [Thumbnail], [Description], [DateCreated], [DateUpdated], [UserCreated], [UserUpdated], [IsShow], [IsHighlights], [idType], [idCategory], [NumerOfBuilding], [NumberOfFloors], [Type]) VALUES (7, N'AN KHANG RESIDENCE', N'Long An', N'469 M3', N'Liên hệ', N'Bắc', NULL, NULL, N'Sổ đỏ', N'/Content/Images/ThumbProject/DuAn8.jpg', NULL, CAST(N'2020-06-25 16:27:49.350' AS DateTime), NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, N'Đất nền')
INSERT [dbo].[Project] ([id], [ProjectName], [Address], [Acreage], [Price], [Direction], [Width], [Height], [Juridical], [Thumbnail], [Description], [DateCreated], [DateUpdated], [UserCreated], [UserUpdated], [IsShow], [IsHighlights], [idType], [idCategory], [NumerOfBuilding], [NumberOfFloors], [Type]) VALUES (8, N'THE TERRA AN HƯNG', N'Hà Tĩnh', N'469 M3', N'800 Triệu -  1 Tỷ', N'Đông Bắc', NULL, NULL, N'Sổ đỏ', N'/Content/Images/ThumbProject/DuAn9.jpg', NULL, CAST(N'2020-06-25 16:27:50.887' AS DateTime), NULL, NULL, NULL, 1, NULL, 2, NULL, N'5', N'43', N'Căn Hộ')
INSERT [dbo].[Project] ([id], [ProjectName], [Address], [Acreage], [Price], [Direction], [Width], [Height], [Juridical], [Thumbnail], [Description], [DateCreated], [DateUpdated], [UserCreated], [UserUpdated], [IsShow], [IsHighlights], [idType], [idCategory], [NumerOfBuilding], [NumberOfFloors], [Type]) VALUES (9, N'ATHENA FULLAND', N'Phú Thọ', N'469 M3', N'4 Tỷ', N'Đông Nam', NULL, NULL, N'Sổ đỏ', N'/Content/Images/ThumbProject/DuAn10.jpg', NULL, CAST(N'2020-06-25 16:28:00.567' AS DateTime), NULL, NULL, NULL, 1, NULL, 1, NULL, NULL, NULL, N'Đất nền')
INSERT [dbo].[Project] ([id], [ProjectName], [Address], [Acreage], [Price], [Direction], [Width], [Height], [Juridical], [Thumbnail], [Description], [DateCreated], [DateUpdated], [UserCreated], [UserUpdated], [IsShow], [IsHighlights], [idType], [idCategory], [NumerOfBuilding], [NumberOfFloors], [Type]) VALUES (10, N'ATHENA FULLAND 2', N'Hồ Chí Minh', N'469 M3', N'12 Tỷ', N'Tây Bắc', NULL, NULL, N'Sổ đỏ', N'/Content/Images/ThumbProject/DuAn11.jpg', NULL, CAST(N'2020-06-25 16:28:04.453' AS DateTime), NULL, NULL, NULL, 1, 1, 2, NULL, N'3', N'23', N'Căn Hộ')
INSERT [dbo].[Project] ([id], [ProjectName], [Address], [Acreage], [Price], [Direction], [Width], [Height], [Juridical], [Thumbnail], [Description], [DateCreated], [DateUpdated], [UserCreated], [UserUpdated], [IsShow], [IsHighlights], [idType], [idCategory], [NumerOfBuilding], [NumberOfFloors], [Type]) VALUES (11, N'ĐẤT NỀN VISIP 2', N'Bình Dương', N'469 M3', N'60 Tỷ', N'Đông', NULL, NULL, N'Sổ đỏ', N'/Content/Images/ThumbProject/DuAn12.jpg', NULL, CAST(N'2020-06-25 16:28:08.973' AS DateTime), NULL, NULL, NULL, 1, NULL, 2, NULL, N'4', N'34', N'Căn Hộ')
INSERT [dbo].[Project] ([id], [ProjectName], [Address], [Acreage], [Price], [Direction], [Width], [Height], [Juridical], [Thumbnail], [Description], [DateCreated], [DateUpdated], [UserCreated], [UserUpdated], [IsShow], [IsHighlights], [idType], [idCategory], [NumerOfBuilding], [NumberOfFloors], [Type]) VALUES (12, N'ĐẤT NỀN ĐỒNG NAI', N'Đồng Nai', N'469 M3', N'12 Tỷ', N'Đông Bắc', NULL, NULL, N'Sổ đỏ', N'/Content/Images/ThumbProject/DuAn13.jpg', NULL, CAST(N'2020-06-25 16:28:15.777' AS DateTime), NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, N'Đất nền')
INSERT [dbo].[Project] ([id], [ProjectName], [Address], [Acreage], [Price], [Direction], [Width], [Height], [Juridical], [Thumbnail], [Description], [DateCreated], [DateUpdated], [UserCreated], [UserUpdated], [IsShow], [IsHighlights], [idType], [idCategory], [NumerOfBuilding], [NumberOfFloors], [Type]) VALUES (13, N'ĐẤT NỀN LONG THÀNH', N'Đồng Nai', N'469 M3', N'7 Tỷ', N'Đông', NULL, NULL, N'Sổ đỏ', N'/Content/Images/ThumbProject/DuAn12.jpg', NULL, CAST(N'2020-06-25 16:28:24.773' AS DateTime), NULL, NULL, NULL, 1, NULL, 1, NULL, NULL, NULL, N'Đất nền')
INSERT [dbo].[Project] ([id], [ProjectName], [Address], [Acreage], [Price], [Direction], [Width], [Height], [Juridical], [Thumbnail], [Description], [DateCreated], [DateUpdated], [UserCreated], [UserUpdated], [IsShow], [IsHighlights], [idType], [idCategory], [NumerOfBuilding], [NumberOfFloors], [Type]) VALUES (14, N'ĐẤT NỀN LONG KHÁNH', N'Đồng Nai', N'469 M3', N'1 Tỷ', N'Nam', NULL, NULL, N'Sổ đỏ', N'/Content/Images/ThumbProject/DuAn11.jpg', NULL, CAST(N'2020-06-25 16:28:28.560' AS DateTime), NULL, NULL, NULL, 1, NULL, 2, NULL, N'2', N'32', N'Căn Hộ')
SET IDENTITY_INSERT [dbo].[Project] OFF
USE [master]
GO
ALTER DATABASE [HoangLienSonDB] SET  READ_WRITE 
GO
