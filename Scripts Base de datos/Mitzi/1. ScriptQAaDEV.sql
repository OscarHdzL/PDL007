
ALTER TABLE IF EXISTS religiosos."TBL_Usuario"
    ADD COLUMN i_activo integer;
	
ALTER TABLE IF EXISTS religiosos."TBL_Persona"
    ADD COLUMN i_activo integer;
	
UPDATE religiosos."TBL_Usuario" set i_activo = 1;

UPDATE religiosos."TBL_Persona" set i_activo = 1;

ALTER TABLE religiosos."CAT_Estatus"
    ALTER COLUMN nombre TYPE character varying(70) COLLATE pg_catalog."default";

ALTER TABLE IF EXISTS religiosos."CAT_Estatus"
    ADD COLUMN b_estatus_declaratoria bit(1);

ALTER TABLE IF EXISTS religiosos."CAT_Estatus"
    ADD COLUMN b_estatus_tnota bit(1);
	
ALTER TABLE IF EXISTS religiosos."CAT_Estatus"
    ADD COLUMN b_estatus_registro bit(1);
	
ALTER TABLE IF EXISTS religiosos."CAT_Estatus"
    ADD COLUMN b_visible bit(1);

INSERT INTO religiosos."CAT_Estatus"(i_id, nombre, descripcion, b_activo, b_estatus_declaratoria)
VALUES (41, 'Declaratoria de Procedencia asignada - En proceso'
, 'Cuando la solicitud ha sido asignada por parte del asignador a un dictaminador.', true, '1');
		
INSERT INTO religiosos."CAT_Estatus"(i_id, nombre, descripcion, b_activo, b_estatus_declaratoria)
VALUES (42, 'Observaciones en Declaratoria de Procedencia - En espera'
, 'Cuando el perfil dictaminador requiera información adicional o corrección de los datos.', true, '1');


INSERT INTO religiosos."CAT_Estatus"(i_id, nombre, descripcion, b_activo, b_estatus_declaratoria)
VALUES (43, 'Observación atendida'
, 'Una vez que el perfil Público ha solventado la observación realizada por la DGAR y ha integrado y/o cargado nuevamente la información y/o documentación correspondiente a la solicitud.', true, '1');

INSERT INTO religiosos."CAT_Estatus"(i_id, nombre, descripcion, b_activo, b_estatus_declaratoria)
VALUES (44, 'Declaratoria de Procedencia Aprobada – En espera'
, 'Es cuando se ha aprobado la solicitud por parte del dictaminador, se ha generado el oficio, y se está en espera de que sea firmado.', true, '1');

INSERT INTO religiosos."CAT_Estatus"(i_id, nombre, descripcion, b_activo, b_estatus_declaratoria)
VALUES (45, 'Declaratoria de Procedencia Autorizada – En espera'
, 'Cuando se ha autorizado la solicitud por parte del dictaminador.', true, '1');

INSERT INTO religiosos."CAT_Estatus"(i_id, nombre, descripcion, b_activo, b_estatus_declaratoria)
VALUES (46, 'Declaratoria de Procedencia Cancelada'
, 'Cuando el representante de la AR no acudió a las oficinas cuando fue requerido.', true, '1');

INSERT INTO religiosos."CAT_Estatus"(i_id, nombre, descripcion, b_activo, b_estatus_declaratoria)
VALUES (47, 'Declaratoria de Procedencia Concluida'
, 'El dictaminador ha concluido el trámite de una solicitud autorizada una vez que ha entregado y cargado al sistema el oficio de autorización debidamente firmado.', true, '1');
	
UPDATE religiosos."CAT_Estatus" SET b_estatus_declaratoria = '1'
WHERE i_id IN (39, 40);

UPDATE religiosos."CAT_Estatus" SET b_estatus_tnota = '1'
WHERE i_id IN (12, 13, 21, 22, 23, 24, 25, 26, 27, 28, 37);

	
UPDATE religiosos."CAT_Estatus" SET b_estatus_registro = '1'
WHERE i_id IN (3, 4, 5, 6, 7, 8, 9, 10, 11, 14, 15, 16, 17, 18, 19, 20, 36);

UPDATE religiosos."ASOC_ModPerfil" SET i_id_tbl_modulo = 40
WHERE i_id_tbl_perfil = 9 AND i_id_tbl_modulo = 26;


ALTER TABLE IF EXISTS religiosos."ASOC_TnotaDictaminador"
    ADD COLUMN b_read boolean;
	
ALTER TABLE IF EXISTS religiosos."ASOC_TraDictaminador"
    ADD COLUMN b_read boolean;
	
ALTER TABLE IF EXISTS religiosos."TBL_Observaciones_Transmision"
    ADD COLUMN b_read boolean;
	
ALTER TABLE IF EXISTS religiosos."ASOC_DeclaratoriaDictaminador"
    ADD COLUMN b_read boolean;
	