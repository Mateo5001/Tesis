CREATE TABLE [dbo].[User] (
    [UserId]             INT          IDENTITY (1, 1) NOT NULL,
    [UserDocuemntTypeId] INT          NOT NULL,
    [UserDocumentNumber] VARCHAR (20) NOT NULL,
    [UserFirstName]      VARCHAR (30) NOT NULL,
    [UserSecondName]     VARCHAR (30) NULL,
    [UserFirsLastName]   VARCHAR (30) NOT NULL,
    [UserSecondLastName] VARCHAR (30) NULL,
    [UserType]           INT          NOT NULL,
    [UserBirthDate]      DATE         NULL,
    [isActive]           BIT          CONSTRAINT [DF_Usurio_esActivo] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Usurio] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_Usurio_BTipoDocumento] FOREIGN KEY ([UserDocuemntTypeId]) REFERENCES [dbo].[BDocumentType] ([DocumentTypeId]),
    CONSTRAINT [FK_Usurio_TipoUsuario] FOREIGN KEY ([UserType]) REFERENCES [dbo].[UserType] ([UserTypeId])
);

