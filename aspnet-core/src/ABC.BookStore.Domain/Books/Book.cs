using ABC.BookStore.Ilceler;
namespace ABC.BookStore.Books;

public class Book : FullAuditedAggregateRoot<Guid>
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public BookType Type { get; set; }
    public Guid? IlId { get; set; }
    public Guid? IlceId { get; set; }
    public Guid? OzelKod1Id { get; set; }
    public Guid? OzelKod2Id { get; set; }
    public Guid? OzelKod3Id { get; set; }
    public Guid? OzelKod4Id { get; set; }
    public Guid? OzelKod5Id { get; set; }
    public DateTime PublishDate { get; set; }
    public float Price { get; set; }
    public string Aciklama { get; set; }
    public bool Durum { get; set; }

    public Il Il { get; set; }
    public Ilce Ilce { get; set; }
    public OzelKod OzelKod1 { get; set; }
    public OzelKod OzelKod2 { get; set; }
    public OzelKod OzelKod3 { get; set; }
    public OzelKod OzelKod4 { get; set; }
    public OzelKod OzelKod5 { get; set; }
}