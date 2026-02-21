namespace ABC.BookStore.BankaHesaplar;
public class SelectBankaHesapDto : EntityDto<Guid>, IOzelKod
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public BankaHesapTuru HesapTuru { get; set; }
    public string HesapTuruAdi { get; set; }
    public DovizTuru DovizT { get; set; }
    public string HesapNo { get; set; }
    public string IbanNo { get; set; }
    public Guid BankaId { get; set; }
    public string BankaAdi { get; set; }
    public Guid BankaSubeId { get; set; }
    public string BankaSubeAdi { get; set; }
    public Guid CariId { get; set; }
    public string CariAdi { get; set; }
    public Guid? OzelKod1Id { get; set; }
    public string OzelKod1Adi { get; set; }
    public Guid? OzelKod2Id { get; set; }
    public string OzelKod2Adi { get; set; }
    public Guid? OzelKod3Id { get; set; }
    public string OzelKod3Adi { get; set; }
    public Guid? OzelKod4Id { get; set; }
    public string OzelKod4Adi { get; set; }
    public Guid? OzelKod5Id { get; set; }
    public string OzelKod5Adi { get; set; }
    public Guid SubeId { get; set; }
    public string Aciklama { get; set; }
    public bool Durum { get; set; }
}