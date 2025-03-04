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

CREATE TABLE [Categoria] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] nvarchar(100) NOT NULL,
    [DataInclusao] datetime2 NOT NULL,
    [DataAlteracao] datetime2 NULL,
    CONSTRAINT [PK_Categoria] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Produto] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] nvarchar(150) NOT NULL,
    [Preco] real(2) NOT NULL,
    [Descricao] nvarchar(max) NULL,
    [CategoriaId] uniqueidentifier NOT NULL,
    [Imagem] varbinary(max) NOT NULL,
    [Peso] real(6) NOT NULL,
    [Estoque] int NOT NULL,
    [SKU] nvarchar(30) NOT NULL,
    [DataInclusao] datetime2 NOT NULL,
    [DataAlteracao] datetime2 NULL,
    CONSTRAINT [PK_Produto] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Produto_Categoria_CategoriaId] FOREIGN KEY ([CategoriaId]) REFERENCES [Categoria] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Produto_CategoriaId] ON [Produto] ([CategoriaId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250205182238_ProdutoDB', N'8.0.12');
GO

COMMIT;
GO

