using CommuneServer.Data.Entities;
using CommuneServer.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CommuneServer.Data
{
    public class CommuneDbContext : DbContext
    {
        public CommuneDbContext()
            : base("name=CommuneDb")
        {
        }

        public DbSet<ActivationInfo> Activations { get; set; }
        public DbSet<St> St { get; set; }   // Structure
        public DbSet<Cm> Cm { get; set; }   // Commune
       public DbSet<W> W { get; set; }      // Wilaya
        public DbSet<Cn> Cn { get; set; }   // Centre
        public DbSet<Bu> Bu { get; set; }   // Bureau
        public DbSet<LE> LE { get; set; }   // ListeElectoral
        public DbSet<Cd> Cd { get; set; }   // Candidat
        public DbSet<U> U { get; set; }     // User

        public DbSet<RB> RB { get; set; }   // ResultBureau
        public DbSet<RL> RL { get; set; }   // ResultList
        public DbSet<RC> RC { get; set; }   // ResultCandidat
        public DbSet<RPV> RPV { get; set; }  // PVResult


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =====================================================
            // TABLES
            // =====================================================

            modelBuilder.Entity<St>()
                .ToTable("St", "public");

            modelBuilder.Entity<Cm>()
                .ToTable("Cm", "public");

            modelBuilder.Entity<W>()
                .ToTable("W", "public");

            modelBuilder.Entity<Cn>()
                .ToTable("Cn", "public");

            modelBuilder.Entity<Bu>()
                .ToTable("Bu", "public");

            modelBuilder.Entity<LE>()
                .ToTable("LE", "public");

            modelBuilder.Entity<Cd>()
                .ToTable("Cd", "public");

            modelBuilder.Entity<U>()
                .ToTable("U", "public");

            modelBuilder.Entity<ActivationInfo>()
                .ToTable("Activations", "public");

            modelBuilder.Entity<RB>()
                .ToTable("RB", "public");

            modelBuilder.Entity<RL>()
                .ToTable("RL", "public");

            modelBuilder.Entity<RC>()
                .ToTable("RC", "public");

            modelBuilder.Entity<RPV>()
               .ToTable("RPV", "public");

         
            // =====================================================
            // Cm -> Cn
            // Commune 1 ---- * Centre
            // =====================================================

            modelBuilder.Entity<Cn>()
                .HasRequired(x => x.Cm)
                .WithMany(x => x.Cns)
                .HasForeignKey(x => x.Cmd)
                .WillCascadeOnDelete(true);


            // =====================================================
            // Cn -> Bu
            // Centre 1 ---- * Bureau
            // =====================================================

            modelBuilder.Entity<Bu>()
                .HasRequired(x => x.Cn)
                .WithMany(x => x.Bux)
                .HasForeignKey(x => x.Cnd)
                .WillCascadeOnDelete(true);


            // =====================================================
            // LE -> Cd
            // ListeElectoral 1 ---- * Candidat
            // =====================================================

            modelBuilder.Entity<Cd>()
                .HasRequired(x => x.LE)
                .WithMany(x => x.Cds)
                .HasForeignKey(x => x.LId)
                .WillCascadeOnDelete(true);


            // =====================================================
            // RB -> Bu
            // ResultBureau -> Bureau
            // =====================================================

            modelBuilder.Entity<RB>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<RB>()
                .HasRequired(x => x.Bu)
                .WithMany()
                .HasForeignKey(x => x.Bid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RB>()
                .HasIndex(x => x.Bid)
                .IsUnique();


            // =====================================================
            // RL -> RB
            // ResultList -> ResultBureau
            // =====================================================

            modelBuilder.Entity<RL>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<RL>()
                .HasRequired(x => x.RB)
                .WithMany(x => x.RL)
                .HasForeignKey(x => x.IdRB)
                .WillCascadeOnDelete(false);


            // =====================================================
            // RL -> LE
            // ResultList -> ListeElectoral
            // =====================================================

            modelBuilder.Entity<RL>()
                .HasRequired(x => x.LE)
                .WithMany()
                .HasForeignKey(x => x.Lid)
                .WillCascadeOnDelete(false);
            
            // One list per bureau
            modelBuilder.Entity<RL>()
                .HasIndex(x => new { x.IdRB, x.Lid })
                .IsUnique();

            // =====================================================
            // RC -> Cd
            // ResultCandidat -> Candidat
            // =====================================================

            modelBuilder.Entity<RC>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<RC>()
                .HasRequired(x => x.Cd)
                .WithMany()
                .HasForeignKey(x => x.Cid)
                .WillCascadeOnDelete(false);

            // One candidate result per result list
            modelBuilder.Entity<RC>()
                .HasIndex(x => new { x.IdRL, x.Cid })
                .IsUnique();

            // =====================================================
            // RL -> RC
            // ResultList -> ResultCandidat
            // =====================================================

            modelBuilder.Entity<RL>()
                .HasMany(x => x.RC)
                .WithRequired(x => x.RL)
                .HasForeignKey(x => x.IdRL)
                .WillCascadeOnDelete(false);

            // =====================================================
            // IDs are supplied by the activation package
            // =====================================================

            modelBuilder.Entity<St>()
              .HasKey(x => x.Id);

            modelBuilder.Entity<St>()
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<RPV>()
             .HasKey(x => x.IdPV);

            modelBuilder.Entity<RPV>()
                .HasRequired(x => x.St)
                .WithMany()
                .HasForeignKey(x => x.IdSt)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cm>()
                .HasRequired(x => x.W)
                .WithMany(x => x.Cms)
                .HasForeignKey(x => x.CW)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cn>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Cn>()
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<Bu>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Bu>()
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<LE>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<LE>()
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<Cd>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Cd>()
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<U>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<U>()
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }

    }
}
