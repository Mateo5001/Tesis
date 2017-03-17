CREATE TABLE [dbo].[LoginKey] (
    [LoginKeyId] INT          IDENTITY (1, 1) NOT NULL,
    [LoginNick]  VARCHAR (20) NOT NULL,
    [LoginPass]  VARCHAR (12) NOT NULL,
    [LoginKey]   VARCHAR (36) NOT NULL,
    [AccessDate] DATE         NULL,
    CONSTRAINT [PK_LoginKey] PRIMARY KEY CLUSTERED ([LoginKeyId] ASC)
);

