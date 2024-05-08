DROP FUNCTION IF EXISTS religiosos.sp_consulta_detalle_tramite_paso_cuatro(integer, integer, boolean);

CREATE OR REPLACE FUNCTION religiosos.sp_consulta_detalle_tramite_paso_cuatro(
	s_id_us integer,
	i_id_c integer DEFAULT NULL::integer,
	is_dictaminador boolean DEFAULT false)
    RETURNS TABLE(s_id integer, s_superficie character varying, s_medidas character varying, s_usos character varying, s_aviso_apertura integer, s_colindancia_text_1 character varying, s_colindancia_text_2 character varying, s_colindancia_text_3 character varying, s_colindancia_text_4 character varying, s_colindancia_num_1 numeric, s_colindancia_num_2 numeric, s_colindancia_num_3 numeric, s_colindancia_num_4 numeric, s_ine_propietario_file boolean, s_ine_usuario_file boolean, s_titulo_propiedad_file boolean, s_aviso_apertura_file boolean, s_estatuto_file boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE 
i integer=1;
temprow record;
m_id integer;
c_id integer;
superficie character varying;
medidas character varying;
colindancia_text_1 character varying;
colindancia_text_2 character varying;
colindancia_text_3 character varying;
colindancia_text_4 character varying;
colindancia_num_1 numeric;
colindancia_num_2 numeric;
colindancia_num_3 numeric;
colindancia_num_4 numeric;
usos character varying;
aviso_apertura integer;
ine_propietario_file boolean;
ine_usuario_file boolean;
titulo_propiedad_file boolean;
aviso_apertura_file boolean;
estatuto_file boolean;
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
         SELECT tt.i_id_tbl_medidas, tt.i_id, cav.i_id, tm.c_superficie, tm.c_medidas, tm.c_usos,
            COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
					  ELSE true END
					  FROM religiosos."ASOC_Tramite_Archivos" asta 
					  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
					  WHERE asta.i_id_tbl_tramite = tt.i_id AND i_estatus =1 
					  AND fa.i_id_tbl_archtram = 6 limit 1),false),
		    COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
					  ELSE true END
					  FROM religiosos."ASOC_Tramite_Archivos" asta 
					  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
					  WHERE asta.i_id_tbl_tramite = tt.i_id AND i_estatus =1 
					  AND fa.i_id_tbl_archtram = 3 limit 1),false),
			COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
					  ELSE true END
					  FROM religiosos."ASOC_Tramite_Archivos" asta 
					  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
					  WHERE asta.i_id_tbl_tramite = tt.i_id AND i_estatus =1 
					  AND fa.i_id_tbl_archtram = 4 limit 1),false),
			COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
					  ELSE true END
					  FROM religiosos."ASOC_Tramite_Archivos" asta 
					  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
					  WHERE asta.i_id_tbl_tramite = tt.i_id AND i_estatus =1 
					  AND fa.i_id_tbl_archtram = 5 limit 1),false),
			COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
					  ELSE true END
					  FROM religiosos."ASOC_Tramite_Archivos" asta 
					  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
					  WHERE asta.i_id_tbl_tramite = tt.i_id AND i_estatus =1 
					  AND fa.i_id_tbl_archtram = 13 limit 1),false)
		 INTO m_id,c_id,aviso_apertura,superficie,medidas,usos,aviso_apertura_file,
		 ine_propietario_file,ine_usuario_file,titulo_propiedad_file,estatuto_file
		 FROM religiosos."TBL_Tramite" tt
		 JOIN religiosos."ASOC_Tramite_Usuario" astu ON astu.i_id_tbl_tramite = tt.i_id
		 LEFT JOIN religiosos."TBL_Medidas" tm ON tm.i_id = tt.i_id_tbl_medidas 
		 LEFT JOIN religiosos."TBL_Avisoap" tav ON tav.i_id = tt.i_id_tbl_avisoap
		 LEFT JOIN religiosos."CAT_Avisoap" cav ON tav.i_id_cat_avisoap = cav.i_id
		 WHERE (tt.i_id = i_id_c or i_id_c is null) 
		 AND (astu.i_id_tbl_usuario = s_id_us or s_id_us is null) limit 1; 
         FOR temprow IN
              SELECT tc.c_descripcion AS descripcion, tc.d_medida AS medida FROM religiosos."ASOC_Medidas_Colindancias" mc 
			   JOIN religiosos."TBL_Colindancias" tc ON tc.i_id = mc.i_id_tbl_colindancias WHERE
			   m_id = mc.i_id_tbl_medidas limit 4
              LOOP
				IF (i=1) THEN 
				colindancia_text_1= temprow.descripcion;
				colindancia_num_1= temprow.medida;
                ELSEIF (i=2) THEN
				colindancia_text_2= temprow.descripcion;
				colindancia_num_2= temprow.medida;
				ELSEIF (i=3) THEN
				colindancia_text_3= temprow.descripcion;
				colindancia_num_3= temprow.medida;
				ELSE 
				colindancia_text_4=temprow.descripcion;
				colindancia_num_4= temprow.medida;
				END IF;
				i=i+1;
         END LOOP;
		
         RETURN QUERY SELECT
		 	c_id AS s_id,
		    superficie AS s_superficie,
		    medidas AS s_medidas,
			usos AS s_usos,
		    COALESCE(aviso_apertura,0) AS s_aviso_apertura,
		    colindancia_text_1 AS s_colindancia_text_1,
            colindancia_text_2 AS s_colindancia_text_2,
            colindancia_text_3 AS s_colindancia_text_3,
            colindancia_text_4 AS s_colindancia_text_4,
			colindancia_num_1 AS s_colindancia_num_1,
            colindancia_num_2 AS s_colindancia_num_2,
            colindancia_num_3 AS s_colindancia_num_3,
            colindancia_num_4 AS s_colindancia_num_4,
			ine_propietario_file AS s_ine_propietario_file ,
            ine_usuario_file AS s_ine_usuario_file ,
            titulo_propiedad_file AS s_titulo_propiedad_file ,
            aviso_apertura_file AS s_aviso_apertura_file ,
            estatuto_file AS s_estatuto_file;
ELSEIF(is_dictaminador)THEN

 SELECT tt.i_id_tbl_medidas, tt.i_id, cav.i_id, tm.c_superficie, tm.c_medidas, tm.c_usos,
            COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
					  ELSE true END
					  FROM religiosos."ASOC_Tramite_Archivos" asta 
					  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
					  WHERE asta.i_id_tbl_tramite = tt.i_id AND i_estatus =1 
					  AND fa.i_id_tbl_archtram = 6 limit 1),false),
		    COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
					  ELSE true END
					  FROM religiosos."ASOC_Tramite_Archivos" asta 
					  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
					  WHERE asta.i_id_tbl_tramite = tt.i_id AND i_estatus =1 
					  AND fa.i_id_tbl_archtram = 3 limit 1),false),
			COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
					  ELSE true END
					  FROM religiosos."ASOC_Tramite_Archivos" asta 
					  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
					  WHERE asta.i_id_tbl_tramite = tt.i_id AND i_estatus =1 
					  AND fa.i_id_tbl_archtram = 4 limit 1),false),
			COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
					  ELSE true END
					  FROM religiosos."ASOC_Tramite_Archivos" asta 
					  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
					  WHERE asta.i_id_tbl_tramite = tt.i_id AND i_estatus =1 
					  AND fa.i_id_tbl_archtram = 5 limit 1),false),
			COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
					  ELSE true END
					  FROM religiosos."ASOC_Tramite_Archivos" asta 
					  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
					  WHERE asta.i_id_tbl_tramite = tt.i_id AND i_estatus =1 
					  AND fa.i_id_tbl_archtram = 13 limit 1),false)
		 INTO m_id,c_id,aviso_apertura,superficie,medidas,usos,aviso_apertura_file,
		 ine_propietario_file,ine_usuario_file,titulo_propiedad_file,estatuto_file
		 FROM religiosos."TBL_Tramite" tt
		 JOIN religiosos."ASOC_TraDictaminador" astu ON astu.i_id_tbl_tramite = tt.i_id
		 LEFT JOIN religiosos."TBL_Medidas" tm ON tm.i_id = tt.i_id_tbl_medidas 
		 LEFT JOIN religiosos."TBL_Avisoap" tav ON tav.i_id = tt.i_id_tbl_avisoap
		 LEFT JOIN religiosos."CAT_Avisoap" cav ON tav.i_id_cat_avisoap = cav.i_id
		 WHERE (tt.i_id = i_id_c or i_id_c is null) 
		 limit 1; 
         FOR temprow IN
              SELECT tc.c_descripcion AS descripcion, tc.d_medida AS medida FROM religiosos."ASOC_Medidas_Colindancias" mc 
			   JOIN religiosos."TBL_Colindancias" tc ON tc.i_id = mc.i_id_tbl_colindancias WHERE
			   m_id = mc.i_id_tbl_medidas limit 4
              LOOP
				IF (i=1) THEN 
				colindancia_text_1= temprow.descripcion;
                colindancia_num_1= temprow.medida;
				ELSEIF (i=2) THEN
				colindancia_text_2= temprow.descripcion;
				colindancia_num_2= temprow.medida;				
				ELSEIF (i=3) THEN
				colindancia_text_3= temprow.descripcion;
				colindancia_num_3= temprow.medida;
                ELSE 
				colindancia_text_4=temprow.descripcion;
				colindancia_num_4= temprow.medida;
                END IF;
				i=i+1;
         END LOOP;

ELSEIF(idPerfilAsignador=7)THEN
		SELECT tt.i_id_tbl_medidas, tt.i_id, cav.i_id, tm.c_superficie, tm.c_medidas, tm.c_usos,
            COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
					  ELSE true END
					  FROM religiosos."ASOC_Tramite_Archivos" asta 
					  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
					  WHERE asta.i_id_tbl_tramite = tt.i_id AND i_estatus =1 
					  AND fa.i_id_tbl_archtram = 6 limit 1),false),
		    COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
					  ELSE true END
					  FROM religiosos."ASOC_Tramite_Archivos" asta 
					  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
					  WHERE asta.i_id_tbl_tramite = tt.i_id AND i_estatus =1 
					  AND fa.i_id_tbl_archtram = 3 limit 1),false),
			COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
					  ELSE true END
					  FROM religiosos."ASOC_Tramite_Archivos" asta 
					  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
					  WHERE asta.i_id_tbl_tramite = tt.i_id AND i_estatus =1 
					  AND fa.i_id_tbl_archtram = 4 limit 1),false),
			COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
					  ELSE true END
					  FROM religiosos."ASOC_Tramite_Archivos" asta 
					  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
					  WHERE asta.i_id_tbl_tramite = tt.i_id AND i_estatus =1 
					  AND fa.i_id_tbl_archtram = 5 limit 1),false),
			COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
					  ELSE true END
					  FROM religiosos."ASOC_Tramite_Archivos" asta 
					  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
					  WHERE asta.i_id_tbl_tramite = tt.i_id AND i_estatus =1 
					  AND fa.i_id_tbl_archtram = 13 limit 1),false)
		 INTO m_id,c_id,aviso_apertura,superficie,medidas,usos,aviso_apertura_file,
		 ine_propietario_file,ine_usuario_file,titulo_propiedad_file,estatuto_file
		 FROM religiosos."TBL_Tramite" tt
		 LEFT JOIN religiosos."ASOC_TraDictaminador" astu ON astu.i_id_tbl_tramite = tt.i_id
		 LEFT JOIN religiosos."TBL_Medidas" tm ON tm.i_id = tt.i_id_tbl_medidas 
		 LEFT JOIN religiosos."TBL_Avisoap" tav ON tav.i_id = tt.i_id_tbl_avisoap
		 LEFT JOIN religiosos."CAT_Avisoap" cav ON tav.i_id_cat_avisoap = cav.i_id
		 WHERE (tt.i_id = i_id_c or i_id_c is null) 
		 limit 1; 
         FOR temprow IN
              SELECT tc.c_descripcion AS descripcion, tc.d_medida AS medida FROM religiosos."ASOC_Medidas_Colindancias" mc 
			   JOIN religiosos."TBL_Colindancias" tc ON tc.i_id = mc.i_id_tbl_colindancias WHERE
			   m_id = mc.i_id_tbl_medidas limit 4
              LOOP
				IF (i=1) THEN 
				colindancia_text_1= temprow.descripcion;
                colindancia_num_1= temprow.medida;
				ELSEIF (i=2) THEN
				colindancia_text_2= temprow.descripcion;
				colindancia_num_2= temprow.medida;				
				ELSEIF (i=3) THEN
				colindancia_text_3= temprow.descripcion;
				colindancia_num_3= temprow.medida;
                ELSE 
				colindancia_text_4=temprow.descripcion;
				colindancia_num_4= temprow.medida;
                END IF;
				i=i+1;
         END LOOP;

	ELSE
		SELECT tt.i_id_tbl_medidas, tt.i_id, cav.i_id, tm.c_superficie, tm.c_medidas, tm.c_usos,
            COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
					  ELSE true END
					  FROM religiosos."ASOC_Tramite_Archivos" asta 
					  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
					  WHERE asta.i_id_tbl_tramite = tt.i_id AND i_estatus =1 
					  AND fa.i_id_tbl_archtram = 6 limit 1),false),
		    COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
					  ELSE true END
					  FROM religiosos."ASOC_Tramite_Archivos" asta 
					  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
					  WHERE asta.i_id_tbl_tramite = tt.i_id AND i_estatus =1 
					  AND fa.i_id_tbl_archtram = 3 limit 1),false),
			COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
					  ELSE true END
					  FROM religiosos."ASOC_Tramite_Archivos" asta 
					  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
					  WHERE asta.i_id_tbl_tramite = tt.i_id AND i_estatus =1 
					  AND fa.i_id_tbl_archtram = 4 limit 1),false),
			COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
					  ELSE true END
					  FROM religiosos."ASOC_Tramite_Archivos" asta 
					  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
					  WHERE asta.i_id_tbl_tramite = tt.i_id AND i_estatus =1 
					  AND fa.i_id_tbl_archtram = 5 limit 1),false),
			COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
					  ELSE true END
					  FROM religiosos."ASOC_Tramite_Archivos" asta 
					  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
					  WHERE asta.i_id_tbl_tramite = tt.i_id AND i_estatus =1 
					  AND fa.i_id_tbl_archtram = 13 limit 1),false)
		 INTO m_id,c_id,aviso_apertura,superficie,medidas,usos,aviso_apertura_file,
		 ine_propietario_file,ine_usuario_file,titulo_propiedad_file,estatuto_file
		 FROM religiosos."TBL_Tramite" tt
		 JOIN religiosos."ASOC_TraDictaminador" astu ON astu.i_id_tbl_tramite = tt.i_id
		 LEFT JOIN religiosos."TBL_Medidas" tm ON tm.i_id = tt.i_id_tbl_medidas 
		 LEFT JOIN religiosos."TBL_Avisoap" tav ON tav.i_id = tt.i_id_tbl_avisoap
		 LEFT JOIN religiosos."CAT_Avisoap" cav ON tav.i_id_cat_avisoap = cav.i_id
		 WHERE (tt.i_id = i_id_c or i_id_c is null) 
		 AND (astu.i_id_tbl_usuariodictam = s_id_us or s_id_us is null) limit 1; 
         FOR temprow IN
              SELECT tc.c_descripcion AS descripcion, tc.d_medida AS medida FROM religiosos."ASOC_Medidas_Colindancias" mc 
			   JOIN religiosos."TBL_Colindancias" tc ON tc.i_id = mc.i_id_tbl_colindancias WHERE
			   m_id = mc.i_id_tbl_medidas limit 4
              LOOP
				IF (i=1) THEN 
				colindancia_text_1= temprow.descripcion;
                colindancia_num_1= temprow.medida;
				ELSEIF (i=2) THEN
				colindancia_text_2= temprow.descripcion;
				colindancia_num_2= temprow.medida;				
				ELSEIF (i=3) THEN
				colindancia_text_3= temprow.descripcion;
				colindancia_num_3= temprow.medida;
                ELSE 
				colindancia_text_4=temprow.descripcion;
				colindancia_num_4= temprow.medida;
                END IF;
				i=i+1;
         END LOOP;

	END IF;
         RETURN QUERY SELECT
		 	c_id AS s_id,
		    superficie AS s_superficie,
		    medidas AS s_medidas,
			usos AS s_usos,			
		    COALESCE(aviso_apertura,0) AS s_aviso_apertura,
		    colindancia_text_1 AS s_colindancia_text_1,
            colindancia_text_2 AS s_colindancia_text_2,
            colindancia_text_3 AS s_colindancia_text_3,
            colindancia_text_4 AS s_colindancia_text_4,
			colindancia_num_1 AS s_colindancia_num_1,
            colindancia_num_2 AS s_colindancia_num_2,
            colindancia_num_3 AS s_colindancia_num_3,
            colindancia_num_4 AS s_colindancia_num_4,
			ine_propietario_file AS s_ine_propietario_file ,
            ine_usuario_file AS s_ine_usuario_file ,
            titulo_propiedad_file AS s_titulo_propiedad_file ,
            aviso_apertura_file AS s_aviso_apertura_file ,
            estatuto_file AS s_estatuto_file;
--ELSE
	--RETURN;
--END IF;
END;
$BODY$;
