CREATE procedure dbo.Usuarios_Insert
 @nombre as varchar(20),
 @pass varchar(20),
 @rol varchar(20),
 @sal varchar(10)
as
begin

insert into Usuarios
   values
    (@nombre,@pass,@rol,@sal)
end
