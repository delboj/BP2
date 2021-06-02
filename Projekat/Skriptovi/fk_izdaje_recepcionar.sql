ALTER TABLE Izdaje
    ADD CONSTRAINT izdaje_recepcionar_fk FOREIGN KEY ( recepcionar_jmbgradnika )
        REFERENCES recepcionar ( jmbgradnika )