create procedure Proveedores_BajaByRut
	@rut as varchar(15)
as
begin
	UPDATE Proveedores
	SET activo = 0
	WHERE rut=@rut
end