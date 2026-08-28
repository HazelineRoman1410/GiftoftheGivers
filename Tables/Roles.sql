/* ---------- ROLES ---------- */
CREATE TABLE Roles (
    RoleId      INT IDENTITY(1,1) PRIMARY KEY,
    RoleName    VARCHAR(50) NOT NULL UNIQUE
);