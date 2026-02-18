namespace ABC.BookStore.Ilceler;
public class CreateIlceDto : IEntityDto
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public Guid? IlId { get; set; }  
    public bool Durum { get; set; }
}