/* ---------- USERS ---------- */
CREATE TABLE Users (
    UserId          INT IDENTITY(1,1) PRIMARY KEY,
    FirstName       VARCHAR(100) NOT NULL,
    LastName        VARCHAR(100) NOT NULL,
    Email           VARCHAR(255) NOT NULL UNIQUE,
    PasswordHash    VARCHAR(255) NOT NULL,
    RoleId          INT NOT NULL,
    PhoneNumber     VARCHAR(20) NULL,
    CreatedAt       DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    IsActive        BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Users_Roles FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);