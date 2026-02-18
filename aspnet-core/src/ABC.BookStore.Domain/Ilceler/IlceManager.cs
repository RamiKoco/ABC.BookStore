namespace ABC.BookStore.Ilceler;
public class IlceManager : DomainService
{
    private readonly IIlRepository _ilRepository;
    private readonly IIlceRepository _ilceRepository;

    public IlceManager(IIlceRepository ilceRepository, IIlRepository ilRepository)
    {
        _ilceRepository = ilceRepository;
        _ilRepository = ilRepository;
    }

    public async Task CheckCreateAsync(string kod, Guid? ilId)
    {
        await _ilceRepository.KodAnyAsync(kod, x => x.Kod == kod && x.IlId == ilId);
        await _ilRepository.EntityAnyAsync(ilId, x => x.Id == ilId);
    }
    public async Task CheckUpdateAsync(Guid id, string kod, Ilce entity)
    {
        await _ilceRepository.KodAnyAsync(kod, x => x.Id != id && x.Kod == kod && x.IlId == entity.IlId, entity.Kod != kod);
    }   
}