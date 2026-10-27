CREATE TABLE Activity (
    Id                  INT            IDENTITY (1, 1) PRIMARY KEY,
    ActivityName        NVARCHAR (100) NOT NULL,
    ActivityDescription NVARCHAR (255) NOT NULL,
    ImagePath           NVARCHAR (255) NULL
);