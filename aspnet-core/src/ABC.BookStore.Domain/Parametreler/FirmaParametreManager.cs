namespace ABC.BookStore.Parametreler;
public class FirmaParametreManager : DomainService
{
    private readonly ISubeRepository _subeRepository;
    private readonly IDonemRepository _donemRepository;
    public FirmaParametreManager(ISubeRepository subeRepository,
        IDonemRepository donemRepository)
    {
        _subeRepository = subeRepository;
        _donemRepository = donemRepository;        
    }
    public async Task CheckCreateAsync(Guid? subeId, Guid? donemId)
    {
        await _subeRepository.EntityAnyAsync(subeId, x => x.Id == subeId);
        await _donemRepository.EntityAnyAsync(donemId, x => x.Id == donemId);
    }
    public async Task CheckUpdateAsync(Guid? subeId, Guid? donemId)
    {
        await _subeRepository.EntityAnyAsync(subeId, x => x.Id == subeId);
        await _donemRepository.EntityAnyAsync(donemId, x => x.Id == donemId);
    }
}