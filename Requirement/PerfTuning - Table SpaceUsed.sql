/*
sp_spaceused [[ @objname = ] 'objname' ] 
[, [ @updateusage = ] 'updateusage' ] -- DBCC UPDATEUSAGE
[, [ @mode = ] 'mode' ]  --Stretch Databases; OLTP DB feature; 2014/2016
[, [ @oneresultset = ] oneresultset ] -- 2014/2016
*/
EXEC sp_spaceused @objname='TEST.dbo.MyLog',@updateusage=N'false'