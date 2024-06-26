﻿CREATE TABLE [Membership].[User]
(
    [Id] UNIQUEIDENTIFIER CONSTRAINT [PK_Membership_User] PRIMARY KEY,
    [Email] NVARCHAR(128) NOT NULL CONSTRAINT [UK_Membership_User_Email] UNIQUE,
    [PasswordHash] NVARCHAR(128) NOT NULL,
    [PasswordSalt] NVARCHAR(128) NOT NULL,
    [FirstName] NVARCHAR(128) NOT NULL,
    [LastName] NVARCHAR(128) NOT NULL,
    [StartDate] DATE NOT NULL
        CONSTRAINT CC_Membership_User_StartDate CHECK ([StartDate] BETWEEN '2001/01/01' AND '2100/12/31'),
    [EndDate] DATE NULL
        CONSTRAINT CC_Membership_User_EndDate CHECK ([EndDate] BETWEEN DATEADD(day, 1, [StartDate]) AND '2100/12/31'),
    [Department] NVARCHAR(128) NULL,
    [Position] NVARCHAR(128) NULL,
    [Location] NVARCHAR(128) NULL,
    [Photo] VARBINARY(MAX) NULL
)