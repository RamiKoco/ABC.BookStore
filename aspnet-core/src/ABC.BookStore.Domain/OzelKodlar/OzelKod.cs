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

    public ICollection<BankaHesap> OzelKod1BankaHesaplar { get; set; }
    public ICollection<BankaHesap> OzelKod2BankaHesaplar { get; set; }
    public ICollection<BankaHesap> OzelKod3BankaHesaplar { get; set; }
    public ICollection<BankaHesap> OzelKod4BankaHesaplar { get; set; }
    public ICollection<BankaHesap> OzelKod5BankaHesaplar { get; set; }

    public ICollection<Birim> OzelKod1Birimler { get; set; }
    public ICollection<Birim> OzelKod2Birimler { get; set; }
    public ICollection<Birim> OzelKod3Birimler { get; set; }
    public ICollection<Birim> OzelKod4Birimler { get; set; }
    public ICollection<Birim> OzelKod5Birimler { get; set; }

    public ICollection<Book> OzelKod1Book { get; set; }
    public ICollection<Book> OzelKod2Book { get; set; }
    public ICollection<Book> OzelKod3Book { get; set; }
    public ICollection<Book> OzelKod4Book { get; set; }
    public ICollection<Book> OzelKod5Book { get; set; }

    public ICollection<Cari> OzelKod1Cariler { get; set; }
    public ICollection<Cari> OzelKod2Cariler { get; set; }
    public ICollection<Cari> OzelKod3Cariler { get; set; }
    public ICollection<Cari> OzelKod4Cariler { get; set; }
    public ICollection<Cari> OzelKod5Cariler { get; set; }

    public ICollection<Depo> OzelKod1Depolar { get; set; }
    public ICollection<Depo> OzelKod2Depolar { get; set; }
    public ICollection<Depo> OzelKod3Depolar { get; set; }
    public ICollection<Depo> OzelKod4Depolar { get; set; }
    public ICollection<Depo> OzelKod5Depolar { get; set; }

    public ICollection<Fatura> OzelKod1Faturalar { get; set; }
    public ICollection<Fatura> OzelKod2Faturalar { get; set; }
    public ICollection<Fatura> OzelKod3Faturalar { get; set; }
    public ICollection<Fatura> OzelKod4Faturalar { get; set; }
    public ICollection<Fatura> OzelKod5Faturalar { get; set; }

    public ICollection<Hizmet> OzelKod1Hizmetler { get; set; }
    public ICollection<Hizmet> OzelKod2Hizmetler { get; set; }
    public ICollection<Hizmet> OzelKod3Hizmetler { get; set; }
    public ICollection<Hizmet> OzelKod4Hizmetler { get; set; }
    public ICollection<Hizmet> OzelKod5Hizmetler { get; set; }

    public ICollection<Kasa> OzelKod1Kasalar { get; set; }
    public ICollection<Kasa> OzelKod2Kasalar { get; set; }
    public ICollection<Kasa> OzelKod3Kasalar { get; set; }
    public ICollection<Kasa> OzelKod4Kasalar { get; set; }
    public ICollection<Kasa> OzelKod5Kasalar { get; set; }

    public ICollection<Kisi> OzelKod1Kisiler { get; set; }
    public ICollection<Kisi> OzelKod2Kisiler { get; set; }

    public ICollection<Makbuz> OzelKod1Makbuzlar { get; set; }
    public ICollection<Makbuz> OzelKod2Makbuzlar { get; set; }
    public ICollection<Makbuz> OzelKod3Makbuzlar { get; set; }
    public ICollection<Makbuz> OzelKod4Makbuzlar { get; set; }
    public ICollection<Makbuz> OzelKod5Makbuzlar { get; set; }

    public ICollection<Masraf> OzelKod1Masraflar { get; set; }
    public ICollection<Masraf> OzelKod2Masraflar { get; set; }
    public ICollection<Masraf> OzelKod3Masraflar { get; set; }
    public ICollection<Masraf> OzelKod4Masraflar { get; set; }
    public ICollection<Masraf> OzelKod5Masraflar { get; set; }

    public ICollection<Stok> OzelKod1Stoklar { get; set; }
    public ICollection<Stok> OzelKod2Stoklar { get; set; }
    public ICollection<Stok> OzelKod3Stoklar { get; set; }
    public ICollection<Stok> OzelKod4Stoklar { get; set; }
    public ICollection<Stok> OzelKod5Stoklar { get; set; }

}