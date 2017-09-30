
CREATE procedure dbo.Proveedores_Insert
 @rut as varchar(12),
 @nomFantasia varchar(30),
 @email varchar(30),
 @telefono varchar(10),
 @fecha datetime,
 @vip bit,
 @porcentajePorVip decimal(10),
 @usuario char(20)
 
as
begin

insert into Proveedores
   values
    (@rut,@nomFantasia,@email,@telefono,@fecha,1,@vip,@porcentajePorVip,@usuario)
end
