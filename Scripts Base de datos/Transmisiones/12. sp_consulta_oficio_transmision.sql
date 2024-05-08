DROP FUNCTION IF EXISTS religiosos.sp_consulta_oficio_transmision(integer);

CREATE OR REPLACE FUNCTION religiosos.sp_consulta_oficio_transmision(
	i_id_transmision integer)
    RETURNS TABLE(id_transmision integer, referencia character varying, expediente character varying, oficio character varying, id_firmante integer, nombre_firmante text, cargo_firmante character varying, titulo_firmante character varying, puesto_firmante character varying, id_ccp integer, nombre_ccp text, cargo_ccp character varying, titulo_ccp character varying, nombre_dictaminador character varying, nombre_asignador character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE

BEGIN

	RETURN QUERY 
		SELECT trans.i_id
			, trans.c_referencia
			, trans.c_expediente
			, trans.c_oficio
			, trans.i_id_firmante
			, CAST(firm.c_nombre || ' ' ||firm.c_apaterno || ' ' ||firm.c_amaterno AS text) as nombre_firmante
			, firm.c_cargo
			, firm.c_titulo
			, trans.c_puesto_firmante
			, trans.i_id_ccp
			, CAST(ccp.c_nombre || ' ' ||ccp.c_apaterno || ' ' ||ccp.c_amaterno AS text) as nombre_ccp
			, ccp.c_cargo
			, ccp.c_titulo
			, CAST(CONCAT(CAST(CONVERT_FROM(DECODE(perDic.c_nombre, 'BASE64'), 'UTF-8')AS character varying), ' ' ,
						CAST(CONVERT_FROM(DECODE(perDic.c_apaterno, 'BASE64'), 'UTF-8')AS character varying), ' ' ,
						CAST(CONVERT_FROM(DECODE(perDic.c_amaterno, 'BASE64'), 'UTF-8')AS character varying)) AS character varying)
			AS nombre_dictaminador
			, CAST(CONCAT(CAST(CONVERT_FROM(DECODE(perAsig.c_nombre, 'BASE64'), 'UTF-8')AS character varying), ' ' ,
						CAST(CONVERT_FROM(DECODE(perAsig.c_apaterno, 'BASE64'), 'UTF-8')AS character varying), ' ' ,
						CAST(CONVERT_FROM(DECODE(perAsig.c_amaterno, 'BASE64'), 'UTF-8')AS character varying)) AS character varying)
			AS nombre_asignador
			FROM religiosos."TBL_Transmision" trans
			INNER JOIN religiosos."CAT_Director" firm ON firm.i_id = trans.i_id_firmante
			INNER JOIN religiosos."ASOC_TransmisionDictaminador" asoc ON asoc.i_id_tbl_transmision = trans.i_id
			INNER JOIN religiosos."TBL_Usuario" userDic ON userDic.i_id = asoc.id_tbl_usuario_dictaminador
			INNER JOIN religiosos."TBL_Persona" perDic ON perDic.i_id = userDic.i_id_tbl_persona
			INNER JOIN religiosos."TBL_Usuario" userAsig ON userAsig.i_id = asoc.id_tbl_usuario_asignador
			INNER JOIN religiosos."TBL_Persona" perAsig ON perAsig.i_id = userAsig.i_id_tbl_persona
			LEFT JOIN religiosos."CAT_Director" ccp ON ccp.i_id = trans.i_id_ccp
			WHERE trans.i_id = i_id_transmision;
		
END;
$BODY$;

