DROP FUNCTION IF EXISTS religiosos.sp_insertar_tramite_paso_uno(integer, integer, integer, character varying, character varying, integer, integer, character varying, character varying, integer, character varying);

CREATE OR REPLACE FUNCTION religiosos.sp_insertar_tramite_paso_uno(
	s_id_us integer,
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
	c_matriz character varying)
    RETURNS TABLE(id_tramite integer, mensaje character varying, proceso_existoso boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
tbl_tramite_id integer;
tbl_fecha_apertura_id integer;
tbl_domicilio_id integer;
t_folio_old character varying;
t_folio_new character varying;

			BEGIN
			SELECT c_nfolio INTO t_folio_old FROM religiosos."TBL_Tramite" WHERE c_nfolio not in ('') ORDER BY i_id DESC Limit 1;
			
			IF (t_folio_old = '') 
			THEN
				t_folio_old:= CAST(CONCAT('00000','/',EXTRACT(YEAR FROM NOW())) AS CHARACTER VARYING);
			END IF;
			
			SELECT
				CASE 
				WHEN CAST(EXTRACT(YEAR FROM NOW()) AS CHARACTER VARYING) = CAST(SUBSTRING(t_folio_old, 7, 4) AS CHARACTER VARYING)
			THEN 
			   CAST(CONCAT(CAST(to_char(CAST(SUBSTRING( t_folio_old, 1, 5) AS INTEGER)+1, 'fm00000')
					 AS CHARACTER VARYING),'/',EXTRACT(YEAR FROM NOW())) AS CHARACTER VARYING)
			ELSE
			    CAST(CONCAT('00001','/',EXTRACT(YEAR FROM NOW())) AS CHARACTER VARYING)
			END INTO t_folio_new;
			
			INSERT INTO religiosos."TBL_Tramite"(c_nregistro, c_denominacion,
												 i_id_tbl_paiso,
												 i_id_tbl_tsolreg, 
												 i_id_tbl_credo,
												  i_id_cat_ttramite, c_nfolio, c_matriz)
	        VALUES ('',S_CAT_DENOMINACION,S_PAIS_ORIGEN, S_CAT_SOLICITUD_ESCRITO, S_CAT_CREDO,1, '', c_matriz) -- ANTES SE AGREGA EL t_folio_new
					RETURNING i_id INTO tbl_tramite_id;
					INSERT INTO religiosos."ASOC_Tramite_Usuario"(
						i_id_tbl_tramite, i_id_tbl_usuario)
						VALUES (tbl_tramite_id, s_id_us);
           INSERT INTO religiosos."TBL_Domicilio"(c_calle, c_numeroe, c_numeroi, i_id_tbl_colonia, i_id_tbl_tdomicilio)
	       VALUES (D_CALLE, D_NUMEROE, D_NUMEROI, D_COLONIA, D_TIPO_DOMICILIO) RETURNING i_id INTO tbl_domicilio_id;
	       INSERT INTO religiosos."ASOC_TramDom"(i_id_tbl_tramite, i_id_tbl_domicilio)
	       VALUES ( tbl_tramite_id, tbl_domicilio_id);
	       
		   INSERT INTO religiosos."ASOC_TramiteEstatus"(
	       i_id_tbl_tramite, i_id_tbl_estatus)
	       VALUES ( tbl_tramite_id, 3);
		   
		   INSERT INTO religiosos."TBL_Fechas"(
			d_inicio, i_estatus)
			VALUES ( current_timestamp, 1)
			RETURNING i_id INTO tbl_fecha_apertura_id;
							
			INSERT INTO religiosos."ASOC_Tramite_Fechas"(
			i_id_tbl_tramite, i_id_tbl_fechas)
			VALUES (tbl_tramite_id, tbl_fecha_apertura_id);
			
					RETURN QUERY SELECT
						 tbl_tramite_id as id_tramite,
						 CAST('La información se guardo correctamente.' as varchar) AS mensaje,
						 (true) AS proceso_existoso;

		END;
$BODY$;
