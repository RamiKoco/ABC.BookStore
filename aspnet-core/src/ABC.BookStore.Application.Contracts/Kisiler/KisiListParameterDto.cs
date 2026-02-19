namespace ABC.BookStore.Kisiler;
public class KisiListParameterDto : PagedResultRequestDto, IDurum, IEntityDto
{   
    //public Guid SubeId { get; set; }
    public bool Durum { get; set; }
    public override int MaxResultCount { get; set; } = MaxMaxResultCount = 5000;
}