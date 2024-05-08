
CREATE OR REPLACE FUNCTION religiosos.sp_consulta_estatus_new(
	tipotramite integer)
    RETURNS TABLE(idestado integer, nombre character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
  	
  	if tipotramite = 1 then
  	RETURN QUERY
  	SELECT 	
		Estatus.i_id AS id,
		Estatus.nombre AS nombre
	FROM religiosos."CAT_Estatus" as Estatus where Estatus.i_id  in (3,4,5,6,7,8,9,10,11,14,15,16,17,18,19,20,36) order by i_orden;
  	end if;
  
  	if tipotramite = 2 then
  	RETURN QUERY
	SELECT 	
		Estatus.i_id AS id,
		Estatus.nombre AS nombre
	FROM religiosos."CAT_Estatus" as Estatus where Estatus.i_id  in (12,13,21,22,23,24,25,26,27,28,37) order by i_orden;
	end if;
	
	if tipotramite = 3 then
  	RETURN QUERY
	SELECT 	
		Estatus.i_id AS id,
		Estatus.nombre AS nombre
	FROM religiosos."CAT_Estatus" as Estatus where Estatus.i_id  in (29,30,31,32,33,34,35,38) order by i_orden;
	end if;
	
	if tipotramite = 4 then
  	RETURN QUERY
	SELECT 	
		Estatus.i_id AS id,
		Estatus.nombre AS nombre
	FROM religiosos."CAT_Estatus" as Estatus where Estatus.i_id  in (10,11,14,15,16,17,18,19,20,36) order by i_orden;
	end if;
	
		if tipotramite = 5 then
  	RETURN QUERY
	SELECT 	
		Estatus.i_id AS id,
		Estatus.nombre AS nombre
	FROM religiosos."CAT_Estatus" as Estatus where Estatus.i_id  in (21,22,23,24,25,26,27,28,37) order by i_orden;
	end if;
	
	if tipotramite = 6 then
  	RETURN QUERY
	SELECT 	
		Estatus.i_id AS id,
		Estatus.nombre AS nombre
	FROM religiosos."CAT_Estatus" as Estatus where Estatus.i_id  in (30,31,32,33,34,38) order by i_orden;
	end if;
	
	

END;
$BODY$;