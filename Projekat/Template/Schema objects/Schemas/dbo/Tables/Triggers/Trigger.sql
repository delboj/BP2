CREATE TRIGGER [Trigger]
	ON [dbo].[konobar]
	INSTEAD OF INSERT
	AS
	declare @godine_iskustva int;
	select @godine_iskustva=i.godine_iskustva from inserted i;
	BEGIN
		IF @godine_iskustva < 3
			BEGIN
				RAISERROR ('Konobar s manje od 3 godine iskustva ne moze biti dodat.', 16, 1);				
			END;
	END;
