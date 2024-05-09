CREATE OR REPLACE FUNCTION religiosos.sp_actuliza_catalogo_plantilla(
	p_id integer,
	p_nombre character varying,
	p_ruta character varying)
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
			    IF p_ruta is null THEN
					--UPDATE religiosos."CAT_Plantilla" 
						 --SET c_nombre = coalesce(p_nombre)
					--WHERE i_id = p_id;
					
					RETURN QUERY SELECT
							p_id AS id,
							true AS proceso_exitoso,
						    CAST('La información se actualizó correctamente.'  as varchar) AS mensaje;
					
				 ELSE
					 UPDATE religiosos."CAT_Plantilla" 
							 SET --c_nombre = p_nombre,
							 	 c_ruta = p_ruta
								WHERE i_id = p_id;
								
						RETURN QUERY SELECT
						p_id AS id,
						true AS proceso_exitoso,
						CAST('La información se actualizó correctamente.'  as varchar) AS mensaje;
			     END IF;
			ELSE 
				RETURN QUERY SELECT
					c_id AS id,
					false AS proceso_exitoso,
					CAST('No existe el catálogo.'  as varchar) AS mensaje;
			END IF;

			END;
$BODY$;