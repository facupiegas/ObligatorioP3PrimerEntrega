
CREATE PROCEDURE dbo.Proveedores_SelectActiveOnly
AS
BEGIN
	SELECT rut,nomFantasia,email,telefono,fecha,activo,vip,porcentajePorVip,nomUsuario 
	FROM Proveedores
	WHERE Proveedores.Activo = 1
END
GO