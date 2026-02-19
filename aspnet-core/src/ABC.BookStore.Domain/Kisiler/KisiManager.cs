namespace ABC.BookStore.Kisiler;
public class KisiManager : DomainService
{   
    private readonly IKisiRepository _kisiRepository;
    private readonly IOzelKodRepository _ozelKodRepository;
    public KisiManager(IKisiRepository kisiRepository,
        IOzelKodRepository ozelKodRepository)
    {
        _kisiRepository = kisiRepository;
        _ozelKodRepository = ozelKodRepository;
    }
    public async Task CheckCreateAsync(string kod, 
        Guid? ozelKod1Id, 
        Guid? ozelKod2Id)
    {
        await _kisiRepository.KodAnyAsync(kod, x => x.Kod == kod);

        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1,
         KartTuru.Kisi);

        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2,
            KartTuru.Kisi);
    }

    public async Task CheckUpdateAsync(Guid id, string kod, Kisi entity,  
        Guid? ozelKod1Id, 
        Guid? ozelKod2Id)
    {
        await _kisiRepository.KodAnyAsync(kod, x => x.Id != id && x.Kod == kod,
            entity.Kod != kod);

        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1,
           KartTuru.Kisi, entity.OzelKod1Id != ozelKod1Id);

        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2,
            KartTuru.Kisi, entity.OzelKod2Id != ozelKod2Id);
    }
}
