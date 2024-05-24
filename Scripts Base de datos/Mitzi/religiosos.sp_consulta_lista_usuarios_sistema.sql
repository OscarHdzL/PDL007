-- FUNCTION: religiosos.sp_consulta_lista_usuarios_sistema(integer)

 DROP FUNCTION IF EXISTS religiosos.sp_consulta_lista_usuarios_sistema(integer);

CREATE OR REPLACE FUNCTION religiosos.sp_consulta_lista_usuarios_sistema(
	p_id_ca_perfiles integer)
    RETURNS TABLE(id_usuario integer, usuario text, nombre text, apellido_paterno text, apellido_materno text, nombre_perfil character varying, estatus integer) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
	RETURN QUERY
	
	SELECT 
				tblu.i_id AS id_usuario,
				CONVERT_FROM(DECODE(tblp.c_correo, 'BASE64'), 'UTF-8') AS usuario, 
				CONVERT_FROM(DECODE(tblp.c_nombre, 'BASE64'), 'UTF-8') AS nombre, 
				CONVERT_FROM(DECODE(tblp.c_apaterno, 'BASE64'), 'UTF-8') AS apellido_paterno, 
				CONVERT_FROM(DECODE(tblp.c_amaterno, 'BASE64'), 'UTF-8') AS apellido_materno, 
				cap.nombre AS nombre_perfil,
				tblp.i_activo AS estatus
			FROM religiosos."TBL_Usuario" AS tblu INNER JOIN 
				religiosos."TBL_Persona" AS tblp ON tblu.i_id_tbl_persona = tblp.i_id INNER JOIN
				religiosos."CAT_Perfiles" AS cap ON tblu.i_id_tbl_perfil = cap.i_id
			where tblu.i_id_tbl_perfil not in (1,2,3) AND tblu.i_activo in (0,1);
	
END
$BODY$;

ALTER FUNCTION religiosos.sp_consulta_lista_usuarios_sistema(integer)
    OWNER TO postgres;
