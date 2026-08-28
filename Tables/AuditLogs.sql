/* ---------- AUDIT LOGS ---------- */
CREATE TABLE AuditLogs (
    LogId           INT IDENTITY(1,1) PRIMARY KEY,
    UserId          INT NULL,
    Action          VARCHAR(50) NOT NULL,
    EntityName      VARCHAR(100) NOT NULL,
    EntityId        INT NULL,
    Timestamp       DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    Details         NVARCHAR(MAX) NULL,
    CONSTRAINT FK_AuditLogs_Users FOREIGN KEY (UserId) REFERENCES Users(UserId)
);