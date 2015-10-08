ALTER TABLE FACT.[ExonyTermination_Call_Detail_Achieve] REBUILD PARTITION = ALL
WITH (DATA_COMPRESSION = PAGE); 
GO

ALTER INDEX [UK_ExonyTermination_Call_Detail_RecoveryKey] ON FACT.ExonyTermination_Call_Detail DISABLE
GO
ALTER INDEX [PK_ExonyTermination_Call_Detail] ON FACT.ExonyTermination_Call_Detail DISABLE
GO

DECLARE @PARNO INT;
SELECT  @PARNO =1485
	
WHILE @PARNO<= 1850
BEGIN
    TRUNCATE TABLE FACT.[ExonyTermination_Call_Detail_Achieve];
	ALTER TABLE FACT.ExonyTermination_Call_Detail SWITCH PARTITION @PARNO TO FACT.[ExonyTermination_Call_Detail_Achieve] ;
	print @PARNO;
	select @PARNO = @PARNO+1
END  --17
-------------------------------
ALTER INDEX [UK_ExonyTermination_Call_Detail_RecoveryKey] ON FACT.ExonyTermination_Call_Detail REBUILD
 GO
 ALTER INDEX [PK_ExonyTermination_Call_Detail] ON FACT.ExonyTermination_Call_Detail REBUILD
 GO
