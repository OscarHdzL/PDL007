
CREATE OR REPLACE FUNCTION religiosos.sp_consulta_lista_catalogos_cnotarioarr(
	p_tiposolicitud integer)
    RETURNS TABLE(c_id integer, c_nombre_n character varying, c_descripcion_n character varying, c_f_inic_vig date, c_f_fin_vig date, c_activo boolean, tipo_escrito integer) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
	if (p_tipoSolicitud > 0)
		then
		RETURN QUERY SELECT 
				i_id AS c_id,
				c_nombre AS c_nombre_n, 
				c_descripcion AS c_descripcion_n, 
				d_finic_vig AS c_f_inic_vig, 
				d_ffin_vig AS c_f_fin_vig, 
				b_activo AS c_activo,
				i_tipo_escrito AS tipo_escrito
			FROM religiosos."CAT_Cnotorioarr"
			where i_tipo_escrito = p_tipoSolicitud;
	else
	RETURN QUERY SELECT 
				i_id AS c_id,
				c_nombre AS c_nombre_n, 
				c_descripcion AS c_descripcion_n, 
				d_finic_vig AS c_f_inic_vig, 
				d_ffin_vig AS c_f_fin_vig, 
				b_activo AS c_activo,
				i_tipo_escrito AS tipo_escrito
			FROM religiosos."CAT_Cnotorioarr";
	

	END IF;
END
$BODY$;