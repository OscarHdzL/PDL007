-- FUNCTION: religiosos.sp_insertar_usuario_sistema_perfil(character varying, character varying, character varying, character varying, integer)

-- DROP FUNCTION IF EXISTS religiosos.sp_insertar_usuario_sistema_perfil(character varying, character varying, character varying, character varying, integer);

CREATE OR REPLACE FUNCTION religiosos.sp_insertar_usuario_sistema_perfil(
	p_nombre character varying,
	p_apellido_p character varying,
	p_apellido_m character varying,
	usuario character varying,
	p_id_ca_perfiles integer)
    RETURNS TABLE(id_usuario integer, contrasenia text, mensaje character varying, proceso_exitoso bit) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
 		 	chars text[] := '{0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z}';
   		 	length integer := 8;
   		 	result text := '';
   		 	i integer := 0;
			
			tbl_usuario_id integer := 0;
			p_tbl_persona_id integer := 0;
			existeusuario CONSTANT integer := (SELECT i_id FROM religiosos."TBL_Persona" WHERE c_correo = encode(usuario::bytea, 'base64') LIMIT 1);
			estatususuario CONSTANT integer := (SELECT i_activo FROM religiosos."TBL_Persona" WHERE i_id = existeusuario LIMIT 1);
			
			BEGIN
			--IF estatususuario = 2 
			--THEN
				--Delete from religiosos."TBL_Usuario" WHERE i_id_tbl_persona = existeusuario;
				--Delete from religiosos."TBL_Persona" WHERE i_id = existeusuario;
			--END IF;
			
			IF existeusuario is null or estatususuario = 2
				THEN
					IF LENGTH < 0 THEN
    					RAISE EXCEPTION 'la longitud de la contraseña no puede ser menor que 0';
  					END IF;
  					FOR i IN 1..LENGTH LOOP
    					RESULT := RESULT || chars[1+random()*(array_length(chars, 1)-1)];
  					END LOOP;
					
				     
					 --encode(p_nombre::bytea, 'base64'), encode(p_apellido_p::bytea, 'base64'),
						--		encode(p_apellido_mencode::bytea, 'base64'),
					INSERT INTO religiosos."TBL_Persona"( c_clave,c_nombre, c_apaterno, c_amaterno,
														b_activo,c_correo, i_activo)
					VALUES ('PENDIENTE', encode(p_nombre::bytea, 'base64'), 
							encode(p_apellido_p::bytea, 'base64'), encode(p_apellido_m::bytea, 'base64'),
						   true,encode(usuario::bytea, 'base64'), 0)
					RETURNING i_id INTO p_tbl_persona_id;
					
					INSERT INTO religiosos."TBL_Usuario"( c_contrasenia, c_usuario, i_id_tbl_persona, 
														 i_id_tbl_perfil, b_terminos, b_privacidad, b_activo, i_activo)
						VALUES (encode(RESULT::bytea, 'base64'), encode(usuario::bytea, 'base64'),p_tbl_persona_id,
														p_id_ca_perfiles,false, false, true, 1)
					RETURNING i_id INTO tbl_usuario_id;

					RETURN QUERY SELECT 
						 tbl_usuario_id as id_usuario,
						 RESULT as contrasenia,
						 CAST('Usuario creado correctamente' as varchar) AS mensaje,
						 (1::bit) AS seProcesoExiosamente;
			ELSE 
				-- Usuario ya existe
				RETURN QUERY SELECT 
						 0 as id_usuario,
						 '' as contrasenia,
						 CAST('Usuario ya existe' as varchar) AS mensaje,
						 (0::bit) AS seProcesoExiosamente;
				
			END IF;

			END;
$BODY$;