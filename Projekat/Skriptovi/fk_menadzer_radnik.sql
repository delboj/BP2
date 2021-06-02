ALTER TABLE Menadzer
    ADD CONSTRAINT menadzer_radnik_fk FOREIGN KEY ( jmbgradnika )
        REFERENCES radnik ( jmbgradnika )