CREATE TABLE [dbo].[Haandvaerker] (
    [HaandvaerkerId]  INT           IDENTITY (1, 1) NOT NULL,
    [Ansaettelsedato] DATE          NOT NULL,
    [Efternavn]       NVARCHAR (50) NOT NULL,
    [Fagomraade]      NVARCHAR (50) NULL,
    [Fornavn]         NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Haandvaerker] PRIMARY KEY CLUSTERED ([HaandvaerkerId] ASC)
);

