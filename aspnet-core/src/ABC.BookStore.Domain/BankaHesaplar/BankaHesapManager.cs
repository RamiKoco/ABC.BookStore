namespace ABC.BookStore.BankaHesaplar;
public class BankaHesapManager : DomainService
{
    private readonly IBankaHesapRepository _bankaHesapRepository;
    private readonly IBankaSubeRepository _bankaSubeRepository;
    private readonly IOzelKodRepository _ozelKodRepository;
    private readonly ISubeRepository _subeRepository;
    private readonly ICariRepository _cariRepository;
    public BankaHesapManager(IBankaHesapRepository bankaHesapRepository, ICariRepository cariRepository,
        IBankaSubeRepository bankaSubeRepository, IOzelKodRepository ozelKodRepository,
        ISubeRepository subeRepository)
    {
        _bankaHesapRepository = bankaHesapRepository;
        _bankaSubeRepository = bankaSubeRepository;
        _ozelKodRepository = ozelKodRepository;
        _subeRepository = subeRepository;
        _cariRepository = cariRepository;
    }

    public async Task CheckCreateAsync(string kod, Guid? bankaSubeId, Guid? cariId,
        Guid? ozelKod1Id,
        Guid? ozelKod2Id,
        Guid? ozelKod3Id, 
        Guid? ozelKod4Id, 
        Guid? ozelKod5Id,
        Guid? subeId)
    {
        await _subeRepository.EntityAnyAsync(subeId, x => x.Id == subeId);
        await _bankaHesapRepository.KodAnyAsync(kod, x => x.Kod == kod && x.SubeId == subeId);
        await _cariRepository.EntityAnyAsync(cariId, x => x.Id == cariId);
        await _bankaSubeRepository.EntityAnyAsync(bankaSubeId, x => x.Id == bankaSubeId);

        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1,
            KartTuru.BankaHesap);

        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2,
            KartTuru.BankaHesap);
        await _ozelKodRepository.EntityAnyAsync(ozelKod3Id, OzelKodTuru.OzelKod3,
           KartTuru.BankaHesap);
        await _ozelKodRepository.EntityAnyAsync(ozelKod4Id, OzelKodTuru.OzelKod4,
            KartTuru.BankaHesap);
        await _ozelKodRepository.EntityAnyAsync(ozelKod5Id, OzelKodTuru.OzelKod5,
            KartTuru.BankaHesap);
    }

    public async Task CheckUpdateAsync(Guid id, string kod, BankaHesap entity,
        Guid? bankaSubeId, 
        Guid? ozelKod1Id, 
        Guid? ozelKod2Id, 
        Guid? ozelKod3Id, 
        Guid? ozelKod4Id, 
        Guid? ozelKod5Id)
    {
        await _bankaHesapRepository.KodAnyAsync(kod,
            x => x.Id != id && x.Kod == kod && x.SubeId == entity.SubeId && x.CariId == entity.CariId,
            entity.Kod != kod);

        await _bankaSubeRepository.EntityAnyAsync(bankaSubeId, x => x.Id == bankaSubeId,
            entity.BankaSubeId != bankaSubeId);

        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1,
            KartTuru.BankaHesap, entity.OzelKod1Id != ozelKod1Id);

        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2,
            KartTuru.BankaHesap, entity.OzelKod2Id != ozelKod2Id);
        await _ozelKodRepository.EntityAnyAsync(ozelKod3Id, OzelKodTuru.OzelKod3,
          KartTuru.BankaHesap, entity.OzelKod3Id != ozelKod3Id);

        await _ozelKodRepository.EntityAnyAsync(ozelKod4Id, OzelKodTuru.OzelKod4,
         KartTuru.BankaHesap, entity.OzelKod4Id != ozelKod4Id);

        await _ozelKodRepository.EntityAnyAsync(ozelKod5Id, OzelKodTuru.OzelKod5,
         KartTuru.BankaHesap, entity.OzelKod5Id != ozelKod5Id);
    }

    public async Task CheckDeleteAsync(Guid id)
    {
        await _bankaHesapRepository.RelationalEntityAnyAsync(
            x => x.Makbuzlar.Any(y => y.BankaHesapId == id) ||
                 x.MakbuzHareketler.Any(y => y.BankaHesapId == id));
    }
}