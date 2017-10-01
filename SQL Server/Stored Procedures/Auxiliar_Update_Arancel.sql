create procedure Auxiliar_Arancel_Update
	@arancel as decimal(10,0)
as
begin
	update Auxiliar
    SET porcentaje_Arancel = @arancel
end
