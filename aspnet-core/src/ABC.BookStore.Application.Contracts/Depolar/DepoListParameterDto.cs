namespace ABC.BookStore.Depolar;
public class DepoListParameterDto : PagedResultRequestDto, IDurum, IEntityDto
{
    public Guid SubeId { get; set; }
    public bool Durum { get; set; }
    public override int MaxResultCount { get; set; } = MaxMaxResultCount = 5000;
}