ALTER TABLE Drzi
    ADD CONSTRAINT drzi_hotel_fk FOREIGN KEY ( hotel_idhotel )
        REFERENCES hotel ( idhotel )