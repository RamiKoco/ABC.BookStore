namespace ABC.BookStore.BankaHesaplar;
public class BankaHesapListParameterDto : PagedResultRequestDto, IEntityDto, IDurum
{
    public BankaHesapTuru? HesapTuru { get; set; }
    public Guid SubeId { get; set; }
    public Guid CariId { get; set; }
    public bool Durum { get; set; }
    public override int MaxResultCount { get; set; } = MaxMaxResultCount = 5000;
}