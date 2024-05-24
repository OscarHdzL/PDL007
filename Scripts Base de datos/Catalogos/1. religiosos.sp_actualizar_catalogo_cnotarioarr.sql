-- FUNCTION: religiosos.sp_actualizar_catalogo_cnotarioarr(integer, character varying, character varying, date, date, integer)

-- DROP FUNCTION IF EXISTS religiosos.sp_actualizar_catalogo_cnotarioarr(integer, character varying, character varying, date, date, integer);

CREATE OR REPLACE FUNCTION religiosos.sp_actualizar_catalogo_cnotarioarr(
	c_id integer,
	c_nombre_n character varying,
	c_descripcion_n character varying,
	c_f_ini_vig date,
	c_f_fin_vig date,
	tipo_escrito integer)
    RETURNS TABLE(id_catalogo integer, mensaje character varying, proceso_exitoso boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
	existeCatalogo CONSTANT integer := (SELECT i_id FROM religiosos."CAT_Cnotorioarr" WHERE i_id = c_id LIMIT 1);
	existeCatalogoNombre CONSTANT integer := (SELECT i_id FROM religiosos."CAT_Cnotorioarr" WHERE i_id <> c_id AND upper(c_nombre) = upper(c_nombre_n) LIMIT 1);

	BEGIN
		IF existeCatalogo is not null 
			THEN
			  IF existeCatalogoNombre is null THEN
					UPDATE religiosos."CAT_Cnotorioarr" 
					SET c_nombre = coalesce(c_nombre_n,c_nombre),
						c_descripcion = coalesce(c_descripcion_n,c_descripcion),
						d_finic_vig = coalesce(c_f_ini_vig,d_finic_vig),
						d_ffin_vig = coalesce(c_f_fin_vig, d_ffin_vig),
						i_tipo_escrito = tipo_escrito
					WHERE i_id = c_id;
					
					RETURN QUERY SELECT
					c_id AS id_catalogo,
					CAST('La información se actualizó correctamente.'  as varchar) 
					AS mensaje,
					true AS proceso_exitoso
					;
			  ELSE
			        RETURN QUERY SELECT
					c_id AS id_catalogo,
					CAST('Ya existe otro catálogo con ese nombre.'  as varchar) 
					AS mensaje,
					false AS proceso_exitoso
					;
				
			  END IF;
			ELSE 
				RETURN QUERY SELECT
					c_id AS id_catalogo,
					CAST('No existe el catálogo.'  as varchar) 
					AS mensaje,
					false AS proceso_exitoso
					;
				
			END IF;

			END;
$BODY$;

ALTER FUNCTION religiosos.sp_actualizar_catalogo_cnotarioarr(integer, character varying, character varying, date, date, integer)
    OWNER TO tramitesdgardev_user;
