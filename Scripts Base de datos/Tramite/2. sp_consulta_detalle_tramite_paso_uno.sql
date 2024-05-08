DROP FUNCTION IF EXISTS religiosos.sp_consulta_detalle_tramite_paso_uno(integer, integer, boolean);

CREATE OR REPLACE FUNCTION religiosos.sp_consulta_detalle_tramite_paso_uno(
	s_id_us integer,
	i_id_c integer DEFAULT NULL::integer,
	is_dictaminador boolean DEFAULT false)
    RETURNS TABLE(s_id integer, s_cat_credo integer, s_cat_solicitud_escrito integer, s_cat_denominacion character varying, s_numero_registro character varying, s_pais_origen integer, d_id_domicilio integer, d_tipo_domicilio integer, d_numeroe character varying, d_numeroi character varying, d_colonia integer, d_calle character varying, c_cpostal_n character varying, c_matriz character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
tbl_tramite_id integer;
tbl_domicilio_id integer;
status integer;
 -----Este estatus es el último registrado para permitir corregir al usuario.
statusEdicion constant integer:=(SELECT astes.i_id_tbl_estatus FROM religiosos."TBL_Tramite" tt 
								 JOIN religiosos."ASOC_TramiteEstatus" astes ON astes.i_id_tbl_tramite = tt.i_id
								 JOIN religiosos."ASOC_Tramite_Usuario" astu ON astu.i_id_tbl_tramite = tt.i_id
								 WHERE   (tt.i_id = i_id_c) 
								 AND (astu.i_id_tbl_usuario = s_id_us) 
								 ORDER BY astes.i_id DESC limit 1);
								 
idPerfilAsignador constant integer :=(SELECT i_id_tbl_perfil
								FROM religiosos."TBL_Usuario"
								WHERE i_id = s_id_us
								limit 1);
BEGIN
	   
 SELECT MAX(i_id_tbl_estatus),tt.i_id INTO status,tbl_tramite_id  FROM religiosos."TBL_Tramite" tt 
								 JOIN religiosos."ASOC_TramiteEstatus" astes ON astes.i_id_tbl_tramite = tt.i_id
								  JOIN religiosos."ASOC_Tramite_Usuario" astu ON astu.i_id_tbl_tramite = tt.i_id
								 WHERE   (tt.i_id = i_id_c or i_id_c is null) 
								 AND (astu.i_id_tbl_usuario = s_id_us or s_id_us is null) GROUP BY tt.i_id ORDER BY tt.i_id DESC limit 1;
 IF (status < 9 or statusEdicion in (3,4,5,6,7,8,9,10,14,15,16,17,18,19,20,36)) THEN
	   IF(i_id_c is null) THEN
            RETURN QUERY 	
			 SELECT
             MAX(tt.i_id) AS s_id,
             MAX(tt.i_id_tbl_credo) as s_cat_credo,
             MAX(tt.i_id_tbl_tsolreg) AS s_cat_solicitud_escrito , 
             tt.c_denominacion AS s_cat_denominacion,
             tt.c_nregistro AS s_numero_registro, 
             MAX(tt.i_id_tbl_paiso) AS s_pais_origen ,
             MAX(td.i_id) AS d_id_domicilio,
             MAX(td.i_id_tbl_tdomicilio) AS d_tipo_domicilio ,
             td.c_numeroe AS d_numeroe, 
             td.c_numeroi AS d_numeroi, 
             td.i_id_tbl_colonia AS d_colonia , 
             td.c_calle AS d_calle ,
             cc.c_cpostal AS c_cpostal_n,
			 tt.c_matriz  AS c_matriz
             FROM religiosos."TBL_Tramite" tt 
			 JOIN religiosos."ASOC_Tramite_Usuario" astu ON astu.i_id_tbl_tramite = tt.i_id
			 JOIN religiosos."ASOC_TramDom" astd ON astd.i_id_tbl_tramite = tt.i_id
             JOIN religiosos."TBL_Domicilio" td ON astd.i_id_tbl_domicilio = td.i_id 
			 JOIN religiosos."CAT_Colonia" cc
             ON cc.i_id = td.i_id_tbl_colonia WHERE td.i_id_tbl_tdomicilio=1 AND tt.i_id=tbl_tramite_id
			 AND (astu.i_id_tbl_usuario = s_id_us)
			 GROUP BY tt.c_denominacion,
              tt.c_nregistro,td.c_numeroe,
              td.c_numeroi,
              td.i_id_tbl_colonia,
              td.c_calle,
              cc.c_cpostal,
			  tt.c_matriz;
	   
	   ELSE
	   
			 RETURN QUERY 	 
			 SELECT
             MAX(tt.i_id) AS s_id,
             MAX(tt.i_id_tbl_credo) as s_cat_credo,
             MAX(tt.i_id_tbl_tsolreg) AS s_cat_solicitud_escrito , 
             tt.c_denominacion AS s_cat_denominacion,
             tt.c_nregistro AS s_numero_registro, 
             MAX(tt.i_id_tbl_paiso) AS s_pais_origen ,
             MAX(td.i_id) AS d_id_domicilio,
             MAX(td.i_id_tbl_tdomicilio) AS d_tipo_domicilio ,
             td.c_numeroe AS d_numeroe, 
             td.c_numeroi AS d_numeroi, 
             td.i_id_tbl_colonia AS d_colonia , 
             td.c_calle AS d_calle ,
             cc.c_cpostal AS c_cpostal_n,
			 tt.c_matriz  AS c_matriz
             FROM religiosos."TBL_Tramite" tt 
			 JOIN religiosos."ASOC_Tramite_Usuario" astu ON astu.i_id_tbl_tramite = tt.i_id
			 JOIN religiosos."ASOC_TramDom" astd ON astd.i_id_tbl_tramite = tt.i_id
             JOIN religiosos."TBL_Domicilio" td ON astd.i_id_tbl_domicilio = td.i_id 
			 JOIN religiosos."CAT_Colonia" cc
             ON cc.i_id = td.i_id_tbl_colonia  WHERE td.i_id_tbl_tdomicilio=1 and 
             tt.i_id = i_id_c AND (astu.i_id_tbl_usuario = s_id_us or s_id_us is null)
			 GROUP BY tt.c_denominacion,
              tt.c_nregistro,td.c_numeroe,
              td.c_numeroi,
              td.i_id_tbl_colonia,
              td.c_calle,
              cc.c_cpostal,
			  tt.c_matriz;
		END IF;
 
 ELSEIF(is_dictaminador) THEN
 	RETURN QUERY 	 
			 SELECT
             MAX(tt.i_id) AS s_id,
             MAX(tt.i_id_tbl_credo) as s_cat_credo,
             MAX(tt.i_id_tbl_tsolreg) AS s_cat_solicitud_escrito , 
             tt.c_denominacion AS s_cat_denominacion,
             tt.c_nregistro AS s_numero_registro, 
             MAX(tt.i_id_tbl_paiso) AS s_pais_origen ,
             MAX(td.i_id) AS d_id_domicilio,
             MAX(td.i_id_tbl_tdomicilio) AS d_tipo_domicilio ,
             td.c_numeroe AS d_numeroe, 
             td.c_numeroi AS d_numeroi, 
             td.i_id_tbl_colonia AS d_colonia , 
             td.c_calle AS d_calle ,
             cc.c_cpostal AS c_cpostal_n,
			 tt.c_matriz  AS c_matriz
             FROM religiosos."TBL_Tramite" tt 
			 JOIN religiosos."ASOC_TraDictaminador" astu ON astu.i_id_tbl_tramite = tt.i_id
			 JOIN religiosos."ASOC_TramDom" astd ON astd.i_id_tbl_tramite = tt.i_id
             JOIN religiosos."TBL_Domicilio" td ON astd.i_id_tbl_domicilio = td.i_id 
			 JOIN religiosos."CAT_Colonia" cc
             ON cc.i_id = td.i_id_tbl_colonia  
			 WHERE td.i_id_tbl_tdomicilio=1 and tt.i_id = i_id_c
			 GROUP BY tt.c_denominacion,
              tt.c_nregistro,td.c_numeroe,
              td.c_numeroi,
              td.i_id_tbl_colonia,
              td.c_calle,
              cc.c_cpostal,
			  tt.c_matriz;
 ELSEIF(idPerfilAsignador = 7) THEN
	 	RETURN QUERY 	 
			 SELECT
             MAX(tt.i_id) AS s_id,
             MAX(tt.i_id_tbl_credo) as s_cat_credo,
             MAX(tt.i_id_tbl_tsolreg) AS s_cat_solicitud_escrito , 
             tt.c_denominacion AS s_cat_denominacion,
             tt.c_nregistro AS s_numero_registro, 
             MAX(tt.i_id_tbl_paiso) AS s_pais_origen ,
             MAX(td.i_id) AS d_id_domicilio,
             MAX(td.i_id_tbl_tdomicilio) AS d_tipo_domicilio ,
             td.c_numeroe AS d_numeroe, 
             td.c_numeroi AS d_numeroi, 
             td.i_id_tbl_colonia AS d_colonia , 
             td.c_calle AS d_calle ,
             cc.c_cpostal AS c_cpostal_n,
			 tt.c_matriz  AS c_matriz
             FROM religiosos."TBL_Tramite" tt 
			 LEFT JOIN religiosos."ASOC_TraDictaminador" astu ON astu.i_id_tbl_tramite = tt.i_id
			 JOIN religiosos."ASOC_TramDom" astd ON astd.i_id_tbl_tramite = tt.i_id
             JOIN religiosos."TBL_Domicilio" td ON astd.i_id_tbl_domicilio = td.i_id 
			 JOIN religiosos."CAT_Colonia" cc
             ON cc.i_id = td.i_id_tbl_colonia  
			 WHERE td.i_id_tbl_tdomicilio=1 and tt.i_id = i_id_c
			 GROUP BY tt.c_denominacion,
              tt.c_nregistro,td.c_numeroe,
              td.c_numeroi,
              td.i_id_tbl_colonia,
              td.c_calle,
              cc.c_cpostal,
			  tt.c_matriz;

	 ELSE
        	RETURN QUERY 	 
			 SELECT
             MAX(tt.i_id) AS s_id,
             MAX(tt.i_id_tbl_credo) as s_cat_credo,
             MAX(tt.i_id_tbl_tsolreg) AS s_cat_solicitud_escrito , 
             tt.c_denominacion AS s_cat_denominacion,
             tt.c_nregistro AS s_numero_registro, 
             MAX(tt.i_id_tbl_paiso) AS s_pais_origen ,
             MAX(td.i_id) AS d_id_domicilio,
             MAX(td.i_id_tbl_tdomicilio) AS d_tipo_domicilio ,
             td.c_numeroe AS d_numeroe, 
             td.c_numeroi AS d_numeroi, 
             td.i_id_tbl_colonia AS d_colonia , 
             td.c_calle AS d_calle ,
             cc.c_cpostal AS c_cpostal_n,
			 tt.c_matriz  AS c_matriz
             FROM religiosos."TBL_Tramite" tt 
			 LEFT JOIN religiosos."ASOC_TraDictaminador" astu ON astu.i_id_tbl_tramite = tt.i_id
			 JOIN religiosos."ASOC_TramDom" astd ON astd.i_id_tbl_tramite = tt.i_id
             JOIN religiosos."TBL_Domicilio" td ON astd.i_id_tbl_domicilio = td.i_id 
			 JOIN religiosos."CAT_Colonia" cc
             ON cc.i_id = td.i_id_tbl_colonia  WHERE td.i_id_tbl_tdomicilio=1 and 
             tt.i_id = i_id_c AND (astu.i_id_tbl_usuariodictam = s_id_us)
			 GROUP BY tt.c_denominacion,
              tt.c_nregistro,td.c_numeroe,
              td.c_numeroi,
              td.i_id_tbl_colonia,
              td.c_calle,
              cc.c_cpostal,
			  tt.c_matriz;
END IF;
--ELSE
--RETURN;
--END IF;
    END;
$BODY$;