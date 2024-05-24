-- FUNCTION: religiosos.sp_insertar_observacion_declaratoria(integer, character varying)

-- DROP FUNCTION IF EXISTS religiosos.sp_insertar_observacion_declaratoria(integer, character varying);

CREATE OR REPLACE FUNCTION religiosos.sp_insertar_observacion_declaratoria(
	p_id_declaratoria integer,
	p_comentarios character varying)
    RETURNS TABLE(id_generico integer, mensaje character varying, proceso_existoso boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$

DECLARE

aux_estatus integer:= (SELECT i_id FROM religiosos."CAT_Estatus" WHERE nombre = 'Observaciones en Declaratoria de Procedencia - En espera' LIMIT 1);

id_usuario integer := (SELECT i_id_tbl_usuario FROM religiosos."TBL_Declaratoria_Procedencia" WHERE i_id = p_id_declaratoria LIMIT 1);

BEGIN

	UPDATE religiosos."TBL_Declaratoria_Procedencia"
			SET i_id_tbl_estatus = aux_estatus
		WHERE i_id = p_id_declaratoria;
	
	UPDATE religiosos."ASOC_DeclaratoriaDictaminador"
		SET c_comentarios = p_comentarios, b_read = false
	WHERE i_id_tbl_declaratoria = p_id_declaratoria;
	
	RETURN QUERY 
		SELECT id_usuario as id_generico,
		CAST(p_comentarios  as character varying) AS mensaje,
		(true) AS proceso_existoso;

END;
$BODY$;

ALTER FUNCTION religiosos.sp_insertar_observacion_declaratoria(integer, character varying)
    OWNER TO postgres;
