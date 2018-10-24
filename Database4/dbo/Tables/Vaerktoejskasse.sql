CREATE TABLE [dbo].[Vaerktoejskasse] (
    [VKasseId]    INT            IDENTITY (1, 1) NOT NULL,
    [Anskaffet]   DATE           NOT NULL,
    [Fabrikat]    NVARCHAR (50)  NULL,
    [EjesAf]      INT            NULL,
    [Model]       NVARCHAR (50)  NULL,
    [Serienummer] NVARCHAR (50)  NULL,
    [Farve]       NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Vaerktoejskasse] PRIMARY KEY CLUSTERED ([VKasseId] ASC),
    CONSTRAINT [FK_Vaerktoejskasse_ToHaandvaerker] FOREIGN KEY ([EjesAf]) REFERENCES [dbo].[Haandvaerker] ([HaandvaerkerId])
);

