namespace ABC.BookStore.CariSubeler;
public class CariSube : FullAuditedEntity<Guid>
{
    public Guid CariId { get; set; }
    public CariSubeTuru HareketTuru { get; set; }
    public string Aciklama { get; set; }
    public Cari Cari { get; set; }
}
