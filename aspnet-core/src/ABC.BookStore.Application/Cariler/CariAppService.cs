namespace ABC.BookStore.Cariler;
[Authorize(BookStorePermissions.Cari.Default)]
public class CariAppService : BookStoreAppService, ICariAppService
{
    private readonly ICariRepository _cariRepository;
    private readonly CariManager _cariManager;

    public CariAppService(ICariRepository cariRepository, CariManager cariManager)
    {
        _cariRepository = cariRepository;
        _cariManager = cariManager;
    }

    public virtual async Task<SelectCariDto> GetAsync(Guid id)
    {
        var entity = await _cariRepository.GetAsync(id, x => x.Id == id, 
            x => x.OzelKod1,
            x => x.OzelKod2, 
            x => x.OzelKod3, 
            x => x.OzelKod4, 
            x => x.OzelKod5);

        var mappedDto = ObjectMapper.Map<Cari, SelectCariDto>(entity);
       
        mappedDto.HesapTuruAdi = L[$"Enum:CariHesapTuru:{(byte)mappedDto.HesapTuru}"];

        mappedDto.CariSubeler.ForEach(x =>
        {
            x.HareketTuruAdi = L[$"Enum:CariSubeTuru:{(byte)x.HareketTuru}"];
        });

        return mappedDto;
        //return ObjectMapper.Map<Cari, SelectCariDto>(entity);
    }

    public virtual async Task<PagedResultDto<ListCariDto>> GetListAsync(
        CariListParameterDto input)
    {
        var entities = await _cariRepository.GetPagedListAsync(input.SkipCount,
            input.MaxResultCount,           
            x => input.HesapTuru == null ? x.Durum == input.Durum : 
            x.HesapTuru == input.HesapTuru && x.Durum == input.Durum,
            x => x.Kod,   
            x => x.OzelKod1, 
            x => x.OzelKod2, 
            x => x.OzelKod3, 
            x => x.OzelKod4, 
            x => x.OzelKod5,
            x => x.Faturalar, 
            x => x.Makbuzlar);

        var totalCount = await _cariRepository.CountAsync(x => input.HesapTuru == null ? x.Durum == input.Durum :
            x.HesapTuru == input.HesapTuru && x.Durum == input.Durum);
        var mappedDtos = ObjectMapper.Map<List<Cari>, List<ListCariDto>>(entities);

        mappedDtos.ForEach(x =>
        {
            x.HesapTuruAdi = L[$"Enum:CariHesapTuru:{(byte)x.HesapTuru}"];

            x.Alacak = x.Faturalar.Where(y => y.FaturaTuru == FaturaTuru.Alis)
             .Sum(y => y.NetTutar);

            x.Alacak += x.Makbuzlar.Where(y => y.MakbuzTuru == MakbuzTuru.Tahsilat)
             .Sum(y => y.CekToplam + y.SenetToplam + y.PosToplam + y.NakitToplam +
              y.BankaToplam);

            x.Borc = x.Faturalar.Where(y => y.FaturaTuru == FaturaTuru.Satis)
             .Sum(y => y.NetTutar);

            x.Borc += x.Makbuzlar.Where(y => y.MakbuzTuru == MakbuzTuru.Odeme)
             .Sum(y => y.CekToplam + y.SenetToplam + y.PosToplam + y.NakitToplam +
              y.BankaToplam);
        });

        return new PagedResultDto<ListCariDto>(totalCount, mappedDtos);
    }

    [Authorize(BookStorePermissions.Cari.Create)]
    public virtual async Task<SelectCariDto> CreateAsync(CreateCariDto input)
    {
        await _cariManager.CheckCreateAsync(input.Kod,
            input.OzelKod1Id, 
            input.OzelKod2Id, 
            input.OzelKod3Id, 
            input.OzelKod4Id, 
            input.OzelKod5Id);

        var entity = ObjectMapper.Map<CreateCariDto, Cari>(input);
        await _cariRepository.InsertAsync(entity);
        return ObjectMapper.Map<Cari, SelectCariDto>(entity);
    }

    [Authorize(BookStorePermissions.Cari.Update)]
    public virtual async Task<SelectCariDto> UpdateAsync(Guid id, UpdateCariDto input)
    {
        var entity = await _cariRepository.GetAsync(id, x => x.Id == id, x => x.CariSubeler);

        await _cariManager.CheckUpdateAsync(id, input.Kod,  entity,
            input.OzelKod1Id,
            input.OzelKod2Id, 
            input.OzelKod3Id, 
            input.OzelKod4Id, 
            input.OzelKod5Id);

        foreach (var cariSubeDto in input.CariSubeler)
        {            

            var cariSube = entity.CariSubeler.FirstOrDefault(
                x => x.Id == cariSubeDto.Id);

            if (cariSube == null)
            {
                entity.CariSubeler.Add(
                    ObjectMapper.Map<CariSubeDto, CariSube>(cariSubeDto));
                continue;
            }

            ObjectMapper.Map(cariSubeDto, cariSube);
        }

        var deletedEntities = entity.CariSubeler.Where(
          x => input.CariSubeler.Select(y => y.Id).ToList().IndexOf(x.Id) == -1);
        entity.CariSubeler.RemoveAll(deletedEntities);

        var mappedEntity = ObjectMapper.Map(input, entity);
        await _cariRepository.UpdateAsync(mappedEntity);
        return ObjectMapper.Map<Cari, SelectCariDto>(mappedEntity);
    }

    [Authorize(BookStorePermissions.Cari.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        var entity = await _cariRepository.GetAsync(id, x => x.Id == id,
          x => x.CariSubeler);

        await _cariManager.CheckDeleteAsync(id);
        await _cariRepository.DeleteAsync(entity);
        entity.CariSubeler.RemoveAll(entity.CariSubeler);
    }

    public virtual async Task<string> GetCodeAsync(CariCodeParameterDto input)
    {
        return await _cariRepository.GetCodeAsync(x => x.Kod, x => x.Durum == input.Durum);
    }
}