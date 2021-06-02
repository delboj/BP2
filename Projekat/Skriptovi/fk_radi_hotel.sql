ALTER TABLE Radi
    ADD CONSTRAINT radi_hotel_fk FOREIGN KEY ( hotel_idhotel )
        REFERENCES hotel ( idhotel )