namespace ABC.BookStore.Iller;
public class IlManager : DomainService
{
    private readonly IIlRepository _ilRepository;
    public IlManager(IIlRepository ilRepository)
    {
        _ilRepository = ilRepository;
    }

    public async Task CheckCreateAsync(string kod)
    {
        await _ilRepository.KodAnyAsync(kod, x => x.Kod == kod);
    }
    public async Task CheckUpdateAsync(Guid id, string kod, Il entity)
    {
        await _ilRepository.KodAnyAsync(kod, x => x.Id != id && x.Kod == kod, entity.Kod != kod);

    }
    //public async Task CheckDeleteAsync(Guid id)
    //{
    //    await _ilRepository.RelationalEntityAnyAsync(
    //        x => x.Ilceler.Any(y => y.IlId == id));
    //}
}
