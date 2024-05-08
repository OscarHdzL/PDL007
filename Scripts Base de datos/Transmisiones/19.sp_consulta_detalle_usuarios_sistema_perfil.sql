CREATE OR REPLACE FUNCTION religiosos.sp_consulta_detalle_usuarios_sistema_perfil(
	p_id_usuario integer)
    RETURNS TABLE(id_usuario integer, id_persona integer, id_perfil integer, usuario text, nombre text, apellido_paterno text, apellido_materno text, telefono_movil text, nombre_perfil character varying, estatus boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
		RETURN Query 
			SELECT 
				tblu.i_id AS id_usuario,
				tblp.i_id AS id_persona,
				cap.i_id AS id_perfil,
				CONVERT_FROM(DECODE(tblp.c_correo, 'BASE64'), 'UTF-8') AS usuario, 
				CONVERT_FROM(DECODE(tblp.c_nombre, 'BASE64'), 'UTF-8') AS nombre, 
				CONVERT_FROM(DECODE(tblp.c_apaterno, 'BASE64'), 'UTF-8') AS apellido_paterno, 
				CONVERT_FROM(DECODE(tblp.c_amaterno, 'BASE64'), 'UTF-8') AS apellido_materno, 
-- 				CONVERT_FROM(DECODE(tblp.c_telefono, 'BASE64'), 'UTF-8') AS telefono_movil, 
                '',
				cap.nombre AS nombre_perfil,
				tblp.b_activo AS estatus
			FROM religiosos."TBL_Usuario" AS tblu INNER JOIN 
				religiosos."TBL_Persona" AS tblp ON tblu.i_id_tbl_persona = tblp.i_id INNER JOIN
				religiosos."CAT_Perfiles" AS cap ON tblu.i_id_tbl_perfil = cap.i_id
			where tblu.i_id_tbl_perfil = p_id_usuario;
	END
$BODY$;