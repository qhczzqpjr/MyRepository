DECLARE @sourceserver sysname
DECLARE @sourcedatabase sysname
DECLARE @sourceschema sysname
DECLARE @sourcetable nvarchar(1000)
DECLARE @destinationserver sysname
DECLARE @destinationdatabase sysname
DECLARE @destinationschema sysname
DECLARE @destinationtable nvarchar(1000)

select @sourceserver = 'HOUYAJUN' 
,@sourcedatabase = 'TEST'
,@sourceschema = 'dbo'
,@sourcetable = 'REPET'
,@destinationserver ='HOUYAJUN\SS005'
,@destinationdatabase =  'TEST'
,@destinationschema = 'dbo'
,@destinationtable = 'REPET'



SELECT 'tablediff -sourceserver ' + @sourceserver + ' -sourcedatabase ' +@sourcedatabase+ ' -sourceschema ' +@sourceschema + ' -sourcetable ' +@sourcetable + ' -destinationserver ' +@destinationserver+ ' -destinationdatabase ' +@destinationdatabase+
