/* ---------- TAX CERTIFICATES (1:1 with Donations) ---------- */
CREATE TABLE TaxCertificates (
    CertificateId       INT IDENTITY(1,1) PRIMARY KEY,
    DonationId          INT NOT NULL UNIQUE,
    CertificateNumber   VARCHAR(50) NOT NULL UNIQUE,
    IssueDate           DATE NOT NULL DEFAULT CAST(SYSUTCDATETIME() AS DATE),
    TaxYear             INT NOT NULL,
    Amount              DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_Certificates_Donations FOREIGN KEY (DonationId) REFERENCES Donations(DonationId)
);
