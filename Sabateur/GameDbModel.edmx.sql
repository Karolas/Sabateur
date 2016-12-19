
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/16/2016 18:16:11
-- Generated from EDMX file: C:\Users\Karolis\documents\visual studio 2015\Projects\Sabateur\Sabateur\GameDbModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [C:\Users\Karolis\Desktop\GameDB.mdf];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_PlayerCard]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CardSet] DROP CONSTRAINT [FK_PlayerCard];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[CardSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CardSet];
GO
IF OBJECT_ID(N'[dbo].[PlayerSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PlayerSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CardSet'
CREATE TABLE [dbo].[CardSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] int  NOT NULL,
    [Status] int  NOT NULL,
    [Owner] nvarchar(16)  NULL,
    [BlockType] int  NULL,
    [PathOpenings] int  NULL,
    [Path] int  NULL,
    [FieldCol] int  NULL,
    [FieldRow] int  NULL,
    [IsPathUpside] bit  NULL
);
GO

-- Creating table 'PlayerSet'
CREATE TABLE [dbo].[PlayerSet] (
    [Name] nvarchar(16)  NOT NULL,
    [IsTurn] bit  NOT NULL,
    [Score] smallint  NOT NULL,
    [Blocks] nvarchar(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id], [Type] in table 'CardSet'
ALTER TABLE [dbo].[CardSet]
ADD CONSTRAINT [PK_CardSet]
    PRIMARY KEY CLUSTERED ([Id], [Type] ASC);
GO

-- Creating primary key on [Name] in table 'PlayerSet'
ALTER TABLE [dbo].[PlayerSet]
ADD CONSTRAINT [PK_PlayerSet]
    PRIMARY KEY CLUSTERED ([Name] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Owner] in table 'CardSet'
ALTER TABLE [dbo].[CardSet]
ADD CONSTRAINT [FK_PlayerCard]
    FOREIGN KEY ([Owner])
    REFERENCES [dbo].[PlayerSet]
        ([Name])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PlayerCard'
CREATE INDEX [IX_FK_PlayerCard]
ON [dbo].[CardSet]
    ([Owner]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------