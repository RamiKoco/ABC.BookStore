namespace ABC.BookStore.Faturalar;
public class Fatura : FullAuditedAggregateRoot<Guid>
{
    public FaturaTuru FaturaTuru { get; set; }
    public string FaturaNo { get; set; }
    public DateTime Tarih { get; set; }
    public decimal BrutTutar { get; set; }
    public decimal IndirimTutar { get; set; }
    public decimal KdvHaricTutar { get; set; }
    public decimal KdvTutar { get; set; }
    public decimal NetTutar { get; set; }
    public int HareketSayisi { get; set; }
    public Guid CariId { get; set; }
    public Guid BaglantiId { get; set; }
    public Guid? AdresId { get; set; }
    public Guid? NoktaId { get; set; }
    public Guid? OzelKod1Id { get; set; }
    public Guid? OzelKod2Id { get; set; }
    public Guid? OzelKod3Id { get; set; }
    public Guid? OzelKod4Id { get; set; }
    public Guid? OzelKod5Id { get; set; }
    public Guid SubeId { get; set; }
    public Guid DonemId { get; set; }
    public string Aciklama { get; set; }
    public bool Durum { get; set; }

    public Cari Cari { get; set; }
    public OzelKod OzelKod1 { get; set; }
    public OzelKod OzelKod2 { get; set; }
    public OzelKod OzelKod3 { get; set; }
    public OzelKod OzelKod4 { get; set; }
    public OzelKod OzelKod5 { get; set; }
    public Sube Sube { get; set; }
    public Donem Donem { get; set; }

    public ICollection<FaturaHareket> FaturaHareketler { get; set; }
}