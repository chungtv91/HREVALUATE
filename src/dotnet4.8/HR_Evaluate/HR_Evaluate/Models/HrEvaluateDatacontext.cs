using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace HR_Evaluate.Models
{
    public partial class HrEvaluateDatacontext : DbContext
    {
        public HrEvaluateDatacontext()
            : base("name=HrEvaluateDatacontext")
        {
        }

        public virtual DbSet<BodMemo> BodMemoes { get; set; }
        public virtual DbSet<BOD> BODs { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EvaluateYear> EvaluateYears { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<ManagerEmp> ManagerEmps { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Sumary> Sumaries { get; set; }
        public virtual DbSet<ListEvaluated> ListEvaluateds { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BOD>()
                .Property(e => e.Code)
                .IsUnicode(false);

            //modelBuilder.Entity<BOD>()
            //    .HasMany(e => e.BodMemoes)
            //    .WithOptional(e => e.BOD)
            //    .HasForeignKey(e => e.EmpId);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Code)
                .IsUnicode(false);

            //modelBuilder.Entity<Employee>()
            //   .HasMany(e => e.BodMemoes)
            //   .WithOptional(e => e.Employee)
            //   .HasForeignKey(e => e.BodId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Sumaries)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmpID);

            modelBuilder.Entity<Level>()
                .HasMany(e => e.Employees)
                .WithOptional(e => e.Level)
                .HasForeignKey(e => e.CurrentLevelId);

            modelBuilder.Entity<Login>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Position>()
                .HasMany(e => e.Employees)
                .WithOptional(e => e.Position)
                .HasForeignKey(e => e.CurrentPositionId);

            modelBuilder.Entity<Sumary>()
                .HasOptional(x => x.Level)
                .WithMany()
                .HasForeignKey(x => x.CurrentLevelID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sumary>()
                .HasOptional(x => x.Position)
                .WithMany()
                .HasForeignKey(x => x.CurrentPositionId)
                .WillCascadeOnDelete(false);

            //add by myself
            modelBuilder.Entity<EvaluateYear>()
                .HasMany(e => e.BodMemoes)
                .WithOptional(e => e.EvaluateYear)
                .HasForeignKey(e=>e.EvaluateYearId);


            modelBuilder.Entity<BodMemo>()
                .HasOptional(e => e.Employee)
                .WithMany()
                .HasForeignKey(e => e.EmpId)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<BodMemo>()
            //    .HasOptional(e => e.BOD)
            //    .WithMany()
            //    .HasForeignKey(e => e.BodId)
            //    .WillCascadeOnDelete(false);
        }
    }
}
