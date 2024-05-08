-- DROP FUNCTION religiosos.sp_insertar_archivo(int4, text, int4);

CREATE OR REPLACE FUNCTION religiosos.sp_insertar_archivo(id_generico integer, archivo text, id_archivo_tramite integer)
 RETURNS TABLE(id integer, ruta character varying, proceso_exitoso boolean, ext character varying)
 LANGUAGE plpgsql
AS $function$
DECLARE id_tbl_archivo_insertado integer;
		id_tbl_cnotorioarr_insertado integer;
		id_tbl_convenioex_insertado integer;
		aux_extension character varying;
		existeArchivoTramite CONSTANT integer := (SELECT ta.i_id 
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
    	 id_old_tbl_cat_notorioarr CONSTANT integer := (SELECT asoc.i_id_cat_cnotorioarr
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
		existeArchivoDeclaratoria CONSTANT integer := (SELECT ta.i_id 
													FROM religiosos."ASOC_Declaratoria_Archivos" asoc 
													INNER JOIN religiosos."TBL_Archivo" ta ON asoc.i_id_tbl_archivo = ta.i_id
										   			WHERE asoc.i_id_tbl_declaratoria = id_generico 
											 		AND ta.i_id_tbl_archtram = id_archivo_tramite 
											 		AND ta.i_estatus = 1 -- ACTIVO											   
											 		LIMIT 1);
	
	BEGIN
	
		IF(id_archivo_tramite  = 30) THEN
			aux_extension = 'jpg';
		ELSE
			aux_extension = 'pdf';
		END IF;
	
		-- Generamos el nuevo archivo
		INSERT INTO religiosos."TBL_Archivo"(c_nombre, c_extension, c_ruta, i_id_tbl_archtram, i_estatus) 
		SELECT
			nombre,
			aux_extension,
			archivo,
			i_id,
			1 -- ACTIVO
		 FROM religiosos."CAT_Archivo_Tramite" 
		WHERE i_id = id_archivo_tramite
		RETURNING i_id INTO id_tbl_archivo_insertado;
	
	
		-- Dependiendo del tipo de documento insertamos en la tabla correspondiente

		-- TBL_Tramite
		-- El id_generico corresponde a TBL_Tramite.i_id
		IF (id_archivo_tramite IN(1,2,3,4,5,6,13,15,16))
		THEN
			IF (existeArchivoTramite) is not null 
			THEN
				-- Si el archivo ya existe, le cambiamos el estatus
				UPDATE religiosos."TBL_Archivo"
				   SET i_estatus = 2 
				 WHERE i_id = existeArchivoTramite;
				
				UPDATE religiosos."ASOC_Tramite_Archivos"
				   SET i_id_tbl_archivo = id_tbl_archivo_insertado 
				 WHERE i_id_tbl_tramite = id_generico 
				   AND i_id_tbl_archivo = existeArchivoTramite;
			ELSE
				INSERT INTO religiosos."ASOC_Tramite_Archivos"(i_id_tbl_tramite, i_id_tbl_archivo)
				VALUES (id_generico, id_tbl_archivo_insertado);
			END IF;
		
		END IF;
		
		--INSERTAR RELACION DE ARCHIVOS CON TRANSMISIONES
		IF (id_archivo_tramite IN(27,28,29)) THEN
		
			IF (existeArchivoTransmision) is not null THEN
				-- Si el archivo ya existe, le cambiamos el estatus
				UPDATE religiosos."TBL_Archivo"
				   SET i_estatus = 2 
				 WHERE i_id = existeArchivoTransmision;
				
				INSERT INTO religiosos."ASOC_Transmision_Archivos"(i_id_tbl_transmision, i_id_tbl_archivo)
				VALUES (id_generico, id_tbl_archivo_insertado);
                
			ELSE
				INSERT INTO religiosos."ASOC_Transmision_Archivos"(i_id_tbl_transmision, i_id_tbl_archivo)
				VALUES (id_generico, id_tbl_archivo_insertado);
			END IF;
		
		END IF;

		-- TBL_Cnotorioarr
		-- El id_generico corresponde a TBL_Tramite.i_id relacionado
		IF (id_archivo_tramite IN(10))
		THEN
		
			IF (existeArchivoCnotarioarr) is not null 
			THEN
				-- Si el archivo ya existe, le cambiamos el estatus
				UPDATE religiosos."TBL_Archivo"
				   SET i_estatus = 2 
				 WHERE i_id = existeArchivoCnotarioarr;
				
				INSERT INTO religiosos."TBL_Cnotorioarr"(i_id_cat_cnotorioarr, 
														 i_id_tbl_archivo, i_estatus)
				VALUES (id_old_tbl_cat_notorioarr, id_tbl_archivo_insertado, 1)
				RETURNING i_id INTO id_tbl_cnotorioarr_insertado;
				UPDATE religiosos."TBL_Tramite" SET
				i_id_tbl_cnotorioarr = id_tbl_cnotorioarr_insertado
				WHERE i_id = id_generico;
			ELSE
				-- Insertamos un valor inicial pero se actualizará al guardar la pantalla
				INSERT INTO religiosos."TBL_Cnotorioarr"(i_id_cat_cnotorioarr, i_id_tbl_archivo, i_estatus)
				VALUES (NULL, id_tbl_archivo_insertado, 1)
				RETURNING i_id INTO id_tbl_cnotorioarr_insertado;
				
				UPDATE religiosos."TBL_Tramite"
				   SET i_id_tbl_cnotorioarr = id_tbl_cnotorioarr_insertado
				 WHERE i_id = id_generico;

			END IF;
		
		END IF;

		-- TBL_Representante
		-- El id_generico corresponde a TBL_Representante.i_id
		IF (id_archivo_tramite IN(7,8,9) )
		THEN
		
			IF (existeArchivoRepresentante) is not null 
			THEN
				-- Si el archivo ya existe, le cambiamos el estatus
				UPDATE religiosos."TBL_Archivo"
				   SET i_estatus = 2 
				 WHERE i_id = existeArchivoRepresentante;
				
				UPDATE religiosos."ASOC_RepreArchivo"
				   SET i_id_tbl_archivo = id_tbl_archivo_insertado 
				 WHERE i_id_tbl_representante = id_generico 
				   AND i_id_tbl_archivo = existeArchivoRepresentante;
			ELSE
				INSERT INTO religiosos."ASOC_RepreArchivo"(i_id_tbl_representante, i_id_tbl_archivo)
				VALUES (id_generico, id_tbl_archivo_insertado);
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
					-- Si el archivo ya existe, le cambiamos el estatus
					UPDATE religiosos."TBL_Archivo"
					   SET i_estatus = 2 
					 WHERE i_id = existeArchivo1Convenioex;

					UPDATE religiosos."TBL_Convenioex"
					   SET i_id_tbl_archivo_1 = id_tbl_archivo_insertado 
					 WHERE i_id_tbl_archivo_1 = existeArchivo1Convenioex;
				ELSE
					id_tbl_convenioex_insertado := (SELECT i_id_tbl_convenioex FROM religiosos."TBL_Tramite" WHERE i_id = id_generico);
				
					IF (id_tbl_convenioex_insertado) is not null
					THEN
						-- Si ya existe un registro, actualizamos
						UPDATE religiosos."TBL_Convenioex"
						   SET i_id_tbl_archivo_1 = id_tbl_archivo_insertado 
						WHERE i_id = id_tbl_convenioex_insertado;
					ELSE
						-- Insertamos un valor inicial pero se actualizará al guardar la pantalla
						INSERT INTO religiosos."TBL_Convenioex"(i_id_tbl_archivo_1, i_id_tbl_archivo_2)
						VALUES (id_tbl_archivo_insertado, null)
						RETURNING i_id INTO id_tbl_convenioex_insertado;

						UPDATE religiosos."TBL_Tramite"
						   SET i_id_tbl_convenioex = id_tbl_convenioex_insertado
						 WHERE i_id = id_generico;
					END IF;
				END IF;
			END IF;
			
			IF id_archivo_tramite = 12
			THEN
				IF (existeArchivo2Convenioex) is not null
				THEN
					-- Si el archivo ya existe, le cambiamos el estatus
					UPDATE religiosos."TBL_Archivo"
					   SET i_estatus = 2 
					 WHERE i_id = existeArchivo2Convenioex;

					UPDATE religiosos."TBL_Convenioex"
					   SET i_id_tbl_archivo_2 = id_tbl_archivo_insertado 
					 WHERE i_id_tbl_archivo_2 = existeArchivo2Convenioex;
				ELSE
					id_tbl_convenioex_insertado := (SELECT i_id_tbl_convenioex FROM religiosos."TBL_Tramite" WHERE i_id = id_generico);
				
					IF (id_tbl_convenioex_insertado) is not null
					THEN
						-- Si ya existe un registro, actualizamos
						UPDATE religiosos."TBL_Convenioex"
						   SET i_id_tbl_archivo_2 = id_tbl_archivo_insertado 
						WHERE i_id = id_tbl_convenioex_insertado;
					ELSE
						-- Insertamos un valor inicial pero se actualizará al guardar la pantalla
						INSERT INTO religiosos."TBL_Convenioex"(i_id_tbl_archivo_1, i_id_tbl_archivo_2)
						VALUES (null, id_tbl_archivo_insertado)
						RETURNING i_id INTO id_tbl_convenioex_insertado;

						UPDATE religiosos."TBL_Tramite"
						   SET i_id_tbl_convenioex = id_tbl_convenioex_insertado
						 WHERE i_id = id_generico;
					END IF;
				END IF;
			END IF;
		
		END IF;
		
		-- TBL_TNota
		-- El id_generico corresponde a TBL_TNota.i_id
		IF (id_archivo_tramite IN(14,17,18,19,20,21,22,23,24,25))
		THEN
		
			IF (existeArchivoTomaNota) is not null 
			THEN
				-- Si el archivo ya existe, le cambiamos el estatus
				UPDATE religiosos."TBL_Archivo"
				   SET i_estatus = 2 
				 WHERE i_id = existeArchivoTomaNota;
				
				UPDATE religiosos."ASOC_TNotaArchivo"
				   SET i_id_tbl_archivo = id_tbl_archivo_insertado 
				 WHERE i_id_tbl_tnota = id_generico 
				   AND i_id_tbl_archivo = existeArchivoTomaNota;
			ELSE
				INSERT INTO religiosos."ASOC_TNotaArchivo"(i_id_tbl_tnota, i_id_tbl_archivo)
				VALUES (id_generico, id_tbl_archivo_insertado);
			END IF;
		
		END IF;
		
		--ARCHIVOS DECLARATORIA
		IF(id_archivo_tramite IN(30, 31, 32, 33, 34)) THEN
		
			IF(existeArchivoDeclaratoria IS NOT NULL) THEN
			
				UPDATE religiosos."TBL_Archivo"
				   SET i_estatus = 2 
				 WHERE i_id = existeArchivoDeclaratoria;
				
				UPDATE religiosos."ASOC_Declaratoria_Archivos"
				   SET i_id_tbl_archivo = id_tbl_archivo_insertado 
				 WHERE i_id_tbl_declaratoria = id_generico 
				   AND i_id_tbl_archivo = existeArchivoDeclaratoria;
				   
			ELSE
			
				INSERT INTO religiosos."ASOC_Declaratoria_Archivos"(i_id_tbl_declaratoria, i_id_tbl_archivo)
				VALUES (id_generico, id_tbl_archivo_insertado);
				
			END IF;
		
		END IF;

		RETURN QUERY SELECT 
			id_tbl_archivo_insertado as id,
			CAST(archivo AS CHARACTER VARYING) AS ruta,
			true as proceso_exitoso,
			CAST('Archivo creado correctamente.' AS CHARACTER VARYING) AS ext;

	END;
$function$
;
