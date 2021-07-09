IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF SCHEMA_ID(N'Enterprise') IS NULL EXEC(N'CREATE SCHEMA [Enterprise];');
GO

CREATE TABLE [Enterprise].[TB_Supplier] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(100) NOT NULL,
    [Documento] varchar(100) NOT NULL,
    [TipoFornecedor] int NOT NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_TB_Supplier] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Enterprise].[TB_Address] (
    [Id] uniqueidentifier NOT NULL,
    [SupplierId] uniqueidentifier NOT NULL,
    [Logradouro] varchar(100) NOT NULL,
    [Numero] varchar(100) NOT NULL,
    [Complemento] varchar(100) NOT NULL,
    [Cep] varchar(100) NOT NULL,
    [Bairro] varchar(100) NOT NULL,
    [Cidade] varchar(100) NOT NULL,
    [Estado] varchar(100) NOT NULL,
    CONSTRAINT [PK_TB_Address] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TB_Address_TB_Supplier_SupplierId] FOREIGN KEY ([SupplierId]) REFERENCES [Enterprise].[TB_Supplier] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Enterprise].[TB_Product] (
    [Id] uniqueidentifier NOT NULL,
    [SupplierId] uniqueidentifier NOT NULL,
    [Nome] varchar(100) NOT NULL,
    [Descricao] varchar(100) NOT NULL,
    [Image] varchar(100) NOT NULL,
    [Valor] decimal(18,2) NOT NULL,
    [DataCadastro] datetime2 NOT NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_TB_Product] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TB_Product_TB_Supplier_SupplierId] FOREIGN KEY ([SupplierId]) REFERENCES [Enterprise].[TB_Supplier] ([Id]) ON DELETE NO ACTION
);
GO

CREATE UNIQUE INDEX [IX_TB_Address_SupplierId] ON [Enterprise].[TB_Address] ([SupplierId]);
GO

CREATE INDEX [IX_TB_Product_SupplierId] ON [Enterprise].[TB_Product] ([SupplierId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210709161828_Initia', N'5.0.7');
GO

COMMIT;
GO

