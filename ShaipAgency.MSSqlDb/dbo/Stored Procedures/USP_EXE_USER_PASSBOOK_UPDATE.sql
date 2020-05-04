
-- =============================================
-- Author:		김성민
-- Create date: 2020.05.04
-- Description:	이용자 예치금 통장을 기록하는 프로시져
-- =============================================
CREATE PROCEDURE [dbo].[USP_EXE_USER_PASSBOOK_UPDATE]	
	@UserId		int
	,@IsRequest	bit
	,@IsCancel	bit
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

		-- 의뢰확정
		IF @IsRequest = 1 AND @IsCancel = 0
		BEGIN
			UPDATE	USER_ACTIVITY
			SET		RequestCount = RequestCount + 1
					,BacktoBackCancellationCount = 0					
			WHERE	UserId	=  @UserId;

		END;
		-- 의뢰취소
		ELSE IF @IsRequest = 0 AND @IsCancel = 1
		BEGIN

			DECLARE @BacktoBackCancellationCount int;
			SET @BacktoBackCancellationCount = (SELECT BacktoBackCancellationCount FROM USER_ACTIVITY WITH(NOLOCK) WHERE UserId = @UserId);

			-- 연속취소 횟수가 3회에 이르면 블락처리
			IF (@BacktoBackCancellationCount = 2)
			BEGIN
				UPDATE	USER_ACTIVITY
				SET		BacktoBackCancellationCount = 3
						,CancellationCount = CancellationCount + 1
						,Blocked = 1
				WHERE	UserId = @UserId;
			END;
			-- 취소가 1회라도 발생하면 연속취소 횟수를 반영
			ELSE IF (@BacktoBackCancellationCount >= 0)
			BEGIN
				UPDATE	USER_ACTIVITY
				SET		CancellationCount = CancellationCount + 1
						,BacktoBackCancellationCount = BacktoBackCancellationCount + 1						
				WHERE	UserId = @UserId;
			END;

		END;
		ELSE
		BEGIN
			SELECT 'NG 파라미터 오류';
		END;

		IF (@@TRANCOUNT > 0 AND @InNestedTransaction = 0)
		BEGIN
			COMMIT;
			SELECT 'OK'

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