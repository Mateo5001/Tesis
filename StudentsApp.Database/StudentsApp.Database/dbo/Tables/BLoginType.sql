CREATE TABLE [dbo].[BLoginType] (
    [LoginTypeId]   INT          IDENTITY (1, 1) NOT NULL,
    [LoginTypeCode] VARCHAR (2)  NOT NULL,
    [LginTypeName]  VARCHAR (30) NOT NULL,
    CONSTRAINT [PK_BTipoLogin] PRIMARY KEY CLUSTERED ([LoginTypeId] ASC)
);

