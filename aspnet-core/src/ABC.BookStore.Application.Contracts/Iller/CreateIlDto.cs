namespace ABC.BookStore.Iller;
public class CreateIlDto : IEntityDto
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public bool Durum { get; set; }
}
