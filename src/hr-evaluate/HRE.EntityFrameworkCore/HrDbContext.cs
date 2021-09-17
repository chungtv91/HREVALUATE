using HRE.Core.Entities;
using HRE.Core.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HRE.EntityFrameworkCore
{
    public class HrDbContext : IdentityDbContext<CustomUser, CustomRole, int, CustomUserClaim, CustomUserRole, CustomUserLogin, CustomRoleClaim, CustomUserToken>
    {
        public const string SchemaName = "hr";
        public const string HistoryTableName = "__EFMigrationsHistory";

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILookupNormalizer _lookupNormalizer;

        public HrDbContext(DbContextOptions<HrDbContext> options)
            : base(options)
        {
        }

        public HrDbContext(DbContextOptions<HrDbContext> options, IHttpContextAccessor httpContextAccessor, ILookupNormalizer lookupNormalizer)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            _lookupNormalizer = lookupNormalizer ?? new UpperInvariantLookupNormalizer();
        }

        public virtual DbSet<BodMemo> BodMemoes { get; set; }
        public virtual DbSet<BOD> BODs { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EvaluateYear> EvaluateYears { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<ManagerEmp> ManagerEmps { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Sumary> Sumaries { get; set; }

        /// <summary>
        /// View
        /// </summary>
        public virtual DbSet<ListEvaluated> ListEvaluateds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CustomUser>().HasMany(x => x.Roles).WithMany(x => x.Users)
                .UsingEntity<CustomUserRole>(
                    x => x.HasOne(x => x.Role).WithMany().HasForeignKey(x => x.RoleId),
                    x => x.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId)
                );

            modelBuilder.Entity<BOD>()
                .Property(e => e.Code)
                .IsUnicode(false);

            //modelBuilder.Entity<BOD>()
            //    .HasMany(e => e.BodMemoes)
            //    .WithOne(e => e.BOD)
            //    .HasForeignKey(e => e.EmpId);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Code)
                .IsUnicode(false);

            //modelBuilder.Entity<Employee>()
            //   .HasMany(e => e.BodMemoes)
            //   .WithOne(e => e.Employee)
            //   .HasForeignKey(e => e.BodId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Sumaries)
                .WithOne(e => e.Employee)
                .HasForeignKey(e => e.EmpID);

            modelBuilder.Entity<Level>()
                .HasMany(e => e.Employees)
                .WithOne(e => e.Level)
                .HasForeignKey(e => e.CurrentLevelId);

            modelBuilder.Entity<Position>()
                .HasMany(e => e.Employees)
                .WithOne(e => e.Position)
                .HasForeignKey(e => e.CurrentPositionId);

            modelBuilder.Entity<Sumary>()
                .HasOne(x => x.Level)
                .WithMany()
                .HasForeignKey(x => x.CurrentLevelID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Sumary>()
                .HasOne(x => x.Position)
                .WithMany()
                .HasForeignKey(x => x.CurrentPositionId)
                .OnDelete(DeleteBehavior.NoAction);

            //add by myself
            //modelBuilder.Entity<EvaluateYear>()
            //    .HasMany(e => e.BodMemoes)
            //    .WithOne(e => e.EvaluateYear)
            //    .HasForeignKey(e => e.EvaluateYearId);

            modelBuilder.Entity<BodMemo>()
                .HasOne(e => e.Employee)
                .WithMany()
                .HasForeignKey(e => e.EmpId)
                .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<BodMemo>()
            //    .HasOne(e => e.BOD)
            //    .WithMany()
            //    .HasForeignKey(e => e.BodId)
            //    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}