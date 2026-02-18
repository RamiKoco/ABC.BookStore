namespace ABC.BookStore.Ilceler;
public class SelectIlceDto : EntityDto<Guid>
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public Guid IlId { get; set; }
    public string IlAdi { get; set; }
    public bool Durum { get; set; }
}