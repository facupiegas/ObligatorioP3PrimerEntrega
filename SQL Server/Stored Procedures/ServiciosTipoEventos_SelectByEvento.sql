create procedure ServiciosTipoEventos_SelectByEvento
	@nombreTipoEvento as varchar(20)
as
begin
	select nombreTipoServicio
	from ServiciosTipoEventos
	where nombreTipoEvento=@nombreTipoEvento
end