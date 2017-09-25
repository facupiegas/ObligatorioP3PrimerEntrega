use ObligatorioP3PrimerEntrega

create procedure Auxiliar_PorcentajeVip_Update
	@porcentajeVip as decimal(10,0)
as
begin
	update Auxiliar
    SET porcentaje_Vip = @porcentajeVip
end
