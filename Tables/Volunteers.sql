/* ---------- VOLUNTEERS (1:1 extension of Users) ---------- */
CREATE TABLE Volunteers (
    VolunteerId             INT IDENTITY(1,1) PRIMARY KEY,
    UserId                  INT NOT NULL UNIQUE,
    Skills                  VARCHAR(500) NULL,
    Availability            VARCHAR(255) NULL,
    BackgroundCheckStatus   VARCHAR(50) NOT NULL DEFAULT 'Pending'
        CHECK (BackgroundCheckStatus IN ('Pending','Cleared','Rejected')),
    RegisteredAt            DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_Volunteers_Users FOREIGN KEY (UserId) REFERENCES Users(UserId)
);