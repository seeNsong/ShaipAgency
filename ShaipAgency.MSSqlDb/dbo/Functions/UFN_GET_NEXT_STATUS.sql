-- =============================================
-- Author:		김성민
-- Create date: 2020.05.04
-- Description:	의뢰요청 상태를 리턴하는 함수
-- =============================================
CREATE FUNCTION [dbo].[UFN_GET_NEXT_STATUS]
(
	-- Add the parameters for the function here
	@RequestCode	char(2)
	,@EventCode		char(2)
)
RETURNS char(2)
AS
BEGIN
	-- Declare the return variable here

	-- Add the T-SQL statements to compute the return value here

	-- Return the result of the function
	RETURN (	SELECT	NextStatusCode 
				FROM	[dbo].[TB_STD_REQUEST_STATUS_ROUTE_INFO] WITH(NOLOCK)
				WHERE	RequestCode = @RequestCode
				AND		EventCode	= @EventCode );
END