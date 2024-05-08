 DROP FUNCTION IF EXISTS religiosos.sp_consulta_detalle_tramite_paso_quinto(integer, integer, boolean);

CREATE OR REPLACE FUNCTION religiosos.sp_consulta_detalle_tramite_paso_quinto(
	s_id_us integer,
	i_id_c integer DEFAULT NULL::integer,
	is_dictaminador boolean DEFAULT false)
    RETURNS TABLE(s_id integer, p_id integer, r_id integer, p_nombre_completo character varying, p_nombre character varying, p_apaterno character varying, p_amaterno character varying, p_telefono character varying, p_correo character varying, p_cargo character varying, c_organo_g character varying, p_ine_exists boolean, p_acta_exists boolean, p_curp_exists boolean, t_rep_legal boolean, t_rep_asociado boolean, t_ministro_culto boolean, t_organo_gob boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE

idPerfilAsignador constant integer :=(SELECT i_id_tbl_perfil
								FROM religiosos."TBL_Usuario"
								WHERE i_id = s_id_us
								limit 1);
BEGIN
IF(idPerfilAsignador = 7) THEN
	RETURN QUERY  
		SELECT MAX(tt.i_id),tp.i_id,tr.i_id,
		CAST(CONCAT(CAST(CONVERT_FROM(DECODE(tp.c_nombre, 'BASE64'), 'UTF-8')AS character varying),' ',
					CAST(CONVERT_FROM(DECODE(tp.c_apaterno, 'BASE64'), 'UTF-8')AS character varying),' ',
					CAST(CONVERT_FROM(DECODE(tp.c_amaterno, 'BASE64'), 'UTF-8')AS character varying))
			 AS character varying)
		AS p_nombre_completo,
		CAST(CONVERT_FROM(DECODE(tp.c_nombre, 'BASE64'), 'UTF-8')AS character varying),
		CAST(CONVERT_FROM(DECODE(tp.c_apaterno, 'BASE64'), 'UTF-8')AS character varying),
		CAST(CONVERT_FROM(DECODE(tp.c_amaterno, 'BASE64'), 'UTF-8')AS character varying),
		tp.c_telefono, tp.c_correo, tr.c_cargo,tr.c_organo_g,
		COALESCE((SELECT CASE WHEN ta.i_id is not null THEN true ELSE false END
		FROM religiosos."TBL_Archivo" ta JOIN religiosos."ASOC_RepreArchivo" asoc 
		ON asoc.i_id_tbl_archivo = ta.i_id WHERE asoc.i_id_tbl_representante = tr.i_id 
		AND ta.i_id_tbl_archtram = 7),false),
		COALESCE((SELECT CASE WHEN ta.i_id is not null THEN true ELSE false END
		FROM religiosos."TBL_Archivo" ta JOIN religiosos."ASOC_RepreArchivo" asoc 
		ON asoc.i_id_tbl_archivo = ta.i_id WHERE asoc.i_id_tbl_representante = tr.i_id 
		AND ta.i_id_tbl_archtram = 8),false),
		COALESCE((SELECT CASE WHEN ta.i_id is not null THEN true ELSE false END
		FROM religiosos."TBL_Archivo" ta JOIN religiosos."ASOC_RepreArchivo" asoc 
		ON asoc.i_id_tbl_archivo = ta.i_id WHERE asoc.i_id_tbl_representante = tr.i_id 
		AND ta.i_id_tbl_archtram = 9),false),
		CASE WHEN (AVG(trep.i_id) FILTER (WHERE trep.i_id_cat_trep = 1)) IS NOT NULL
		THEN TRUE ELSE FALSE END,
		CASE WHEN (AVG(trep.i_id) FILTER (WHERE trep.i_id_cat_trep = 2)) IS NOT  NULL
		THEN TRUE ELSE FALSE END,
		CASE WHEN (AVG(trep.i_id) FILTER (WHERE trep.i_id_cat_trep = 3)) IS NOT NULL
		THEN TRUE ELSE FALSE END,
		CASE WHEN (AVG(trep.i_id) FILTER (WHERE trep.i_id_cat_trep = 4)) IS NOT  NULL
		THEN TRUE ELSE FALSE END
		FROM religiosos."TBL_Tramite" tt 
		LEFT JOIN religiosos."ASOC_TraDictaminador" astu ON astu.i_id_tbl_tramite = tt.i_id
		JOIN religiosos."ASOC_TramRep" astr ON astr.i_id_tbl_tramite = tt.i_id 
		JOIN religiosos."TBL_Representantes" tr ON tr.i_id = astr.i_id_tbl_representante 
		LEFT JOIN religiosos."ASOC_REPRE_TREP" trep ON tr.i_id = trep.i_id_tbl_representante 
		JOIN religiosos."TBL_Persona" tp ON tp.i_id = tr.i_id_tbl_persona
		WHERE (tt.i_id = i_id_c or i_id_c is null)
		GROUP BY tp.i_id,tr.i_id,tp.c_nombre,tp.c_apaterno,tp.c_amaterno,
		tp.c_telefono, tp.c_correo, tr.c_cargo,tr.c_organo_g;

ELSE
	IF(is_dictaminador) THEN
	--IF(idPerfilAsignador = 7)THEN
		RETURN QUERY  SELECT MAX(tt.i_id),tp.i_id,tr.i_id,
		CAST(CONCAT(CAST(CONVERT_FROM(DECODE(tp.c_nombre, 'BASE64'), 'UTF-8')AS character varying),' ',
					CAST(CONVERT_FROM(DECODE(tp.c_apaterno, 'BASE64'), 'UTF-8')AS character varying),' ',
					CAST(CONVERT_FROM(DECODE(tp.c_amaterno, 'BASE64'), 'UTF-8')AS character varying))
			 AS character varying)
		AS p_nombre_completo,
		CAST(CONVERT_FROM(DECODE(tp.c_nombre, 'BASE64'), 'UTF-8')AS character varying),
		CAST(CONVERT_FROM(DECODE(tp.c_apaterno, 'BASE64'), 'UTF-8')AS character varying),
		CAST(CONVERT_FROM(DECODE(tp.c_amaterno, 'BASE64'), 'UTF-8')AS character varying),
		tp.c_telefono, tp.c_correo, tr.c_cargo,tr.c_organo_g,
		COALESCE((SELECT CASE WHEN ta.i_id is not null THEN true ELSE false END
		FROM religiosos."TBL_Archivo" ta JOIN religiosos."ASOC_RepreArchivo" asoc 
		ON asoc.i_id_tbl_archivo = ta.i_id WHERE asoc.i_id_tbl_representante = tr.i_id 
		AND ta.i_id_tbl_archtram = 7),false),
		COALESCE((SELECT CASE WHEN ta.i_id is not null THEN true ELSE false END
		FROM religiosos."TBL_Archivo" ta JOIN religiosos."ASOC_RepreArchivo" asoc 
		ON asoc.i_id_tbl_archivo = ta.i_id WHERE asoc.i_id_tbl_representante = tr.i_id 
		AND ta.i_id_tbl_archtram = 8),false),
		COALESCE((SELECT CASE WHEN ta.i_id is not null THEN true ELSE false END
		FROM religiosos."TBL_Archivo" ta JOIN religiosos."ASOC_RepreArchivo" asoc 
		ON asoc.i_id_tbl_archivo = ta.i_id WHERE asoc.i_id_tbl_representante = tr.i_id 
		AND ta.i_id_tbl_archtram = 9),false),
		CASE WHEN (AVG(trep.i_id) FILTER (WHERE trep.i_id_cat_trep = 1)) IS NOT NULL
		THEN TRUE ELSE FALSE END,
		CASE WHEN (AVG(trep.i_id) FILTER (WHERE trep.i_id_cat_trep = 2)) IS NOT  NULL
		THEN TRUE ELSE FALSE END,
		CASE WHEN (AVG(trep.i_id) FILTER (WHERE trep.i_id_cat_trep = 3)) IS NOT NULL
		THEN TRUE ELSE FALSE END,
		CASE WHEN (AVG(trep.i_id) FILTER (WHERE trep.i_id_cat_trep = 4)) IS NOT  NULL
		THEN TRUE ELSE FALSE END
		FROM religiosos."TBL_Tramite" tt 
		JOIN religiosos."ASOC_TraDictaminador" astu ON astu.i_id_tbl_tramite = tt.i_id
		JOIN religiosos."ASOC_TramRep" astr ON astr.i_id_tbl_tramite = tt.i_id 
		JOIN religiosos."TBL_Representantes" tr ON tr.i_id = astr.i_id_tbl_representante 
		LEFT JOIN religiosos."ASOC_REPRE_TREP" trep ON tr.i_id = trep.i_id_tbl_representante 
		JOIN religiosos."TBL_Persona" tp ON tp.i_id = tr.i_id_tbl_persona
		WHERE (tt.i_id = i_id_c or i_id_c is null)
		GROUP BY tp.i_id,tr.i_id,tp.c_nombre,tp.c_apaterno,tp.c_amaterno,
		tp.c_telefono, tp.c_correo, tr.c_cargo,tr.c_organo_g;

	ELSE
		RETURN QUERY  SELECT MAX(tt.i_id),tp.i_id,tr.i_id,
			CAST(CONCAT(CAST(CONVERT_FROM(DECODE(tp.c_nombre, 'BASE64'), 'UTF-8')AS character varying),' ',
						CAST(CONVERT_FROM(DECODE(tp.c_apaterno, 'BASE64'), 'UTF-8')AS character varying),' ',
							CAST(CONVERT_FROM(DECODE(tp.c_amaterno, 'BASE64'), 'UTF-8')AS character varying))
				 AS character varying)
			AS p_nombre_completo,
			CAST(CONVERT_FROM(DECODE(tp.c_nombre, 'BASE64'), 'UTF-8')AS character varying),
			CAST(CONVERT_FROM(DECODE(tp.c_apaterno, 'BASE64'), 'UTF-8')AS character varying),
			CAST(CONVERT_FROM(DECODE(tp.c_amaterno, 'BASE64'), 'UTF-8')AS character varying),
			tp.c_telefono, tp.c_correo, tr.c_cargo,tr.c_organo_g,
			COALESCE((SELECT CASE WHEN ta.i_id is not null THEN true ELSE false END
			FROM religiosos."TBL_Archivo" ta JOIN religiosos."ASOC_RepreArchivo" asoc 
			ON asoc.i_id_tbl_archivo = ta.i_id WHERE asoc.i_id_tbl_representante = tr.i_id 
			AND ta.i_id_tbl_archtram = 7),false),
			COALESCE((SELECT CASE WHEN ta.i_id is not null THEN true ELSE false END
			FROM religiosos."TBL_Archivo" ta JOIN religiosos."ASOC_RepreArchivo" asoc 
			ON asoc.i_id_tbl_archivo = ta.i_id WHERE asoc.i_id_tbl_representante = tr.i_id 
			AND ta.i_id_tbl_archtram = 8),false),
			COALESCE((SELECT CASE WHEN ta.i_id is not null THEN true ELSE false END
			FROM religiosos."TBL_Archivo" ta JOIN religiosos."ASOC_RepreArchivo" asoc 
			ON asoc.i_id_tbl_archivo = ta.i_id WHERE asoc.i_id_tbl_representante = tr.i_id 
			AND ta.i_id_tbl_archtram = 9),false),
			CASE WHEN (AVG(trep.i_id) FILTER (WHERE trep.i_id_cat_trep = 1)) IS NOT NULL
			THEN TRUE ELSE FALSE END,
			CASE WHEN (AVG(trep.i_id) FILTER (WHERE trep.i_id_cat_trep = 2)) IS NOT  NULL
			THEN TRUE ELSE FALSE END,
			CASE WHEN (AVG(trep.i_id) FILTER (WHERE trep.i_id_cat_trep = 3)) IS NOT NULL
			THEN TRUE ELSE FALSE END,
			CASE WHEN (AVG(trep.i_id) FILTER (WHERE trep.i_id_cat_trep = 4)) IS NOT  NULL
			THEN TRUE ELSE FALSE END
			FROM religiosos."TBL_Tramite" tt 
			JOIN religiosos."ASOC_Tramite_Usuario" astu ON astu.i_id_tbl_tramite = tt.i_id
			JOIN religiosos."ASOC_TramRep" astr ON astr.i_id_tbl_tramite = tt.i_id 
			JOIN religiosos."TBL_Representantes" tr ON tr.i_id = astr.i_id_tbl_representante 
			LEFT JOIN religiosos."ASOC_REPRE_TREP" trep ON tr.i_id = trep.i_id_tbl_representante 
			JOIN religiosos."TBL_Persona" tp ON tp.i_id = tr.i_id_tbl_persona
			WHERE (tt.i_id = i_id_c or i_id_c is null) 
				 AND (astu.i_id_tbl_usuario = s_id_us or s_id_us is null)
			GROUP BY tp.i_id,tr.i_id,tp.c_nombre,tp.c_apaterno,tp.c_amaterno,
			tp.c_telefono, tp.c_correo, tr.c_cargo,tr.c_organo_g;

	END IF;
	
/*RETURN QUERY  SELECT MAX(tt.i_id),tp.i_id,tr.i_id,
		CAST(CONCAT(CAST(CONVERT_FROM(DECODE(tp.c_nombre, 'BASE64'), 'UTF-8')AS character varying),' ',
					CAST(CONVERT_FROM(DECODE(tp.c_apaterno, 'BASE64'), 'UTF-8')AS character varying),' ',
					CAST(CONVERT_FROM(DECODE(tp.c_amaterno, 'BASE64'), 'UTF-8')AS character varying))
			 AS character varying)
		AS p_nombre_completo,
		CAST(CONVERT_FROM(DECODE(tp.c_nombre, 'BASE64'), 'UTF-8')AS character varying),
		CAST(CONVERT_FROM(DECODE(tp.c_apaterno, 'BASE64'), 'UTF-8')AS character varying),
		CAST(CONVERT_FROM(DECODE(tp.c_amaterno, 'BASE64'), 'UTF-8')AS character varying),
		tp.c_telefono, tp.c_correo, tr.c_cargo,tr.c_organo_g,
		COALESCE((SELECT CASE WHEN ta.i_id is not null THEN true ELSE false END
		FROM religiosos."TBL_Archivo" ta JOIN religiosos."ASOC_RepreArchivo" asoc 
		ON asoc.i_id_tbl_archivo = ta.i_id WHERE asoc.i_id_tbl_representante = tr.i_id 
		AND ta.i_id_tbl_archtram = 7),false),
		COALESCE((SELECT CASE WHEN ta.i_id is not null THEN true ELSE false END
		FROM religiosos."TBL_Archivo" ta JOIN religiosos."ASOC_RepreArchivo" asoc 
		ON asoc.i_id_tbl_archivo = ta.i_id WHERE asoc.i_id_tbl_representante = tr.i_id 
		AND ta.i_id_tbl_archtram = 8),false),
		COALESCE((SELECT CASE WHEN ta.i_id is not null THEN true ELSE false END
		FROM religiosos."TBL_Archivo" ta JOIN religiosos."ASOC_RepreArchivo" asoc 
		ON asoc.i_id_tbl_archivo = ta.i_id WHERE asoc.i_id_tbl_representante = tr.i_id 
		AND ta.i_id_tbl_archtram = 9),false),
		CASE WHEN (AVG(trep.i_id) FILTER (WHERE trep.i_id_cat_trep = 1)) IS NOT NULL
		THEN TRUE ELSE FALSE END,
		CASE WHEN (AVG(trep.i_id) FILTER (WHERE trep.i_id_cat_trep = 2)) IS NOT  NULL
		THEN TRUE ELSE FALSE END,
		CASE WHEN (AVG(trep.i_id) FILTER (WHERE trep.i_id_cat_trep = 3)) IS NOT NULL
		THEN TRUE ELSE FALSE END,
		CASE WHEN (AVG(trep.i_id) FILTER (WHERE trep.i_id_cat_trep = 4)) IS NOT  NULL
		THEN TRUE ELSE FALSE END
		FROM religiosos."TBL_Tramite" tt 
		LEFT JOIN religiosos."ASOC_TraDictaminador" astu ON astu.i_id_tbl_tramite = tt.i_id
		JOIN religiosos."ASOC_TramRep" astr ON astr.i_id_tbl_tramite = tt.i_id 
		JOIN religiosos."TBL_Representantes" tr ON tr.i_id = astr.i_id_tbl_representante 
		LEFT JOIN religiosos."ASOC_REPRE_TREP" trep ON tr.i_id = trep.i_id_tbl_representante 
		JOIN religiosos."TBL_Persona" tp ON tp.i_id = tr.i_id_tbl_persona
		WHERE (tt.i_id = i_id_c or i_id_c is null) 
			 AND (astu.i_id_tbl_usuariodictam = s_id_us)
		GROUP BY tp.i_id,tr.i_id,tp.c_nombre,tp.c_apaterno,tp.c_amaterno,
		tp.c_telefono, tp.c_correo, tr.c_cargo,tr.c_organo_g;*/
	END IF;
	
	END;

--END IF;
$BODY$;
