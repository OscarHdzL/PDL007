
CREATE OR REPLACE FUNCTION religiosos.sp_insertar_actos_emisoras(
	frecuencia_canal character varying,
	proveedor character varying,
	televisora_radiodifusora character varying,
	i_id_estado integer,
	televisora bit,
	i_id_acto integer,
	municipio character varying)
    RETURNS TABLE(mensaje character varying, proceso_exitoso boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$

BEGIN

    INSERT INTO religiosos."CAT_Medios_Transmision"(c_frecuencia_canal, c_proveedor, c_tel_radio, i_id_cat_estados, b_televisora, i_id_tbl_acto, c_municipio)
		VALUES (frecuencia_canal
			, proveedor
			, televisora_radiodifusora
			, i_id_estado
			, televisora
			, i_id_acto
			, municipio);
		
	RETURN QUERY
		SELECT CAST('La información se ha cargado de forma exitosa.'  as varchar) AS mensaje,
		(true) AS proceso_exitoso;

END;
$BODY$;