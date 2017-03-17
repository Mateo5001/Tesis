CREATE TABLE [dbo].[BDocumentType] (
    [DocumentTypeId]   INT          IDENTITY (1, 1) NOT NULL,
    [DocuemntTypeCode] VARCHAR (3)  NOT NULL,
    [DocumentTypeName] VARCHAR (30) NOT NULL,
    CONSTRAINT [PK_BTipoDocumento] PRIMARY KEY CLUSTERED ([DocumentTypeId] ASC)
);

