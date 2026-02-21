namespace ABC.BookStore.Cariler;
public class CariCodeParameterDto : IDurum, IEntityDto
{
    public Guid SubeId { get; set; }
    public bool Durum { get; set; }
}
