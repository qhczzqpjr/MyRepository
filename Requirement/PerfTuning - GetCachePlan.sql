select top 1000 st.text, cp.cacheobjtype, cp.objtype, cp.refcounts, cp.usecounts, cp.size_in_bytes, cp.bucketid, cp.plan_handle,*

from sys.dm_exec_cached_plans cp 

cross apply sys.dm_exec_sql_text(cp.plan_handle) st

where cp.cacheobjtype = 'Compiled Plan' 

and st.text like '%raymlv2%'  and  cp.objtype = 'Prepared'


go


