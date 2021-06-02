CREATE PROCEDURE Procedura
	(@Mbr varchar(30))
AS
DELETE FROM dbo.Audit;
DECLARE HOTEL_CURSOR CURSOR
FOR SELECT hotel_idhotel, vlasnik_mbr FROM dbo.drzi
DECLARE @idh INT;
DECLARE @vmbr varchar(15);
BEGIN
	OPEN HOTEL_CURSOR;
	FETCH NEXT FROM HOTEL_CURSOR INTO @idh, @vmbr;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		IF (@vmbr = @Mbr)
			BEGIN
				DECLARE ODSEDA_CURSOR CURSOR
				FOR SELECT soba_hotel_idhotel, gost_jmbg FROM dbo.odseda
				DECLARE @hotel INT;
				DECLARE @jmbg INT;
				DECLARE @cnt INT = 0;

				OPEN ODSEDA_CURSOR;
				FETCH NEXT FROM ODSEDA_CURSOR INTO @hotel, @jmbg;
				WHILE @@FETCH_STATUS = 0
				BEGIN
					IF (@idh = @hotel)
						BEGIN
							set @cnt = @cnt + 1;
							IF(@cnt = 2)
								BEGIN
									DECLARE @ret_jmbg INT, @ret_ime varchar(30), @ret_prezime varchar(30); 
									SELECT @ret_jmbg = jmbg, @ret_ime = ime_gosta, @ret_prezime = prezime_gosta FROM dbo.gost WHERE jmbg = @jmbg;
									INSERT INTO dbo.Audit(Jmbg, Ime, Prezime) values(@ret_jmbg, @ret_ime, @ret_prezime);
								END;
						END;
					FETCH NEXT FROM ODSEDA_CURSOR INTO @hotel, @jmbg;
				END;
				CLOSE ODSEDA_CURSOR;
				DEALLOCATE ODSEDA_CURSOR;
			END;
		FETCH NEXT FROM HOTEL_CURSOR INTO @idh, @vmbr;
	END;
	CLOSE HOTEL_CURSOR;
	DEALLOCATE HOTEL_CURSOR;
END;
