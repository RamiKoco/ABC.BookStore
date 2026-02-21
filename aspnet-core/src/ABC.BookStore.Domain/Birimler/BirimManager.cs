namespace ABC.BookStore.Birimler;
public class BirimManager : DomainService
{
    private readonly IBirimRepository _birimRepository;
    private readonly IOzelKodRepository _ozelKodRepository;

    public BirimManager(IBirimRepository birimRepository, IOzelKodRepository ozelKodRepository)
    {
        _birimRepository = birimRepository;
        _ozelKodRepository = ozelKodRepository;
    }
    public async Task CheckCreateAsync(string kod, Guid? ozelKod1Id, Guid? ozelKod2Id, Guid? ozelKod3Id, Guid? ozelKod4Id, Guid? ozelKod5Id)
    {
        await _birimRepository.KodAnyAsync(kod, x => x.Kod == kod);

        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1,
            KartTuru.Birim);

        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2,
            KartTuru.Birim);
        await _ozelKodRepository.EntityAnyAsync(ozelKod3Id, OzelKodTuru.OzelKod3,
            KartTuru.Birim);
        await _ozelKodRepository.EntityAnyAsync(ozelKod4Id, OzelKodTuru.OzelKod4,
            KartTuru.Birim);
        await _ozelKodRepository.EntityAnyAsync(ozelKod5Id, OzelKodTuru.OzelKod5,
            KartTuru.Birim);
    }
    public async Task CheckUpdateAsync(Guid id, string kod, Birim entity,
        Guid? ozelKod1Id, Guid? ozelKod2Id, Guid? ozelKod3Id, Guid? ozelKod4Id, Guid? ozelKod5Id)
    {
        await _birimRepository.KodAnyAsync(kod, x => x.Id != id && x.Kod == kod,
            entity.Kod != kod);

        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1,
            KartTuru.Birim, entity.OzelKod1Id != ozelKod1Id);

        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2,
            KartTuru.Birim, entity.OzelKod2Id != ozelKod2Id);

        await _ozelKodRepository.EntityAnyAsync(ozelKod3Id, OzelKodTuru.OzelKod3,
           KartTuru.Birim, entity.OzelKod3Id != ozelKod3Id);

        await _ozelKodRepository.EntityAnyAsync(ozelKod4Id, OzelKodTuru.OzelKod4,
         KartTuru.Birim, entity.OzelKod4Id != ozelKod4Id);

        await _ozelKodRepository.EntityAnyAsync(ozelKod5Id, OzelKodTuru.OzelKod5,
         KartTuru.Birim, entity.OzelKod5Id != ozelKod5Id);
    }
    public async Task CheckDeleteAsync(Guid id)
    {
        await _birimRepository.RelationalEntityAnyAsync(
            x => x.Hizmetler.Any(y => y.BirimId == id) ||
                 x.Masraflar.Any(y => y.BirimId == id) ||
                 x.Stoklar.Any(y => y.BirimId == id));
    }
}