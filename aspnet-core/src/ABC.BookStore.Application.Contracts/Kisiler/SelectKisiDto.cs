namespace ABC.BookStore.Kisiler;
public class SelectKisiDto : EntityDto<Guid>, IOzelKod
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public string Soyad { get; set; }
    public string Image { get; set; }
    public string AcikAdres { get; set; }
    public MedeniHal MedeniHal { get; set; }
    public Guid? IlId { get; set; }
    public string IlAdi { get; set; }
    public Guid? IlceId { get; set; }
    public string IlceAdi { get; set; }
    public Guid? OzelKod1Id { get; set; }
    public string OzelKod1Adi { get; set; }
    public Guid? OzelKod2Id { get; set; }
    public string OzelKod2Adi { get; set; }
    public string TCNo { get; set; }
    public string Telefon { get; set; }
    public string Email { get; set; }
    public DateTime DogumTarihi { get; set; } = new DateTime(2000, DateTime.Today.Month, DateTime.Today.Day);
    public string DogumYeri { get; set; }
    public Cinsiyet Cinsiyet { get; set; }
    public KanGrubu KanGrubu { get; set; }
    public Guid SubeId { get; set; }
    public string Aciklama { get; set; }
    public bool Durum { get; set; }
    public Guid? OzelKod3Id { get; set; }
    public string OzelKod3Adi { get; set; }
    public Guid? OzelKod4Id { get; set; }
    public string OzelKod4Adi { get; set; }
    public Guid? OzelKod5Id { get; set; }
    public string OzelKod5Adi { get; set; }
}
