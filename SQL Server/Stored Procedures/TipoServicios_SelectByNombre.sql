create procedure TipoServicios_SelectByNombre
	@nombre as varchar(20)
as
begin 
	select * from TipoServicios
	where nombre=@nombre
end