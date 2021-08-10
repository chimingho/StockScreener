
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 10/20/2013 23:51:11
-- Generated from EDMX file: D:\GoogleDrive\Code\StockScreener\StockScreener\Model\StockScreener.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [StockScreener];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ExchangeStock]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Stocks] DROP CONSTRAINT [FK_ExchangeStock];
GO
IF OBJECT_ID(N'[dbo].[FK_SectorIndustry]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Industries] DROP CONSTRAINT [FK_SectorIndustry];
GO
IF OBJECT_ID(N'[dbo].[FK_IndustryStock]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Stocks] DROP CONSTRAINT [FK_IndustryStock];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Industries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Industries];
GO
IF OBJECT_ID(N'[dbo].[Sectors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sectors];
GO
IF OBJECT_ID(N'[dbo].[Stocks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Stocks];
GO
IF OBJECT_ID(N'[dbo].[Exchanges]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Exchanges];
GO
IF OBJECT_ID(N'[dbo].[Quotes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Quotes];
GO
IF OBJECT_ID(N'[dbo].[StagingStocks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StagingStocks];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Industries'
CREATE TABLE [dbo].[Industries] (
    [Id] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [PriceChange] decimal(18,4)  NULL,
    [MarketCapitalization] decimal(18,4)  NULL,
    [PE] decimal(18,4)  NULL,
    [ROE] decimal(18,4)  NULL,
    [DividendYield] decimal(18,4)  NULL,
    [DebtToEquityRatio] decimal(18,4)  NULL,
    [PriceBook] decimal(18,4)  NULL,
    [NetProfitMargin] decimal(18,4)  NULL,
    [PriceToFreeCashFlow] decimal(18,4)  NULL,
    [SectorId] int  NULL,
    [SectorFeed] nvarchar(20)  NOT NULL
);
GO

-- Creating table 'Sectors'
CREATE TABLE [dbo].[Sectors] (
    [Id] int  NOT NULL,
    [Feed] nvarchar(20)  NOT NULL,
    [Name] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'Stocks'
CREATE TABLE [dbo].[Stocks] (
    [Symbol] varchar(10)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ExchangeSymbol] nvarchar(10)  NULL,
    [IndustryId] int  NULL,
    [IndustrySectorFeed] nvarchar(20)  NULL,
    [SecurityTypeType] nvarchar(max)  NULL
);
GO

-- Creating table 'Exchanges'
CREATE TABLE [dbo].[Exchanges] (
    [Symbol] nvarchar(10)  NOT NULL,
    [Name] nvarchar(max)  NULL
);
GO

-- Creating table 'Quotes'
CREATE TABLE [dbo].[Quotes] (
    [Symobl] varchar(10)  NOT NULL,
    [RecordDate] datetime  NOT NULL,
    [BookValue] decimal(18,4)  NULL,
    [DividendShare] decimal(18,4)  NULL,
    [EarningsShare] decimal(18,4)  NULL,
    [EPSEstimateCurrentYear] decimal(18,4)  NULL,
    [EPSEstimateNextYear] decimal(18,4)  NULL,
    [EPSEstimateNextQuarter] decimal(18,4)  NULL,
    [YearLow] decimal(18,4)  NULL,
    [YearHigh] decimal(18,4)  NULL,
    [MarketCapitalization] decimal(18,4)  NULL,
    [EBITDA] decimal(18,4)  NULL,
    [PercentChangeFromTwoHundreddayMovingAverage] decimal(18,4)  NULL,
    [PercentChangeFromFiftydayMovingAverage] decimal(18,4)  NULL,
    [PriceSales] decimal(18,4)  NULL,
    [PriceBook] decimal(18,4)  NULL,
    [PERatio] decimal(18,4)  NULL,
    [PEGRatio] decimal(18,4)  NULL,
    [PriceEPSEstimateCurrentYear] decimal(18,4)  NULL,
    [PriceEPSEstimateNextYear] decimal(18,4)  NULL,
    [ShortRatio] decimal(18,4)  NULL,
    [OneyrTargetPrice] decimal(18,4)  NULL
);
GO

-- Creating table 'StagingStocks'
CREATE TABLE [dbo].[StagingStocks] (
    [symbol] nvarchar(200)  NULL,
    [name] nvarchar(200)  NULL,
    [exch] nvarchar(200)  NULL,
    [type] nvarchar(200)  NULL,
    [exchDisp] nvarchar(200)  NULL,
    [typeDisp] nvarchar(200)  NULL,
    [Id] int IDENTITY(1,1) NOT NULL,
    [recordDate] datetime  NOT NULL
);
GO

-- Creating table 'SecurityTypes'
CREATE TABLE [dbo].[SecurityTypes] (
    [Type] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id], [SectorFeed] in table 'Industries'
ALTER TABLE [dbo].[Industries]
ADD CONSTRAINT [PK_Industries]
    PRIMARY KEY CLUSTERED ([Id], [SectorFeed] ASC);
GO

-- Creating primary key on [Id], [Feed] in table 'Sectors'
ALTER TABLE [dbo].[Sectors]
ADD CONSTRAINT [PK_Sectors]
    PRIMARY KEY CLUSTERED ([Id], [Feed] ASC);
GO

-- Creating primary key on [Symbol] in table 'Stocks'
ALTER TABLE [dbo].[Stocks]
ADD CONSTRAINT [PK_Stocks]
    PRIMARY KEY CLUSTERED ([Symbol] ASC);
GO

-- Creating primary key on [Symbol] in table 'Exchanges'
ALTER TABLE [dbo].[Exchanges]
ADD CONSTRAINT [PK_Exchanges]
    PRIMARY KEY CLUSTERED ([Symbol] ASC);
GO

-- Creating primary key on [Symobl], [RecordDate] in table 'Quotes'
ALTER TABLE [dbo].[Quotes]
ADD CONSTRAINT [PK_Quotes]
    PRIMARY KEY CLUSTERED ([Symobl], [RecordDate] ASC);
GO

-- Creating primary key on [Id] in table 'StagingStocks'
ALTER TABLE [dbo].[StagingStocks]
ADD CONSTRAINT [PK_StagingStocks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Type] in table 'SecurityTypes'
ALTER TABLE [dbo].[SecurityTypes]
ADD CONSTRAINT [PK_SecurityTypes]
    PRIMARY KEY CLUSTERED ([Type] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ExchangeSymbol] in table 'Stocks'
ALTER TABLE [dbo].[Stocks]
ADD CONSTRAINT [FK_ExchangeStock]
    FOREIGN KEY ([ExchangeSymbol])
    REFERENCES [dbo].[Exchanges]
        ([Symbol])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ExchangeStock'
CREATE INDEX [IX_FK_ExchangeStock]
ON [dbo].[Stocks]
    ([ExchangeSymbol]);
GO

-- Creating foreign key on [SectorId], [SectorFeed] in table 'Industries'
ALTER TABLE [dbo].[Industries]
ADD CONSTRAINT [FK_SectorIndustry]
    FOREIGN KEY ([SectorId], [SectorFeed])
    REFERENCES [dbo].[Sectors]
        ([Id], [Feed])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SectorIndustry'
CREATE INDEX [IX_FK_SectorIndustry]
ON [dbo].[Industries]
    ([SectorId], [SectorFeed]);
GO

-- Creating foreign key on [IndustryId], [IndustrySectorFeed] in table 'Stocks'
ALTER TABLE [dbo].[Stocks]
ADD CONSTRAINT [FK_IndustryStock]
    FOREIGN KEY ([IndustryId], [IndustrySectorFeed])
    REFERENCES [dbo].[Industries]
        ([Id], [SectorFeed])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_IndustryStock'
CREATE INDEX [IX_FK_IndustryStock]
ON [dbo].[Stocks]
    ([IndustryId], [IndustrySectorFeed]);
GO

-- Creating foreign key on [SecurityTypeType] in table 'Stocks'
ALTER TABLE [dbo].[Stocks]
ADD CONSTRAINT [FK_StockSecurityType]
    FOREIGN KEY ([SecurityTypeType])
    REFERENCES [dbo].[SecurityTypes]
        ([Type])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StockSecurityType'
CREATE INDEX [IX_FK_StockSecurityType]
ON [dbo].[Stocks]
    ([SecurityTypeType]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------