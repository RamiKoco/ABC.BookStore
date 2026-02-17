using System;
using Volo.Abp.Application.Dtos;

namespace ABC.BookStore.Books;

public class ListBookDto : EntityDto<Guid>
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public BookType Type { get; set; }
    public DateTime PublishDate { get; set; }
    public float Price { get; set; }
    public string Aciklama { get; set; }
}