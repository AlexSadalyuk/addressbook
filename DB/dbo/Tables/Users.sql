CREATE TABLE [dbo].[Users] (
    [UserId]    INT           IDENTITY (1, 1) NOT NULL,
    [Firstname] NVARCHAR (50) NULL,
    [Lastname]  NVARCHAR (50) NULL,
    [Address]   NVARCHAR (50) NULL,
    [City]      NVARCHAR (50) NULL,
    [Country]   NVARCHAR (50) NULL,
    [Company]   NVARCHAR (50) NULL,
    [Domain]    NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC)
);



