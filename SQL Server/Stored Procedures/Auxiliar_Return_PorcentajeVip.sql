use ObligatorioP3PrimerEntrega

create procedure dbo.Auxiliar_Devolver_PorcentajeVip
as
begin
	select Auxiliar.porcentaje_Vip from Auxiliar
end