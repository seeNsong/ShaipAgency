CREATE PROCEDURE [dbo].[UserPassbook_Sel_01]
	@UserId		Int
	
AS
BEGIN

	SET NOCOUNT ON

	SELECT		A.RequestNo, A.UserID, B.ShaipName, A.Charge, A.CDateTime
	FROM		dbo.UserPassbook		A		WITH(NOLOCK)
	LEFT JOIN	dbo.AspNetUsers			B		WITH(NOLOCK)		ON	A.UserID = B.Id
	WHERE		A.UserID	= @UserId

END
