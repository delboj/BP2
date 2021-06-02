ALTER TABLE Odseda
    ADD CONSTRAINT odseda_gost_fk FOREIGN KEY ( gost_jmbg )
        REFERENCES gost ( jmbg )