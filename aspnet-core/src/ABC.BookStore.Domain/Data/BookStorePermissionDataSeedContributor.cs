using System.Threading.Tasks;
using ABC.BookStore.Permissions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;

namespace ABC.BookStore.Data;

public class BookStorePermissionDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IPermissionManager _permissionManager;
    private readonly ICurrentTenant _currentTenant;

    public BookStorePermissionDataSeedContributor(
        IPermissionManager permissionManager,
        ICurrentTenant currentTenant)
    {
        _permissionManager = permissionManager;
        _currentTenant = currentTenant;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        var permissions = new[]
        {
            BookStorePermissions.Books.Default,
            BookStorePermissions.Books.Create,
            BookStorePermissions.Books.Edit,
            BookStorePermissions.Books.Delete,
        };

        foreach (var permission in permissions)
        {
            await _permissionManager.SetForRoleAsync("admin", permission, true);
        }
    }
}