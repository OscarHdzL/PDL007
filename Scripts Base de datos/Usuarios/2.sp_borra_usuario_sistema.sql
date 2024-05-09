-- FUNCTION: religiosos.sp_borra_usuario_sistema(integer, integer)

-- DROP FUNCTION IF EXISTS religiosos.sp_borra_usuario_sistema(integer, integer);

CREATE OR REPLACE FUNCTION religiosos.sp_borra_usuario_sistema(
	p_id_usuario integer,
	p_estatus integer)
    RETURNS TABLE(id_usuario integer, mensaje character varying, proceso_exitoso bit) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE

	perfilUsuario CONSTANT integer := (SELECT i_id_tbl_perfil FROM religiosos."TBL_Usuario" where i_id = p_id_usuario);

	existeUsuario CONSTANT integer := (SELECT i_id_tbl_persona FROM religiosos."TBL_Usuario" WHERE i_id = p_id_usuario LIMIT 1);
	
	usuarioConRPendientes CONSTANT integer := (SELECT COUNT(asoctd.i_id_tbl_tramite) FROM religiosos."ASOC_TraDictaminador" asoctd INNER JOIN 
												 (SELECT i_id_tbl_estatus, i_id_tbl_tramite FROM religiosos."ASOC_TramiteEstatus"
												WHERE i_id in (SELECT MAX (i_id) FROM religiosos."ASOC_TramiteEstatus" GROUP BY i_id_tbl_tramite) 
												order by i_id_tbl_tramite desc) as tramest on asoctd.i_id_tbl_tramite = tramest.i_id_tbl_tramite
											    where asoctd.i_id_tbl_usuariodictam = p_id_usuario and tramest.i_id_tbl_estatus
												   in (10,11,14,15,16,17,18,21,22,23,24,25,26));
	usuarioConTNPendientes CONSTANT integer := (SELECT COUNT(asocttn.i_id_tbl_tramite) FROM religiosos."ASOC_TnotaDictaminador" asoctnd
												INNER JOIN religiosos."ASOC_TramTNota" asocttn ON asoctnd.i_id_tbl_tnota = asocttn.i_id_tbl_tnota
												INNER JOIN (SELECT i_id_tbl_estatus, i_id_tbl_tramite FROM religiosos."ASOC_TramiteEstatus"
												WHERE i_id in (SELECT MAX (i_id) FROM religiosos."ASOC_TramiteEstatus" GROUP BY i_id_tbl_tramite) 
												order by i_id_tbl_tramite desc) as tramest on asocttn.i_id_tbl_tramite = tramest.i_id_tbl_tramite
												where asoctnd.i_id_tbl_usuariodictam = p_id_usuario and tramest.i_id_tbl_estatus in (10,11,14,15,16,17,18,21,22,23,24,25,26));
	usuarioConTransPendientes CONSTANT integer := (SELECT COUNT(asoctd.i_id_tbl_transmision) FROM religiosos."ASOC_TransmisionDictaminador" asoctd
													INNER JOIN religiosos."ASOC_Transmision_Estatus" asocte ON asoctd.i_id_tbl_transmision = asocte.i_id_tbl_transmision
												where asoctd.id_tbl_usuario_dictaminador = p_id_usuario and asocte.i_id_tbl_estatus in (30,31,32));
												
	asignadorConRPendientes CONSTANT integer := (SELECT DISTINCT COUNT(tblt.i_id) AS id_registro
												FROM religiosos."TBL_Tramite" AS tblt 
												LEFT JOIN (SELECT i_id_tbl_estatus, i_id_tbl_tramite FROM religiosos."ASOC_TramiteEstatus"
															WHERE i_id in (SELECT MAX (i_id) FROM religiosos."ASOC_TramiteEstatus" GROUP BY i_id_tbl_tramite) 
															order by i_id_tbl_tramite desc) as tramest on tblt.i_id = tramest.i_id_tbl_tramite
												LEFT JOIN religiosos."CAT_Estatus" AS cats on tramest.i_id_tbl_estatus = cats.i_id
												WHERE cats.i_id in (9,10,11,14,15,16,17,18));
	BEGIN	
		IF existeUsuario is not null THEN
		
			IF  p_estatus = 0 THEN
			
				UPDATE religiosos."TBL_Persona" SET i_activo = p_estatus
				WHERE i_id = existeUsuario;
				
				UPDATE religiosos."TBL_Usuario" SET i_activo = p_estatus
				WHERE i_id = p_id_usuario;
				
				UPDATE religiosos."TBL_Persona" SET b_activo = (p_estatus :: boolean)
				WHERE i_id = existeUsuario;
				
				UPDATE religiosos."TBL_Usuario" SET b_activo = (p_estatus :: boolean)
				WHERE i_id = p_id_usuario;
				
				RETURN QUERY SELECT 
					 p_id_usuario as id_usuario,
					 CAST('Usuario suspendido' as varchar) AS mensaje,
					 (1::bit) AS seProcesoExiosamente;
			
			ELSEIF p_estatus = 1 THEN
			
				UPDATE religiosos."TBL_Persona" SET i_activo = p_estatus
				WHERE i_id = existeUsuario;
				
				UPDATE religiosos."TBL_Usuario" SET i_activo = p_estatus
				WHERE i_id = p_id_usuario;
				
				UPDATE religiosos."TBL_Persona" SET b_activo = (p_estatus :: boolean)
				WHERE i_id = existeUsuario;
				
				UPDATE religiosos."TBL_Usuario" SET b_activo = (p_estatus :: boolean)
				WHERE i_id = p_id_usuario;
				
				RETURN QUERY SELECT 
					 p_id_usuario as id_usuario,
					 CAST('Usuario restablecido' as varchar) AS mensaje,
					 (1::bit) AS seProcesoExiosamente;
				 
			ELSEIF p_estatus = 2 THEN
				IF(perfilUsuario = 4 OR perfilUsuario = 9 OR perfilUsuario = 10 OR perfilUsuario = 12 )THEN
					IF usuarioConRPendientes = 0 AND usuarioConTNPendientes = 0 AND usuarioConTransPendientes = 0 
					THEN
						UPDATE religiosos."TBL_Persona" SET i_activo = p_estatus
						WHERE i_id = existeUsuario;

						UPDATE religiosos."TBL_Usuario" SET i_activo = p_estatus
						WHERE i_id = p_id_usuario;

						UPDATE religiosos."TBL_Persona" SET b_activo = (0 :: boolean)
						WHERE i_id = existeUsuario;

						UPDATE religiosos."TBL_Usuario" SET b_activo = (0 :: boolean)
						WHERE i_id = p_id_usuario;

						RETURN QUERY SELECT
						p_id_usuario as id_usuario,
						 CAST('Usuario eliminado' as varchar) AS mensaje,
						 (1::bit) AS seProcesoExiosamente;
					ELSE
						RETURN QUERY SELECT
						p_id_usuario as id_usuario,
						CAST('No se puede eliminar el usuario, tiene al menos 1 trámite pendiente por dictaminar' as varchar) AS mensaje,
						(0::bit) AS seProcesoExiosamente;
					END IF;
				END IF;
				
				IF (perfilUsuario = 5 OR perfilUsuario = 7 OR perfilUsuario = 8 OR perfilUsuario = 11) THEN
				
						RETURN QUERY SELECT
						p_id_usuario as id_usuario,
						CAST('No se puede eliminar el usuario, tiene al menos 1 trámite pendiente por asignar' as varchar) AS mensaje,
						(0::bit) AS seProcesoExiosamente;
						
				END IF;
				
				IF (perfilUsuario = 6) THEN
					UPDATE religiosos."TBL_Persona" SET i_activo = p_estatus
					WHERE i_id = existeUsuario;

					UPDATE religiosos."TBL_Usuario" SET i_activo = p_estatus
					WHERE i_id = p_id_usuario;

					UPDATE religiosos."TBL_Persona" SET b_activo = (p_estatus :: boolean)
					WHERE i_id = existeUsuario;

					UPDATE religiosos."TBL_Usuario" SET b_activo = (p_estatus :: boolean)
					WHERE i_id = p_id_usuario;

					RETURN QUERY SELECT 
						 p_id_usuario as id_usuario,
						 CAST('Usuario eliminado' as varchar) AS mensaje,
						 (1::bit) AS seProcesoExiosamente;
						
				END IF;
		END IF;
		ELSE 
			-- Usuario no existe
			RETURN QUERY SELECT 
				 0 as id_usuario,
				 CAST('Usuario no existe' as varchar) AS mensaje,
				 (0::bit) AS seProcesoExiosamente;
					
		END IF;
END
$BODY$;