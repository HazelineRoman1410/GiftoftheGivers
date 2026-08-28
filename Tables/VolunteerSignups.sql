/* ---------- VOLUNTEER SIGNUPS (many-to-many junction) ---------- */
CREATE TABLE VolunteerSignups (
    SignupId        INT IDENTITY(1,1) PRIMARY KEY,
    VolunteerId     INT NOT NULL,
    ProjectId       INT NOT NULL,
    SignupDate      DATE NOT NULL DEFAULT CAST(SYSUTCDATETIME() AS DATE),
    Status          VARCHAR(20) NOT NULL DEFAULT 'Pending'
        CHECK (Status IN ('Pending','Confirmed','Completed','Cancelled')),
    CONSTRAINT FK_Signups_Volunteers FOREIGN KEY (VolunteerId) REFERENCES Volunteers(VolunteerId),
    CONSTRAINT FK_Signups_Projects FOREIGN KEY (ProjectId) REFERENCES ReliefProjects(ProjectId),
    CONSTRAINT UQ_Signups_VolunteerProject UNIQUE (VolunteerId, ProjectId)
);