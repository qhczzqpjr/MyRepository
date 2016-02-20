/***********
Author: Thomas Zhu
Date: 2015-07-10
Comments: Add logic for StoreProcedures
*/

BEGIN TRAN
	BEGIN TRY


 
	COMMIT

	END TRY
	BEGIN CATCH
		
		

		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT 
			@ErrorMessage = ERROR_MESSAGE(),
			@ErrorSeverity = ERROR_SEVERITY(),
			@ErrorState = ERROR_STATE();
		 
		RAISERROR(	@ErrorMessage, 
					@ErrorSeverity,
					@ErrorState );
	ROLLBACK

	END CATCH


 

 


