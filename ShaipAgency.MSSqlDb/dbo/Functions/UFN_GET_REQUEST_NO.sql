-- =============================================
-- Author:		김성민
-- Create date: 2020.05.02
-- Description:	의뢰요청 번호를 생성하는 함수
-- =============================================
CREATE FUNCTION [dbo].[UFN_GET_REQUEST_NO]
(
	-- Add the parameters for the function here
	@Datetime	datetime
)
RETURNS VARCHAR(15)
AS
BEGIN
	-- Declare the return variable here

	-- Add the T-SQL statements to compute the return value here

	-- Return the result of the function
	RETURN CONVERT(VARCHAR(8),@Datetime,112) + REPLACE(CONVERT(VARCHAR(10),@Datetime, 114),':','')
END
