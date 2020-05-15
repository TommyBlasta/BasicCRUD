CREATE TABLE [dbo].[Vzdelavani] (
    [Id]       INT           NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [Subject]  NVARCHAR (60) NULL,
    [Duration] FLOAT (53)    NULL,
    [Date]     DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

