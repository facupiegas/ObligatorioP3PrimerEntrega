/*Inserto valores en AUxiliar (Utilizados por la clase proveedor*/
insert into Auxiliar  values (10,10) 

/*Inserto datos en Usuario*/
insert into Usuarios values('admin','123','Administrador')
insert into Usuarios values('administrador','admin','Administrador')
insert into Usuarios values('prov','123','Proveedor')
insert into Usuarios values('prov2','123','Proveedor')
insert into Usuarios values('prov3','123','Proveedor')
insert into Usuarios values('prov4','123','Proveedor')
insert into Usuarios values('org','123','Organizador')
insert into Usuarios values('org2','123','Organizador')

/*Inserto datos en TipoEvento*/
insert into TiposEventos values('Fiesta de quince','Fiestas para festejar los quince anios de las mujeres.')
insert into TiposEventos values('Asado','Asado con amigos.')
insert into TiposEventos values('Casamiento','Fiesta para festejar un casamiento.')
insert into TiposEventos values('Cumpleaños Infantil','Fiesta para festejar el cumpleaños de un niño.')

/*Inserto datos en Proveedor*/
insert into Proveedores values('210000123477','Almacen de la esquina','almacenDeLaEsquina@almacen.com','24090000','12/12/2017',1,1,50,'prov')
insert into Proveedores values('210000123400','Carniceria Carnasa','carne@carnasa.com','24090021','08/10/2017',1,1,50,'prov2')
insert into Proveedores values('210000123401','Farmacia del Pepe','farma@delpepe.com','24092026','05/07/2017',1,1,50,'prov3')
insert into Proveedores values('210000123402','Animadon','anima@payaso.com','24092087','03/03/2017',1,1,30,'prov4')

/*Inserto datos en tablas de TipoServicios*/
insert into TipoServicios
values('Lunch','Servicio de catering')
insert into TipoServicios
values('Fotografia','Servicio de fotografia')
insert into TipoServicios
values('Aprovisionamiento','Servicio para reponer productos')
insert into TipoServicios
values('Animacion','Servicio para animar fiestas')

/*Inserto datos en tabla ServiciosTipoEventos*/
insert into ServiciosTipoEventos
values('Lunch','Fiesta de quince')
insert into ServiciosTipoEventos
values('Fotografia','Fiesta de quince')
insert into ServiciosTipoEventos
values('Aprovisionamiento','Asado')
insert into ServiciosTipoEventos
values('Animacion','Cumpleaños Infantil')
Insert into ServiciosTipoEventos 
values('Fotografia','Cumpleaños Infantil')

/*Inserto datos en tabla Servicios*/
/*inserto servicios al rut: 210000123477 */
insert into Servicios 
values('210000123477','El Gloton','Mucha comida','','Lunch')
UPDATE Servicios
SET imagen = 'img\elGloton.jpg'
WHERE rutProveedor = '210000123477' and nombre='El Gloton';
insert into Servicios 
values('210000123477','Fotasas','Las mejores fotos','','Fotografia')
UPDATE Servicios
SET imagen = 'img\fotasas.jpg'
WHERE rutProveedor = '210000123477' and nombre='Fotasas';
/*inserto servicios al rut: 210000123400 */
insert into Servicios 
values('210000123400','Asado del Pepe','Los mejores cortes de carne','','Aprovisionamiento')
UPDATE Servicios
SET imagen = 'img\asadoDelPepe.jpg'
WHERE rutProveedor = '210000123400' and nombre='Asado del Pepe';
/*inserto servicios al rut: 210000123402 */
insert into Servicios 
values('210000123402','Animate!','Servicio de payasos','img\payaso.jpg','Animacion')

