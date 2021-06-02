ALTER TABLE Odgovara
    ADD CONSTRAINT odgovara_drzi_fk FOREIGN KEY ( drzi_hotel_idhotel,
    drzi_mbr )
        REFERENCES drzi ( hotel_idhotel,
        vlasnik_mbr )