// CartonMohamad_PriceEntities.cs
using System.Data.Entity;

namespace cartonmohamad_sales.Models
{
    public partial class CartonMohamad_PriceEntities : DbContext
    {
        public CartonMohamad_PriceEntities()
            : base("name=CartonMohamad_PriceEntities")
        {
            // تنظیمات متداول EF6
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
        }

        // DbSets
        public virtual DbSet<crm_CustomerMarketingActivities> crm_CustomerMarketingActivities { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<FinalCharge> FinalCharges { get; set; }
        public virtual DbSet<Info_Compuni> Info_Compuni { get; set; }
        public virtual DbSet<OverheadCost> OverheadCosts { get; set; }
        public virtual DbSet<PaperCombo> PaperCombos { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Tb_Body_Order> Tb_Body_Order { get; set; }
        public virtual DbSet<Tb_Catgori> Tb_Catgori { get; set; }
        public virtual DbSet<Tb_join_Categori_Paper> Tb_join_Categori_Paper { get; set; }
        public virtual DbSet<Tb_Order> Tb_Order { get; set; }
        public virtual DbSet<Tb_Paper> Tb_Paper { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // DB-First: تنظیمات اضافی لازم نیست؛ همه‌چیز از اسکیمای دیتابیس خوانده می‌شود.
            // اگر بعداً نیاز به مپینگ خاص داشتی، اینجا اضافه کن.
            base.OnModelCreating(modelBuilder);
        }
    }
}
