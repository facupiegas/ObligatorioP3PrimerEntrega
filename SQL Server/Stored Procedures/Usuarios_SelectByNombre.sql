use ObligatorioP3PrimerEntrega

create procedure dbo.Usuarios_SelectByNombre
	@nombre as varchar(20)

as
begin

	select * from Usuarios
	where nombre=@nombre

end