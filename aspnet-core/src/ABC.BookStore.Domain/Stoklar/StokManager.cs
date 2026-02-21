namespace ABC.BookStore.Stoklar;
public class StokManager : DomainService
{
    private readonly IStokRepository _stokRepository;
    private readonly IBirimRepository _birimRepository;
    private readonly IOzelKodRepository _ozelKodRepository;

    public StokManager(IStokRepository stokRepository, IBirimRepository birimRepository, 
        IOzelKodRepository ozelKodRepository)
    {
        _stokRepository = stokRepository;
        _birimRepository = birimRepository;
        _ozelKodRepository = ozelKodRepository;
    }

    public async Task CheckCreateAsync(string kod, Guid? birimId, 
        Guid? ozelKod1Id,
        Guid? ozelKod2Id,
        Guid? ozelKod3Id,
        Guid? ozelKod4Id,
        Guid? ozelKod5Id)
    {
        await _stokRepository.KodAnyAsync(kod, x => x.Kod == kod);
        await _birimRepository.EntityAnyAsync(birimId, x => x.Id == birimId);

        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1,
            KartTuru.Stok);

        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2,
            KartTuru.Stok);
        await _ozelKodRepository.EntityAnyAsync(ozelKod3Id, OzelKodTuru.OzelKod3,
         KartTuru.Stok);

        await _ozelKodRepository.EntityAnyAsync(ozelKod4Id, OzelKodTuru.OzelKod4,
            KartTuru.Stok);

        await _ozelKodRepository.EntityAnyAsync(ozelKod5Id, OzelKodTuru.OzelKod5,
            KartTuru.Stok);
    }

    public async Task CheckUpdateAsync(Guid id, string kod, Stok entity,
        Guid? birimId, 
        Guid? ozelKod1Id, 
        Guid? ozelKod2Id,
        Guid? ozelKod3Id,
        Guid? ozelKod4Id,
        Guid? ozelKod5Id)
    {
        await _stokRepository.KodAnyAsync(kod, x => x.Id != id && x.Kod == kod,
            entity.Kod != kod);

        await _birimRepository.EntityAnyAsync(birimId, x => x.Id == birimId);

        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1,
            KartTuru.Stok, entity.OzelKod1Id != ozelKod1Id);

        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2,
            KartTuru.Stok, entity.OzelKod2Id != ozelKod2Id);
        await _ozelKodRepository.EntityAnyAsync(ozelKod3Id, OzelKodTuru.OzelKod3,
          KartTuru.Stok, entity.OzelKod3Id != ozelKod3Id);

        await _ozelKodRepository.EntityAnyAsync(ozelKod4Id, OzelKodTuru.OzelKod4,
            KartTuru.Stok, entity.OzelKod4Id != ozelKod4Id);

        await _ozelKodRepository.EntityAnyAsync(ozelKod5Id, OzelKodTuru.OzelKod5,
            KartTuru.Stok, entity.OzelKod5Id != ozelKod5Id);
    }

    public async Task CheckDeleteAsync(Guid id)
    {
        await _stokRepository.RelationalEntityAnyAsync(
            x => x.FaturaHareketler.Any(y => y.StokId == id));
    }
}