use Administrator;

go

CREATE TABLE admin.update_log_information (
    id INT IDENTITY(1,1) PRIMARY KEY,
    update_sl nvarchar(50),
    update_table_name NVARCHAR(100),
    update_column_name NVARCHAR(100),
    update_column_value NVARCHAR(MAX),
    old_column_value NVARCHAR(MAX),
    condition_primary_column NVARCHAR(100),
    condition_primary_value NVARCHAR(100),
    update_by NVARCHAR(50),
    update_date nvarchar(11),
	project_id nvarchar(6) ,
	is_reverced int
);

go

ALTER TABLE admin.update_log_information ADD CONSTRAINT FK_updateLogInfo_project_id FOREIGN KEY (project_id) REFERENCES admin.user_project_details (project_sh_name);

go

CREATE PROCEDURE admin.UpdateRecordDataLog 
	@slNo NVARCHAR(50),
	@tableName NVARCHAR(50),
	@columnName NVARCHAR(50),
	@columnValue NVARCHAR(50),
	@primaryColumnValue INT,
	@userid NVARCHAR(10),
		@date NVARCHAR(11),
		@projectId NVARCHAR(6),
		@isReverced int = 0
AS
BEGIN
	DECLARE @sesql NVARCHAR(max);
	DECLARE @old_value NVARCHAR(max);

	SET @sesql = 'SELECT @out_value = CAST(' + QUOTENAME(@columnName) + ' AS NVARCHAR(MAX)) ' + 'FROM ' + QUOTENAME(@tableName) + ' WHERE id = @id';

	-- Execute with sp_executesql
	EXEC sp_executesql @sesql,
		N'@id INT, @out_value NVARCHAR(MAX) OUTPUT',
		@id = @primaryColumnValue,
		@out_value = @old_value OUTPUT;

	DECLARE @sql NVARCHAR(max);

	IF TRY_CAST(@columnValue AS INT) IS NOT NULL
		OR TRY_CAST(@columnValue AS FLOAT) IS NOT NULL
	BEGIN
		SET @sql = 'update ' + QUOTENAME(@tableName) + ' set ' + QUOTENAME(@columnName) + ' = ' + @columnValue + ' where [id] = ' + cast(@primaryColumnValue AS NVARCHAR);
	END;
	ELSE
	BEGIN
		SET @sql = 'update ' + QUOTENAME(@tableName) + ' set ' + QUOTENAME(@columnName) + ' = ' + '''' + REPLACE(@columnValue, '''', '''''') + '''' + ' where [id] = ' + cast(@primaryColumnValue AS NVARCHAR);
	END

	EXEC (@sql);

	INSERT INTO [admin].[update_log_information] (
		[update_sl],
		[update_table_name],
		[update_column_name],
		[update_column_value],
		[old_column_value],
		[condition_primary_column],
		[condition_primary_value],
		[update_by],
		[update_date],
		[project_id] ,
		[is_reverced]
		)
	VALUES (
		@slNo,
		@tableName,
		@columnName,
		@columnValue,
		@old_value,
		'id',
		@primaryColumnValue,
		@userid,
		@date,
		@projectId,
		@isReverced
		);
END;

go

CREATE PROCEDURE admin.getUpdateLogInformationList @projectName NVARCHAR(11)
AS
BEGIN
	IF @projectName = 'SuperAdmin'
	BEGIN
		SELECT t.*
		FROM admin.update_log_information t;
	END
	ELSE
	BEGIN
		SELECT t.*
		FROM admin.update_log_information t
		WHERE t.project_id = @projectName;
	END
END;
