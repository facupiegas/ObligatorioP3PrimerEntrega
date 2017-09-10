set dateformat dmy;

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
						email varchar(30),
						telefono varchar(15),
						nomUsuario varchar(20),
						constraint PK_Organizadores primary key(nombre),
						constraint UK_Email_Organizadores unique(email),
						constraint FK_NomUsuario_Organizadores foreign key(nomUsuario) references Usuarios(nombre))

create table TiposEventos(nombre varchar(20),
						descripcion varchar(150),
						constraint PK_TiposEventos primary key(nombre))

create table Servicios(nombre varchar(20),
					 descripcion varchar(150),
					 imagen varchar(100),
					 constraint PK_Servicios primary key(nombre))

create table ServiciosTipoEventos(nombreServicio varchar(20),
							    nombreTipoEvento varchar(20),
							    constraint PK_ServicioTipoEventos primary key(nombreServicio,nombreTipoEvento),
								constraint FK_NomServ_ServicioTipoEventos foreign key(nombreServicio) references Servicios(nombre),
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
					   constraint UK_Email_Proveedores unique(email))

create table ServiciosDeProveedores(nomServicio varchar(20),
								 rutProveedor varchar(15),
								 constraint PK_ServiciosDeProveedores primary key(nomServicio,rutProveedor),
								 constraint FK_NomSer_ServiciosDeProveedores foreign key(nomServicio) references Servicios(nombre),
								 constraint FK_RutProveedor_ServiciosDeProveedores foreign key(rutProveedor) references Proveedores(rut))
/*La tabla ServicioDeProveedor tambien se utiliza para la clase asociativa ServicioContratado*/

create table Eventos(nombre varchar(20),
					fecha date,
					tipo varchar(20),
					direccion varchar(30),
					constraint PK_Eventos primary key(nombre),
					constraint FK_Tipo_Eventos foreign key(tipo) references TiposEventos(nombre))

