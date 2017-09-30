
CREATE PROCEDURE dbo.Proveedores_SelectAll
AS
BEGIN
	SELECT rut,nomFantasia,email,telefono,fecha,activo,vip,porcentajePorVip,nomUsuario FROM Proveedores
END
GO