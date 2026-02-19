namespace ABC.BookStore.BankaSubeler;
public class BankaSubeManager : DomainService
{
    private readonly IBankaSubeRepository _bankaSubeRepository;
    private readonly IBankaRepository _bankaRepository;
    private readonly IOzelKodRepository _ozelKodRepository;

    public BankaSubeManager(IBankaSubeRepository bankaSubeRepository,
        IBankaRepository bankaRepository, IOzelKodRepository ozelKodRepository)
    {
        _bankaSubeRepository = bankaSubeRepository;
        _bankaRepository = bankaRepository;
        _ozelKodRepository = ozelKodRepository;
    }

    public async Task CheckCreateAsync(string kod, Guid? bankaId, Guid? ozelKod1Id,
        Guid? ozelKod2Id, Guid? ozelKod3Id, Guid? ozelKod4Id, Guid? ozelKod5Id)
    {
        await _bankaRepository.EntityAnyAsync(bankaId, x => x.Id == bankaId);
        await _bankaSubeRepository.KodAnyAsync(kod, x => x.Kod == kod && x.BankaId == bankaId);

        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1,
            KartTuru.BankaSube);

        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2,
            KartTuru.BankaSube);

        await _ozelKodRepository.EntityAnyAsync(ozelKod3Id, OzelKodTuru.OzelKod3,
           KartTuru.BankaSube);

        await _ozelKodRepository.EntityAnyAsync(ozelKod4Id, OzelKodTuru.OzelKod4,
            KartTuru.BankaSube);

        await _ozelKodRepository.EntityAnyAsync(ozelKod5Id, OzelKodTuru.OzelKod5,
            KartTuru.BankaSube);
    }

    public async Task CheckUpdateAsync(Guid id, string kod, BankaSube entity,
        Guid? ozelKod1Id, Guid? ozelKod2Id, Guid? ozelKod3Id, Guid? ozelKod4Id, Guid? ozelKod5Id)
    {
        await _bankaSubeRepository.KodAnyAsync(kod,
            x => x.Id != id && x.Kod == kod && x.BankaId == entity.BankaId, entity.Kod != kod);

        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1,
             KartTuru.BankaSube, entity.OzelKod1Id != ozelKod1Id);

        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2,
            KartTuru.BankaSube, entity.OzelKod2Id != ozelKod2Id);

        await _ozelKodRepository.EntityAnyAsync(ozelKod3Id, OzelKodTuru.OzelKod3,
            KartTuru.BankaSube, entity.OzelKod3Id != ozelKod3Id);

        await _ozelKodRepository.EntityAnyAsync(ozelKod4Id, OzelKodTuru.OzelKod4,
            KartTuru.BankaSube, entity.OzelKod4Id != ozelKod4Id);

        await _ozelKodRepository.EntityAnyAsync(ozelKod5Id, OzelKodTuru.OzelKod5,
            KartTuru.BankaSube, entity.OzelKod5Id != ozelKod5Id);
    }

    //public async Task CheckDeleteAsync(Guid id)
    //{
    //    await _bankaSubeRepository.RelationalEntityAnyAsync(
    //        x => x.BankaHesaplar.Any(y => y.BankaSubeId == id) ||
    //             x.MakbuzHareketler.Any(y => y.CekBankaSubeId == id));
    //}
}