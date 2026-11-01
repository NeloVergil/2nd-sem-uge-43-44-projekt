CREATE TABLE UserActivity (
    UserId     INT NOT NULL,
    ActivityId INT NOT NULL,
    PRIMARY KEY (UserId, ActivityId),
    FOREIGN KEY (UserId) REFERENCES UserAuth (Id),
    FOREIGN KEY (ActivityId) REFERENCES Activity (Id)
);