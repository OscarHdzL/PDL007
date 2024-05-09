-- FUNCTION: religiosos.sp_estatus_autorizar_declaratoria(integer, character varying, character varying, character varying)

-- DROP FUNCTION IF EXISTS religiosos.sp_estatus_autorizar_declaratoria(integer, character varying, character varying, character varying);

CREATE OR REPLACE FUNCTION religiosos.sp_estatus_autorizar_declaratoria(
	p_id_declaratoria integer,
	p_fecha character varying,
	p_horario character varying,
	p_direccion character varying)
    RETURNS TABLE(id_generico integer, mensaje character varying, proceso_existoso boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
	aux_estatus integer:= (SELECT i_id FROM religiosos."CAT_Estatus" WHERE nombre = 'Declaratoria de Procedencia Autorizada – En espera' LIMIT 1);
	id_usuario integer := (SELECT i_id FROM religiosos."TBL_Usuario" WHERE i_id_tbl_perfil = 11 LIMIT 1);
BEGIN

	UPDATE religiosos."TBL_Declaratoria_Procedencia"
	SET i_id_tbl_estatus = aux_estatus
		, d_fecha_autorizacion = current_date
	WHERE i_id = p_id_declaratoria;
	
	INSERT INTO religiosos."TBL_Declaratoria_Autoriza"(i_id_tbl_declaratoria, d_fecha, d_horario, c_direccion)
	VALUES (p_id_declaratoria, TO_DATE(p_fecha,'YYYY-MM-DD'), p_horario, p_direccion);
	
	RETURN QUERY 
	SELECT p_id_declaratoria as id_generico,
		CAST('La información se guardo correctamente.' as varchar) AS mensaje,
		(true) AS proceso_existoso;

END;
$BODY$;

ALTER FUNCTION religiosos.sp_estatus_autorizar_declaratoria(integer, character varying, character varying, character varying)
    OWNER TO postgres;