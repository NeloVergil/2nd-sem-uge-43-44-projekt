CREATE TABLE UserAuth (
    Id           INT            IDENTITY (1, 1) PRIMARY KEY,
    UserName     NVARCHAR (100) NOT NULL,
    PasswordHash NVARCHAR (255) NOT NULL,
    IsLeader     BIT            NOT NULL,
    CONSTRAINT UQ_UserAuth_UserName UNIQUE (UserName)
);