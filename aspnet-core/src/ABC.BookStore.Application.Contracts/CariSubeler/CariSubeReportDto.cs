namespace ABC.BookStore.CariSubeler;
public class CariSubeReportDto : EntityDto<Guid>
{
    public string HareketTuruAdi { get; set; }
    public string HareketAdi { get; set; }
    public string HareketKodu { get; set; }
    public string Aciklama { get; set; }
}
