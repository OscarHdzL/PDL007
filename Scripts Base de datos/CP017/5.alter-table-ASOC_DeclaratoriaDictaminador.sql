ALTER TABLE religiosos."ASOC_DeclaratoriaDictaminador"
   DROP CONSTRAINT "FK_Declaratoria_Procedencia_d"; 
   
   ALTER TABLE religiosos."ASOC_DeclaratoriaDictaminador" 
    ADD CONSTRAINT "FK_Declaratoria_Procedencia_d" FOREIGN KEY (i_id_tbl_declaratoria)
        REFERENCES religiosos."TBL_Declaratoria_Procedencia" (i_id)