use Administrator;

go

create schema _session authorization dbo;

go

CREATE TABLE _session.userSessions (
    SessionId nvarchar(100) PRIMARY KEY not null,
    UserId NVARCHAR(50) NOT NULL,
    AccessToken NVARCHAR(MAX) NULL,       -- store JWT or reference token (encrypt/hash for safety)
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    ExpiresAt DATETIME2 NOT NULL,         -- session expiration timestamp
);

go
