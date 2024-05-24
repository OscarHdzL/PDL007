-- FUNCTION: religiosos.sp_consulta_estatus_reporte(integer)

-- DROP FUNCTION IF EXISTS religiosos.sp_consulta_estatus_reporte(integer);

CREATE OR REPLACE FUNCTION religiosos.sp_consulta_estatus_reporte(
	p_tipo_tramite integer DEFAULT NULL::integer)
    RETURNS TABLE(i_id integer, c_nombre character varying, i_estatus integer) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
     IF(p_tipo_tramite=1) THEN --REGISTRO
	 	RETURN QUERY
		   	SELECT estatus.i_id AS i_id
				, estatus.nombre AS c_nombre
				, estatus.b_activo::integer AS i_estatus
			FROM religiosos."CAT_Estatus" estatus
			WHERE estatus.b_estatus_registro = '1'
			ORDER BY estatus.i_id ASC;
     ELSEIF (p_tipo_tramite=2) THEN --TRAMITE DE NOTA
	 	RETURN QUERY
		   	SELECT estatus.i_id AS i_id
				, estatus.nombre AS c_nombre
				, estatus.b_activo::integer AS i_estatus
			FROM religiosos."CAT_Estatus" estatus
			WHERE estatus.b_estatus_tnota = '1'
			ORDER BY estatus.i_id ASC;
     ELSEIF (p_tipo_tramite=3) THEN --TRANSMISIONES
	 	RETURN QUERY
		   	SELECT estatus.i_id AS i_id
				, estatus.nombre AS c_nombre
				, estatus.b_activo::integer AS i_estatus
			FROM religiosos."CAT_Estatus" estatus
			WHERE estatus.b_estatus_transmision = '1'
			ORDER BY estatus.i_id ASC;
	 ELSEIF (p_tipo_tramite=4) THEN --DECLARATORIA
		 RETURN QUERY
			SELECT estatus.i_id AS i_id
				, estatus.nombre AS c_nombre
				, estatus.b_activo::integer AS i_estatus
			FROM religiosos."CAT_Estatus" estatus
			WHERE estatus.b_estatus_declaratoria = '1'
			ORDER BY estatus.i_id ASC;
     ELSE
	 RETURN;
	END IF;
	 
END
$BODY$;

ALTER FUNCTION religiosos.sp_consulta_estatus_reporte(integer)
    OWNER TO postgres;
