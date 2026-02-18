namespace ABC.BookStore.Ilceler;
public class ListIlceDto : EntityDto<Guid>
{
    public string Kod { get; set; }
    public string Ad { get; set; }    
    public string IlAdi { get; set; }
}
