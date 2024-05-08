DROP FUNCTION IF EXISTS religiosos.sp_consulta_detalle_tramite_paso_tres(integer, integer, boolean);

CREATE OR REPLACE FUNCTION religiosos.sp_consulta_detalle_tramite_paso_tres(
	s_id_us integer,
	i_id_c integer DEFAULT NULL::integer,
	is_dictaminador boolean DEFAULT false)
    RETURNS TABLE(s_id_tramite integer, s_cat_sjuridica integer, s_f_apertura_id integer, s_f_apertura character varying, s_file_exist_sjuridica boolean, s_file_exist_manifiesto boolean, d_id_domicilio integer, d_tipo_domicilio integer, d_numeroe character varying, d_numeroi character varying, d_colonia integer, d_calle character varying, c_cpostal_n character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
 status constant integer:=(SELECT MAX(i_id_tbl_estatus) FROM religiosos."TBL_Tramite" tt 
								 JOIN religiosos."ASOC_TramiteEstatus" astes ON astes.i_id_tbl_tramite = tt.i_id
								  JOIN religiosos."ASOC_Tramite_Usuario" astu ON astu.i_id_tbl_tramite = tt.i_id
								 WHERE   (tt.i_id = i_id_c or i_id_c is null) 
								 AND (astu.i_id_tbl_usuario = s_id_us or s_id_us is null));
 -----Este estatus es el último registrado para permitir corregir al usuario.
 statusEdicion constant integer:=(SELECT astes.i_id_tbl_estatus FROM religiosos."TBL_Tramite" tt 
								 JOIN religiosos."ASOC_TramiteEstatus" astes ON astes.i_id_tbl_tramite = tt.i_id
								 JOIN religiosos."ASOC_Tramite_Usuario" astu ON astu.i_id_tbl_tramite = tt.i_id
								 WHERE   (tt.i_id = i_id_c) 
								 AND (astu.i_id_tbl_usuario = s_id_us) ORDER BY astes.i_id DESC limit 1);
								 
idPerfilAsignador constant integer :=(SELECT i_id_tbl_perfil
								FROM religiosos."TBL_Usuario"
								WHERE i_id = s_id_us
								limit 1);
BEGIN
	   
 IF (status < 9 or statusEdicion in (3,4,5,6,7,8,9,10,14,15,16,17,18,19,20,36)) THEN
         RETURN QUERY SELECT
		 tt.i_id AS s_id_tramite,
         tt.i_id_tbl_sjuridica AS s_cat_sjuridica,
		 tf.i_id AS s_f_apertura_id,
         CAST(tf.d_inicio AS character varying)  AS s_f_apertura,
		 COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
		  ELSE true END
		  FROM religiosos."ASOC_Tramite_Archivos" asta 
		  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
		  WHERE asta.i_id_tbl_tramite = tt.i_id AND fa.i_id_tbl_archtram = 1 AND i_estatus =1 limit 1),false)
		  AS s_file_str_sjuridica,
         COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
		  ELSE true END
		  FROM religiosos."ASOC_Tramite_Archivos" asta 
		  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
		  WHERE asta.i_id_tbl_tramite = tt.i_id AND i_estatus =1 
		  AND fa.i_id_tbl_archtram = 2 limit 1),false)
		  AS s_file_str_manifiesto,
		 td.i_id AS d_id_domicilio,
		 td.i_id_tbl_tdomicilio AS d_tipo_domicilio ,
		 td.c_numeroe AS d_numeroe,
		 td.c_numeroi AS d_numeroi,
		 td.i_id_tbl_colonia AS d_colonia ,
		 td.c_calle AS d_calle ,
		 cc.c_cpostal AS c_cpostal_n
		 FROM religiosos."TBL_Tramite" tt 
		 JOIN religiosos."ASOC_Tramite_Usuario" astu ON astu.i_id_tbl_tramite = tt.i_id
		 LEFT JOIN religiosos."ASOC_TramDom" astd ON astd.i_id_tbl_tramite = tt.i_id
		 LEFT JOIN (SELECT * FROM religiosos."TBL_Domicilio"
					WHERE  i_id_tbl_tdomicilio=3 ) td ON astd.i_id_tbl_domicilio = td.i_id
		 LEFT JOIN religiosos."CAT_Colonia" cc ON cc.i_id = td.i_id_tbl_colonia 
		 LEFT JOIN religiosos."ASOC_Tramite_Fechas" asft ON asft.i_id_tbl_tramite = tt.i_id
		 LEFT JOIN (SELECT * FROM religiosos."TBL_Fechas"
				   WHERE i_estatus = 1 AND i_id_cat_fechas =1) tf 
				   ON tf.i_id = asft.i_id_tbl_fechas
		 WHERE (tt.i_id = i_id_c or i_id_c is null) AND (astu.i_id_tbl_usuario = s_id_us or s_id_us is null) 
		 ORDER BY tf.d_inicio,td.i_id limit 1;

ELSEIF(is_dictaminador)THEN
	RETURN QUERY SELECT
		 tt.i_id AS s_id_tramite,
         tt.i_id_tbl_sjuridica AS s_cat_sjuridica,
		 tf.i_id AS s_f_apertura_id,
         CAST(tf.d_inicio AS character varying)  AS s_f_apertura,
		 COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
		  ELSE true END
		  FROM religiosos."ASOC_Tramite_Archivos" asta 
		  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
		  WHERE asta.i_id_tbl_tramite = tt.i_id AND fa.i_id_tbl_archtram = 1 AND i_estatus =1 limit 1),false)
		  AS s_file_str_sjuridica,
         COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
		  ELSE true END
		  FROM religiosos."ASOC_Tramite_Archivos" asta 
		  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
		  WHERE asta.i_id_tbl_tramite = tt.i_id AND i_estatus =1 
		  AND fa.i_id_tbl_archtram = 2 limit 1),false)
		  AS s_file_str_manifiesto,
		 td.i_id AS d_id_domicilio,
		 td.i_id_tbl_tdomicilio AS d_tipo_domicilio ,
		 td.c_numeroe AS d_numeroe,
		 td.c_numeroi AS d_numeroi,
		 td.i_id_tbl_colonia AS d_colonia ,
		 td.c_calle AS d_calle ,
		 cc.c_cpostal AS c_cpostal_n
		 FROM religiosos."TBL_Tramite" tt 
		 JOIN religiosos."ASOC_TraDictaminador" astu ON astu.i_id_tbl_tramite = tt.i_id
		 LEFT JOIN religiosos."ASOC_TramDom" astd ON astd.i_id_tbl_tramite = tt.i_id
		 LEFT JOIN (SELECT * FROM religiosos."TBL_Domicilio"
					WHERE  i_id_tbl_tdomicilio=3 ) td ON astd.i_id_tbl_domicilio = td.i_id
		 LEFT JOIN religiosos."CAT_Colonia" cc ON cc.i_id = td.i_id_tbl_colonia 
		 LEFT JOIN religiosos."ASOC_Tramite_Fechas" asft ON asft.i_id_tbl_tramite = tt.i_id
		 LEFT JOIN (SELECT * FROM religiosos."TBL_Fechas"
				   WHERE i_estatus = 1 AND i_id_cat_fechas =1) tf 
				   ON tf.i_id = asft.i_id_tbl_fechas
		 WHERE (tt.i_id = i_id_c or i_id_c is null)
		 ORDER BY tf.d_inicio,td.i_id limit 1;

ELSEIF(idPerfilAsignador = 7)THEN
		RETURN QUERY SELECT
		 tt.i_id AS s_id_tramite,
         tt.i_id_tbl_sjuridica AS s_cat_sjuridica,
		 tf.i_id AS s_f_apertura_id,
         CAST(tf.d_inicio AS character varying)  AS s_f_apertura,
		 COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
		  ELSE true END
		  FROM religiosos."ASOC_Tramite_Archivos" asta 
		  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
		  WHERE asta.i_id_tbl_tramite = tt.i_id AND fa.i_id_tbl_archtram = 1 AND i_estatus =1 limit 1),false)
		  AS s_file_str_sjuridica,
         COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
		  ELSE true END
		  FROM religiosos."ASOC_Tramite_Archivos" asta 
		  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
		  WHERE asta.i_id_tbl_tramite = tt.i_id AND i_estatus =1 
		  AND fa.i_id_tbl_archtram = 2 limit 1),false)
		  AS s_file_str_manifiesto,
		 td.i_id AS d_id_domicilio,
		 td.i_id_tbl_tdomicilio AS d_tipo_domicilio ,
		 td.c_numeroe AS d_numeroe,
		 td.c_numeroi AS d_numeroi,
		 td.i_id_tbl_colonia AS d_colonia ,
		 td.c_calle AS d_calle ,
		 cc.c_cpostal AS c_cpostal_n
		 FROM religiosos."TBL_Tramite" tt 
		 LEFT JOIN religiosos."ASOC_TraDictaminador" astu ON astu.i_id_tbl_tramite = tt.i_id
		 LEFT JOIN religiosos."ASOC_TramDom" astd ON astd.i_id_tbl_tramite = tt.i_id
		 LEFT JOIN (SELECT * FROM religiosos."TBL_Domicilio"
					WHERE  i_id_tbl_tdomicilio=3 ) td ON astd.i_id_tbl_domicilio = td.i_id
		 LEFT JOIN religiosos."CAT_Colonia" cc ON cc.i_id = td.i_id_tbl_colonia 
		 LEFT JOIN religiosos."ASOC_Tramite_Fechas" asft ON asft.i_id_tbl_tramite = tt.i_id
		 LEFT JOIN (SELECT * FROM religiosos."TBL_Fechas"
				   WHERE i_estatus = 1 AND i_id_cat_fechas =1) tf 
				   ON tf.i_id = asft.i_id_tbl_fechas
		 WHERE (tt.i_id = i_id_c or i_id_c is null)
		 ORDER BY tf.d_inicio,td.i_id limit 1;
	
	ELSE
		RETURN QUERY SELECT
		 tt.i_id AS s_id_tramite,
         tt.i_id_tbl_sjuridica AS s_cat_sjuridica,
		 tf.i_id AS s_f_apertura_id,
         CAST(tf.d_inicio AS character varying)  AS s_f_apertura,
		 COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
		  ELSE true END
		  FROM religiosos."ASOC_Tramite_Archivos" asta 
		  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
		  WHERE asta.i_id_tbl_tramite = tt.i_id AND fa.i_id_tbl_archtram = 1 AND i_estatus =1 limit 1),false)
		  AS s_file_str_sjuridica,
         COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
		  ELSE true END
		  FROM religiosos."ASOC_Tramite_Archivos" asta 
		  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
		  WHERE asta.i_id_tbl_tramite = tt.i_id AND i_estatus =1 
		  AND fa.i_id_tbl_archtram = 2 limit 1),false)
		  AS s_file_str_manifiesto,
		 td.i_id AS d_id_domicilio,
		 td.i_id_tbl_tdomicilio AS d_tipo_domicilio ,
		 td.c_numeroe AS d_numeroe,
		 td.c_numeroi AS d_numeroi,
		 td.i_id_tbl_colonia AS d_colonia ,
		 td.c_calle AS d_calle ,
		 cc.c_cpostal AS c_cpostal_n
		 FROM religiosos."TBL_Tramite" tt 
		 JOIN religiosos."ASOC_TraDictaminador" astu ON astu.i_id_tbl_tramite = tt.i_id
		 LEFT JOIN religiosos."ASOC_TramDom" astd ON astd.i_id_tbl_tramite = tt.i_id
		 LEFT JOIN (SELECT * FROM religiosos."TBL_Domicilio"
					WHERE  i_id_tbl_tdomicilio=3 ) td ON astd.i_id_tbl_domicilio = td.i_id
		 LEFT JOIN religiosos."CAT_Colonia" cc ON cc.i_id = td.i_id_tbl_colonia 
		 LEFT JOIN religiosos."ASOC_Tramite_Fechas" asft ON asft.i_id_tbl_tramite = tt.i_id
		 LEFT JOIN (SELECT * FROM religiosos."TBL_Fechas"
				   WHERE i_estatus = 1 AND i_id_cat_fechas =1) tf 
				   ON tf.i_id = asft.i_id_tbl_fechas
		 WHERE (tt.i_id = i_id_c or i_id_c is null) AND (astu.i_id_tbl_usuariodictam = s_id_us)
		 ORDER BY tf.d_inicio,td.i_id limit 1;
	END IF;
--ELSE 
	--RETURN;
	--END IF;
END;
$BODY$;
