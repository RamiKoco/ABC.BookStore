namespace ABC.BookStore.Ilceler;
[Authorize(BookStorePermissions.Ilce.Default)]
public class IlceAppService : BookStoreAppService, IIlceAppService
{
    private readonly IIlceRepository _ilceRepository;
    private readonly IlceManager _ilceManager;

    public IlceAppService(IIlceRepository ilceRepository, IlceManager ilceManager)
    {
        _ilceRepository = ilceRepository;
        _ilceManager = ilceManager;
    }

    public virtual async Task<SelectIlceDto> GetAsync(Guid id)
    {
        var entity = await _ilceRepository.GetAsync(id, b => b.Id == id, x => x.Il);

        return ObjectMapper.Map<Ilce, SelectIlceDto>(entity);
    }

    public virtual async Task<PagedResultDto<ListIlceDto>> GetListAsync(IlceListParameterDto input)
    {
        var entities = await _ilceRepository.GetPagedListAsync(input.SkipCount,
            input.MaxResultCount,
            x => x.IlId == input.IlId && x.Durum == input.Durum,
            x => x.Kod, x => x.Il);

        var totalCount = await _ilceRepository.CountAsync(x =>
          x.IlId == input.IlId && x.Durum == input.Durum);

        return new PagedResultDto<ListIlceDto>(
            totalCount,
            ObjectMapper.Map<List<Ilce>, List<ListIlceDto>>(entities)
            );
    }

    [Authorize(BookStorePermissions.Ilce.Create)]
    public virtual async Task<SelectIlceDto> CreateAsync(CreateIlceDto input)
    {
        await _ilceManager.CheckCreateAsync(input.Kod, input.IlId);

        var entity = ObjectMapper.Map<CreateIlceDto, Ilce>(input);
        await _ilceRepository.InsertAsync(entity);
        return ObjectMapper.Map<Ilce, SelectIlceDto>(entity);
    }

    [Authorize(BookStorePermissions.Ilce.Update)]
    public virtual async Task<SelectIlceDto> UpdateAsync(Guid id, UpdateIlceDto input)
    {
        var entity = await _ilceRepository.GetAsync(id, b => b.Id == id);

        await _ilceManager.CheckUpdateAsync(id, input.Kod, entity);

        var mappedEntity = ObjectMapper.Map(input, entity);
        await _ilceRepository.UpdateAsync(mappedEntity);

        return ObjectMapper.Map<Ilce, SelectIlceDto>(mappedEntity);
    }

    [Authorize(BookStorePermissions.Ilce.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        //await _ilceManager.CheckDeleteAsync(id);
        await _ilceRepository.DeleteAsync(id);
    }

    public virtual async Task<string> GetCodeAsync(IlceCodeParameterDto input)
    {
        return await _ilceRepository.GetCodeAsync(x => x.Kod,
           x => x.IlId == input.IlId && x.Durum == input.Durum);
    }


}
