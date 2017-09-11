create procedure ServiciosDeProveedores_SelectServiciosByRut
	@rut varchar(20)
as
begin
	select nomServicio 
	from ServiciosDeProveedores
	where rutProveedor = @rut
end
