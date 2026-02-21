namespace ABC.BookStore.BankaHesaplar;
public class BankaHesap : FullAuditedAggregateRoot<Guid>
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public BankaHesapTuru HesapTuru { get; set; }
    public DovizTuru DovizT { get; set; }
    public string HesapNo { get; set; }
    public string IbanNo { get; set; }
    public Guid BankaSubeId { get; set; }
    public Guid? CariId { get; set; }
    public Guid? OzelKod1Id { get; set; }
    public Guid? OzelKod2Id { get; set; }
    public Guid? OzelKod3Id { get; set; }
    public Guid? OzelKod4Id { get; set; }
    public Guid? OzelKod5Id { get; set; }
    public Guid SubeId { get; set; }
    public string Aciklama { get; set; }
    public bool Durum { get; set; }

    public BankaSube BankaSube { get; set; }
    public OzelKod OzelKod1 { get; set; }
    public OzelKod OzelKod2 { get; set; }
    public OzelKod OzelKod3 { get; set; }
    public OzelKod OzelKod4 { get; set; }
    public OzelKod OzelKod5 { get; set; }
    public Sube Sube { get; set; }
    public Cari Cari { get; set; }

    public ICollection<Makbuz> Makbuzlar { get; set; }
    public ICollection<MakbuzHareket> MakbuzHareketler { get; set; }
}