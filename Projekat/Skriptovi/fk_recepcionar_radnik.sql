ALTER TABLE Recepcionar
    ADD CONSTRAINT recepcionar_radnik_fk FOREIGN KEY ( jmbgradnika )
        REFERENCES radnik ( jmbgradnika )