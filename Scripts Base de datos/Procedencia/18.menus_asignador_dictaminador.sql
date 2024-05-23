--CORRECCION MENUS ASIGNADOR Y DICTAMINADOR DECLARATORIA DE PROCEDENCIA

--- CAMBIO EN EL HEAD 
update religiosos."CAT_Modulo" set head = 'Asignación' where i_id = 42;


--INSERTS PARA PANTALLA ATENCION EN DICTAMINADOR EN DECLARATORIA PROCEDENCIA
INSERT INTO religiosos."CAT_Modulo" VALUES (45, 'Atención de Declaratoria de procedencia', 'Atención de Declaratoria de procedencia', NULL, NULL, true, 'vista-principal-declaratoria', 'Atención de solicitudes');

--se actualiza el modulo del dictaminador al nuevo registro
UPDATE religiosos."ASOC_ModPerfil" SET i_id_tbl_modulo = 45 where i_id = 29;