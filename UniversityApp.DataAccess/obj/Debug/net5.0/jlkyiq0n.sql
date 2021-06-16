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

CREATE TABLE [Students] (
    [Id] int NOT NULL IDENTITY,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [Age] int NOT NULL,
    [Gender] nvarchar(max) NULL,
    CONSTRAINT [PK_Students] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Grades] (
    [Id] int NOT NULL IDENTITY,
    [Value] int NOT NULL,
    [Description] nvarchar(max) NULL,
    [StudentId] int NOT NULL,
    CONSTRAINT [PK_Grades] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Grades_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Students] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Grades_StudentId] ON [Grades] ([StudentId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210616101640_initial', N'5.0.7');
GO

COMMIT;
GO

