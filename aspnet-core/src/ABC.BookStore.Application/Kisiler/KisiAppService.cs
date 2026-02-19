namespace ABC.BookStore.Kisiler;
[Authorize(BookStorePermissions.Kisi.Default)]
public class KisiAppService : BookStoreAppService, IKisiAppService
{
    private readonly IKisiRepository _kisiRepository;
    private readonly KisiManager _kisiManager;
    public KisiAppService(IKisiRepository kisiRepository, KisiManager kisiManager)
    {
        _kisiRepository = kisiRepository;
        _kisiManager = kisiManager;
    }
    public virtual async Task<SelectKisiDto> GetAsync(Guid id)
    {
        var entity = await _kisiRepository.GetAsync(id, x => x.Id == id,               
            x => x.Il,
            x => x.Ilce,           
            x => x.OzelKod1,
            x => x.OzelKod2);
        return ObjectMapper.Map<Kisi, SelectKisiDto>(entity);
    }
    public virtual async Task<PagedResultDto<ListKisiDto>> GetListAsync(KisiListParameterDto input)
    {
        var entities = await _kisiRepository.GetPagedListAsync(input.SkipCount,
            input.MaxResultCount,
             x => x.Durum == input.Durum,
             x => x.Kod,
             x => x.Il,
             x => x.Ilce,
             x => x.OzelKod1,
             x => x.OzelKod2);

        var totalCount = await _kisiRepository.CountAsync(x => x.Durum == input.Durum);

        return new PagedResultDto<ListKisiDto>(totalCount,
            ObjectMapper.Map<List<Kisi>, List<ListKisiDto>>(entities)
            );
    }
    [Authorize(BookStorePermissions.Kisi.Create)]
    public virtual async Task<SelectKisiDto> CreateAsync(CreateKisiDto input)
    {
        await _kisiManager.CheckCreateAsync(input.Kod,
            input.OzelKod1Id,
            input.OzelKod2Id);

        var entity = ObjectMapper.Map<CreateKisiDto, Kisi>(input);
        await _kisiRepository.InsertAsync(entity);
        return ObjectMapper.Map<Kisi, SelectKisiDto>(entity);
    }
    [Authorize(BookStorePermissions.Kisi.Update)]
    public virtual async Task<SelectKisiDto> UpdateAsync(Guid id, UpdateKisiDto input)
    {
        var entity = await _kisiRepository.GetAsync(id, x => x.Id == id);

        await _kisiManager.CheckUpdateAsync(id, input.Kod, entity,
            input.OzelKod1Id,
            input.OzelKod2Id);

        var mappedEntity = ObjectMapper.Map(input, entity);
        await _kisiRepository.UpdateAsync(mappedEntity);
        return ObjectMapper.Map<Kisi, SelectKisiDto>(mappedEntity);
    }
    [Authorize(BookStorePermissions.Kisi.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _kisiRepository.DeleteAsync(id);
    }
    public virtual async Task<string> GetCodeAsync(KisiCodeParameterDto input)
    {
        return await _kisiRepository.GetCodeAsync(x => x.Kod, x => x.Durum == input.Durum);
    }
}