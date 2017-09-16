set dateformat dmy;
/*
USE master;
GO
ALTER DATABASE ObligatorioP3PrimerEntrega SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO
drop dataBase ObligatorioP3PrimerEntrega
*/
create database ObligatorioP3PrimerEntrega
use ObligatorioP3PrimerEntrega
/*
drop table Servicio
drop table TipoEvento
drop table ServicioTipoEvento
drop table Usuarios
drop table Organizador
drop table Proveedor
drop table ServicioDeProveedor
drop table Evento
*/
create table Usuarios(nombre varchar(20),
					pass varchar(20),
					rol varchar(20),
					constraint PK_Usuarios primary key(nombre),
					constraint CK_RolUsuarios check(rol in ('Administrador','Proveedor','Organizador')))

create table Organizadores(nombre varchar(20),
						email varchar(20),
						telefono varchar(15),
						nomUsuario varchar(20),
						constraint PK_Organizadores primary key(nombre),
						constraint UK_Email_Organizadores unique(email),
						constraint FK_NomUsuario_Organizadores foreign key(nomUsuario) references Usuarios(nombre),
						constraint UK_nomUsuario_Organizadores unique(nomUsuario))

create table TiposEventos(nombre varchar(20),
						descripcion varchar(150),
						constraint PK_TiposEventos primary key(nombre))

create table TipoServicios(nombre varchar(20),
						   descripcion varchar(150),
						   constraint PK_TipoServicios primary key(nombre))

create table Servicios(rutProveedor varchar(15),
					   nombre varchar(20),
					   descripcion varchar(150),
					   imagen varchar(100),
					   tipoServicio varchar(20),
					   constraint PK_Servicios primary key(rutProveedor,nombre),
					   constraint FK_tipoServicio_Servicios foreign key(tipoServicio) references TipoServicios(nombre))
					  

create table ServiciosTipoEventos(nombreTipoServicio varchar(20),
							    nombreTipoEvento varchar(20),
							    constraint PK_ServicioTipoEventos primary key(nombreTipoServicio,nombreTipoEvento),
								constraint FK_NomTipoServ_ServicioTipoEventos foreign key(nombreTipoServicio) references TipoServicios(nombre),
								constraint FK_NomTipEv_ServicioTipoEventos foreign key(nombreTipoEvento) references TiposEventos(nombre))

create table Proveedores(rut varchar(15),
					   nomFantasia varchar(30),
					   email varchar(30),
					   telefono varchar(10),
					   fecha date,
					   activo bit,
					   vip bit,
					   porcentajePorVip decimal(10),
					   nomUsuario varchar(20),
					   constraint PK_Proveedores primary key(rut),
					   constraint FK_NomUsuario_Proveedores foreign key(nomUsuario) references Usuarios(nombre),
					   constraint UK_Email_Proveedores unique(email),
					   constraint UK_nomUsuario_Proveedores unique(nomUsuario))


/*La tabla Servicios tambien se utiliza para la clase asociativa ServicioContratado, donde se guardan los servicios contratados para cada evento*/

create table Eventos(nombre varchar(20),
					fecha date,
					tipo varchar(20),
					direccion varchar(30),
					constraint PK_Eventos primary key(nombre),
					constraint FK_Tipo_Eventos foreign key(tipo) references TiposEventos(nombre))




/*Inserto datos en Usuario*/
insert into Usuarios values('admin','123','Administrador')
insert into Usuarios values('administrador','admin','Administrador')
insert into Usuarios values('prov','123','Proveedor')
insert into Usuarios values('prov2','123','Proveedor')
insert into Usuarios values('prov3','123','Proveedor')
insert into Usuarios values('org','123','Organizador')
insert into Usuarios values('org2','123','Organizador')

/*Inserto datos en TipoEvento*/
insert into TiposEventos values('Fiesta de quince','Fiestas para festejar los quince anios de las mujeres.')
insert into TiposEventos values('Asado','Asado con amigos.')
insert into TiposEventos values('Casamiento','Fiesta para festejar un casamiento.')

/*Inserto datos en Proveedor*/
insert into Proveedores values('210000123477','Almacen de la esquina','almacenDeLaEsquina@almacen.com','24090000','12/12/2017',1,1,50,'prov')
insert into Proveedores values('210000123400','Carniceria Carnasa','carne@carnasa.com','24090021','08/10/2017',1,1,50,'prov2')
insert into Proveedores values('210000123401','Farmacia del Pepe','farma@delpepe.com','24092026','05/07/2017',1,1,50,'prov3')
/*Inserto datos en tablas de servicios*/
insert into TipoServicios
values('Lunch','Servicio de catering')
insert into TipoServicios
values('Fotografia','Servicio de fotografia')
insert into TipoServicios
values('Aprovisionamiento','Servicio para reponer productos')

insert into ServiciosTipoEventos
values('Lunch','Fiesta de quince')
insert into ServiciosTipoEventos
values('Fotografia','Fiesta de quince')
insert into ServiciosTipoEventos
values('Aprovisionamiento','Asado')

insert into Servicios 
values('210000123477','El Gloton','Mucha comida','','Lunch')
insert into Servicios 
values('210000123477','Fotasas','Las mejores fotos','','Fotografia')
insert into Servicios 
values('210000123400','Asado del Pepe','Los mejores cortes de carne','','Aprovisionamiento')