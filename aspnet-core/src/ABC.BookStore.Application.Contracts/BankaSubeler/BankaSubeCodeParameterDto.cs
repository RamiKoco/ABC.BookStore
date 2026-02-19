namespace ABC.BookStore.BankaSubeler;
public class BankaSubeCodeParameterDto : IDurum, IEntityDto
{
    public Guid BankaId { get; set; }
    public bool Durum { get; set; }
}