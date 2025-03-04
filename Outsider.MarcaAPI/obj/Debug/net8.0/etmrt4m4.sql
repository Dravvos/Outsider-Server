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
*/
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

CREATE INDEX [IX_Marcas_CategoriaId] ON [Marcas] ([CategoriaId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250205181856_MarcaDB', N'8.0.12');
GO

COMMIT;
GO

