
-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_PurchaseOrderPurchaseOrderLineItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PurchaseOrderLineItems] DROP CONSTRAINT [FK_PurchaseOrderPurchaseOrderLineItem];
GO
IF OBJECT_ID(N'[dbo].[FK_SupplierPurchaseOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PurchaseOrders] DROP CONSTRAINT [FK_SupplierPurchaseOrder];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[PurchaseOrders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PurchaseOrders];
GO
IF OBJECT_ID(N'[dbo].[PurchaseOrderLineItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PurchaseOrderLineItems];
GO
IF OBJECT_ID(N'[dbo].[Suppliers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Suppliers];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'PurchaseOrders'
CREATE TABLE [dbo].[PurchaseOrders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MaxValue] decimal(18,0)  NOT NULL,
    [SupplierId] int  NOT NULL,
    [Status] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PurchaseOrderLineItems'
CREATE TABLE [dbo].[PurchaseOrderLineItems] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Price] decimal(18,0)  NOT NULL,
    [Quantity] int  NOT NULL,
    [PurchaseOrderId] int  NOT NULL,
    [Position] int  NOT NULL
);
GO

-- Creating table 'Suppliers'
CREATE TABLE [dbo].[Suppliers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'PurchaseOrders'
ALTER TABLE [dbo].[PurchaseOrders]
ADD CONSTRAINT [PK_PurchaseOrders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PurchaseOrderLineItems'
ALTER TABLE [dbo].[PurchaseOrderLineItems]
ADD CONSTRAINT [PK_PurchaseOrderLineItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Suppliers'
ALTER TABLE [dbo].[Suppliers]
ADD CONSTRAINT [PK_Suppliers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [PurchaseOrderId] in table 'PurchaseOrderLineItems'
ALTER TABLE [dbo].[PurchaseOrderLineItems]
ADD CONSTRAINT [FK_PurchaseOrderPurchaseOrderLineItem]
    FOREIGN KEY ([PurchaseOrderId])
    REFERENCES [dbo].[PurchaseOrders]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PurchaseOrderPurchaseOrderLineItem'
CREATE INDEX [IX_FK_PurchaseOrderPurchaseOrderLineItem]
ON [dbo].[PurchaseOrderLineItems]
    ([PurchaseOrderId]);
GO

-- Creating foreign key on [SupplierId] in table 'PurchaseOrders'
ALTER TABLE [dbo].[PurchaseOrders]
ADD CONSTRAINT [FK_SupplierPurchaseOrder]
    FOREIGN KEY ([SupplierId])
    REFERENCES [dbo].[Suppliers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SupplierPurchaseOrder'
CREATE INDEX [IX_FK_SupplierPurchaseOrder]
ON [dbo].[PurchaseOrders]
    ([SupplierId]);
GO

declare @apple int
declare @dell int
declare @gadgetShop int


INSERT INTO Suppliers ( Name ) VALUES ( 'Apple' )
set @apple = SCOPE_IDENTITY() 

INSERT INTO Suppliers ( Name ) VALUES ( 'DELL' )

set @dell = SCOPE_IDENTITY ()
INSERT INTO Suppliers ( Name ) VALUES ( 'GadgetShop' )

set @gadgetShop = SCOPE_IDENTITY ()


INSERT INTO PurchaseOrders ( MaxValue , Status , SupplierId ) Values ( 1000 , 'Open' , @apple )
INSERT INTO PurchaseOrders ( MaxValue , Status , SupplierId ) Values ( 700 , 'Open' , @dell )
INSERT INTO PurchaseOrders ( MaxValue , Status , SupplierId ) Values ( 100 , 'Open' , @gadgetShop )

go

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------