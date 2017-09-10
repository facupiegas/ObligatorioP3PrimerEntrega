create procedure dbo.Usuarios_SelectAll
as
begin
 select nombre,pass,rol from Usuarios
end
go