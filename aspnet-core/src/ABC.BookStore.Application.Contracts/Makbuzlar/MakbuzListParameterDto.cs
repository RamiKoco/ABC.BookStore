namespace ABC.BookStore.Makbuzlar;
public class MakbuzListParameterDto : PagedResultRequestDto, IDurum, IEntityDto
{
    public MakbuzTuru MakbuzTuru { get; set; }
    public Guid SubeId { get; set; }
    public Guid DonemId { get; set; }
    public bool Durum { get; set; }
    public override int MaxResultCount { get; set; } = MaxMaxResultCount = 5000;
}