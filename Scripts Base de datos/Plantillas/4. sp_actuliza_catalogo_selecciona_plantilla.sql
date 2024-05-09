
CREATE OR REPLACE FUNCTION religiosos.sp_actuliza_catalogo_selecciona_plantilla(
	p_id integer,
	p_estatus integer)
    RETURNS TABLE(id integer, proceso_exitoso boolean, mensaje character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
	existeCatalogo CONSTANT integer := (SELECT i_id FROM religiosos."CAT_Plantilla" WHERE i_id = p_id LIMIT 1);

	BEGIN
		IF existeCatalogo is not null 
			THEN
					UPDATE religiosos."CAT_Plantilla" SET i_estatus = 2 WHERE i_estatus < 3;
					
					UPDATE religiosos."CAT_Plantilla" SET i_estatus = 1 WHERE i_id = p_id;
					
					RETURN QUERY SELECT
							p_id AS id,
							true AS proceso_exitoso,
						    CAST('La información se actualizó correctamente.'  as varchar) AS mensaje;
					

			ELSE 
				RETURN QUERY SELECT
					c_id AS id,
					false AS proceso_exitoso,
					CAST('No existe el catálogo.'  as varchar) AS mensaje;
			END IF;

			END;
$BODY$;