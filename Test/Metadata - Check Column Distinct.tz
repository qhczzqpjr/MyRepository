DECLARE @sql NVARCHAR(MAX)
DECLARE @columnName NVARCHAR(50) = ''
DECLARE @tableName NVARCHAR(50) = ''

SELECT  @sql = 'SELECT 
	 ' + @columnName + '
	,COUNT(1) 
FROM  ' + @tableName + ' (NOLOCK)
GROUP BY  ' + @columnName + '
HAVING COUNT(1) >=2'

EXEC sp_executesql @sql