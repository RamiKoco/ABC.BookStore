namespace ABC.BookStore.Makbuzlar;
public class SelectMakbuzDto : EntityDto<Guid>, IOzelKod
{
    public MakbuzTuru MakbuzTuru { get; set; }
    public string MakbuzNo { get; set; }
    public DateTime Tarih { get; set; }
    public Guid? AdresId { get; set; }
    public Guid? CariId { get; set; }
    public string CariKodu { get; set; }
    public string AdresAdi { get; set; }
    public string CariAdi { get; set; }
    public Guid? KasaId { get; set; }
    public string KasaAdi { get; set; }
    public Guid? BankaHesapId { get; set; }
    public string BankaHesapAdi { get; set; }
    public int HareketSayisi { get; set; }
    public decimal CekToplam { get; set; }
    public decimal SenetToplam { get; set; }
    public decimal PosToplam { get; set; }
    public decimal NakitToplam { get; set; }
    public decimal BankaToplam { get; set; }
    public decimal GenelToplam => CekToplam + SenetToplam + PosToplam + NakitToplam + BankaToplam;
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
    public string Aciklama { get; set; }
    public bool Durum { get; set; }
    public Guid SubeId { get; set; }
    public string SubeAdi { get; set; }
    public Guid DonemId { get; set; }
    public List<SelectMakbuzHareketDto> MakbuzHareketler { get; set; }
}