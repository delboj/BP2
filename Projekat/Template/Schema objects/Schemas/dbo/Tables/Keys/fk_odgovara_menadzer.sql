ALTER TABLE Odgovara
    ADD CONSTRAINT odgovara_menadzer_fk FOREIGN KEY ( menadzer_jmbgradnika )
        REFERENCES menadzer ( jmbgradnika )