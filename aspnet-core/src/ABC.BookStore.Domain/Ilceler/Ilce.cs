namespace ABC.BookStore.Ilceler;
public class Ilce : FullAuditedAggregateRoot<Guid>
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public Guid IlId { get; set; }
    public bool Durum { get; set; }

    public Il Il { get; set; }

    public ICollection<Cari> Cariler { get; set; }
    public ICollection<Kisi> Kisiler { get; set; }
}