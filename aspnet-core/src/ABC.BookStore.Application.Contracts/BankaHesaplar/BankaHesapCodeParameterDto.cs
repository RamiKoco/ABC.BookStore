namespace ABC.BookStore.BankaHesaplar;
public class BankaHesapCodeParameterDto : IEntityDto, IDurum
{
    public Guid SubeId { get; set; }
    public Guid CariId { get; set; }
    public bool Durum { get; set; }
}