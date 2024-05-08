DROP FUNCTION IF EXISTS religiosos.sp_consulta_archivo(integer, integer);

CREATE OR REPLACE FUNCTION religiosos.sp_consulta_archivo(
	id_generico integer,
	id_archivo_tramite integer)
    RETURNS TABLE(id integer, ruta character varying, proceso_exitoso boolean, ext character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE existeArchivoTramite CONSTANT integer := (SELECT ta.i_id 
													FROM religiosos."ASOC_Tramite_Archivos" asoc INNER JOIN religiosos."TBL_Archivo" ta ON asoc.i_id_tbl_archivo = ta.i_id
												   WHERE asoc.i_id_tbl_tramite = id_generico 
													 AND ta.i_id_tbl_archtram = id_archivo_tramite 
													 AND ta.i_estatus = 1 -- ACTIVO
												   LIMIT 1);
		existeArchivoRepresentante CONSTANT integer := (SELECT ta.i_id 
													FROM religiosos."ASOC_RepreArchivo" asoc INNER JOIN religiosos."TBL_Archivo" ta ON asoc.i_id_tbl_archivo = ta.i_id
												   WHERE asoc.i_id_tbl_representante = id_generico 
													 AND ta.i_id_tbl_archtram = id_archivo_tramite 
													 AND ta.i_estatus = 1 -- ACTIVO
												   LIMIT 1);
		existeArchivoCnotarioarr CONSTANT integer := (SELECT ta.i_id
													  FROM religiosos."TBL_Cnotorioarr" asoc INNER JOIN religiosos."TBL_Archivo" ta ON asoc.i_id_tbl_archivo = ta.i_id
													 										 INNER JOIN religiosos."TBL_Tramite" t ON asoc.i_id = t.i_id_tbl_cnotorioarr
													 WHERE t.i_id = id_generico
													   AND ta.i_id_tbl_archtram = id_archivo_tramite 
													   AND ta.i_estatus = 1 -- ACTIVO
													 LIMIT 1);
		existeArchivo1Convenioex CONSTANT integer := (SELECT ta.i_id
													  FROM religiosos."TBL_Convenioex" asoc INNER JOIN religiosos."TBL_Archivo" ta ON asoc.i_id_tbl_archivo_1 = ta.i_id
													 										INNER JOIN religiosos."TBL_Tramite" t ON asoc.i_id = t.i_id_tbl_convenioex
													 WHERE t.i_id = id_generico
													   AND ta.i_id_tbl_archtram = id_archivo_tramite 
													   AND ta.i_estatus = 1 -- ACTIVO
													 LIMIT 1);
		existeArchivo2Convenioex CONSTANT integer := (SELECT ta.i_id
													  FROM religiosos."TBL_Convenioex" asoc INNER JOIN religiosos."TBL_Archivo" ta ON asoc.i_id_tbl_archivo_2 = ta.i_id
													 										INNER JOIN religiosos."TBL_Tramite" t ON asoc.i_id = t.i_id_tbl_convenioex
													 WHERE t.i_id = id_generico
													   AND ta.i_id_tbl_archtram = id_archivo_tramite 
													   AND ta.i_estatus = 1 -- ACTIVO
													 LIMIT 1);
		existeArchivoTomaNota CONSTANT integer := (SELECT ta.i_id 
													FROM religiosos."ASOC_TNotaArchivo" asoc INNER JOIN religiosos."TBL_Archivo" ta ON asoc.i_id_tbl_archivo = ta.i_id
												   WHERE asoc.i_id_tbl_tnota = id_generico 
													 AND ta.i_id_tbl_archtram = id_archivo_tramite 
													 AND ta.i_estatus = 1 -- ACTIVO
												   LIMIT 1);
		existeArchivoTransmision CONSTANT integer := (SELECT ta.i_id 
													FROM religiosos."ASOC_Transmision_Archivos" asoc 
													INNER JOIN religiosos."TBL_Archivo" ta ON asoc.i_id_tbl_archivo = ta.i_id
												   WHERE asoc.i_id_tbl_transmision = id_generico 
													 AND ta.i_id_tbl_archtram = id_archivo_tramite 
													 AND ta.i_estatus = 1 -- ACTIVO
												   LIMIT 1);
-- 29/04/2024 LIM DESCOMENTAR PARA DECLARATORIA												   
--		existeArchivoDeclaratoria CONSTANT integer := (SELECT ta.i_id 
--													FROM religiosos."ASOC_Declaratoria_Archivos" asoc 
--													INNER JOIN religiosos."TBL_Archivo" ta ON asoc.i_id_tbl_archivo = ta.i_id
--										   			WHERE asoc.i_id_tbl_declaratoria = id_generico 
--											 		AND ta.i_id_tbl_archtram = id_archivo_tramite 
--											 		AND ta.i_estatus = 1 -- ACTIVO											   
--											 		LIMIT 1);
	
	BEGIN
	
		-- Dependiendo del tipo de documento consultamos en la tabla correspondiente

		-- TBL_Tramite
		-- El id_generico corresponde a TBL_Tramite.i_id
		IF (id_archivo_tramite IN(1,2,3,4,5,6,13,15,16))
		THEN
		
			IF (existeArchivoTramite) is not null 
			THEN
				RETURN QUERY SELECT
					i_id as id,
					CAST(c_ruta AS CHARACTER VARYING) AS ruta,
					true as proceso_exitoso,
					CAST('Archivo cargado correctamente.' AS CHARACTER VARYING) AS ext
				FROM religiosos."TBL_Archivo"
				WHERE i_id = existeArchivoTramite;
			END IF;
		
		END IF;
		
		--CONSULTA ARCHIVOS RELACIONADOS CON UNA TRANSMISION
		IF (id_archivo_tramite IN(27,28,29)) THEN
		
			IF (existeArchivoTransmision) is not null THEN
				RETURN QUERY 
					SELECT i_id as id,
					CAST(c_ruta AS CHARACTER VARYING) AS ruta,
					true as proceso_exitoso,
					CAST('Archivo cargado correctamente.' AS CHARACTER VARYING) AS ext
				FROM religiosos."TBL_Archivo"
				WHERE i_id = existeArchivoTransmision;
			END IF;
		
		END IF;

		-- TBL_Cnotorioarr
		-- El id_generico corresponde a TBL_Tramite.i_id relacionado
		IF (id_archivo_tramite IN(10))
		THEN
		
			IF (existeArchivoCnotarioarr) is not null 
			THEN
				RETURN QUERY SELECT
					i_id as id,
					CAST(c_ruta AS CHARACTER VARYING) AS ruta,
					true as proceso_exitoso,
					CAST('Archivo cargado correctamente.' AS CHARACTER VARYING) AS ext
				FROM religiosos."TBL_Archivo"
				WHERE i_id = existeArchivoCnotarioarr;
			END IF;
		
		END IF;

		-- TBL_Representante
		-- El id_generico corresponde a TBL_Representante.i_id
		IF (id_archivo_tramite IN(7,8,9) )
		THEN
		
			IF (existeArchivoRepresentante) is not null 
			THEN
				RETURN QUERY SELECT
					i_id as id,
					CAST(c_ruta AS CHARACTER VARYING) AS ruta,
					true as proceso_exitoso,
					CAST('Archivo cargado correctamente.' AS CHARACTER VARYING) AS ext
				FROM religiosos."TBL_Archivo"
				WHERE i_id = existeArchivoRepresentante;
			END IF;
		
		END IF;
		
		-- TBL_Convenioex
		-- El id_generico corresponde a TBL_Convenioex
		IF (id_archivo_tramite IN(11, 12) )
		THEN
		
			IF (id_archivo_tramite = 11)
			THEN
				IF (existeArchivo1Convenioex) is not null
				THEN
					RETURN QUERY SELECT
						i_id as id,
						CAST(c_ruta AS CHARACTER VARYING) AS ruta,
						true as proceso_exitoso,
						CAST('Archivo cargado correctamente.' AS CHARACTER VARYING) AS ext
					FROM religiosos."TBL_Archivo"
					WHERE i_id = existeArchivo1Convenioex;
				END IF;
			END IF;
			
			IF id_archivo_tramite = 12
			THEN
				IF (existeArchivo2Convenioex) is not null
				THEN
					RETURN QUERY SELECT
						i_id as id,
						CAST(c_ruta AS CHARACTER VARYING) AS ruta,
						true as proceso_exitoso,
						CAST('Archivo cargado correctamente.' AS CHARACTER VARYING) AS ext
					FROM religiosos."TBL_Archivo"
					WHERE i_id = existeArchivo2Convenioex;
				END IF;
			END IF;
		
		END IF;
		
		-- TBL_TNota
		-- El id_generico corresponde a TBL_TNota.i_id
		IF (id_archivo_tramite IN(14,17,18,19,20,21,22,23,24,25))
		THEN
		
			IF (existeArchivoTomaNota) is not null 
			THEN
				RETURN QUERY SELECT
					i_id as id,
					CAST(c_ruta AS CHARACTER VARYING) AS ruta,
					true as proceso_exitoso,
					CAST('Archivo cargado correctamente.' AS CHARACTER VARYING) AS ext
				FROM religiosos."TBL_Archivo"
				WHERE i_id = existeArchivoTomaNota;
			END IF;
		
		END IF;
		
		IF (id_archivo_tramite IN(30, 31, 32, 33, 34)) THEN
			IF (existeArchivoDeclaratoria) is not null THEN
				RETURN QUERY 
					SELECT i_id as id,
					CAST(c_ruta AS CHARACTER VARYING) AS ruta,
					true as proceso_exitoso,
					CAST('Archivo cargado correctamente.' AS CHARACTER VARYING) AS ext
				FROM religiosos."TBL_Archivo"
				WHERE i_id = existeArchivoDeclaratoria;
			END IF;
		
		END IF;

		--RETURN QUERY SELECT 
		--	0 as id,
		--	CAST('' AS CHARACTER VARYING) AS ruta,
		--	false as proceso_exitoso,
		--	CAST('No se encontró el archivo.' AS CHARACTER VARYING) AS mensaje;

	END;
$BODY$;