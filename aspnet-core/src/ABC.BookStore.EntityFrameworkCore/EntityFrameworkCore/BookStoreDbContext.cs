namespace ABC.BookStore.EntityFrameworkCore;
[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class BookStoreDbContext :
    AbpDbContext<BookStoreDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */
    public DbSet<Banka> Bankalar { get; set; }
    public DbSet<BankaSube> BankaSubeler { get; set; }
    public DbSet<BankaHesap> BankaHesaplar { get; set; }
    public DbSet<Birim> Birimler { get; set; }
    public DbSet<Cari> Cariler { get; set; }
    public DbSet<Depo> Depolar { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Donem> Donemler { get; set; }
    public DbSet<FirmaParametre> FirmaParametreler { get; set; }
    public DbSet<Fatura> Faturalar { get; set; }
    public DbSet<Hizmet> Hizmetler { get; set; }
    public DbSet<Il> Iller { get; set; }
    public DbSet<Ilce> Ilceler { get; set; }
    public DbSet<Kasa> Kasalar { get; set; }
    public DbSet<Kisi> Kisiler { get; set; }
    public DbSet<Makbuz> Makbuzlar { get; set; }
    public DbSet<Masraf> Masraflar { get; set; }
    public DbSet<OzelKod> OzelKodlar { get; set; }
    public DbSet<Stok> Stoklar { get; set; }
    public DbSet<Sube> Subeler { get; set; }

    #region Entities from the modules   

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }
    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */
        builder.ConfigureBanka();
        builder.ConfigureBankaSube();
        builder.ConfigureBankaHesap();
        builder.ConfigureBook();
        builder.ConfigureBirim();
        builder.ConfigureCari();
        builder.ConfigureCariSube();
        builder.ConfigureDepo();
        builder.ConfigureDonem();
        builder.ConfigureFatura();
        builder.ConfigureFaturaHareket();
        builder.ConfigureFirmaParametre();
        builder.ConfigureHizmet();
        builder.ConfigureKasa();
        builder.ConfigureIl();
        builder.ConfigureIlce();
        builder.ConfigureKisi();
        builder.ConfigureMakbuz();
        builder.ConfigureMakbuzHareket();
        builder.ConfigureMasraf();
        builder.ConfigureOzelKod();
        builder.ConfigureStok();
        builder.ConfigureSube();
    }
}
