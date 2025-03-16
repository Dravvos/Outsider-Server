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
/*
CREATE TABLE [Categoria] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] nvarchar(100) NOT NULL,
    [DataInclusao] datetime2 NOT NULL,
    [DataAlteracao] datetime2 NULL,
    CONSTRAINT [PK_Categoria] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [TabelaGeral] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] nvarchar(100) NOT NULL,
    [Descricao] nvarchar(250) NOT NULL,
    [DataInclusao] datetime2 NOT NULL,
    [DataAlteracao] datetime2 NULL,
    CONSTRAINT [PK_TabelaGeral] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Marcas] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] nvarchar(200) NOT NULL,
    [CategoriaId] uniqueidentifier NOT NULL,
    [DataInclusao] datetime2 NOT NULL,
    [DataAlteracao] datetime2 NULL,
    CONSTRAINT [PK_Marcas] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Marcas_Categoria_CategoriaId] FOREIGN KEY ([CategoriaId]) REFERENCES [Categoria] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [TabelaGeralItemModel] (
    [Id] uniqueidentifier NOT NULL,
    [TabelaGeralId] uniqueidentifier NOT NULL,
    [Sigla] nvarchar(5) NOT NULL,
    [Descricao] nvarchar(150) NOT NULL,
    [DataInclusao] datetime2 NOT NULL,
    [DataAlteracao] datetime2 NULL,
    CONSTRAINT [PK_TabelaGeralItemModel] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TabelaGeralItemModel_TabelaGeral_TabelaGeralId] FOREIGN KEY ([TabelaGeralId]) REFERENCES [TabelaGeral] ([Id]) ON DELETE CASCADE
);
GO
*/
CREATE TABLE [Produto] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] nvarchar(150) NOT NULL,
    [Preco] real(2) NOT NULL,
    [Descricao] nvarchar(max) NULL,
    [Imagem] varbinary(max) NOT NULL,
    [Peso] real(6) NOT NULL,
    [Estoque] int NOT NULL,
    [SKU] nvarchar(30) NOT NULL,
    [CategoriaId] uniqueidentifier NOT NULL,
    [MarcaId] uniqueidentifier NOT NULL,
    [IdTGCor] uniqueidentifier NOT NULL,
    [IdTGTamanho] uniqueidentifier NOT NULL,
    [CorId] uniqueidentifier NOT NULL,
    [TamanhoId] uniqueidentifier NOT NULL,
    [DataInclusao] datetime2 NOT NULL,
    [DataAlteracao] datetime2 NULL,
    CONSTRAINT [PK_Produto] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Produto_Categoria_CategoriaId] FOREIGN KEY ([CategoriaId]) REFERENCES [Categoria] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Produto_Marcas_MarcaId] FOREIGN KEY ([MarcaId]) REFERENCES [Marcas] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Produto_TabelaGeralItemModel_CorId] FOREIGN KEY ([CorId]) REFERENCES [TabelaGeralItemModel] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Produto_TabelaGeralItemModel_TamanhoId] FOREIGN KEY ([TamanhoId]) REFERENCES [TabelaGeralItemModel] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Marcas_CategoriaId] ON [Marcas] ([CategoriaId]);
GO

CREATE INDEX [IX_Produto_CategoriaId] ON [Produto] ([CategoriaId]);
GO

CREATE INDEX [IX_Produto_CorId] ON [Produto] ([CorId]);
GO

CREATE INDEX [IX_Produto_MarcaId] ON [Produto] ([MarcaId]);
GO

CREATE INDEX [IX_Produto_TamanhoId] ON [Produto] ([TamanhoId]);
GO

CREATE INDEX [IX_TabelaGeralItemModel_TabelaGeralId] ON [TabelaGeralItemModel] ([TabelaGeralId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250205202636_ProdutoDB', N'8.0.12');
GO

COMMIT;
GO

