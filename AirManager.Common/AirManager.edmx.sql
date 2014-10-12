
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server Compact Edition
-- --------------------------------------------------
-- Date Created: 10/12/2014 11:45:22
-- Generated from EDMX file: C:\Users\Dieter\Documents\GitHub\AirManager\AirManager.Common\AirManager.edmx
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

    ALTER TABLE [Airlines] DROP CONSTRAINT [FK_AirlineCountry];
GO
    ALTER TABLE [Cities] DROP CONSTRAINT [FK_CountryCity];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- NOTE: if the table does not exist, an ignorable error will be reported.
-- --------------------------------------------------

    DROP TABLE [Airlines];
GO
    DROP TABLE [Countries];
GO
    DROP TABLE [Cities];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Airlines'
CREATE TABLE [Airlines] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(4000)  NOT NULL,
    [IATA] nvarchar(4000)  NOT NULL,
    [CEO] nvarchar(4000)  NOT NULL,
    [Story] nvarchar(4000)  NOT NULL,
    [Real] nvarchar(4000)  NOT NULL,
    [From] nvarchar(4000)  NOT NULL,
    [To] nvarchar(4000)  NOT NULL,
    [Country_Id] nvarchar(4000)  NOT NULL
);
GO

-- Creating table 'Countries'
CREATE TABLE [Countries] (
    [Id] nvarchar(4000)  NOT NULL,
    [Name] nvarchar(4000)  NOT NULL
);
GO

-- Creating table 'Cities'
CREATE TABLE [Cities] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(4000)  NOT NULL,
    [CountryId] nvarchar(4000)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Airlines'
ALTER TABLE [Airlines]
ADD CONSTRAINT [PK_Airlines]
    PRIMARY KEY ([Id] );
GO

-- Creating primary key on [Id] in table 'Countries'
ALTER TABLE [Countries]
ADD CONSTRAINT [PK_Countries]
    PRIMARY KEY ([Id] );
GO

-- Creating primary key on [Id] in table 'Cities'
ALTER TABLE [Cities]
ADD CONSTRAINT [PK_Cities]
    PRIMARY KEY ([Id] );
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Country_Id] in table 'Airlines'
ALTER TABLE [Airlines]
ADD CONSTRAINT [FK_AirlineCountry]
    FOREIGN KEY ([Country_Id])
    REFERENCES [Countries]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AirlineCountry'
CREATE INDEX [IX_FK_AirlineCountry]
ON [Airlines]
    ([Country_Id]);
GO

-- Creating foreign key on [CountryId] in table 'Cities'
ALTER TABLE [Cities]
ADD CONSTRAINT [FK_CountryCity]
    FOREIGN KEY ([CountryId])
    REFERENCES [Countries]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CountryCity'
CREATE INDEX [IX_FK_CountryCity]
ON [Cities]
    ([CountryId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------