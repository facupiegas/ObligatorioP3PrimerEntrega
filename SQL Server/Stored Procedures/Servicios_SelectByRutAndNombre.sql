create procedure Servicios_SelectByRutAndNombre
	@rutProveedor as varchar(15),
	@nombre as varchar(20)
as
begin
	select * from Servicios
	where rutProveedor=@rutProveedor and nombre=@nombre
end