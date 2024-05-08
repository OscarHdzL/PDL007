DROP FUNCTION IF EXISTS religiosos.sp_insertar_tramite_transmision(integer, integer, character varying, character varying, character varying, character varying, character varying, character varying);
CREATE OR REPLACE FUNCTION religiosos.sp_insertar_tramite_transmision(
	id_transmision integer,
	id_usuario integer,
	denominacion character varying,
	numero_sgar character varying,
	domicilio character varying,
	correo_electronico character varying,
	numero_tel character varying,
	rep_nombre_completo character varying)
    RETURNS TABLE(i_id_transmision integer, mensaje character varying, proceso_exitoso boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
t_folio_old character varying;
t_folio_new character varying;
    v_id_aux integer;
	
BEGIN
	
	IF( id_transmision <> 0) THEN
	
		v_id_aux := id_transmision;
		
		UPDATE religiosos."TBL_Transmision" transmision
			 SET c_denominacion = COALESCE(denominacion, transmision.c_denominacion)
			 	, c_numero_sgar = COALESCE(numero_sgar, transmision.c_numero_sgar)
			 	, c_domicilio = COALESCE(domicilio, transmision.c_domicilio)
				, c_correo_electronico = COALESCE(correo_electronico, transmision.c_correo_electronico)
				, c_numero_tel = COALESCE(numero_tel, transmision.c_numero_tel)
			 WHERE transmision.i_id = id_transmision;
			 
		 
	ELSE
	
	SELECT c_nfolio INTO t_folio_old FROM religiosos."TBL_Transmision" WHERE c_nfolio not in ('') ORDER BY i_id DESC Limit 1;
			
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
			
	
	
		INSERT INTO religiosos."TBL_Transmision"(c_denominacion, c_numero_sgar, c_domicilio, c_correo_electronico, c_numero_tel, c_representante)
			VALUES (denominacion, numero_sgar, domicilio, correo_electronico,  numero_tel, rep_nombre_completo)
			RETURNING religiosos."TBL_Transmision".i_id INTO v_id_aux;
			
		INSERT INTO religiosos."ASOC_Transmision_Usuario"(i_id_tbl_transmision, i_id_tbl_usuario)
			VALUES (v_id_aux, id_usuario);
			
	    INSERT INTO religiosos."ASOC_Transmision_Estatus"(i_id_tbl_transmision, i_id_tbl_estatus)
			values (v_id_aux, 29);
	END IF;

	RETURN QUERY
		SELECT v_id_aux as i_id_transmision,
		CAST('La información se ha cargado de forma exitosa.'  as varchar) AS mensaje,
		(true) AS proceso_exitoso;

END;
$BODY$;