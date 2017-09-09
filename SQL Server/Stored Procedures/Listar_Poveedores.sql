CREATE PROCEDURE dbo.Proveedor_SelectAll
AS
BEGIN
	SELECT rut,nomFantasia,email,telefono,fecha,activo,vip,porcentajePorVip,nomUsuario FROM Proveedor
END
GO