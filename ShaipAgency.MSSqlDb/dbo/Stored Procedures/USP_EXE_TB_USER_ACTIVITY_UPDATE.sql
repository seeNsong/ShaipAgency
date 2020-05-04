
-- =============================================
-- Author:		김성민
-- Create date: 2020.05.04
-- Description:	사용자 활동정보 업데이트 하는 프로시져
-- =============================================
CREATE PROCEDURE [dbo].[USP_EXE_TB_USER_ACTIVITY_UPDATE]	
	@EventCode		varchar(2)
	,@UserId		int

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
		
		DECLARE @NowDatetime DATETIME = GETDATE();

		-- 예치금이 사용되는 이벤트에 실행시 연속 취소 횟수 초기화
		IF (@EventCode = '21'
			OR @EventCode = '')
		BEGIN
			UPDATE	TB_USER_ACTIVITY
			SET		RequestCount = RequestCount + 1
					,BacktoBackCancellationCount = 0					
			WHERE	UserId	=  @UserId;			
		END;

		-- 회원가입
		IF @EventCode = '00'
		BEGIN
			INSERT INTO TB_USER_ACTIVITY
			(
				UserId							,RequestCount			,CancellationCount		
				,BacktoBackCancellationCount	,Grade					,Blocked				
				,SignUpDateTime					,LastSignInDateTime		,WithdrawalDateTime			
			)
			VALUES
			(
				@UserId,						0,						0,
				0,								0,						0,
				@NowDatetime,					@NowDatetime,			@NowDatetime
			);

			
		END;

		-- 회원탈퇴
		ELSE IF @EventCode = '01'
		BEGIN
			UPDATE		TB_USER_ACTIVITY
			SET			WithdrawalDateTime	= @NowDatetime			
			WHERE		UserId	= @UserId;

		END;

		-- 로그인
		ELSE IF @EventCode = '02'
		BEGIN
			UPDATE		TB_USER_ACTIVITY
			SET			LastSignInDateTime	= @NowDatetime			
			WHERE		UserId	= @UserId;

		END;
		
		-- 로그아웃
		--ELSE IF @EventCode = '03'
		--BEGIN
		--	RETURN;

		--END;
		;

		-- 의뢰등록(이용자)
		--ELSE IF @EventCode = '10'
		--BEGIN			
		--	RETURN;
		--END;
		;
		
		-- 의뢰취소(이용자,샤입)
		ELSE IF @EventCode = '11' OR @EventCode = '15'
		BEGIN		

			-- 업무방해(고의적 취소) 판단 => 블락

			DECLARE @BacktoBackCancellationCount int;
			SET @BacktoBackCancellationCount = (SELECT BacktoBackCancellationCount FROM TB_USER_ACTIVITY WITH(NOLOCK) WHERE UserId = @UserId);

			-- 연속취소 횟수가 3회에 이르면 블락처리
			IF (@BacktoBackCancellationCount = 2)
			BEGIN
				UPDATE	TB_USER_ACTIVITY
				SET		BacktoBackCancellationCount = 3
						,CancellationCount = CancellationCount + 1
						,Blocked = 1
				WHERE	UserId = @UserId;
			END;
			-- 취소가 1회라도 발생하면 연속취소 횟수를 반영
			ELSE IF (@BacktoBackCancellationCount >= 0)
			BEGIN
				UPDATE	TB_USER_ACTIVITY
				SET		CancellationCount = CancellationCount + 1
						,BacktoBackCancellationCount = BacktoBackCancellationCount + 1						
				WHERE	UserId = @UserId;
			END;

		END;
		
		-- 의뢰수정(이용자)
		--ELSE IF @EventCode = '12'
		--BEGIN			
		--	RETURN;
		--END;
		;

		-- 의뢰삭제(이용자)
		--ELSE IF @EventCode = '13'
		--BEGIN			
		--	RETURN;
		--END;
		;

		-- 의뢰취소(샤입)
		--ELSE IF @EventCode = '15'
		--BEGIN			
		--	RETURN;
		--END;
		;

		-- 의뢰접수(샤입)
		--ELSE IF @EventCode = '16'
		--BEGIN			
		--	RETURN;
		--END;
		;


		-- 입금확인
		--ELSE IF @EventCode = '20'
		--BEGIN			
		--	RETURN;
		--END;
		;

		-- 예치금충전(시스템)
		ELSE IF @EventCode = '21'
		BEGIN			
			UPDATE		TB_USER_ACTIVITY
			SET			RequestCount = RequestCount + 1
						,BacktoBackCancellationCount = 0
			WHERE		UserId = @UserId
		END;

		-- 예치금출금이체(시스템)
		--ELSE IF @EventCode = '22'
		--BEGIN			
		--	RETURN;
		--END;
		;


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