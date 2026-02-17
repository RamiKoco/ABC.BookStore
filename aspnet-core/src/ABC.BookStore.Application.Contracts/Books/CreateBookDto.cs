using System;
using Volo.Abp.Application.Dtos;

namespace ABC.BookStore.Books;

public class CreateBookDto : IEntityDto
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public BookType Type { get; set; }
    public DateTime PublishDate { get; set; }
    public float Price { get; set; }
    public string Aciklama { get; set; }
    public bool Durum { get; set; }
}