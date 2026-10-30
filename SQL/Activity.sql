CREATE TABLE Activity (
    Id                   INT            IDENTITY (1, 1) PRIMARY KEY,
    Name                 NVARCHAR (100) NOT NULL,
    Description          NVARCHAR (255) NOT NULL,
    DayOfWeek            NVARCHAR (20)  NOT NULL,
    TimeSlot             NVARCHAR (20)  NOT NULL,
    MinimumAge           INT            NOT NULL,
    MaxParticipantNumber INT            NOT NULL,
    ImagesPath           NVARCHAR (255) NULL
);