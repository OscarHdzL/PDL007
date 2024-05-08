
CREATE OR REPLACE FUNCTION religiosos.sp_consulta_lista_registros_transmisiones(
	id_usuario integer,
	numero_sgar_desc character varying,
	denominacion_desc character varying,
	estatus_desc integer,
	representante_desc character varying)
    RETURNS TABLE(numero_registros bigint, id_transmision integer, folio character varying, domicilio character varying, numero_sgar character varying, denominacion character varying, representante character varying, id_estatus integer, estatus character varying, fecha_solicitud date, fecha_autorizacion date) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
IF estatus_desc =33 OR estatus_desc=34 THEN

RETURN QUERY
	SELECT 
	            ROW_NUMBER() OVER (ORDER BY id_transmision) AS numero_registro,
				tbl_transmision.i_id AS id_transmision,
				tbl_transmision.c_nfolio AS folio,
				tbl_transmision.c_domicilio AS domicilio, 
				tbl_transmision.c_numero_sgar AS numero_sgar,
				tbl_transmision.c_denominacion AS denominacion,
				tbl_transmision.c_representante AS representante,
				tbl_estatus.i_id AS id_estatus,
				tbl_estatus.nombre AS estatus,
				tbl_transmision.d_fecha_solicitud AS fecha_solicitud,
				(SELECT tblf.d_inicio FROM religiosos."ASOC_Transmision_Fechas" asoctf 
				 	INNER JOIN religiosos."TBL_Fechas" AS tblf ON asoctf.i_id_tbl_fechas = tblf.i_id
					 WHERE asoctf.i_id_tbl_transmision = tbl_transmision.i_id and tblf.i_id_cat_fechas = 6 limit 1) AS fecha_autorizacion
			    FROM religiosos."TBL_Transmision" AS tbl_transmision 
				 LEFT JOIN religiosos."ASOC_Transmision_Usuario" AS tbl_asoc_transmision ON tbl_transmision.i_id = tbl_asoc_transmision.i_id_tbl_transmision
			     JOIN religiosos."ASOC_Transmision_Estatus" astes ON astes.i_id_tbl_transmision = tbl_transmision.i_id
		       JOIN religiosos."CAT_Estatus" tbl_estatus ON tbl_estatus.i_id= astes.i_id_tbl_estatus
				 WHERE  tbl_asoc_transmision.i_id_tbl_usuario = id_usuario 
				  AND ((denominacion_desc IS null) OR (LOWER(tbl_transmision.c_denominacion) LIKE ('%'||LOWER(denominacion_desc)||'%'))) 
				  AND ((numero_sgar_desc IS null) OR (LOWER(tbl_transmision.c_numero_sgar) LIKE ('%'||LOWER(numero_sgar_desc)||'%'))) 
		          AND ((estatus_desc IS null ) OR ( tbl_estatus.i_id=estatus_desc ))
				  AND ((representante_desc IS null ) OR  (LOWER(tbl_transmision.c_representante) LIKE ('%'||LOWER(representante_desc)||'%')))
				 AND astes.i_id_tbl_estatus = (SELECT i_id_tbl_estatus FROM religiosos."ASOC_Transmision_Estatus"
									  WHERE i_id_tbl_transmision=tbl_transmision.i_id ORDER BY i_id DESC limit 1 )
									  AND astes.i_id_tbl_estatus in (33,34);

ELSE
	RETURN QUERY
	SELECT 
	            ROW_NUMBER() OVER (ORDER BY id_transmision) AS numero_registro,
				tbl_transmision.i_id AS id_transmision,
				tbl_transmision.c_nfolio AS folio,
				tbl_transmision.c_domicilio AS domicilio, 
				tbl_transmision.c_numero_sgar AS numero_sgar,
				tbl_transmision.c_denominacion AS denominacion,
				tbl_transmision.c_representante AS representante,
				tbl_estatus.i_id AS id_estatus,
				tbl_estatus.nombre AS estatus,
				tbl_transmision.d_fecha_solicitud AS fecha_solicitud,
				(SELECT tblf.d_inicio FROM religiosos."ASOC_Transmision_Fechas" asoctf 
				 	INNER JOIN religiosos."TBL_Fechas" AS tblf ON asoctf.i_id_tbl_fechas = tblf.i_id
					 WHERE asoctf.i_id_tbl_transmision = tbl_transmision.i_id and tblf.i_id_cat_fechas = 6 limit 1) AS fecha_autorizacion
			    FROM religiosos."TBL_Transmision" AS tbl_transmision 
				 LEFT JOIN religiosos."ASOC_Transmision_Usuario" AS tbl_asoc_transmision ON tbl_transmision.i_id = tbl_asoc_transmision.i_id_tbl_transmision
			     JOIN religiosos."ASOC_Transmision_Estatus" astes ON astes.i_id_tbl_transmision = tbl_transmision.i_id
		       JOIN religiosos."CAT_Estatus" tbl_estatus ON tbl_estatus.i_id= astes.i_id_tbl_estatus
				 WHERE  tbl_asoc_transmision.i_id_tbl_usuario = id_usuario 
				  AND ((denominacion_desc IS null) OR (LOWER(tbl_transmision.c_denominacion) LIKE ('%'||LOWER(denominacion_desc)||'%'))) 
				  AND ((numero_sgar_desc IS null) OR (LOWER(tbl_transmision.c_numero_sgar) LIKE ('%'||LOWER(numero_sgar_desc)||'%'))) 
		          AND ((estatus_desc IS null ) OR ( tbl_estatus.i_id=estatus_desc ))
				  AND ((representante_desc IS null ) OR  (LOWER(tbl_transmision.c_representante) LIKE ('%'||LOWER(representante_desc)||'%')))
				 AND astes.i_id_tbl_estatus = (SELECT i_id_tbl_estatus FROM religiosos."ASOC_Transmision_Estatus"
									  WHERE i_id_tbl_transmision=tbl_transmision.i_id ORDER BY i_id DESC limit 1 )
									  AND astes.i_id_tbl_estatus in (29,30,31,32,35,38, 33, 34);
									  
		END IF;

END
$BODY$;
