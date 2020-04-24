CREATE PROCEDURE [dbo].[UserPassbook_Sel_01]
	@ShaipName		NVarChar(40)
	
AS
BEGIN

	SET NOCOUNT ON

	SELECT		ApplyNo, ShaipName, Charge, CDateTime
	FROM		dbo.UserPassbook							WITH(NOLOCK)
	WHERE		ShaipName	= @ShaipName

END
