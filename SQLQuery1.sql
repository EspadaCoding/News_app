 
CREATE TABLE [dbo].[Like] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [NewsId]    INT            NOT NULL,
    [UserID]    NVARCHAR (450) NULL,
    [LikedDate] DATETIME2 (7)  NOT NULL,
    [Liked]     BIT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([NewsId]) REFERENCES [dbo].[News] ([Id]),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[AspNetUsers] ([Id])
);
 
CREATE TABLE [dbo].[News] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Title]      NVARCHAR (100) NOT NULL,
    [Content]    NVARCHAR (MAX) NOT NULL,
    [PosterUrl]  NVARCHAR (MAX) NULL,
    [Quantity]   INT            NOT NULL,
    [Likes]      INT            NOT NULL,
    [CategoryId] INT            NOT NULL,
    [Date]       DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_News_Category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([Id]) ON DELETE CASCADE,
    CHECK ([Likes]>=(0))
);
 
CREATE TABLE [dbo].[NewsTag] (
    [Id]     INT IDENTITY (1, 1) NOT NULL,
    [NewsId] INT NOT NULL,
    [TagId]  INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([NewsId]) REFERENCES [dbo].[News] ([Id]),
    FOREIGN KEY ([TagId]) REFERENCES [dbo].[Tag] ([Id])
);
 
CREATE TABLE [dbo].[Tag] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (60) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
 
CREATE TABLE [dbo].[Category] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (60) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);