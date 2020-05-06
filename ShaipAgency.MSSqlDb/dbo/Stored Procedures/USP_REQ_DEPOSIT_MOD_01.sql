-- =============================================
-- Author:		김성민
-- Create date: 2020.05.03
-- Description:	예치금관련 요청 처리 하는 프로시져
-- =============================================
CREATE PROCEDURE [dbo].[USP_REQ_DEPOSIT_MOD_01]	
	@RequestCode	char(2)
	,@RequestNo		varchar(15)		= ''
	,@DepositDate	datetime
	,@Amount		int
	,@PersonName	Nvarchar(20)
	,@AccountNumber	Nvarchar(40)	= ''
	,@UserId		int
	,@EventCode		char(2)

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

		DECLARE @NowDatetime DATETIME = GETDATE();
		
		DECLARE @RequestTitle NVARCHAR(512);
		DECLARE @RequestName NVARCHAR(40);

		DECLARE	@NextStatusCode char(2) = dbo.UFN_GET_NEXT_STATUS(@RequestCode, @EventCode);
		
		-- 신규요청 '10'
		IF ( @EventCode = '10' )
		BEGIN

			SET @RequestName = (SELECT RequestName FROM TB_STD_REQUEST_CODE WITH(NOLOCK) WHERE RequestCode = @RequestCode);
			SET @RequestTitle = @RequestName + N'금액 ' + FORMAT(@Amount, '#,###') + N' 원 요청';


			-- 충전신청 '10'
			IF (@RequestCode = '10')	
			BEGIN
				SET @RequestNo = [dbo].[UFN_GET_REQUEST_NO](@NowDatetime);
				-- 신규의뢰요청

				-- INSERT TB_REQ_MASTERS
				-- 예치금충전/환불 요청은 Detail이 1만 존재
				INSERT INTO	TB_REQ_MASTERS
				(
					RequestNo			,RequestCode			,RequestUserId			,RequestTitle
					,DetailCount		,RemainClaims			,DeliveryGroupNo		,IsLocked
					,CreationDateTime	,ModificationDateTime
				)
				VALUES
				(
					@RequestNo,			@RequestCode,			@UserId,				@RequestTitle,
					1,					0,						@RequestNo,				0,
					@NowDatetime,		@NowDatetime			
				);	

				-- INSERT TB_REQ_DETAILS_DEPOSIT
				INSERT INTO TB_REQ_DETAILS_DEPOSIT
				(
					RequestNo			,DepositDate			,Amount				,PersonName			,AccountNumber,		RequestStatusCode
				)
				VALUES
				(
					@RequestNo,			@DepositDate,			@Amount,			@PersonName,		@AccountNumber,		@NextStatusCode
				);			
			
				-- INSERT TB_REQ_TIMESTAMPS				
				INSERT INTO TB_REQ_TIMESTAMPS
				(
					RequestNo			,DetailNo				,EventCode				,[Timestamp]
				)
				VALUES
				(
					@RequestNo,			1,						@EventCode,				@NowDatetime				
				);

				-- UPDATE USER_ACTIVITY
				-- 필요 없음 (단순 등록은 사용자 활동 내역에 영향을 미치지 않음 (2020.05.05 기준)
				--EXEC USP_EXE_TB_USER_ACTIVITY_UPDATE @EventCode = @EventCode, @UserId = @UserId


				-- RETURN MESSAGE
				SET	@ReturnMessage = N'OK, 의뢰등록 성공 : ' + @RequestTitle;

			END;
			;
			-- 환불신청 '11'
			ELSE IF (@RequestCode = '11')
			BEGIN
				SET @RequestNo = [dbo].[UFN_GET_REQUEST_NO](@NowDatetime);
				-- 신규의뢰요청

				-- INSERT TB_REQ_MASTERS
				-- 예치금충전/환불 요청은 Detail이 1만 존재
				INSERT INTO	TB_REQ_MASTERS
				(
					RequestNo			,RequestCode			,RequestUserId			,RequestTitle
					,DetailCount		,RemainClaims			,DeliveryGroupNo		,IsLocked
					,CreationDateTime	,ModificationDateTime
				)
				VALUES
				(
					@RequestNo,			@RequestCode,			@UserId,				@RequestTitle,
					1,					0,						@RequestNo,				0,
					@NowDatetime,		@NowDatetime			
				);	

				-- INSERT TB_REQ_DETAILS_DEPOSIT
				INSERT INTO TB_REQ_DETAILS_DEPOSIT
				(
					RequestNo			,DepositDate			,Amount				,PersonName			,AccountNumber,		RequestStatusCode
				)
				VALUES
				(
					@RequestNo,			@DepositDate,			@Amount,			@PersonName,		@AccountNumber,		@NextStatusCode
				);			
			
				-- INSERT TB_REQ_TIMESTAMPS				
				INSERT INTO TB_REQ_TIMESTAMPS
				(
					RequestNo			,DetailNo				,EventCode				,[Timestamp]
				)
				VALUES
				(
					@RequestNo,			1,						@EventCode,				@NowDatetime				
				);

				-- UPDATE USER_ACTIVITY
				-- 필요 없음 (단순 등록은 사용자 활동 내역에 영향을 미치지 않음 (2020.05.05 기준)
				--EXEC USP_EXE_TB_USER_ACTIVITY_UPDATE @EventCode = @EventCode, @UserId = @UserId


				-- RETURN MESSAGE
				SET	@ReturnMessage = N'OK, 의뢰등록 성공 : ' + @RequestTitle;

			END;
		END;
		
		-- 의뢰취소(이용자)		
		ELSE IF (@EventCode = '11' )
		BEGIN

			-- TB_REQ_MASTERS 의 IsLocked = 0 : 취소 및 수정 가능
			IF ( SELECT IsLocked FROM TB_REQ_MASTERS WITH(NOLOCK) WHERE RequestNo = @RequestNo ) = 0
			BEGIN
				
				-- UPDATE TB_REQ_DETAILS_DEPOSIT
				UPDATE	TB_REQ_DETAILS_DEPOSIT
				SET		RequestStatusCode	= @NextStatusCode
				WHERE	RequestNo			= @RequestNo;
				--AND	DetailNo			= 1

				-- INSERT TB_REQ_TIMESTAMPS				
				INSERT INTO TB_REQ_TIMESTAMPS
				(
					RequestNo			,DetailNo				,EventCode				,[Timestamp]
				)
				VALUES
				(
					@RequestNo,			1,						@EventCode,				@NowDatetime				
				);

				-- UPDATE USER_ACTIVITY		
				-- 필요 없음 ( 등록 후 락킹 걸린 상태에서 취소 시 Block 판단 )
				EXEC USP_EXE_TB_USER_ACTIVITY_UPDATE @EventCode = @EventCode, @UserId = @UserId;


				-- RETURN MESSAGE
				SET @ReturnMessage = N'OK, 의뢰번호 : '+ @RequestNo +N' 취소 성공';

			END;
			ELSE
			BEGIN
				-- RETURN MESSAGE
				SELECT N'NG, 의뢰취소 실패 : 취소할 수 없는 상태입니다. 관리자에게 문의해주세요. IsLocked = 1' 
				RETURN ;
			END

		END;
		-- 의뢰수정(이용자)
		ELSE IF (@EventCode = '12' )
		BEGIN	
		
			SET @RequestName = (SELECT RequestName FROM TB_STD_REQUEST_CODE WITH(NOLOCK) WHERE RequestCode = @RequestCode);
			SET @RequestTitle = @RequestName + N'금액 ' + FORMAT(@Amount, '#,###') + N' 원 요청';

			
			-- UPDATE TB_REQ_MASTERS
			UPDATE	TB_REQ_MASTERS
			SET		RequestTitle = @RequestTitle
					,ModificationDateTime = @NowDatetime
			WHERE	RequestNo = @RequestNo;
				
			-- UPDATE TB_REQ_DETAILS_DEPOSIT
			UPDATE	TB_REQ_DETAILS_DEPOSIT
			SET		DepositDate	= @DepositDate
					,Amount		= @Amount
					,PersonName = @PersonName
			WHERE	RequestNo	= @RequestNo;
			--AND	DetailNo	= 1
						
			-- INSERT TB_REQ_TIMESTAMPS
			INSERT INTO REQ_TIMESTAMPS
			(
				RequestNo		,DetailNo		,EventCode		,[Timestamp]
			)
			VALUES
			(
				@RequestNo,		1,				@EventCode,		@NowDatetime
			);
			
			-- UPDATE USER_ACTIVITY	 => 필요없음

			-- RETURN MESSAGE
			SET @ReturnMessage = N'OK, 의뢰번호 : ' + @RequestNo + N' 수정을 완료하였습니다.'

		END;
		-- 의뢰삭제(이용자)
		ELSE IF (@EventCode = '13' )
		BEGIN
			
			-- 접수 대기인 상황에서만 삭제 가능
			IF ( SELECT RequestStatusCode FROM TB_REQ_DETAILS_DEPOSIT WITH(NOLOCK) WHERE RequestNo = @RequestCode ) = '01'
			BEGIN
				
				DELETE	TB_REQ_DETAILS_DEPOSIT
				WHERE	RequestNo	= @RequestNo;
				--AND		DetailNo	= 1

				DELETE	TB_REQ_MASTERS
				WHERE	RequestNo = @RequestNo;
				

				-- INSERT TB_REQ_TIMESTAMPS					
				INSERT INTO TB_REQ_TIMESTAMPS
				(
					RequestNo			,DetailNo				,EventCode				,[Timestamp]
				)
				VALUES
				(
					@RequestNo,			1,						@EventCode,				@NowDatetime				
				);

				-- UPDATE USER_ACTIVITY	 => 필요없음

				-- RETURN MESSAGE
				SET @ReturnMessage = N'OK, 의뢰번호 : ' + @RequestNo + N' 삭제를 완료하였습니다.';

			END;
			ELSE
			BEGIN				
				-- RETURN MESSAGE
				SELECT N'NG, 삭제 실패 : 접수 대기인 상태에서만 삭제가 가능합니다. RequestStatusCode <> ''01''' ;
				RETURN ;
				
			END;
		END;
		-- 의뢰취소(샤입)
		ELSE IF (@EventCode = '15' )
		BEGIN
				
			-- UPDATE TB_REQ_DETAILS_DEPOSIT
			UPDATE	TB_REQ_DETAILS_DEPOSIT
			SET		RequestStatusCode = @NextStatusCode
			WHERE	RequestNo	= @RequestNo;
			--AND	DetailNo	= 1

			-- INSERT TB_REQ_TIMESTAMPS				
			INSERT INTO TB_REQ_TIMESTAMPS
			(
				RequestNo			,DetailNo				,EventCode				,[Timestamp]
			)
			VALUES
			(
				@RequestNo,			1,						@EventCode,				@NowDatetime				
			);

			-- UPDATE USER_ACTIVITY		
			-- 필요 없음 ( 등록 후 락킹 걸린 상태에서 취소 시 Block 판단 )
			EXEC USP_EXE_TB_USER_ACTIVITY_UPDATE @EventCode = @EventCode, @UserId = @UserId;


			-- RETURN MESSAGE
			SET @ReturnMessage = N'OK, 의뢰번호 : '+ @RequestNo +N' 취소 성공(샤입)';
		END;
		-- 의뢰접수
		ELSE IF (@EventCode = '16' )
		BEGIN

			-- UPDATE TB_REQ_DETAILS_DEPOSIT
			UPDATE	TB_REQ_DETAILS_DEPOSIT
			SET		RequestStatusCode	= @NextStatusCode
			WHERE	RequestNo			= @RequestNo;
			--AND		DetailNo		= 1


			-- INSERT TB_REQ_TIMESTAMPS				
			INSERT INTO TB_REQ_TIMESTAMPS
			(
				RequestNo			,DetailNo				,EventCode				,[Timestamp]
			)
			VALUES
			(
				@RequestNo,			1,						@EventCode,				@NowDatetime				
			);

		END;	

		/********************************************************************
		*				의뢰별 접수 이후 주요 로직 구현	- ELSE IF 문			*
		********************************************************************/
		-- 입금확인(샤입)
		--ELSE IF (@EventCode = '20' )
		--BEGIN
		--	RETURN;
		--END;
		;
		-- 예치금 충전
		ELSE IF (@EventCode = '21' )
		BEGIN					

			-- UPDATE TB_REQ_DETAILS_DEPOSIT
			UPDATE	TB_REQ_DETAILS_DEPOSIT
			SET		RequestStatusCode	= @NextStatusCode
			WHERE	RequestNo			= @RequestNo;
			--AND		DetailNo		= 1

			-- TB_REQ_TIMESTAMPS UPDATE
			INSERT INTO TB_REQ_TIMESTAMPS	(RequestNo		,DetailNo		,EventCode		,[Timestamp])
			VALUES							(@RequestNo,	1,				@EventCode,		@NowDatetime);			
			
			-- UPDATE USER_ACTIVITY					
			EXEC USP_EXE_TB_USER_ACTIVITY_UPDATE @EventCode = @EventCode, @UserId = @UserId;

			-- TB_USER_PASSBOOK UPDATE
			EXEC USP_EXE_TB_USER_ACTIVITY_UPDATE  @RequestNo = @RequestNo ,@EventCode = @EventCode ,@UserId	= @UserId ,@Amount = @Amount ,@CreationDateTime	= @NowDatetime

			SET	@ReturnMessage = N'OK, 예치금 충전 완료'
		END;
		;



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