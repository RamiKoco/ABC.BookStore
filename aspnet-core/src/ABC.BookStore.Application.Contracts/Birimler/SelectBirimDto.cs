namespace ABC.BookStore.Birimler;
public class SelectBirimDto : EntityDto<Guid>, IOzelKod
{
    public string Kod { get; set; }
    public string Ad { get; set; }
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
}