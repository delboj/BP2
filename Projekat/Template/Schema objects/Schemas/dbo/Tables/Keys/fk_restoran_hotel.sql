ALTER TABLE Restoran
    ADD CONSTRAINT restoran_hotel_fk FOREIGN KEY ( hotel_idhotel )
        REFERENCES hotel ( idhotel )