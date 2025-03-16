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

CREATE TABLE [TabelaGeralItem] (
    [Id] uniqueidentifier NOT NULL,
    [TabelaGeralId] uniqueidentifier NOT NULL,
    [Sigla] nvarchar(5) NOT NULL,
    [Descricao] nvarchar(150) NOT NULL,
    [DataInclusao] datetime2 NOT NULL,
    [DataAlteracao] datetime2 NULL,
    CONSTRAINT [PK_TabelaGeralItem] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TabelaGeralItem_TabelaGeral_TabelaGeralId] FOREIGN KEY ([TabelaGeralId]) REFERENCES [TabelaGeral] ([Id]) ON DELETE CASCADE
);
GO

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
    [DataInclusao] datetime2 NOT NULL,
    [DataAlteracao] datetime2 NULL,
    CONSTRAINT [PK_Produto] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Produto_Categoria_CategoriaId] FOREIGN KEY ([CategoriaId]) REFERENCES [Categoria] ([Id]),
    CONSTRAINT [FK_Produto_Marcas_MarcaId] FOREIGN KEY ([MarcaId]) REFERENCES [Marcas] ([Id]),
    CONSTRAINT [FK_Produto_TabelaGeralItem_IdTGCor] FOREIGN KEY ([IdTGTamanho]) REFERENCES [TabelaGeralItem] ([Id]),
    CONSTRAINT [FK_Produto_TabelaGeralItem_IdTGTamanho] FOREIGN KEY ([IdTGTamanho]) REFERENCES [TabelaGeralItem] ([Id])
);
GO

CREATE INDEX [IX_Marcas_CategoriaId] ON [Marcas] ([CategoriaId]);
GO

CREATE INDEX [IX_Produto_CategoriaId] ON [Produto] ([CategoriaId]);
GO

CREATE INDEX [IX_Produto_IdTGCor] ON [Produto] ([IdTGCore]);
GO

CREATE INDEX [IX_Produto_MarcaId] ON [Produto] ([MarcaId]);
GO

CREATE INDEX [IX_Produto_IdTGTamanho] ON [Produto] ([IdTGTamanho]);
GO

CREATE INDEX [IX_TabelaGeralItem_TabelaGeralId] ON [TabelaGeralItem] ([TabelaGeralId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250205212317_ProdutoDB', N'8.0.12');
GO

COMMIT;
GO

