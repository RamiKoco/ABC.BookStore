namespace ABC.BookStore.Bankalar;
public interface IBankaAppService : ICrudAppService<SelectBankaDto, ListBankaDto,
    BankaListParameterDto, CreateBankaDto, UpdateBankaDto, CodeParameterDto>
{
}