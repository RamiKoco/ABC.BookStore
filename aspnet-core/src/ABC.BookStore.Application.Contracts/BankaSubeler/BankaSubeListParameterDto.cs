namespace ABC.BookStore.BankaSubeler;
public class BankaSubeListParameterDto : PagedResultRequestDto, IDurum, IEntityDto
{
    public Guid BankaId { get; set; }
    public bool Durum { get; set; }
    public override int MaxResultCount { get; set; } = MaxMaxResultCount = 5000;
}