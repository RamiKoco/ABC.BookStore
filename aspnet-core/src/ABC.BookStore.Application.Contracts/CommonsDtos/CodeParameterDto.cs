using Volo.Abp.Application.Dtos;

namespace ABC.BookStore.CommonDtos;

public class CodeParameterDto : IDurum, IEntityDto
{
    public bool Durum { get; set; }
}