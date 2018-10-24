CREATE TABLE [dbo].[Værktøj] (
    [VærktøjsId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [Anskaffet]  DATE           NOT NULL,
    [Fabrikat]   NVARCHAR (MAX) NULL,
    [Model]      NVARCHAR (50)  NULL,
    [Serienr]    NVARCHAR (50)  NULL,
    [Type]       NVARCHAR (50)  NULL,
    [LiggerIVTK] INT            NULL,
    CONSTRAINT [PK_Værktøj] PRIMARY KEY CLUSTERED ([VærktøjsId] ASC),
    CONSTRAINT [FK_Værktøj_Værktøjskasse] FOREIGN KEY ([LiggerIVTK]) REFERENCES [dbo].[Værktøjskasse] ([VKasseId])
);

