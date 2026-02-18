namespace ABC.BookStore.Ilceler;
public class IlceCodeParameterDto : IDurum, IEntityDto
{
    public Guid IlId { get; set; }
    public bool Durum { get; set; }
}