namespace ABC.BookStore.Ilceler;
public class IlceListParameterDto : PagedResultRequestDto, IDurum, IEntityDto
{
    public Guid IlId { get; set; }
    public bool Durum { get; set; }
    public override int MaxResultCount { get; set; } = MaxMaxResultCount = 5000;
}
