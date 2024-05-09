1000

AS $BODY$
DECLARE

BEGIN

	UPDATE religiosos."TBL_Declaratoria_Procedencia"
	SET b_genera_oficio = '1'
	WHERE i_id = p_id_declaratoria;
	
	RETURN QUERY 
	SELECT p_id_declaratoria as id_generico,
		CAST('La información se guardo correctamente.' as varchar) AS mensaje,
		(true) AS proceso_existoso;

END;
$BODY$;

ALTER FUNCTION religiosos.sp_generar_oficio_declaratoria(integer)
    OWNER TO postgres;