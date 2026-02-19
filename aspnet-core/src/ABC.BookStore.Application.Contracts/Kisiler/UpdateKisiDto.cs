namespace ABC.BookStore.Kisiler;
public class UpdateKisiDto : IEntityDto
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public string Soyad { get; set; }
    public string Image { get; set; }
    public MedeniHal MedeniHal { get; set; }
    public Guid? IlId { get; set; }
    public Guid? IlceId { get; set; }
    public string AcikAdres { get; set; }
    public Guid? OzelKod1Id { get; set; }
    public Guid? OzelKod2Id { get; set; }   
    public string TCNo { get; set; }
    public string Telefon { get; set; }
    public string Email { get; set; }
    public DateTime DogumTarihi { get; set; }
    public DateTime KimlikGT { get; set; }
    public string DogumYeri { get; set; }
    public Cinsiyet Cinsiyet { get; set; }
    public KanGrubu KanGrubu { get; set; }
    public string Aciklama { get; set; }
    public bool Durum { get; set; }
}