set dateformat dmy;

create database ObligatorioP3PrimerEntrega
use ObligatorioP3PrimerEntrega

create table Usuario(nombre char(20),
					pass varchar(20),
					rol varchar(20),
					constraint PK_Usuario primary key(nombre),
					constraint CK_RolUsuario check(rol in ('Administrador','Proveedor','Organizador')))

create table Organizador(nombre char(20),
						email varchar(30),
						telefono varchar(15),
						nomUsuario char(20),
						constraint PK_Organizador primary key(nombre),
						constraint UK_Email_Organizador unique(email),
						constraint FK_NomUsuario_Organizador foreign key(nomUsuario) references Usuario(nombre))

create table TipoEvento(nombre char(20),
						descripcion varchar(150),
						constraint PK_TipoEvento primary key(nombre))

create table Servicio(nombre char(20),
					 descripcion varchar(150),
					 imagen varchar(100),
					 constraint PK_Servicio primary key(nombre))

create table ServicioTipoEvento(nombreServicio char(20),
							    nombreTipoEvento char(20),
							    constraint PK_ServicioTipoEvento primary key(nombreServicio,nombreTipoEvento),
								constraint FK_NomServ_ServicioTipoEvento foreign key(nombreServicio) references Servicio(nombre),
								constraint FK_NomTipEv_ServicioTipoEvento foreign key(nombreTipoEvento) references TipoEvento(nombre))

create table Proveedor(rut char(15),
					   nomFantasia varchar(30),
					   email varchar(30),
					   telefono varchar(10),
					   fecha date,
					   activo bit,
					   vip bit,
					   porcentajePorVip decimal(10),
					   nomUsuario char(20),
					   constraint PK_Proveedor primary key(rut),
					   constraint FK_NomUsuario_Proveedor foreign key(nomUsuario) references Usuario(nombre),
					   constraint UK_Email_Proveedor unique(email))

create table ServicioDeProveedor(nomServicio char(20),
								 rutProveedor char(15),
								 constraint PK_ServicioDeProveedor primary key(nomServicio,rutProveedor),
								 constraint FK_NomSer_ServicioDeProveedor foreign key(nomServicio) references Servicio(nombre),
								 constraint FK_RutProveedor_ServicioDeProveedor foreign key(rutProveedor) references Proveedor(rut))
/*La tabla ServicioDeProveedor tambien se utiliza para la clase asociativa ServicioContratado*/

create table Evento(nombre char(20),
					fecha date,
					tipo char(20),
					direccion varchar(30),
					constraint PK_Evento primary key(nombre),
					constraint FK_Tipo_Evento foreign key(tipo) references TipoEvento(nombre))



/*Inserto datos en Usuario*/
insert into Usuario values('admin','123','Administrador')
insert into Usuario values('administrador','admin','Administrador')
insert into Usuario values('prov','123','Proveedor')
insert into Usuario values('prov2','123','Proveedor')
insert into Usuario values('org','123','Organizador')
insert into Usuario values('org2','123','Organizador')

/*Inserto datos en TipoEvento*/
insert into TipoEvento values('Fiesta de quince','Fiestas para festejar los quince anios de las mujeres.')
insert into TipoEvento values('Asado','Asado con amigos.')
insert into TipoEvento values('Casamiento','Fiesta para festejar un casamiento.')
