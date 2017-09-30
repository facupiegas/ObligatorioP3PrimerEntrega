
create procedure Proveedores_SelectByRut
	@rut varchar(15)
as
begin
	Select *
	from Proveedores
	where rut=@rut
end