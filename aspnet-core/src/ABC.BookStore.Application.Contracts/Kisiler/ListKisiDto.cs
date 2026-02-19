namespace ABC.BookStore.Kisiler;
public class ListKisiDto : EntityDto<Guid>
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public string Soyad { get; set; } 
    public string Image { get; set; }
    public string AcikAdres { get; set; }
    public MedeniHal MedeniHal { get; set; } 
    public string IlAdi { get; set; }
    public string IlceAdi { get; set; }
    public string OzelKod1Adi { get; set; }
    public string OzelKod2Adi { get; set; }
    public string TCNo { get; set; }
    public string Telefon { get; set; }
    public string Email { get; set; }
    public DateTime DogumTarihi { get; set; }
    public string DogumYeri { get; set; }
    public Cinsiyet Cinsiyet { get; set; }
    public KanGrubu KanGrubu { get; set; }
    public string Aciklama { get; set; }
    public bool Durum { get; set; }
}