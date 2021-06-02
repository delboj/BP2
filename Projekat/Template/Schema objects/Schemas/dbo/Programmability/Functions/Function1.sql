CREATE FUNCTION [dbo].[Function1]
(
	@ime_hotela varchar(30)
)
RETURNS DECIMAL
AS
BEGIN
	DECLARE @ret_value AS DECIMAL;
	SELECT Top 1 @ret_value = soba_broj from (SELECT soba_broj, count(*) as cnt from dbo.odseda where soba_hotel_idhotel = (Select idhotel from dbo.hotel where naziv = @ime_hotela) GROUP BY soba_broj) br order by cnt desc;
	RETURN @ret_value;
END
