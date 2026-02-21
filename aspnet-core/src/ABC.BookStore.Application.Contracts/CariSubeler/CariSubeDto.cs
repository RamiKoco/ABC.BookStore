namespace ABC.BookStore.CariSubeler;
public class CariSubeDto : EntityDto<Guid?>
{    
    public CariSubeTuru HareketTuru { get; set; }
    public string Aciklama { get; set; }
}
