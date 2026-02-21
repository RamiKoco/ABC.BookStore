namespace ABC.BookStore.Stoklar;
public class UpdateStokDto : IEntityDto
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
    public Guid? BirimId { get; set; }
    public Guid? OzelKod1Id { get; set; }
    public Guid? OzelKod2Id { get; set; }
    public Guid? OzelKod3Id { get; set; }
    public Guid? OzelKod4Id { get; set; }
    public Guid? OzelKod5Id { get; set; }
    public string Aciklama { get; set; }
    public bool Durum { get; set; }
}