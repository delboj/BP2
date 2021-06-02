ALTER TABLE Izdaje
    ADD CONSTRAINT izdaje_odseda_fk FOREIGN KEY ( odseda_gost_jmbg,
    odseda_broj,
    odseda_idhotel )
        REFERENCES odseda ( gost_jmbg,
        soba_broj,
        soba_hotel_idhotel )