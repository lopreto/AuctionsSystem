USE [AuctionsSystem]
GO

/****** Object:  Create Tables    Script Date: 14/10/2019 20:36:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[WalletHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BuyerWalletId] [int] NULL,
	[SellerWalletId] [int] NULL,
	[TransactionId] [int] NULL,
	[TransactionDate] [datetime] NOT NULL,
	[TransactionValue] [decimal](18, 2) NOT NULL,
	[Description] [varchar](200) NULL,
	[WalletId] [int] NULL
) ON [PRIMARY]
GO

USE [AuctionsSystem]
GO

/****** Object:  Table [dbo].[Transaction]    Script Date: 14/10/2019 20:36:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Transaction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[AuctionId] [int] NOT NULL,
	[BuyerId] [varchar](50) NOT NULL,
	[SellerId] [varchar](50) NOT NULL,
	[Bid] [decimal](18, 2) NOT NULL,
	[TransactionEndDate] [datetime] NULL,
	[TransactionContemplated] [bit] NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Auctions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Comments] [varchar](500) NULL,
	[ImagePath] [varchar](50) NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[InitialDate] [datetime] NOT NULL,
	[FinalDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
	[UserId] [varchar](100) NOT NULL,
	[BestBid] [decimal](18, 0) NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Wallet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [varchar](50) NOT NULL,
	[Balance] [decimal](18, 2) NOT NULL
) ON [PRIMARY]
GO