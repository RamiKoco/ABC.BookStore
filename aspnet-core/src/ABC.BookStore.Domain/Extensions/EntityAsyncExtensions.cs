using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace ABC.BookStore.Extensions;

public static class EntityAsyncExtensions
{
    public static async Task KodAnyAsync<TEntity>(this IReadOnlyRepository<TEntity> repository,
        string kod, Expression<Func<TEntity, bool>> predicate, bool check = true)
        where TEntity : class, IEntity
    {
        if (check && await repository.AnyAsync(predicate))
            throw new DuplicateCodeException(kod);
    }

    public static async Task RelationalEntityAnyAsync<TEntity>(this IReadOnlyRepository<TEntity> repository,
        Expression<Func<TEntity, bool>> predicate)
        where TEntity : class, IEntity
    {
        var anyAsync = await repository.AnyAsync(predicate);
        if (anyAsync)
            throw new CannotBeDeletedException();
    }
}