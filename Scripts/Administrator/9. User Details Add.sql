USE Administrator;
GO

CREATE SCHEMA _user AUTHORIZATION dbo;
GO

CREATE TABLE _user.user_details (
	id INT IDENTITY(1, 1) PRIMARY KEY,
	user_id NVARCHAR(50) UNIQUE NOT NULL,
	user_name NVARCHAR(100) NOT NULL,
	user_password NVARCHAR(100) NOT NULL,
	user_email NVARCHAR(100) UNIQUE NOT NULL,
	user_phone NVARCHAR(15) UNIQUE NOT NULL,
	user_dob NVARCHAR(11) NOT NULL,
	user_nid NVARCHAR(20) NOT NULL,
	user_county_id INT DEFAULT 14, -- FOREIGN KEY defined later
	user_division_id INT, -- FOREIGN KEY defined later
	user_district_id INT, -- FOREIGN KEY defined later
	user_upazilla_id INT, -- FOREIGN KEY defined later
	user_union_id INT, -- FOREIGN KEY defined later
	user_active INT DEFAULT 1,
	user_allow_access_any_office INT DEFAULT 0,
	STATUS NVARCHAR(7),
	make_by NVARCHAR(50),
	make_dt NVARCHAR(11),
	last_modify_by NVARCHAR(50),
	last_modify_dt NVARCHAR(11),
	project_id NVARCHAR(6),
	user_type nvarchar(10)
	);
GO

ALTER TABLE _user.user_details ADD CONSTRAINT FK_UserDetails_Country FOREIGN KEY (user_county_id) REFERENCES geo.countries (id);

ALTER TABLE _user.user_details ADD CONSTRAINT FK_UserDetails_Division FOREIGN KEY (user_division_id) REFERENCES geo.divisions (id);

ALTER TABLE _user.user_details ADD CONSTRAINT FK_UserDetails_District FOREIGN KEY (user_district_id) REFERENCES geo.districts (id);

ALTER TABLE _user.user_details ADD CONSTRAINT FK_UserDetails_Upazila FOREIGN KEY (user_upazilla_id) REFERENCES geo.upazillas (id);

ALTER TABLE _user.user_details ADD CONSTRAINT FK_UserDetails_Union FOREIGN KEY (user_union_id) REFERENCES geo.unions (id);

ALTER TABLE _user.user_details ADD CONSTRAINT FK_UserDetails_project FOREIGN KEY (project_id) REFERENCES admin.user_project_details (project_sh_name);
GO

INSERT INTO _user.user_details (
	user_id,
	user_name,
	user_password,
	user_email,
	user_phone,
	user_dob,
	user_nid,
	user_division_id,
	user_district_id,
	user_upazilla_id,
	user_union_id,
	STATUS,
	make_by,
	project_id,
	user_type
	)
VALUES (
	'USR001',
	'Monaem Khan',
	'Pass@123',
	'monaem@example.com',
	'01711111111',
	'1995-01-15',
	'1234567890',
	1,
	10,
	100,
	1000,
	'INS',
	'Admin',
	'Jony',
	'User'
	),
	(
	'USR002',
	'Tania Akter',
	'Pass@234',
	'tania@example.com',
	'01722222222',
	'1993-03-22',
	'2345678901',
	2,
	11,
	101,
	1001,
	'INS',
	'Admin',
	'Jony',
	'Admin'
	),
	(
	'USR003',
	'Rakib Hossain',
	'Pass@345',
	'rakib@example.com',
	'01733333333',
	'1990-06-10',
	'3456789012',
	3,
	12,
	102,
	1002,
	'INS',
	'Admin',
	'PMS',
	'User'
	),
	(
	'USR004',
	'Nusrat Jahan',
	'Pass@456',
	'nusrat@example.com',
	'01744444444',
	'1992-09-25',
	'4567890123',
	4,
	13,
	103,
	1003,
	'INS',
	'Admin',
	'PMS',
	'Admin'
	),
	(
	'USR005',
	'Hasib Mahmud',
	'Pass@567',
	'hasib@example.com',
	'01755555555',
	'1996-12-30',
	'5678901234',
	5,
	14,
	104,
	1004,
	'INS',
	'Admin',
	'Jony',
	'User'
	);
GO

SELECT *
FROM _user.user_details;
GO

CREATE PROCEDURE _user.getUserList @projectName NVARCHAR(11),
	@allUser INT = 0,
	@allDeactiveUser INT = 0
AS
BEGIN
	DECLARE @status1 INT = 1;
	DECLARE @status2 INT = 1;

	IF @allUser = 1
	BEGIN
		SET @status1 = 1;
		SET @status2 = 0;
	END

	IF @allDeactiveUser = 1
	BEGIN
		SET @status1 = 0;
		SET @status2 = 0;
	END

	IF @projectName = 'SuperAdmin'
	BEGIN
		SELECT t.*,
			c.country_name,
			div.name AS division_name,
			dis.name AS district_name,
			upz.name AS upozilla_name,
			uno.name AS union_name
		FROM _user.user_details t,
			geo.countries c,
			geo.divisions div,
			geo.districts dis,
			geo.upazillas upz,
			geo.unions uno
		WHERE t.user_county_id = c.id
			AND t.user_division_id = div.id
			AND t.user_district_id = dis.id
			AND t.user_upazilla_id = upz.id
			AND t.user_union_id = uno.id
			AND t.user_active IN (@status1, @status2);
	END
	ELSE
	BEGIN
		SELECT t.*,
			c.country_name,
			div.name AS division_name,
			dis.name AS district_name,
			upz.name AS upozilla_name,
			uno.name AS union_name
		FROM _user.user_details t,
			geo.countries c,
			geo.divisions div,
			geo.districts dis,
			geo.upazillas upz,
			geo.unions uno
		WHERE t.user_county_id = c.id
			AND t.user_division_id = div.id
			AND t.user_district_id = dis.id
			AND t.user_upazilla_id = upz.id
			AND t.user_union_id = uno.id
			AND t.user_active IN (@status1, @status2)
			AND t.project_id = @projectName;
	END
END;
GO

CREATE PROCEDURE _user.getUserListById @projectName NVARCHAR(11),
	@userId NVARCHAR(10)
AS
BEGIN
	IF @projectName = 'SuperAdmin'
	BEGIN
		SELECT t.*,
			c.country_name,
			div.name AS division_name,
			dis.name AS district_name,
			upz.name AS upozilla_name,
			uno.name AS union_name
		FROM _user.user_details t,
			geo.countries c,
			geo.divisions div,
			geo.districts dis,
			geo.upazillas upz,
			geo.unions uno
		WHERE t.user_county_id = c.id
			AND t.user_division_id = div.id
			AND t.user_district_id = dis.id
			AND t.user_upazilla_id = upz.id
			AND t.user_id = @userId
			AND t.user_union_id = uno.id;
	END
	ELSE
	BEGIN
		SELECT t.*,
			c.country_name,
			div.name AS division_name,
			dis.name AS district_name,
			upz.name AS upozilla_name,
			uno.name AS union_name
		FROM _user.user_details t,
			geo.countries c,
			geo.divisions div,
			geo.districts dis,
			geo.upazillas upz,
			geo.unions uno
		WHERE t.user_county_id = c.id
			AND t.user_division_id = div.id
			AND t.user_district_id = dis.id
			AND t.user_upazilla_id = upz.id
			AND t.user_union_id = uno.id
			AND t.user_id = @userId
			AND t.project_id = @projectName;
	END
END;