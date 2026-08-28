/* ---------- DONATIONS ---------- */
CREATE TABLE Donations (
    DonationId          INT IDENTITY(1,1) PRIMARY KEY,
    UserId              INT NULL,               -- NULL when anonymous
    ScheduleId          INT NULL,               -- NULL when once-off
    ProjectId           INT NULL,               -- NULL when going to general fund
    Amount              DECIMAL(18,2) NOT NULL CHECK (Amount > 0),
    Currency            CHAR(3) NOT NULL CHECK (Currency IN ('ZAR','USD','EUR')),
    DonationType        VARCHAR(20) NOT NULL CHECK (DonationType IN ('OnceOff','Recurring')),
    IsAnonymous         BIT NOT NULL DEFAULT 0,
    PaymentStatus       VARCHAR(20) NOT NULL DEFAULT 'Pending'
        CHECK (PaymentStatus IN ('Pending','Completed','Failed','Refunded')),
    TransactionDate     DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_Donations_Users FOREIGN KEY (UserId) REFERENCES Users(UserId),
    CONSTRAINT FK_Donations_Schedules FOREIGN KEY (ScheduleId) REFERENCES DonationSchedules(ScheduleId),
    CONSTRAINT FK_Donations_Projects FOREIGN KEY (ProjectId) REFERENCES ReliefProjects(ProjectId),
    -- If anonymous, UserId must be NULL (data integrity rule)
    CONSTRAINT CK_Donations_Anonymous CHECK (
        (IsAnonymous = 1 AND UserId IS NULL) OR (IsAnonymous = 0)
    )
);
