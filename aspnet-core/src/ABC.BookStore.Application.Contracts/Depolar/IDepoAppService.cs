namespace ABC.BookStore.Depolar;
public interface IDepoAppService : ICrudAppService<SelectDepoDto, ListDepoDto,
    Depolar.DepoListParameterDto, CreateDepoDto, UpdateDepoDto, DepoCodeParameterDto>
{
}