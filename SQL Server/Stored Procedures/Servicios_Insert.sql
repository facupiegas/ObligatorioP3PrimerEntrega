use ObligatorioP3PrimerEntrega

create procedure Servicios_Insert
	@rutProveedor as varchar(15),
	@nombre as varchar(20),
	@descripcion as varchar(150),
	@imagen as varchar(100),
	@tipoServicio as varchar(20)

as
begin
	insert into Servicios
    values(@rutProveedor,@nombre,@descripcion,@imagen,@tipoServicio)
end