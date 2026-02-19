namespace ABC.BookStore.Bankalar;
public class ListBankaDto : EntityDto<Guid>
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public string OzelKod1Adi { get; set; }
    public string OzelKod2Adi { get; set; }
    public string OzelKod3Adi { get; set; }
    public string OzelKod4Adi { get; set; }
    public string OzelKod5Adi { get; set; }
    public string Aciklama { get; set; }    
}