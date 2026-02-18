using ABC.BookStore.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace ABC.BookStore.OzelKodlar;
public class OzelKodManager : DomainService
{
    private readonly IOzelKodRepository _ozelKodRepository;

    public OzelKodManager(IOzelKodRepository ozelKodRepository)
    {
        _ozelKodRepository = ozelKodRepository;
    }

    public async Task CheckCreateAsync(string kod, OzelKodTuru? kodTuru, KartTuru? kartTuru)
    {
        await _ozelKodRepository.KodAnyAsync(kod, x => x.Kod == kod && x.KodTuru == kodTuru &&
                                                       x.KartTuru == kartTuru);
    }

    public async Task CheckUpdateAsync(Guid id, string kod, OzelKod entity)
    {
        await _ozelKodRepository.KodAnyAsync(kod, x => x.Id != id && x.Kod == kod &&
         x.KodTuru == entity.KodTuru && x.KartTuru == entity.KartTuru,
         entity.Kod != kod);
    }

    public async Task CheckDeleteAsync(Guid id)
    {
        await _ozelKodRepository.RelationalEntityAnyAsync(
            x => x.OzelKod1Book.Any(y => y.OzelKod1Id == id) ||
                 x.OzelKod2Book.Any(y => y.OzelKod2Id == id) ||
                 x.OzelKod3Book.Any(y => y.OzelKod3Id == id) ||
                 x.OzelKod4Book.Any(y => y.OzelKod4Id == id) ||
                 x.OzelKod5Book.Any(y => y.OzelKod5Id == id));

    }
}
