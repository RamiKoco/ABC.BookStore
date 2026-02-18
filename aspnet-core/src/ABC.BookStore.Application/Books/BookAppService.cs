namespace ABC.BookStore.Books;

[Authorize(BookStorePermissions.Books.Default)]
public class BookAppService : BookStoreAppService, IBookAppService
{
    private readonly IBookRepository _bookRepository;
    private readonly BookManager _bookManager;

    public BookAppService(IBookRepository bookRepository, BookManager bookManager)
    {
        _bookRepository = bookRepository;
        _bookManager = bookManager;
    }

    public virtual async Task<SelectBookDto> GetAsync(Guid id)
    {
        var entity = await _bookRepository.GetAsync(id, b => b.Id == id);
        return ObjectMapper.Map<Book, SelectBookDto>(entity);
    }

    public virtual async Task<PagedResultDto<ListBookDto>> GetListAsync(BookListParameterDto input)
    {
        var entities = await _bookRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount,
            b => b.Durum == input.Durum,
            b => b.Kod);

        var totalCount = await _bookRepository.CountAsync(b => b.Durum == input.Durum);

        return new PagedResultDto<ListBookDto>(
            totalCount,
            ObjectMapper.Map<List<Book>, List<ListBookDto>>(entities)
        );
    }

    [Authorize(BookStorePermissions.Books.Create)]
    public virtual async Task<SelectBookDto> CreateAsync(CreateBookDto input)
    {
        await _bookManager.CheckCreateAsync(input.Kod);
        var entity = ObjectMapper.Map<CreateBookDto, Book>(input);
        await _bookRepository.InsertAsync(entity);
        return ObjectMapper.Map<Book, SelectBookDto>(entity);
    }

    [Authorize(BookStorePermissions.Books.Edit)]
    public virtual async Task<SelectBookDto> UpdateAsync(Guid id, UpdateBookDto input)
    {
        var entity = await _bookRepository.GetAsync(id, b => b.Id == id);
        await _bookManager.CheckUpdateAsync(id, input.Kod, entity);
        var mappedEntity = ObjectMapper.Map(input, entity);
        await _bookRepository.UpdateAsync(mappedEntity);
        return ObjectMapper.Map<Book, SelectBookDto>(mappedEntity);
    }

    [Authorize(BookStorePermissions.Books.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _bookManager.CheckDeleteAsync(id);
        await _bookRepository.DeleteAsync(id);
    }

    public virtual async Task<string> GetCodeAsync(CodeParameterDto input)
    {
        return await _bookRepository.GetCodeAsync(b => b.Kod, b => b.Durum == input.Durum);
    }
}