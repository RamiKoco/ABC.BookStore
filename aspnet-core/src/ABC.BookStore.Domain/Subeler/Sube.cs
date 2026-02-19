using ABC.BookStore.Parametreler;

namespace ABC.BookStore.Subeler;
public class Sube : FullAuditedAggregateRoot<Guid>
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public string Aciklama { get; set; }
    public bool Durum { get; set; }

    public ICollection<FirmaParametre> FirmaParemetreler { get; set; }
}