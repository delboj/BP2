ALTER TABLE Drzi
    ADD CONSTRAINT drzi_vlasnik_fk FOREIGN KEY ( vlasnik_mbr )
        REFERENCES vlasnik ( mbr )