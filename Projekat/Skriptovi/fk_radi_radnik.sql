ALTER TABLE Radi
    ADD CONSTRAINT radi_radnik_fk FOREIGN KEY ( radnik_jmbgradnika )
        REFERENCES radnik ( jmbgradnika )