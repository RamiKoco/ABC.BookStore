namespace ABC.BookStore.Faturalar;
public class ListFaturaDto : EntityDto<Guid>
{
    public string FaturaNo { get; set; }
    public DateTime Tarih { get; set; } 
    public string CariAdi { get; set; }
    public string AdresAdi { get; set; }  
    public string IlAdi { get; set; }
    public decimal BrutTutar { get; set; }
    public decimal IndirimTutar { get; set; }
    public decimal KdvHaricTutar { get; set; }
    public decimal KdvTutar { get; set; }
    public decimal NetTutar { get; set; }
    public int HareketSayisi { get; set; }
    public string OzelKod1Adi { get; set; }
    public string OzelKod2Adi { get; set; }
    public string OzelKod3Adi { get; set; }
    public string OzelKod4Adi { get; set; }
    public string OzelKod5Adi { get; set; }
    public string Aciklama { get; set; }    
}