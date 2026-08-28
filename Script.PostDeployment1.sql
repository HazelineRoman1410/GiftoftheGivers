/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

 
   -- Roles
IF NOT EXISTS (SELECT 1 FROM Roles WHERE RoleName = 'Donor')
    INSERT INTO Roles (RoleName) VALUES ('Donor');
IF NOT EXISTS (SELECT 1 FROM Roles WHERE RoleName = 'Employee')
    INSERT INTO Roles (RoleName) VALUES ('Employee');
    
    -- Users
IF NOT EXISTS (SELECT 1 FROM Users WHERE Email = 'thabo.nkosi@example.com')
    INSERT INTO Users (FirstName, LastName, Email, PasswordHash, RoleId)
    VALUES ('Thabo', 'Nkosi', 'thabo.nkosi@example.com', 'HASHED_PW_1',
        (SELECT RoleId FROM Roles WHERE RoleName = 'Donor'));
IF NOT EXISTS (SELECT 1 FROM Users WHERE Email = 'sarah.botha@example.com')
    INSERT INTO Users (FirstName, LastName, Email, PasswordHash, RoleId)
    VALUES ('Sarah', 'Botha', 'sarah.botha@example.com', 'HASHED_PW_2',
        (SELECT RoleId FROM Roles WHERE RoleName = 'Employee'));
        
        -- Relief Projects
IF NOT EXISTS (SELECT 1 FROM ReliefProjects WHERE ProjectName = 'Flood Relief - KZN')
    INSERT INTO ReliefProjects (ProjectName, Description, Location, StartDate, Status, CreatedByUserId)
    VALUES ('Flood Relief - KZN', 'Emergency flood response', 'KwaZulu-Natal', '2026-01-15', 'Active',
        (SELECT UserId FROM Users WHERE Email = 'sarah.botha@example.com'));
        
        -- Volunteers
IF NOT EXISTS (
    SELECT 1 FROM Volunteers v
    JOIN Users u ON v.UserId = u.UserId
    WHERE u.Email = 'thabo.nkosi@example.com'
)
      INSERT INTO Volunteers (UserId, Skills, Availability)   
    VALUES (
        (SELECT UserId FROM Users WHERE Email = 'thabo.nkosi@example.com'),
        'First Aid, Logistics', 'Weekends'
    );