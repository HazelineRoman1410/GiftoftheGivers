/* ---------- EMERGENCY UPDATES ---------- */
CREATE TABLE EmergencyUpdates (
    UpdateId            INT IDENTITY(1,1) PRIMARY KEY,
    ProjectId           INT NULL,
    Title               VARCHAR(200) NOT NULL,
    Content             VARCHAR(2000) NOT NULL,
    Severity            VARCHAR(20) NOT NULL DEFAULT 'Medium'
        CHECK (Severity IN ('Low','Medium','High','Critical')),
    PostedByUserId      INT NOT NULL,
    PostedAt            DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_Updates_Projects FOREIGN KEY (ProjectId) REFERENCES ReliefProjects(ProjectId),
    CONSTRAINT FK_Updates_Users FOREIGN KEY (PostedByUserId) REFERENCES Users(UserId)
);