-- FUNCTION: religiosos.sp_consulta_lista_estatus_transmision(integer)

-- DROP FUNCTION IF EXISTS religiosos.sp_consulta_lista_estatus_transmision(integer);

CREATE OR REPLACE FUNCTION religiosos.sp_consulta_lista_estatus_transmision(
	p_id integer)
    RETURNS TABLE(estatus_id integer, estatus_nombre character varying, estatus_transmision boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
	RETURN QUERY	
	select 
    i_id as estatus_id,
    nombre as estatus_nombre,
    b_estatus_transmision as estatus_transmision
    from religiosos."CAT_Estatus"
    where i_id in (30,31,32,33,34,38);	
END
$BODY$;

ALTER FUNCTION religiosos.sp_consulta_lista_estatus_transmision(integer)
    OWNER TO tramitesdgardev_user;
