namespace ABC.BookStore.Faturalar;
public class FaturaHareketManager : DomainService
{   
    private readonly IStokRepository _stokRepository;
    private readonly IHizmetRepository _hizmetRepository;
    private readonly IMasrafRepository _masrafRepository;
    private readonly IDepoRepository _depoRepository;
    //private readonly INoktaRepository _noktaRepository;

    public FaturaHareketManager(IStokRepository stokRepository, 
        IHizmetRepository hizmetRepository, IMasrafRepository masrafRepository,
        IDepoRepository depoRepository)
    {
      
        _stokRepository = stokRepository;
        _hizmetRepository = hizmetRepository;
        _masrafRepository = masrafRepository;
        _depoRepository = depoRepository;
        //_noktaRepository = noktaRepository;
    }

    public async Task CheckCreateAsync(Guid? stokId, Guid? hizmetId, Guid? masrafId,
        Guid? depoId)
    {
        
        await _stokRepository.EntityAnyAsync(stokId, x => x.Id == stokId);
        await _hizmetRepository.EntityAnyAsync(hizmetId, x => x.Id == hizmetId);
        await _masrafRepository.EntityAnyAsync(masrafId, x => x.Id == masrafId);
        await _depoRepository.EntityAnyAsync(depoId, x => x.Id == depoId);
        //await _noktaRepository.EntityAnyAsync(noktaId, x => x.Id == noktaId);
    }

    public async Task CheckUpdateAsync(Guid? stokId, Guid? hizmetId, Guid? masrafId,
        Guid? depoId)
    {
       
        await _stokRepository.EntityAnyAsync(stokId, x => x.Id == stokId);
        await _hizmetRepository.EntityAnyAsync(hizmetId, x => x.Id == hizmetId);
        await _masrafRepository.EntityAnyAsync(masrafId, x => x.Id == masrafId);
        await _depoRepository.EntityAnyAsync(depoId, x => x.Id == depoId);
        //await _noktaRepository.EntityAnyAsync(noktaId, x => x.Id == noktaId);
    }
}