namespace ABC.BookStore.CariSubeler;
public class SelectCariSubeDto : EntityDto<Guid>
{
    public Guid CariId { get; set; }
    public CariSubeTuru HareketTuru { get; set; }
    public string HareketTuruAdi { get; set; }
    public string HareketAdi { get; set; }
    public string HareketKodu { get; set; }
    public string Aciklama { get; set; }
}
