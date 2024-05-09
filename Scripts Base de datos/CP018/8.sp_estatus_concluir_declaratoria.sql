-- FUNCTION: religiosos.sp_estatus_concluir_declaratoria(integer, integer, character varying)

-- DROP FUNCTION IF EXISTS religiosos.sp_estatus_concluir_declaratoria(integer, integer, character varying);

CREATE OR REPLACE FUNCTION religiosos.sp_estatus_concluir_declaratoria(
	p_id_declaratoria integer,
	p_estatus integer,
	p_comentarios character varying)
    RETURNS TABLE(id_generico integer, mensaje character varying, proceso_existoso boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
	aux_estatus integer;
	id_usuario integer := (SELECT i_id FROM religiosos."TBL_Usuario" WHERE i_id_tbl_perfil = 11 LIMIT 1);
BEGIN

	IF( p_estatus = 1) THEN --CONCLUIDA
		aux_estatus:= (SELECT i_id FROM religiosos."CAT_Estatus" WHERE nombre = 'Declaratoria de Procedencia Concluida' LIMIT 1);
	ELSE -- CANCELADA
		aux_estatus:= (SELECT i_id FROM religiosos."CAT_Estatus" WHERE nombre = 'Declaratoria de Procedencia Cancelada' LIMIT 1);
	END IF;

	UPDATE religiosos."TBL_Declaratoria_Procedencia"
	SET i_id_tbl_estatus = aux_estatus
		, c_observaciones = p_comentarios
	WHERE i_id = p_id_declaratoria;
	
	RETURN QUERY 
	SELECT id_usuario as id_generico,
		CAST('La información se guardo correctamente.' as varchar) AS mensaje,
		(true) AS proceso_existoso;

END;
$BODY$;

ALTER FUNCTION religiosos.sp_estatus_concluir_declaratoria(integer, integer, character varying)
    OWNER TO postgres;