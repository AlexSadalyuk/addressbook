CREATE TABLE [dbo].[Phones] (
    [Number]   NVARCHAR (50) NOT NULL,
    [IsActive] BIT           NOT NULL,
    [PhoneId]  INT           IDENTITY (1, 1) NOT NULL,
    [UserId]   INT           NULL,
    PRIMARY KEY CLUSTERED ([PhoneId] ASC),
    CONSTRAINT [FK_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId]) ON DELETE CASCADE
);

