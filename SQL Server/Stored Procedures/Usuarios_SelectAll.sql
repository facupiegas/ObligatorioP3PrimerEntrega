create procedure dbo.Usuarios_SelectAll
as
begin
 select nombre,pass,rol,sal from Usuarios
end
go