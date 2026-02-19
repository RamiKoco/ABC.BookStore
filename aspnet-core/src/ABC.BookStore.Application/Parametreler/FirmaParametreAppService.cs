using Volo.Abp.Domain.Entities;

namespace ABC.BookStore.Parametreler;

[Authorize]
public class FirmaParametreAppService : BookStoreAppService, IFirmaParametreAppService
{
    private readonly IFirmaParametreRepository _firmaParametreRepository;
    private readonly FirmaParametreManager _firmaParametreManager;

    public FirmaParametreAppService(IFirmaParametreRepository firmaParametreRepository, 
        FirmaParametreManager firmaParametreManager)
    {
        _firmaParametreRepository = firmaParametreRepository;
        _firmaParametreManager = firmaParametreManager;
    }

    public virtual async Task<SelectFirmaParametreDto> GetAsync(Guid userId)
    {
        var entity = await _firmaParametreRepository.FirstOrDefaultAsync(x => x.UserId == userId);
        if (entity == null) return null;

        // Include'ları manuel yükle
        entity = await _firmaParametreRepository.GetAsync(entity.Id, x => x.Id == entity.Id,
            x => x.Sube,
            x => x.Donem);

        return ObjectMapper.Map<FirmaParametre, SelectFirmaParametreDto>(entity);
    }

    [NonAction]
    public virtual Task<PagedResultDto<SelectFirmaParametreDto>> GetListAsync(
        FirmaParametreListParameterDto input)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<SelectFirmaParametreDto> CreateAsync(
        CreateFirmaParametreDto input)
    {
        await _firmaParametreManager.CheckCreateAsync(input.SubeId, input.DonemId);

        var entity = ObjectMapper.Map<CreateFirmaParametreDto, FirmaParametre>(input);
        await _firmaParametreRepository.InsertAsync(entity);
        return ObjectMapper.Map<FirmaParametre, SelectFirmaParametreDto>(entity);
    }

    public virtual async Task<SelectFirmaParametreDto> UpdateAsync(Guid id,
       UpdateFirmaParametreDto input)
    {
        await _firmaParametreManager.CheckUpdateAsync(input.SubeId, input.DonemId);
        var entity = await _firmaParametreRepository.FirstOrDefaultAsync(x => x.UserId == id);
        if (entity == null) throw new EntityNotFoundException(typeof(FirmaParametre), id);

        var mappedEntity = ObjectMapper.Map(input, entity);
        await _firmaParametreRepository.UpdateAsync(mappedEntity);
        return ObjectMapper.Map<FirmaParametre, SelectFirmaParametreDto>(mappedEntity);
    }

    [NonAction]
    public virtual async Task<bool> UserAnyAsync(Guid userId)
    {
        return await _firmaParametreRepository.AnyAsync(x => x.UserId == userId);
    }
}