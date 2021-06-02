ALTER TABLE Spremacica
    ADD CONSTRAINT spremacica_radnik_fk FOREIGN KEY ( jmbgradnika )
        REFERENCES radnik ( jmbgradnika )