namespace ABC.BookStore.Makbuzlar;
public class ListMakbuzDto : EntityDto<Guid>
{
    public string MakbuzNo { get; set; }
    public DateTime Tarih { get; set; }
    public string AdresAdi { get; set; }
    public string CariAdi { get; set; }
    public string KasaAdi { get; set; }
    public string BankaHesapAdi { get; set; }   
    public int HareketSayisi { get; set; }
    public decimal CekToplam { get; set; }
    public decimal SenetToplam { get; set; }
    public decimal PosToplam { get; set; }
    public decimal NakitToplam { get; set; }
    public decimal BankaToplam { get; set; }
    public decimal GenelToplam => CekToplam + SenetToplam + PosToplam + NakitToplam + BankaToplam;
    public string OzelKod1Adi { get; set; }
    public string OzelKod2Adi { get; set; }
    public string OzelKod3Adi { get; set; }
    public string OzelKod4Adi { get; set; }
    public string OzelKod5Adi { get; set; }
    public string Aciklama { get; set; }
}