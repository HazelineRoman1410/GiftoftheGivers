/* ---------- RELIEF PROJECTS ---------- */
CREATE TABLE ReliefProjects (
    ProjectId           INT IDENTITY(1,1) PRIMARY KEY,
    ProjectName         VARCHAR(200) NOT NULL,
    Description         VARCHAR(1000) NULL,
    Location            VARCHAR(200) NULL,
    StartDate           DATE NOT NULL,
    EndDate             DATE NULL,
    Status              VARCHAR(50) NOT NULL DEFAULT 'Planned'
        CHECK (Status IN ('Planned','Active','Completed','Cancelled')),
    CreatedByUserId     INT NOT NULL,
    CONSTRAINT FK_Projects_Users FOREIGN KEY (CreatedByUserId) REFERENCES Users(UserId),
    CONSTRAINT CK_Projects_Dates CHECK (EndDate IS NULL OR EndDate >= StartDate)
);