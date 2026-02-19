namespace ABC.BookStore.Parametreler;
public class CreateFirmaParametreDto : IEntityDto
{
    public Guid UserId { get; set; }
    public Guid SubeId { get; set; }
    public Guid DonemId { get; set; }
}