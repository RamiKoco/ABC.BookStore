namespace ABC.BookStore.Cariler;
public class CariManager : DomainService
{
  
    private readonly ICariRepository _cariRepository;
    private readonly IOzelKodRepository _ozelKodRepository;

    public CariManager(ICariRepository cariRepository, IOzelKodRepository ozelKodRepository)
    {
        _cariRepository = cariRepository;
        _ozelKodRepository = ozelKodRepository;
        
    }
    public async Task CheckCreateAsync(string kod,
        Guid? ozelKod1Id, 
        Guid? ozelKod2Id, 
        Guid? ozelKod3Id, 
        Guid? ozelKod4Id, 
        Guid? ozelKod5Id)
    {
        await _cariRepository.KodAnyAsync(kod, x => x.Kod == kod);

        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1,
            KartTuru.Cari);

        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2,
            KartTuru.Cari);

        await _ozelKodRepository.EntityAnyAsync(ozelKod3Id, OzelKodTuru.OzelKod3,
            KartTuru.Cari);

        await _ozelKodRepository.EntityAnyAsync(ozelKod4Id, OzelKodTuru.OzelKod4,
            KartTuru.Cari);

        await _ozelKodRepository.EntityAnyAsync(ozelKod5Id, OzelKodTuru.OzelKod5,
            KartTuru.Cari);
    }

    public async Task CheckUpdateAsync(Guid id, string kod, Cariler.Cari entity,        
        Guid? ozelKod1Id, 
        Guid? ozelKod2Id, 
        Guid? ozelKod3Id, 
        Guid? ozelKod4Id, 
        Guid? ozelKod5Id)
    {
        await _cariRepository.KodAnyAsync(kod, x => x.Id != id && x.Kod == kod,
            entity.Kod != kod);

        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1,
            KartTuru.Cari, entity.OzelKod1Id != ozelKod1Id);

        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2,
            KartTuru.Cari, entity.OzelKod2Id != ozelKod2Id);

        await _ozelKodRepository.EntityAnyAsync(ozelKod3Id, OzelKodTuru.OzelKod3,
            KartTuru.Cari, entity.OzelKod3Id != ozelKod3Id);

        await _ozelKodRepository.EntityAnyAsync(ozelKod4Id, OzelKodTuru.OzelKod4,
            KartTuru.Cari, entity.OzelKod4Id != ozelKod4Id);

        await _ozelKodRepository.EntityAnyAsync(ozelKod5Id, OzelKodTuru.OzelKod5,
            KartTuru.Cari, entity.OzelKod5Id != ozelKod5Id);
    }
    public async Task CheckDeleteAsync(Guid id)
    {
        await _cariRepository.RelationalEntityAnyAsync(
            x => x.Makbuzlar.Any(y => y.CariId == id) ||
                 x.Faturalar.Any(y => y.CariId == id));
    }
}