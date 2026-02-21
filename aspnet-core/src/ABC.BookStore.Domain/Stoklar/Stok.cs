namespace ABC.BookStore.Stoklar;
public class Stok : FullAuditedAggregateRoot<Guid>
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public int KdvOrani { get; set; }
    public decimal BirimFiyat { get; set; }
    public string Barkod { get; set; }
    public string En { get; set; }
    public string Boy { get; set; }
    public string Yukseklik { get; set; }
    public string Alan { get; set; }
    public string NetHacim { get; set; }
    public string BrutHacim { get; set; }
    public string NetAgirlik { get; set; }
    public string BrutAgirlik { get; set; }
    public Guid BirimId { get; set; }
    public Guid? OzelKod1Id { get; set; }
    public Guid? OzelKod2Id { get; set; }
    public Guid? OzelKod3Id { get; set; }
    public Guid? OzelKod4Id { get; set; }
    public Guid? OzelKod5Id { get; set; }
    public string Aciklama { get; set; }
    public string Aciklama2 { get; set; }
    public string Aciklama3 { get; set; }
    public bool Durum { get; set; }

    public Birim Birim { get; set; }
    public OzelKod OzelKod1 { get; set; }
    public OzelKod OzelKod2 { get; set; }
    public OzelKod OzelKod3 { get; set; }
    public OzelKod OzelKod4 { get; set; }
    public OzelKod OzelKod5 { get; set; }

    public ICollection<FaturaHareket> FaturaHareketler { get; set; }
}