-- FUNCTION: religiosos.sp_consulta_lista_estatus(integer)

-- DROP FUNCTION IF EXISTS religiosos.sp_consulta_lista_estatus(integer);

CREATE OR REPLACE FUNCTION religiosos.sp_consulta_lista_estatus(
	tipo_lista integer DEFAULT NULL::integer)
    RETURNS TABLE(c_id integer, c_nombre character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
     IF(tipo_lista=7) THEN
	       RETURN QUERY SELECT
	       i_id AS c_id,
	       nombre AS c_nombre
	       FROM religiosos."CAT_Estatus" WHERE  i_id in(14,15);
     ELSEIF (tipo_lista=8) THEN
	       RETURN QUERY SELECT
	       i_id AS c_id,
	       nombre AS c_nombre
	       FROM religiosos."CAT_Estatus" WHERE  i_id in(16,17);
     ELSEIF (tipo_lista=9) THEN
	       RETURN QUERY SELECT
	       i_id AS c_id,
	       nombre AS c_nombre
	       FROM religiosos."CAT_Estatus" WHERE  i_id in(17,18);
	 ELSEIF (tipo_lista=10) THEN
	       RETURN QUERY SELECT
	       i_id AS c_id,
	       nombre AS c_nombre
	       FROM religiosos."CAT_Estatus" WHERE  i_id in(19,20);
     ELSE
	 RETURN;
	 END IF;
	 
	 
END
$BODY$;

ALTER FUNCTION religiosos.sp_consulta_lista_estatus(integer)
    OWNER TO tramitesdgardev_user;
