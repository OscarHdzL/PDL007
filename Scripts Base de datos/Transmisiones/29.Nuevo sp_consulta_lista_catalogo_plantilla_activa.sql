
CREATE OR REPLACE FUNCTION religiosos.sp_consulta_lista_catalogo_plantilla_activa(
	p_id_plantilla integer)
    RETURNS TABLE(i_id integer, c_nombre character varying, c_ruta character varying, i_estatus integer) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
	RETURN QUERY 
		   SELECT 
				plantilla.i_id AS i_id,
				plantilla.c_nombre AS c_nombre, 
				plantilla.c_ruta AS c_nombre, 
				plantilla.i_estatus AS i_estatus
			FROM religiosos."CAT_Plantilla" AS plantilla
			WHERE plantilla.i_estatus = 1 
			AND plantilla.i_id = p_id_plantilla
			LIMIT 1;
	
END
$BODY$;