namespace ABC.BookStore.Depolar;
public class DepoHareketListParameterDto : PagedResultRequestDto, IDurum, IEntityDto
{
    public Guid DepoId { get; set; }
    public Guid NoktaId { get; set; }
    public Guid SubeId { get; set; }
    public Guid DonemId { get; set; }
    public bool Durum { get; set; }
    public override int MaxResultCount { get; set; } = MaxMaxResultCount = 5000;
}