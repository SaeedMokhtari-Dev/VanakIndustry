using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VanakIndustry.DataAccess.Entities;

#nullable disable

namespace VanakIndustry.DataAccess.Contexts
{
    public partial class VanakIndustryContext : DbContext
    {
        private readonly string _connectionString;
        
        public VanakIndustryContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public VanakIndustryContext(DbContextOptions<VanakIndustryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<Election> Elections { get; set; }
        public virtual DbSet<ElectionCandidate> ElectionCandidates { get; set; }
        public virtual DbSet<ElectionCandidateType> ElectionCandidateTypes { get; set; }
        public virtual DbSet<ElectionLimit> ElectionLimits { get; set; }
        public virtual DbSet<ElectionResult> ElectionResults { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<SelectElectionCandidate> SelectElectionCandidates { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }

        public async Task ExecuteTransactionAsync(Func<Task> action)
        {
            await Database.BeginTransactionAsync();

            await action();

            try
            {
                await Database.CurrentTransaction.CommitAsync();
            }
            catch
            {
                await Database.CurrentTransaction.RollbackAsync();

                throw;
            }
        }

        public async Task<T> ExecuteTransactionAsync<T>(Func<Task<T>> action)
        {
            await Database.BeginTransactionAsync();

            var result = await action();

            try
            {
                await Database.CurrentTransaction.CommitAsync();
            }
            catch
            {
                await Database.CurrentTransaction.RollbackAsync();

                throw;
            }

            return result;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.ToTable("Attachment");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Image).IsRequired();
            });

            modelBuilder.Entity<Election>(entity =>
            {
                entity.ToTable("Election");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.FinalizeDate).HasColumnType("datetime");

                entity.Property(e => e.Iplimit).HasColumnName("IPLimit");

                entity.Property(e => e.Iplist)
                    .HasMaxLength(500)
                    .HasColumnName("IPList");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ElectionCandidate>(entity =>
            {
                entity.ToTable("ElectionCandidate");

                entity.HasIndex(e => new { e.ElectionId, e.PersonId, e.ElectionCandidateTypeId }, "IX_ElectionCandidate_Unique")
                    .IsUnique();

                entity.HasOne(d => d.ElectionCandidateType)
                    .WithMany(p => p.ElectionCandidates)
                    .HasForeignKey(d => d.ElectionCandidateTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ElectionCandidate_ElectionCandidateType");

                entity.HasOne(d => d.Election)
                    .WithMany(p => p.ElectionCandidates)
                    .HasForeignKey(d => d.ElectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ElectionCandidate_Election");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.ElectionCandidates)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ElectionCandidate_Person");
            });

            modelBuilder.Entity<ElectionCandidateType>(entity =>
            {
                entity.ToTable("ElectionCandidateType");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ElectionLimit>(entity =>
            {
                entity.ToTable("ElectionLimit");

                entity.HasIndex(e => new { e.ElectionCandidateTypeId, e.ElectionId }, "IX_ElectionLimit_Unique")
                    .IsUnique();

                entity.HasOne(d => d.ElectionCandidateType)
                    .WithMany(p => p.ElectionLimits)
                    .HasForeignKey(d => d.ElectionCandidateTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ElectionLimit_ElectionCandidateType");

                entity.HasOne(d => d.Election)
                    .WithMany(p => p.ElectionLimits)
                    .HasForeignKey(d => d.ElectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ElectionLimit_Election");
            });

            modelBuilder.Entity<ElectionResult>(entity =>
            {
                entity.ToTable("ElectionResult");

                entity.HasIndex(e => new { e.ElectionId, e.PersonId }, "IX_ElectionResult_ElectionPersonUnique")
                    .IsUnique();

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.HasOne(d => d.ElectionCandidateType)
                    .WithMany(p => p.ElectionResults)
                    .HasForeignKey(d => d.ElectionCandidateTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ElectionResult_ElectionCandidateType");

                entity.HasOne(d => d.Election)
                    .WithMany(p => p.ElectionResults)
                    .HasForeignKey(d => d.ElectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ElectionResult_Election");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.ElectionResults)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ElectionResult_Person");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.HasIndex(e => e.Barcode, "IX_Person")
                    .IsUnique();

                entity.HasIndex(e => e.NationalId, "IX_Person_NationalId")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "IX_Person_Username")
                    .IsUnique();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Barcode).HasMaxLength(50);

                entity.Property(e => e.BirthDate)
                    .IsRequired()
                    .HasMaxLength(8);

                entity.Property(e => e.CertificateId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.FatherName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FieldOfStudy).HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastLoginAt).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedAt).HasColumnType("datetime");

                entity.Property(e => e.MotherName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NationalId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NickName).HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PostalCode).HasMaxLength(50);

                entity.Property(e => e.Qualification).HasMaxLength(50);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Card)
                    .WithMany(p => p.PersonCards)
                    .HasForeignKey(d => d.CardId)
                    .HasConstraintName("FK_Person_Image");

                entity.HasOne(d => d.Picture)
                    .WithMany(p => p.PersonPictures)
                    .HasForeignKey(d => d.PictureId)
                    .HasConstraintName("FK_Person_Image1");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Person_Role");
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("RefreshToken");

                entity.HasIndex(e => new { e.PersonId, e.IsActive }, "IX_RefreshToken_PersonActive")
                    .IsUnique();

                entity.HasIndex(e => e.Token, "IX_RefreshToken_Token")
                    .IsUnique();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.ExpiresAt).HasColumnType("datetime");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RefreshToken_Person");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Role1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Role");
            });

            modelBuilder.Entity<SelectElectionCandidate>(entity =>
            {
                entity.ToTable("SelectElectionCandidate");

                entity.HasIndex(e => new { e.ElectionCandidateId, e.PersonId }, "IX_SelectElectionCandidate_Unique")
                    .IsUnique();

                entity.HasOne(d => d.ElectionCandidate)
                    .WithMany(p => p.SelectElectionCandidates)
                    .HasForeignKey(d => d.ElectionCandidateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SelectElectionCandidate_ElectionCandidate");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.SelectElectionCandidates)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SelectElectionCandidate_Person");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("Ticket");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.AnswerPerson)
                    .WithMany(p => p.TicketAnswerPeople)
                    .HasForeignKey(d => d.AnswerPersonId)
                    .HasConstraintName("FK_Ticket_Person1");

                entity.HasOne(d => d.Attachment)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.AttachmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ticket_Image");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.TicketPeople)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ticket_Person");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
