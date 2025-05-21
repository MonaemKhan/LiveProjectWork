use Administrator;

go

create table admin.user_project_details (
id INT IDENTITY(1,1) PRIMARY KEY,
    project_sh_name NVARCHAR(6) UNIQUE NOT NULL,
    project_title NVARCHAR(50) NOT NULL,
	project_live_date nvarchar(20) not null,
	project_description nvarchar(200) null
)

go 


INSERT INTO [admin].[user_project_details]
           ([project_sh_name]
           ,[project_title],[project_live_date])
     VALUES
           ('Jony','Jony Portfolio','5/12/2025');
GO

INSERT INTO [admin].[user_project_details]
           ([project_sh_name]
           ,[project_title],[project_live_date])
     VALUES
           ('PMS','Pharmacy MangeMent System','5/12/2025');

go

CREATE PROCEDURE admin.getUserProjectDetailsList @projectName NVARCHAR(11)
AS
BEGIN
	IF @projectName = 'SuperAdmin'
	BEGIN
		SELECT t.*
		FROM admin.user_project_details t;
	END
	ELSE
	BEGIN
		SELECT t.*
		FROM admin.user_project_details t
		WHERE t.project_sh_name = @projectName;
	END
END;

go

CREATE PROCEDURE admin.InsertProjectDetails
@projectshNm nvarchar(6),
@projectTitle nvarchar(50),
@date nvarchar(11),
@projectdes nvarchar(100)
AS
BEGIN

INSERT INTO [admin].[user_project_details]
           ([project_sh_name]
           ,[project_title]
           ,[project_live_date]
           ,[project_description])
     VALUES
           (@projectshNm,@projectTitle,@date,@projectdes) ;
END;

go