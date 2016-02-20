SELECT object_name, cntr_value
  FROM master.sys.dm_os_performance_counters
  WHERE counter_name IN ('Total Server Memory (KB)', 'Target Server Memory (KB)');

-- SQL Server 2012:
SELECT physical_memory_kb FROM master.sys.dm_os_sys_info;

-- Prior versions:
EXEC sp_configure 'max server memory';