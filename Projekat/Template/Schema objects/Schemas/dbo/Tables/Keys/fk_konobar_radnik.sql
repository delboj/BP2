ALTER TABLE Konobar
    ADD CONSTRAINT konobar_radnik_fk FOREIGN KEY ( jmbgradnika )
        REFERENCES radnik ( jmbgradnika )