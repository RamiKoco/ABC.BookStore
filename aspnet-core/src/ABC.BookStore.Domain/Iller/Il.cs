namespace ABC.BookStore.Iller;
public class Il : FullAuditedAggregateRoot<Guid>
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public bool Durum { get; set; }
    public ICollection<Book> Books { get; set; }
    public ICollection<Ilce> Ilceler { get; set; }
    public ICollection<Kisi> Kisiler { get; set; }
}
