CREATE TABLE [dbo].[Login] (
    [LoginId]     INT IDENTITY (1, 1) NOT NULL,
    [LoginTypeId] INT NOT NULL,
    [LoginKeyId]  INT NULL,
    [UserId]      INT NULL,
    CONSTRAINT [PK_Logueo] PRIMARY KEY CLUSTERED ([LoginId] ASC),
    CONSTRAINT [FK_Logueo_BTipoLogin] FOREIGN KEY ([LoginTypeId]) REFERENCES [dbo].[BLoginType] ([LoginTypeId]),
    CONSTRAINT [FK_Logueo_LoginKey] FOREIGN KEY ([LoginKeyId]) REFERENCES [dbo].[LoginKey] ([LoginKeyId]),
    CONSTRAINT [FK_Logueo_Usurio] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId])
);

