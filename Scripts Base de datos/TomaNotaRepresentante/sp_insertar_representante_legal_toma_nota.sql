-- FUNCTION: religiosos.sp_insertar_representante_legal_toma_nota(integer, integer, character varying, character varying, character varying, boolean, boolean, boolean, boolean, character varying, character varying, integer, integer)

-- DROP FUNCTION IF EXISTS religiosos.sp_insertar_representante_legal_toma_nota(integer, integer, character varying, character varying, character varying, boolean, boolean, boolean, boolean, character varying, character varying, integer, integer);

CREATE OR REPLACE FUNCTION religiosos.sp_insertar_representante_legal_toma_nota(
	s_id integer,
	p_id integer,
	p_nombre character varying,
	p_apellido_p character varying,
	p_apellido_m character varying,
	t_rep_legal boolean,
	t_ministro_culto boolean,
	t_rep_asociado boolean,
	t_organo_gob boolean,
	p_cargo character varying,
	p_organo_g character varying,
	r_cat_poderes integer,
	r_cat_movimiento integer)
    RETURNS TABLE(mensaje character varying, proceso_exitoso boolean, c_repre integer) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
			
	--tbl_tramite_id integer;
	tbl_tomanota_id integer;
	tbl_representante_id integer;
	p_tbl_persona_id integer;
	--ultimoestatus CONSTANT integer := (SELECT i_id_tbl_estatus FROM religiosos."ASOC_TramiteEstatus" WHERE i_id_tbl_tramite = tbl_tramite_id ORDER BY i_id DESC LIMIT 1);
	existRepresentante integer := (select trep.i_id from religiosos."TBL_Representantes" as trep
								 JOIN religiosos."TBL_Persona" as tper on tper.i_id = trep.i_id_tbl_persona 
								 JOIN religiosos."ASOC_TNotaRepre" as asrep on asrep.i_id_tbl_representante = trep.i_id
								 Where 
								 --tper.c_nombre = ENCODE(p_nombre::bytea, 'BASE64') AND 
								 --tper.c_apaterno = ENCODE(p_apellido_p::bytea, 'BASE64') AND
								 --tper.c_amaterno = ENCODE(p_apellido_m::bytea, 'BASE64')
								 --AND 
								asrep.i_id_tbl_tnota = s_id LIMIT 1);
	existRepresentantePersona integer := (select tper.i_id from religiosos."TBL_Representantes" as trep
								 JOIN religiosos."TBL_Persona" as tper on tper.i_id = trep.i_id_tbl_persona 
								 JOIN religiosos."ASOC_TNotaRepre" as asrep on asrep.i_id_tbl_representante = trep.i_id
								 Where 
								 --tper.c_nombre = ENCODE(p_nombre::bytea, 'BASE64') AND 
								 --tper.c_apaterno = ENCODE(p_apellido_p::bytea, 'BASE64') AND
								 --tper.c_amaterno = ENCODE(p_apellido_m::bytea, 'BASE64')
								 asrep.i_id_tbl_tnota = s_id LIMIT 1);
	existAsocTnotaRep integer := (select asrep.i_id from religiosos."TBL_Representantes" as trep
								 JOIN religiosos."TBL_Persona" as tper on tper.i_id = trep.i_id_tbl_persona 
								 JOIN religiosos."ASOC_TNotaRepre" as asrep on asrep.i_id_tbl_representante = trep.i_id
								 Where 
								 --tper.c_nombre = ENCODE(p_nombre::bytea, 'BASE64') AND 
								 --tper.c_apaterno = ENCODE(p_apellido_p::bytea, 'BASE64') AND
								 --tper.c_amaterno = ENCODE(p_apellido_m::bytea, 'BASE64')
								 asrep.i_id_tbl_tnota = s_id LIMIT 1);							 
	BEGIN
			
	   SELECT i_id INTO tbl_tomanota_id FROM religiosos."TBL_TNota"
	   WHERE i_id = s_id limit 1;
		  IF (tbl_tomanota_id is not null) THEN
			 IF((p_id = 0 or p_id is null) AND (existRepresentante is null or existRepresentante = 0)) THEN
				 INSERT INTO religiosos."TBL_Persona"( c_clave,c_nombre,
					c_apaterno, c_amaterno,b_activo)
					VALUES ('PENDIENTE', encode(p_nombre::bytea, 'base64'), 
							encode(p_apellido_p::bytea, 'base64'), 
							encode(p_apellido_m::bytea, 'base64'),
							true)
				 RETURNING i_id INTO p_tbl_persona_id;
							 
				 INSERT INTO religiosos."TBL_Representantes"(
				 	i_id_tbl_persona,
					c_cargo,
					c_organo_g,
					i_id_tbl_reprdocu,
					i_id_cat_poderes)
					VALUES (p_tbl_persona_id, p_cargo, p_organo_g, null, r_cat_poderes) 
				 RETURNING i_id INTO tbl_representante_id;	
							
				 INSERT INTO religiosos."ASOC_TNotaRepre"(
					i_id_tbl_tnota, i_id_tbl_representante,i_id_cat_tmovimiento)
					VALUES (tbl_tomanota_id, tbl_representante_id,r_cat_movimiento);
								
				 IF(t_rep_legal) THEN 
				 	INSERT INTO religiosos."ASOC_REPRE_TREP"(
					   i_id_tbl_representante, i_id_cat_trep)
					   VALUES ( tbl_representante_id, 1);
				 END IF;
				 IF(t_rep_asociado) THEN 
				    INSERT INTO religiosos."ASOC_REPRE_TREP"(
					   i_id_tbl_representante, i_id_cat_trep)
					   VALUES ( tbl_representante_id, 2);
				 END IF;
				 IF(t_ministro_culto) THEN 
					INSERT INTO religiosos."ASOC_REPRE_TREP"(
					   i_id_tbl_representante, i_id_cat_trep)
					   VALUES ( tbl_representante_id, 3);
				 END IF;
				 IF(t_organo_gob) THEN 
					INSERT INTO religiosos."ASOC_REPRE_TREP"(
					   i_id_tbl_representante, i_id_cat_trep)
					   VALUES ( tbl_representante_id, 4);
				 END IF;

				 --INSERT INTO religiosos."TBL_Representantes"(
				 	--i_id_tbl_persona, c_cargo, c_organo_g, 
					--i_id_tbl_reprdocu)
					--VALUES ( p_tbl_persona_id, p_cargo, p_organo_g, null) 
				 --RETURNING i_id INTO tbl_representante_id;	
							
				 RETURN QUERY SELECT 
					CAST('La información se ha cargado de forma exitosa.'  as varchar) AS mensaje,
						(true) AS seProcesoExiosamente,
						tbl_representante_id AS c_repre;
			 ELSE
			 	IF(p_id > 0) THEN
					SELECT i_id,i_id_tbl_persona INTO tbl_representante_id,p_tbl_persona_id FROM religiosos."TBL_Representantes"
					 WHERE i_id_tbl_persona =p_id;
				ELSE
					SELECT i_id,i_id_tbl_persona INTO tbl_representante_id,p_tbl_persona_id FROM religiosos."TBL_Representantes"
					 WHERE i_id_tbl_persona = existRepresentantePersona;
				END IF;
						 
				 IF(tbl_representante_id is null) THEN
					RETURN QUERY SELECT 
					CAST('La información no se ha cargado de forma exitosa.' as varchar) AS mensaje,
					(false) AS seProcesoExiosamente,
					0 AS c_repre;
							
				 ELSE
					 UPDATE religiosos."TBL_Representantes"
					 SET 
					 i_id_cat_poderes=r_cat_poderes
					 WHERE
					 i_id = tbl_representante_id;
					 
					 UPDATE religiosos."ASOC_TNotaRepre"
					 SET 
					 i_id_cat_tmovimiento=r_cat_movimiento
					 WHERE
					 i_id = existAsocTnotaRep;
					 
					 IF(t_rep_legal) THEN 
						INSERT INTO religiosos."ASOC_REPRE_TREP"(
						   i_id_tbl_representante, i_id_cat_trep)
						   VALUES ( tbl_representante_id, 1);
					 ELSE
						 DELETE FROM religiosos."ASOC_REPRE_TREP"
						 WHERE i_id_tbl_representante = tbl_representante_id
						 AND i_id_cat_trep=1;
					 END IF;
								
					 UPDATE religiosos."TBL_Persona"
					 SET c_clave='PENDIENTE',
						 c_nombre=encode(p_nombre::bytea, 'base64'),
						 c_apaterno=encode(p_apellido_p::bytea, 'base64'),
						 c_amaterno = encode(p_apellido_m::bytea, 'base64')
					 WHERE i_id = p_tbl_persona_id;
					
					 IF(t_rep_asociado) THEN 
						INSERT INTO religiosos."ASOC_REPRE_TREP"(
						   i_id_tbl_representante, i_id_cat_trep)
						   VALUES ( tbl_representante_id, 2);
					 ELSE
						 DELETE FROM religiosos."ASOC_REPRE_TREP"
						 WHERE i_id_tbl_representante = tbl_representante_id
						 AND i_id_cat_trep=2;
					 END IF;
								 
					 IF(t_ministro_culto) THEN 
						INSERT INTO religiosos."ASOC_REPRE_TREP"(
						   i_id_tbl_representante, i_id_cat_trep)
						   VALUES ( tbl_representante_id, 3);
					 ELSE
						 DELETE FROM religiosos."ASOC_REPRE_TREP"
						 WHERE i_id_tbl_representante = tbl_representante_id
						 AND i_id_cat_trep=3;
				     END IF;
							 
					 IF(t_organo_gob) THEN 
						INSERT INTO religiosos."ASOC_REPRE_TREP"(
						   i_id_tbl_representante, i_id_cat_trep)
						   VALUES ( tbl_representante_id, 4);
					 ELSE
						 DELETE FROM religiosos."ASOC_REPRE_TREP"
						 WHERE i_id_tbl_representante = tbl_representante_id
						 AND i_id_cat_trep=4;
					 END IF;
					 
					 UPDATE religiosos."TBL_Representantes"
					 SET 
					 c_cargo=p_cargo,
					 c_organo_g=p_organo_g 
					 WHERE
					 i_id = tbl_representante_id;

					 RETURN QUERY SELECT 
					 CAST('La información se ha actualizado de forma exitosa.'  as varchar) AS mensaje,
					 (true) AS seProcesoExiosamente,
					 tbl_representante_id AS c_repre;
							
				 END IF;
						 
			END IF;
		  ELSE 
			RETURN QUERY SELECT 
		    CAST('La información no se ha actualizado de forma exitosa.' as varchar) AS mensaje,
		    (false) AS seProcesoExiosamente,
			0 AS c_repre;
				
		END IF;

	END;
$BODY$;

ALTER FUNCTION religiosos.sp_insertar_representante_legal_toma_nota(integer, integer, character varying, character varying, character varying, boolean, boolean, boolean, boolean, character varying, character varying, integer, integer)
    OWNER TO postgres;
