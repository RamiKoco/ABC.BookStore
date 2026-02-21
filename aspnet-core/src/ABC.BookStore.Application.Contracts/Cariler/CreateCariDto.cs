namespace ABC.BookStore.Cariler;
public class CreateCariDto : IEntityDto
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public string Soyad { get; set; }
    public string Unvan { get; set; }
    public CariHesapTuru? HesapTuru { get; set; }
    public string VergiDairesi { get; set; }
    public string TCNo { get; set; }
    public string VergiNo { get; set; }
    public string VDKodu { get; set; } 
    public string Telefon { get; set; }
    public string Email { get; set; }
    public Guid IlId { get; set; }
    public Guid IlceId { get; set; }
    public string Adres { get; set; }
    public Guid? OzelKod1Id { get; set; }
    public Guid? OzelKod2Id { get; set; }
    public Guid? OzelKod3Id { get; set; }
    public Guid? OzelKod4Id { get; set; }
    public Guid? OzelKod5Id { get; set; }
    public string Aciklama { get; set; }
    public bool Durum { get; set; }
}