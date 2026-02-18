using ABC.BookStore.Books;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace ABC.BookStore.OzelKodlar;
public class OzelKod : FullAuditedAggregateRoot<Guid>
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public OzelKodTuru KodTuru { get; set; }
    public KartTuru KartTuru { get; set; }
    public string Aciklama { get; set; }
    public bool Durum { get; set; }

    public ICollection<Book> OzelKod1Book { get; set; }
    public ICollection<Book> OzelKod2Book { get; set; }
    public ICollection<Book> OzelKod3Book { get; set; }
    public ICollection<Book> OzelKod4Book { get; set; }
    public ICollection<Book> OzelKod5Book { get; set; }
}
