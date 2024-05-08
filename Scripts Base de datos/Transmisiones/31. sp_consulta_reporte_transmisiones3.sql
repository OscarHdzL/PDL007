DROP FUNCTION IF EXISTS religiosos.sp_consulta_reporte_transmisiones3(character varying, integer, character varying, character varying, date, date);

CREATE OR REPLACE FUNCTION religiosos.sp_consulta_reporte_transmisiones3(
	p_medio_comunicacion character varying,
	p_estatus_transmision integer,
	p_denominacion character varying,
	p_acto_religioso character varying,
	p_fecha_inicio date,
	p_fecha_fin date)
    RETURNS TABLE(transmision_id integer, transmision_fecha_solicitud date, registro_nregistro character varying, registro_denominacion character varying, estatus_nombre character varying, representante_nombre_completo character varying, transmision_no_oficio character varying, transmision_fecha_autorizacion date, no_transmisiones bigint, no_dias bigint, horarios_transmision bigint, n_medios_transmision bigint, total_transmisiones bigint) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
    tipo_medio_comunicacion bit := null;

BEGIN
    IF p_medio_comunicacion is not null
        THEN
        IF p_medio_comunicacion = 'Radio'
            THEN
            tipo_medio_comunicacion := 0::bit;
            ELSE
            tipo_medio_comunicacion := 1::bit;
        END IF;
    END IF;
	RETURN QUERY
	
        select 
            resultado.transmision_id,
            resultado.transmision_fecha_solicitud,
            resultado.registro_nregistro,
            resultado.registro_denominacion,
            resultado.estatus_nombre,
            resultado.representante_nombre_completo,
            resultado.transmision_no_oficio,
            resultado.transmision_fecha_autorizacion,
            resultado.no_transmisiones,
            resultado.no_dias,
            resultado.horarios_transmision,
            resultado.n_medios_transmision,            
			COALESCE( (resultado.no_transmisiones * resultado.no_dias * resultado.horarios_transmision * resultado.n_medios_transmision)  , 0)::bigint as total_transmisiones
            from (
            select 
                    transmision.i_id as transmision_id,
                    transmision.d_fecha_solicitud as transmision_fecha_solicitud,        
                    transmision.c_numero_sgar as registro_nregistro,
                    transmision.c_denominacion as registro_denominacion,
                    c_estatus.nombre as estatus_nombre,
                    transmision.c_representante as representante_nombre_completo,                    
                    transmision.c_oficio as transmision_no_oficio,
                    transmision.d_fecha_autorizacion as transmision_fecha_autorizacion,
                    (select 
                        count( distinct actos.i_id)
                        from religiosos."TBL_Actos_Religiosos" as actos
                        inner join religiosos."TBL_Actos_Fechas" as actos_fechas
                        on actos.i_id = actos_fechas.i_id_acto_religioso
                    where actos.i_id_tbl_transmision = transmision.i_id
                    and ( (p_acto_religioso is null) or (actos.c_nombre ILIKE '%' || p_acto_religioso || '%' )  ) )::bigint as no_transmisiones,
                    (select 
                        SUM(
                    CASE
                            WHEN actos_fechas.i_id_cat_periodo is null AND actos_fechas.d_fecha_fin is null
                            THEN 1 
                            WHEN actos_fechas.i_id_cat_periodo is null AND actos_fechas.d_fecha_fin is not null
                            THEN (actos_fechas.d_fecha_fin - actos_fechas.d_fecha_inicio) + 1
                        END
                        )
                        from religiosos."TBL_Actos_Religiosos" as actos
                        inner join religiosos."TBL_Actos_Fechas" as actos_fechas
                        on actos.i_id = actos_fechas.i_id_acto_religioso
                    where actos.i_id_tbl_transmision = transmision.i_id
                    and ( (p_acto_religioso is null) or (actos.c_nombre ILIKE '%' || p_acto_religioso || '%' )  ) )::bigint as no_dias,
                    (select 
                        count( distinct actos_fechas.t_inicio)
                        from religiosos."TBL_Actos_Religiosos" as actos
                        inner join religiosos."TBL_Actos_Fechas" as actos_fechas
                        on actos.i_id = actos_fechas.i_id_acto_religioso
                    where actos.i_id_tbl_transmision = transmision.i_id
                    and ( (p_acto_religioso is null) or (actos.c_nombre ILIKE '%' || p_acto_religioso || '%' )  ) )::bigint as horarios_transmision,
                    (select 
                    count(medio_transmision.i_id) 
                    from religiosos."TBL_Actos_Religiosos" as actos                    
                    left join religiosos."CAT_Medios_Transmision" as medio_transmision
                    on medio_transmision.i_id_tbl_acto = actos.i_id
                    where actos.i_id_tbl_transmision = transmision.i_id
                    and ( (p_acto_religioso is null) or (actos.c_nombre ILIKE '%' || p_acto_religioso || '%' )  )
                    and ( (tipo_medio_comunicacion is null) or (tipo_medio_comunicacion = medio_transmision.b_televisora ) )
                    )::bigint as n_medios_transmision
                        
                    from religiosos."TBL_Transmision" as transmision        
                    --left join religiosos."TBL_Tramite" as tramite on tramite.i_id = transmision.i_id_tbl_tramite
                    left join
                    religiosos."ASOC_Transmision_Estatus" as asoc_e_trans
                    on asoc_e_trans.i_id_tbl_transmision = transmision.i_id
                    left join
                    religiosos."CAT_Estatus" as c_estatus
                    on asoc_e_trans.i_id_tbl_estatus = c_estatus.i_id      
                    where c_estatus.i_id in (30,31,32,33,34,38)
                    and ( (p_denominacion is null) or (transmision.c_denominacion ILIKE '%' || p_denominacion || '%' ) )
                    --and ( (p_estatus_transmision is null) or ( c_estatus.i_id = p_estatus_transmision and asoc_e_trans.i_id_tbl_estatus = ) )
				
				--CORRECCION PARA DUPLICADOS POR EL ESTATUS
				and ( --(p_estatus_transmision is null) or 
						( (asoc_e_trans.i_id_tbl_estatus = 
														   (SELECT i_id_tbl_estatus FROM religiosos."ASOC_Transmision_Estatus"
									  WHERE i_id_tbl_transmision = transmision.i_id ORDER BY i_id DESC limit 1 )) 
														  --and asoc_e_trans.i_id_tbl_estatus = p_estatus_transmision 
														 )
					)
				and (p_estatus_transmision is null or (asoc_e_trans.i_id_tbl_estatus = p_estatus_transmision))
                    and ( (p_fecha_inicio is null) or ( p_fecha_inicio <= transmision.d_fecha_solicitud ) )
                    and ( (p_fecha_fin is null) or (p_fecha_fin >= transmision.d_fecha_solicitud) )
                    ) as resultado;	

END
$BODY$;
