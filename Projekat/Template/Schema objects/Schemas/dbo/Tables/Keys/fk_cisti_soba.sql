ALTER TABLE Cisti
    ADD CONSTRAINT cisti_soba_fk FOREIGN KEY ( soba_broj,
    soba_idhotel )
        REFERENCES soba ( broj,
        hotel_idhotel )