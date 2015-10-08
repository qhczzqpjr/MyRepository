SELECT 
	sc.name AS PartitionScheme
	, fn.name AS PartitionFunction
	, fn.type
	,fn.type_desc
	,rv.boundary_id
	,rv.value
FROM sys.partition_range_values rv 
join sys.partition_functions fn
  on rv.function_id = fn.function_id
join sys.partition_schemes sc
  on fn.function_id = sc.function_id