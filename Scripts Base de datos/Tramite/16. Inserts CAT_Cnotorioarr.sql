	insert into religiosos."CAT_Cnotorioarr" values (27,'Constancia de Notorio Arraigo',null,null,null,'1',2);
	insert into religiosos."CAT_Cnotorioarr" values (28,'Escrito de matriz',null,null,null,'1',1);
	
	update religiosos."CAT_Cnotorioarr" set b_activo = '0' where i_id in (24,25,26);
	