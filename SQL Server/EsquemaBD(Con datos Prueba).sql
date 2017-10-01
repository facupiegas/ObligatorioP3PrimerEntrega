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

create table Auxiliar(porcentaje_Arancel decimal (10,0),
					  porcentaje_Vip decimal (10,0))