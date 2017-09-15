create procedure Servicios_SelectServiciosByRut
	@rut varchar(20)
as
begin
	select nombre,descripcion,tipoServicio 
	from Servicios
	where rutProveedor = @rut
end
