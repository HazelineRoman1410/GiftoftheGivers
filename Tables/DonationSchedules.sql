/* ---------- DONATION SCHEDULES (recurring donations) ---------- */
CREATE TABLE DonationSchedules (
    ScheduleId      INT IDENTITY(1,1) PRIMARY KEY,
    UserId          INT NOT NULL,
    Frequency       VARCHAR(20) NOT NULL
        CHECK (Frequency IN ('Weekly','Monthly','Quarterly')),
    StartDate       DATE NOT NULL,
    EndDate         DATE NULL,
    NextRunDate     DATE NOT NULL,
    Status          VARCHAR(20) NOT NULL DEFAULT 'Active'
        CHECK (Status IN ('Active','Paused','Cancelled')),
    CONSTRAINT FK_Schedules_Users FOREIGN KEY (UserId) REFERENCES Users(UserId)
);
