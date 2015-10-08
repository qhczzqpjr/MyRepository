--Check Last updated datetime
SELECT    [TableName] = OBJECT_NAME(object_id),
last_user_update, last_user_seek, last_user_scan, last_user_lookup
FROM    sys.dm_db_index_usage_stats
WHERE    database_id = DB_ID('Staging')
AND        OBJECT_NAME(object_id) = 'MDM_QueueAttributes'

--Check last datetime in monitored table
SELECT MAX(ETLUpdatedDateTime) from DataWarehouse.Dim.MSGeography

--Check Schema updated datetime
SELECT    [TableName] = name,
create_date,
modify_date
FROM    sys.tables
WHERE    name = 'TransactionHistoryArchive'