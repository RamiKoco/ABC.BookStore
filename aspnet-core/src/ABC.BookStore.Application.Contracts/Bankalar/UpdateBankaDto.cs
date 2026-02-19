namespace ABC.BookStore.Bankalar;
public class UpdateBankaDto : IEntityDto
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public Guid? OzelKod1Id { get; set; }
    public Guid? OzelKod2Id { get; set; }
    public Guid? OzelKod3Id { get; set; }
    public Guid? OzelKod4Id { get; set; }
    public Guid? OzelKod5Id { get; set; }
    public string Aciklama { get; set; }
    public bool Durum { get; set; }
}