-- =============================================
-- Author:		김성민
-- Create date: 2020.05.04
-- Description:	샤입 USP 기본 템플릿 (커밋,롤백 -> 케스케이딩)
-- =============================================
CREATE PROCEDURE [dbo].[USP_SAHIP_TEMPLATE]	
	@RequestNo	varchar(15)
	,@EventCode	char(2)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
	DECLARE @InNestedTransaction BIT;
	DECLARE @ReturnMessage		NVARCHAR(100);
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




		/************************
		* 프로시져 주요 로직 종료 *
		************************/

		IF (@@TRANCOUNT > 0 AND @InNestedTransaction = 0)
		BEGIN
			COMMIT;
			SELECT @ReturnMessage;

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
		
		SELECT 'NG, 처리 중 오류가 발생하여 처리에 실패하였습니다.'

	END CATCH;

END