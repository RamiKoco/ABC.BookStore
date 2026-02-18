namespace ABC.BookStore.Iller;
[Authorize(BookStorePermissions.Il.Default)]
public class IlAppService : BookStoreAppService, IIlAppService
{
    private readonly IIlRepository _ilRepository;
    private readonly IlManager _ilManager;

    public IlAppService(IIlRepository ilRepository, IlManager ilManager)
    {
        _ilRepository = ilRepository;
        _ilManager = ilManager;
    }

    public virtual async Task<SelectIlDto> GetAsync(Guid id)
    {
        var entity = await _ilRepository.GetAsync(id, b => b.Id == id);
        return ObjectMapper.Map<Il, SelectIlDto>(entity);
    }


    public virtual async Task<PagedResultDto<ListIlDto>> GetListAsync(IlListParameterDto input)
    {
        var entities = await _ilRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount,
           x => x.Durum == input.Durum,//predicate
           x => x.Kod//orderBy
           );//include properties

        var totalCount = await _ilRepository.CountAsync(x =>
         x.Durum == input.Durum);

        return new PagedResultDto<ListIlDto>(
            totalCount,
            ObjectMapper.Map<List<Il>, List<ListIlDto>>(entities)
            );
    }

    [Authorize(BookStorePermissions.Il.Create)]
    public virtual async Task<SelectIlDto> CreateAsync(CreateIlDto input)
    {
        await _ilManager.CheckCreateAsync(input.Kod);

        var entity = ObjectMapper.Map<CreateIlDto, Il>(input);
        await _ilRepository.InsertAsync(entity);

        return ObjectMapper.Map<Il, SelectIlDto>(entity);
    }

    [Authorize(BookStorePermissions.Il.Update)]
    public virtual async Task<SelectIlDto> UpdateAsync(Guid id, UpdateIlDto input)
    {
        var entity = await _ilRepository.GetAsync(id, b => b.Id == id);

        await _ilManager.CheckUpdateAsync(id, input.Kod, entity);

        var mappedEntity = ObjectMapper.Map(input, entity);
        await _ilRepository.UpdateAsync(mappedEntity);

        return ObjectMapper.Map<Il, SelectIlDto>(mappedEntity);

    }

    [Authorize(BookStorePermissions.Il.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        //await _ilManager.CheckDeleteAsync(id);
        await _ilRepository.DeleteAsync(id);
    }

    public virtual async Task<string> GetCodeAsync(IlCodeParameterDto input)
    {
        return await _ilRepository.GetCodeAsync(b => b.Kod,
            b => b.Durum == input.Durum);
    }

}
