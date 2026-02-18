namespace ABC.BookStore.OzelKodlar;
public class OzelKodListParameterDto : PagedResultRequestDto, IDurum, IEntityDto
{
    public OzelKodTuru KodTuru { get; set; }
    public KartTuru KartTuru { get; set; }
    public bool Durum { get; set; }
    public override int MaxResultCount { get; set; } = MaxMaxResultCount = 5000;
}
