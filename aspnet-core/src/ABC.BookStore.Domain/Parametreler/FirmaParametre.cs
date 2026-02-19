namespace ABC.BookStore.Parametreler;
public class FirmaParametre : Entity<Guid>
{
    public Guid UserId { get; set; }
    public Guid SubeId { get; set; }   
    public Guid DonemId { get; set; }
    public IdentityUser User { get; set; }
    public Sube Sube { get; set; }    
    public Donem Donem { get; set; }
}