namespace ABC.BookStore.Stoklar;
public class ListStokDto : EntityDto<Guid>
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public int KdvOrani { get; set; }
    public decimal BirimFiyat { get; set; }
    public string Barkod { get; set; }
    public string En { get; set; }
    public string Boy { get; set; }
    public string Yukseklik { get; set; }
    public string Alan { get; set; }
    public string NetHacim { get; set; }
    public string BrutHacim { get; set; }
    public string NetAgirlik { get; set; }
    public string BrutAgirlik { get; set; }
    public string BirimAdi { get; set; }
    public string OzelKod1Adi { get; set; }
    public string OzelKod2Adi { get; set; }
    public string OzelKod3Adi { get; set; }
    public string OzelKod4Adi { get; set; }
    public string OzelKod5Adi { get; set; }
    public decimal Giren { get; set; }
    public decimal Cikan { get; set; }
    public decimal Mevcut => Giren - Cikan;
    public string Aciklama { get; set; }
}