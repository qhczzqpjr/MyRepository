
SELECT TOP 1000 
 [folder_name]
,[project_name]
,[package_name]
,DATEDIFF(ss,start_time,end_time)DurationInSeconds
,E.*
  FROM [SSISDB].[catalog].[projects] P 
  CROSS APPLY (
  SELECT TOP 1 *
  FROM [SSISDB].[catalog].[executions] 
  WHERE   [project_name] = p.[name] AND ( [status] = 7 )
  ORDER BY [start_time] DESC 
  ) E
  ORDER BY DATEDIFF(ss,start_time,end_time)  DESC
