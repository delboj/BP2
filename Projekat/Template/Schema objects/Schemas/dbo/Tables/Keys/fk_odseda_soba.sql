ALTER TABLE Odseda
    ADD CONSTRAINT odseda_soba_fk FOREIGN KEY ( soba_broj,
    soba_hotel_idhotel )
        REFERENCES soba ( broj,
        hotel_idhotel )