-- FUNCTION: religiosos.sp_insertar_catalogo_cnotorioarr(character varying, text, integer)

-- DROP FUNCTION IF EXISTS religiosos.sp_insertar_catalogo_cnotorioarr(character varying, text, integer);

CREATE OR REPLACE FUNCTION religiosos.sp_insertar_catalogo_cnotorioarr(
	c_nombre_n character varying,
	c_descripcion_n text,
	i_tipo_escrito integer)
    RETURNS TABLE(id integer, proceso_exitoso boolean, mensaje character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
	existeCatalogo CONSTANT integer := (SELECT i_id FROM religiosos."CAT_Cnotorioarr" WHERE upper(c_nombre) = upper(c_nombre_n) LIMIT 1);
	c_id integer;
	BEGIN
		IF existeCatalogo is null 
			THEN
					INSERT INTO religiosos."CAT_Cnotorioarr"(c_nombre, c_descripcion,
-- 														  f_inic_vig,f_fin_vig,
															 b_activo,
															 i_tipo_escrito)
														  VALUES(c_nombre_n,c_descripcion_n,
-- 																COALESCE(c_f_ini_vig, CAST(NOW() AS DATE)),
-- 																 COALESCE(c_f_fin_vig, CAST(NOW() AS DATE)+ interval '2 year'),
																 true,
																 i_tipo_escrito)
					RETURNING i_id INTO c_id;
					RETURN QUERY SELECT 
						 c_id as id,
						 true as proceso_exitoso,
						 CAST(c_nombre_n || ' se agregó correctamente.' AS CHARACTER VARYING) AS mensaje;
					
			ELSE 
				RETURN QUERY SELECT 
						 c_id as id,
						 false as proceso_exitoso,
						 CAST(c_nombre_n || ' ya se registro anteriormente.' AS CHARACTER VARYING) AS mensaje;
				
			END IF;

			END;
$BODY$;

ALTER FUNCTION religiosos.sp_insertar_catalogo_cnotorioarr(character varying, text, integer)
    OWNER TO tramitesdgardev_user;
