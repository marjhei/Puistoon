CREATE TABLE [dbo].[Käyttäjä] (
    [Id]                 NVARCHAR (450) NOT NULL,
    [KäyttäjäTunnus]     NVARCHAR (256) NULL,
    [NormalizedUserName] NVARCHAR (256) NULL,
    [PasswordHash]       NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Käyttäjä] PRIMARY KEY CLUSTERED ([Id] ASC)
);