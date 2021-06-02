ALTER TABLE Soba
    ADD CONSTRAINT soba_hotel_fk FOREIGN KEY ( hotel_idhotel )
        REFERENCES hotel ( idhotel )