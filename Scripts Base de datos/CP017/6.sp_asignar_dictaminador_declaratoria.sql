-- FUNCTION: religiosos.sp_asignar_dictaminador_declaratoria(integer, integer, integer)

-- DROP FUNCTION IF EXISTS religiosos.sp_asignar_dictaminador_declaratoria(integer, integer, integer);

CREATE OR REPLACE FUNCTION religiosos.sp_asignar_dictaminador_declaratoria(
	p_id_declaratoria integer,
	p_id_dictaminador integer,
	p_id_asignador integer)
    RETURNS TABLE(id_generico integer, mensaje character varying, proceso_existoso boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$

DECLARE

existeTramite CONSTANT integer := (SELECT i_id FROM religiosos."TBL_Declaratoria_Procedencia" WHERE i_id = p_id_declaratoria LIMIT 1);
existeDictaminador CONSTANT integer := (SELECT i_id FROM religiosos."ASOC_DeclaratoriaDictaminador" WHERE i_id_tbl_declaratoria = p_id_declaratoria LIMIT 1);
aux_estatus integer := (SELECT i_id FROM religiosos."CAT_Estatus" WHERE nombre = 'Declaratoria de Procedencia asignada - En proceso' LIMIT 1);

BEGIN

	IF existeTramite is not null THEN
	
		IF(existeDictaminador is null) THEN
		
			INSERT INTO religiosos."ASOC_DeclaratoriaDictaminador"(i_id_tbl_declaratoria, id_tbl_dictaminador, id_tbl_asignador)
			VALUES (p_id_declaratoria, p_id_dictaminador, p_id_asignador);

		ELSE
		
			UPDATE religiosos."ASOC_DeclaratoriaDictaminador"
				SET	id_tbl_dictaminador = p_id_dictaminador,
				id_tbl_asignador = p_id_asignador
			WHERE i_id_tbl_declaratoria = p_id_declaratoria;

		END IF;
		
		UPDATE religiosos."TBL_Declaratoria_Procedencia"
			SET i_id_tbl_estatus = aux_estatus
		WHERE i_id = p_id_declaratoria;

		RETURN QUERY SELECT 
			existeTramite as id_generico,
			CAST('La asinación se ha realizado de forma exitosa.' as character varying) AS mensaje,
			(true) AS proceso_existoso;
	ELSE 
	
		RETURN QUERY SELECT 
		 	0 as id_generico,
			CAST('Por favor, asigne un dictaminador.' AS character varying) AS mensaje,
		 	(false) as proceso_existoso;
		 	
	END IF;

END;
$BODY$;

ALTER FUNCTION religiosos.sp_asignar_dictaminador_declaratoria(integer, integer, integer)
    OWNER TO postgres;
