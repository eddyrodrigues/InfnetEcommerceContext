-- Users.dbo.users_logins definition

-- Drop table

-- DROP TABLE Users.dbo.users_logins;

CREATE TABLE Users.dbo.users_logins (
	Id uniqueidentifier NOT NULL,
	UserName varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Password varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CreatedAt datetime DEFAULT getdate() NOT NULL,
	UpdatedAt datetime NOT NULL,
	CONSTRAINT PK_Users PRIMARY KEY (Id)
);