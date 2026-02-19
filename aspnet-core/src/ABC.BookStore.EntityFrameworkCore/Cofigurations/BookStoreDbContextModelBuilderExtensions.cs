using ABC.BookStore.Bankalar;
using ABC.BookStore.BankaSubeler;
using ABC.BookStore.Ilceler;

namespace ABC.BookStore.Cofigurations;
public static class BookStoreDbContextModelBuilderExtensions
{
    public static void ConfigureBanka(this ModelBuilder builder)
    {
        builder.Entity<Banka>(b =>
        {
            b.ToTable(BookStoreConsts.DbTablePrefix + "Bankalar", BookStoreConsts.DbSchema);
            b.ConfigureByConvention();

            //properties
            b.Property(x => x.Kod)
                .IsRequired()
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxKodLength);

            b.Property(x => x.Ad)
                .IsRequired()
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxAdLength);

            b.Property(x => x.OzelKod1Id)
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());

            b.Property(x => x.OzelKod2Id)
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());

            b.Property(x => x.OzelKod3Id)
             .HasColumnType(SqlDbType.UniqueIdentifier.ToString());

            b.Property(x => x.OzelKod4Id)
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());

            b.Property(x => x.OzelKod5Id)
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());

            b.Property(x => x.Aciklama)
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxAciklamaLength);

            b.Property(x => x.Durum)
                .HasColumnType(SqlDbType.Bit.ToString());

            //indexs
            b.HasIndex(x => x.Kod);

            //relations
            b.HasOne(x => x.OzelKod1)
                .WithMany(x => x.OzelKod1Bankalar)
                .OnDelete(DeleteBehavior.NoAction);

            b.HasOne(x => x.OzelKod2)
                .WithMany(x => x.OzelKod2Bankalar)
                .OnDelete(DeleteBehavior.NoAction);

            b.HasOne(x => x.OzelKod3)
               .WithMany(x => x.OzelKod3Bankalar)
               .OnDelete(DeleteBehavior.NoAction);

            b.HasOne(x => x.OzelKod4)
               .WithMany(x => x.OzelKod4Bankalar)
               .OnDelete(DeleteBehavior.NoAction);

            b.HasOne(x => x.OzelKod5)
               .WithMany(x => x.OzelKod5Bankalar)
               .OnDelete(DeleteBehavior.NoAction);
        });
    }
    public static void ConfigureBankaSube(this ModelBuilder builder)
    {
        builder.Entity<BankaSube>(b =>
        {
            b.ToTable(BookStoreConsts.DbTablePrefix + "BankaSubeler", BookStoreConsts.DbSchema);
            b.ConfigureByConvention();

            //properties
            b.Property(x => x.Kod)
                .IsRequired()
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxKodLength);

            b.Property(x => x.Ad)
                .IsRequired()
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxAdLength);

            b.Property(x => x.BankaId)
                .IsRequired()
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());

            b.Property(x => x.OzelKod1Id)
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());

            b.Property(x => x.OzelKod2Id)
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());

            b.Property(x => x.OzelKod3Id)
               .HasColumnType(SqlDbType.UniqueIdentifier.ToString());

            b.Property(x => x.OzelKod4Id)
               .HasColumnType(SqlDbType.UniqueIdentifier.ToString());

            b.Property(x => x.OzelKod5Id)
               .HasColumnType(SqlDbType.UniqueIdentifier.ToString());

            b.Property(x => x.Aciklama)
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxAciklamaLength);

            b.Property(x => x.Durum)
                .HasColumnType(SqlDbType.Bit.ToString());

            //indexs
            b.HasIndex(x => x.Kod);

            //relations
            b.HasOne(x => x.Banka)
                .WithMany(x => x.BankaSubeler)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.OzelKod1)
                .WithMany(x => x.OzelKod1BankaSubeler)
                .OnDelete(DeleteBehavior.NoAction);

            b.HasOne(x => x.OzelKod2)
                .WithMany(x => x.OzelKod2BankaSubeler)
                .OnDelete(DeleteBehavior.NoAction);

            b.HasOne(x => x.OzelKod3)
               .WithMany(x => x.OzelKod3BankaSubeler)
               .OnDelete(DeleteBehavior.NoAction);

            b.HasOne(x => x.OzelKod4)
               .WithMany(x => x.OzelKod4BankaSubeler)
               .OnDelete(DeleteBehavior.NoAction);

            b.HasOne(x => x.OzelKod5)
               .WithMany(x => x.OzelKod5BankaSubeler)
               .OnDelete(DeleteBehavior.NoAction);
        });
    }
    public static void ConfigureBook(this ModelBuilder builder)
    {
        builder.Entity<Book>(b =>
        {
            b.ToTable(BookStoreConsts.DbTablePrefix + "Books", BookStoreConsts.DbSchema);
            b.ConfigureByConvention();

            //properties
            b.Property(x => x.Kod)
                .IsRequired()
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxKodLength);

            b.Property(x => x.Ad)
                .IsRequired()
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxAdLength);

            b.Property(x => x.OzelKod1Id)
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());

            b.Property(x => x.OzelKod2Id)
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());

            b.Property(x => x.OzelKod3Id)
             .HasColumnType(SqlDbType.UniqueIdentifier.ToString());

            b.Property(x => x.OzelKod4Id)
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());

            b.Property(x => x.OzelKod5Id)
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());

            b.Property(x => x.Aciklama)
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxAciklamaLength);

            b.Property(x => x.Durum)
                .HasColumnType(SqlDbType.Bit.ToString());

            //indexs
            b.HasIndex(x => x.Kod);

            //relations
            b.HasOne(x => x.OzelKod1)
                .WithMany(x => x.OzelKod1Book)
                .OnDelete(DeleteBehavior.NoAction);

            b.HasOne(x => x.OzelKod2)
                .WithMany(x => x.OzelKod2Book)
                .OnDelete(DeleteBehavior.NoAction);

            b.HasOne(x => x.OzelKod3)
               .WithMany(x => x.OzelKod3Book)
               .OnDelete(DeleteBehavior.NoAction);

            b.HasOne(x => x.OzelKod4)
               .WithMany(x => x.OzelKod4Book)
               .OnDelete(DeleteBehavior.NoAction);

            b.HasOne(x => x.OzelKod5)
               .WithMany(x => x.OzelKod5Book)
               .OnDelete(DeleteBehavior.NoAction);
        });
    }
    public static void ConfigureIl(this ModelBuilder builder)
    {
        builder.Entity<Il>(b =>
        {
            b.ToTable(BookStoreConsts.DbTablePrefix + "Iller", BookStoreConsts.DbSchema);
            b.ConfigureByConvention();

            //properties
            b.Property(x => x.Kod)
                .IsRequired()
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxKodLength);

            b.Property(x => x.Ad)
                .IsRequired()
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxAdLength);

            //b.Property(x => x.UlkeId)
            //   .IsRequired()
            //   .HasColumnType(SqlDbType.UniqueIdentifier.ToString());

            b.Property(x => x.Durum)
                .HasColumnType(SqlDbType.Bit.ToString());

            //indexs
            b.HasIndex(x => x.Kod);

            //relations
            //b.HasOne(x => x.Ulke)
            //   .WithMany(x => x.Iller)
            //   .OnDelete(DeleteBehavior.Cascade);

        });
    }
    public static void ConfigureIlce(this ModelBuilder builder)
    {
        builder.Entity<Ilce>(b =>
        {
            b.ToTable(BookStoreConsts.DbTablePrefix + "Ilceler", BookStoreConsts.DbSchema);
            b.ConfigureByConvention();

            //properties
            b.Property(x => x.Kod)
                .IsRequired()
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxKodLength);

            b.Property(x => x.Ad)
                .IsRequired()
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxAdLength);

            b.Property(x => x.IlId)
                .IsRequired()
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());

            b.Property(x => x.Durum)
                .HasColumnType(SqlDbType.Bit.ToString());

            //indexs
            b.HasIndex(x => x.Kod);

            //relations
            b.HasOne(x => x.Il)
                .WithMany(x => x.Ilceler)
                .OnDelete(DeleteBehavior.Cascade);

        });
    }
    public static void ConfigureOzelKod(this ModelBuilder builder)
    {
        builder.Entity<OzelKod>(b =>
        {
            b.ToTable(BookStoreConsts.DbTablePrefix + "OzelKodlar", BookStoreConsts.DbSchema);
            b.ConfigureByConvention();

            //properties
            b.Property(x => x.Kod)
                .IsRequired()
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxKodLength);

            b.Property(x => x.Ad)
                .IsRequired()
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxAdLength);

            b.Property(x => x.KodTuru)
                .IsRequired()
                .HasColumnType(SqlDbType.TinyInt.ToString());

            b.Property(x => x.KartTuru)
                .IsRequired()
                .HasColumnType(SqlDbType.TinyInt.ToString());

            b.Property(x => x.Aciklama)
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxAciklamaLength);

            b.Property(x => x.Durum)
                .HasColumnType(SqlDbType.Bit.ToString());

            //indexs
            b.HasIndex(x => x.Kod);

            //relations


        });
    }
}
