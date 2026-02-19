namespace ABC.BookStore.OzelKodlar;
public class OzelKod : FullAuditedAggregateRoot<Guid>
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public OzelKodTuru KodTuru { get; set; }
    public KartTuru KartTuru { get; set; }
    public string Aciklama { get; set; }
    public bool Durum { get; set; }

    public ICollection<Banka> OzelKod1Bankalar { get; set; }
    public ICollection<Banka> OzelKod2Bankalar { get; set; }
    public ICollection<Banka> OzelKod3Bankalar { get; set; }
    public ICollection<Banka> OzelKod4Bankalar { get; set; }
    public ICollection<Banka> OzelKod5Bankalar { get; set; }

    public ICollection<BankaSube> OzelKod1BankaSubeler { get; set; }
    public ICollection<BankaSube> OzelKod2BankaSubeler { get; set; }
    public ICollection<BankaSube> OzelKod3BankaSubeler { get; set; }
    public ICollection<BankaSube> OzelKod4BankaSubeler { get; set; }
    public ICollection<BankaSube> OzelKod5BankaSubeler { get; set; }

    public ICollection<Book> OzelKod1Book { get; set; }
    public ICollection<Book> OzelKod2Book { get; set; }
    public ICollection<Book> OzelKod3Book { get; set; }
    public ICollection<Book> OzelKod4Book { get; set; }
    public ICollection<Book> OzelKod5Book { get; set; }

}