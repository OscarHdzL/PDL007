CREATE OR REPLACE FUNCTION religiosos.sp_eliminar_declaratoria(
	p_id_declaratoria integer,
	p_activo bit)
    RETURNS TABLE(id_generico integer, mensaje character varying, proceso_existoso boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE

BEGIN

	UPDATE religiosos."TBL_Declaratoria_Procedencia"
	SET b_activo = p_activo
	WHERE i_id = p_id_declaratoria;
	
	RETURN QUERY 
	SELECT p_id_declaratoria as id_generico,
		CAST('La información se guardo correctamente.' as varchar) AS mensaje,
		(true) AS proceso_existoso;

END;
$BODY$;