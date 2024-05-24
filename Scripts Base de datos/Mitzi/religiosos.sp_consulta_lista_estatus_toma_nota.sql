-- FUNCTION: religiosos.sp_consulta_lista_estatus_toma_nota(integer)

-- DROP FUNCTION IF EXISTS religiosos.sp_consulta_lista_estatus_toma_nota(integer);

CREATE OR REPLACE FUNCTION religiosos.sp_consulta_lista_estatus_toma_nota(
	tipo_lista integer DEFAULT NULL::integer)
    RETURNS TABLE(c_id integer, c_nombre character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
     IF(tipo_lista=9) THEN
	       RETURN QUERY SELECT
	       i_id AS c_id,
	       nombre AS c_nombre
	       FROM religiosos."CAT_Estatus" WHERE  i_id in(22,23);
     ELSEIF (tipo_lista=10) THEN
	       RETURN QUERY SELECT
	       i_id AS c_id,
	       nombre AS c_nombre
	       FROM religiosos."CAT_Estatus" WHERE  i_id in(24,25);
     ELSEIF (tipo_lista=11) THEN
	       RETURN QUERY SELECT
	       i_id AS c_id,
	       nombre AS c_nombre
	       FROM religiosos."CAT_Estatus" WHERE  i_id in(25,26);
	 ELSEIF (tipo_lista=12) THEN
	       RETURN QUERY SELECT
	       i_id AS c_id,
	       nombre AS c_nombre
	       FROM religiosos."CAT_Estatus" WHERE  i_id in(27,28);
     ELSE
	 RETURN;
	 END IF;
	 
	 
END
$BODY$;

ALTER FUNCTION religiosos.sp_consulta_lista_estatus_toma_nota(integer)
    OWNER TO tramitesdgardev_user;
