-- =============================================
-- Author:		김성민
-- Create date: 2020.05.04
-- Description:	이용자 예치금 통장을 기록하는 프로시져
-- =============================================
CREATE PROCEDURE [dbo].[USP_EXE_USER_PASSBOOK_UPDATE]		
	@RequestNo			varchar(15)
	,@EventCode			char(2)
	,@UserId			int
	,@Amount			int
	,@CreationDateTime	datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
	DECLARE @InNestedTransaction BIT;
	
	-- Insert statements for procedure here
	
	BEGIN TRY
		IF (@@TRANCOUNT = 0)
		BEGIN
			SET @InNestedTransaction = 0;
			BEGIN TRAN;
		END;
		ELSE
		BEGIN
			SET @InNestedTransaction = 1;
		END;		

		/************************
		* 프로시져 주요 로직 시작 *
		************************/

		INSERT INTO TB_USER_PASSBOOK
		(
			RequestNo			,EventCode
			,UserId				,Amount			,CreationDateTime
		)
		VALUES
		(
			@RequestNo			,@EventCode
			,@UserId			,@Amount		,@CreationDateTime
		)


		/************************
		* 프로시져 주요 로직 종료 *
		************************/

		IF (@@TRANCOUNT > 0 AND @InNestedTransaction = 0)
		BEGIN
			COMMIT;
		END;
	END TRY
	BEGIN CATCH

		IF (@@TRANCOUNT > 0 AND @InNestedTransaction = 0)
		BEGIN
			ROLLBACK;
		END;
			
		DECLARE @ErrorMessage NVARCHAR(4000);  
		DECLARE @ErrorSeverity INT;  
		DECLARE @ErrorState INT;  
  
		SELECT   
			@ErrorMessage = ERROR_MESSAGE(),  
			@ErrorSeverity = ERROR_SEVERITY(),  
			@ErrorState = ERROR_STATE();  
		
  
		-- Use RAISERROR inside the CATCH block to return error  
		-- information about the original error that caused  
		-- execution to jump to the CATCH block.  
		RAISERROR (@ErrorMessage, -- Message text.  
				   @ErrorSeverity, -- Severity.  
				   @ErrorState -- State.  
				   );  
		
	END CATCH;

END