ALTER TABLE Cisti
    ADD CONSTRAINT cisti_spremacica_fk FOREIGN KEY ( spremacica_jmbgradnika )
        REFERENCES spremacica ( jmbgradnika )