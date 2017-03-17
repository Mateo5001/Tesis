CREATE TABLE [dbo].[LoginKeyHistorial] (
    [LoginKeyHistorialId] INT          IDENTITY (1, 1) NOT NULL,
    [LoginKeyId]          INT          NULL,
    [RegisterUserId]      INT          NULL,
    [BeforeLoginNick]     VARCHAR (10) NULL,
    [BeforeLoginPass]     VARCHAR (12) NULL,
    [ModifyDate]          DATE         NULL,
    CONSTRAINT [PK_LoginKeyHistorial] PRIMARY KEY CLUSTERED ([LoginKeyHistorialId] ASC),
    CONSTRAINT [FK_LoginKeyHistorial_LoginKey] FOREIGN KEY ([LoginKeyId]) REFERENCES [dbo].[LoginKey] ([LoginKeyId])
);

