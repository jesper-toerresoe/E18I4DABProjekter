CREATE TABLE [dbo].[Vaerktoej] (
    [VaerktoejsId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [Anskaffet]    DATE           NOT NULL,
    [Fabrikat]     NVARCHAR (MAX) NULL,
    [Model]        NVARCHAR (50)  NULL,
    [Serienr]      NVARCHAR (50)  NULL,
    [Type]         NVARCHAR (50)  NULL,
    [LiggerIVTK]   INT            NULL,
    CONSTRAINT [PK_Vaerktoej] PRIMARY KEY CLUSTERED ([VaerktoejsId] ASC),
    CONSTRAINT [FK_Vaerktoej_Vaerktsejskasse] FOREIGN KEY ([LiggerIVTK]) REFERENCES [dbo].[Vaerktoejskasse] ([VKasseId])
);

