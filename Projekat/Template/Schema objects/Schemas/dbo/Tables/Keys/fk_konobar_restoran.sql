ALTER TABLE Konobar
    ADD CONSTRAINT konobar_restoran_fk FOREIGN KEY ( restoran_idrest )
        REFERENCES restoran ( idrest )