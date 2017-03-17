CREATE TABLE [dbo].[UserType] (
    [UserTypeId]   INT           IDENTITY (1, 1) NOT NULL,
    [UserTypeName] VARCHAR (100) NULL,
    [UserTypeCode] VARCHAR (5)   NULL,
    [isActive]     BIT           NOT NULL,
    CONSTRAINT [PK_TipoUsuario] PRIMARY KEY CLUSTERED ([UserTypeId] ASC)
);

