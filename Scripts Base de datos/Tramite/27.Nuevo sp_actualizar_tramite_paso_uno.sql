
CREATE OR REPLACE FUNCTION religiosos.sp_actualizar_tramite_paso_uno(
	s_id integer,
	s_cat_credo integer,
	s_cat_solicitud_escrito integer,
	s_cat_denominacion character varying,
	s_numero_registro character varying,
	s_pais_origen integer,
	d_tipo_domicilio integer,
	d_numeroe character varying,
	d_numeroi character varying,
	d_colonia integer,
	d_calle character varying,
	p_matriz character varying)
    RETURNS TABLE(id_tramite integer, mensaje character varying, proceso_existoso boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
tbl_domicilio_id integer;
idTipoEscrito integer := (SELECT i_id_tbl_tsolreg FROM religiosos."TBL_Tramite" WHERE  i_id = s_id limit 1);

			BEGIN
			          SELECT td.i_id INTO tbl_domicilio_id 
					  FROM religiosos."TBL_Tramite" tt 
					  LEFT JOIN religiosos."ASOC_TramDom" astd ON astd.i_id_tbl_tramite = tt.i_id
					  LEFT JOIN religiosos."TBL_Domicilio" td ON td.i_id = astd.i_id_tbl_domicilio
					  WHERE tt.i_id=s_id AND td.i_id_tbl_tdomicilio =D_TIPO_DOMICILIO limit 1;
					  
					  IF(tbl_domicilio_id is not null or tbl_domicilio_id > 0) THEN
					  
					        UPDATE  religiosos."TBL_Tramite"
					        SET c_denominacion=s_cat_denominacion,
												 i_id_tbl_paiso= s_pais_origen,
												 i_id_tbl_tsolreg=s_cat_solicitud_escrito,
												 i_id_cat_ttramite = 1,
												 i_id_tbl_credo = s_cat_credo, 
												 c_matriz = p_MATRIZ WHERE i_id=s_id;
							
					        UPDATE religiosos."TBL_Domicilio"
							SET c_calle = D_CALLE, c_numeroe = D_NUMEROE,c_numeroi = D_NUMEROI,i_id_tbl_colonia = D_COLONIA,
							i_id_tbl_tdomicilio = D_TIPO_DOMICILIO
							WHERE i_id =tbl_domicilio_id;
							
							IF(idTipoEscrito <> s_cat_solicitud_escrito)THEN
								UPDATE religiosos."TBL_Cnotorioarr"
								SET i_id_cat_cnotorioarr = 1
								WHERE i_id IN (SELECT i_id_tbl_cnotorioarr FROM religiosos."TBL_Tramite" WHERE i_id = s_id);
							END IF;
							
							RETURN QUERY SELECT
							s_id as id_tramite,
							CAST('La información se ha cargado de forma exitosa.'  as varchar) AS mensaje,
							(true) AS proceso_existoso;
					  ELSE
							
							RETURN QUERY SELECT
							0 as id_tramite,
							CAST('La información no se ha cargado de forma exitosa.' as varchar) AS mensaje,
							(false) AS proceso_existoso;
					  END IF;
		END;
$BODY$;
