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
        public virtual DbSet<ElectionPresentUser> ElectionPresentUsers { get; set; }
        public virtual DbSet<ElectionResult> ElectionResults { get; set; }
        public virtual DbSet<PasswordResetToken> PasswordResetTokens { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<SelectElectionCandidate> SelectElectionCandidates { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

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

                entity.HasIndex(e => new { e.ElectionId, e.UserId, e.ElectionCandidateTypeId }, "IX_ElectionCandidate_Unique")
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

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ElectionCandidates)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ElectionCandidate_User");
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

            modelBuilder.Entity<ElectionPresentUser>(entity =>
            {
                entity.ToTable("ElectionPresentUser");

                entity.HasIndex(e => new { e.ElectionId, e.UserId }, "IX_ElectionPresentUser_Unique")
                    .IsUnique();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.ExpireDate).HasColumnType("datetime");

                entity.HasOne(d => d.Election)
                    .WithMany(p => p.ElectionPresentUsers)
                    .HasForeignKey(d => d.ElectionId)
                    .HasConstraintName("FK_ElectionPresentUser_Election");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ElectionPresentUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_ElectionPresentUser_User");
            });

            modelBuilder.Entity<ElectionResult>(entity =>
            {
                entity.ToTable("ElectionResult");

                entity.HasIndex(e => new { e.ElectionId, e.UserId }, "IX_ElectionResult_ElectionUserUnique")
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

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ElectionResults)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ElectionResult_User");
            });

            modelBuilder.Entity<PasswordResetToken>(entity =>
            {
                entity.ToTable("PasswordResetToken");

                entity.HasIndex(e => new { e.UserId, e.Token }, "IX_PasswordResetToken_Unique")
                    .IsUnique();

                entity.Property(e => e.ResetRequestDate).HasColumnType("datetime");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                /*entity.HasOne(d => d.User)
                    .WithMany(p => p.PasswordResetToken)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PasswordResetToken_User");*/
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("RefreshToken");

                entity.HasIndex(e => e.Token, "IX_RefreshToken_Token")
                    .IsUnique();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.ExpiresAt).HasColumnType("datetime");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_RefreshToken_User");
            });

            modelBuilder.Entity<SelectElectionCandidate>(entity =>
            {
                entity.ToTable("SelectElectionCandidate");

                entity.HasIndex(e => new { e.ElectionCandidateId, e.UserId }, "IX_SelectElectionCandidate_Unique")
                    .IsUnique();

                entity.HasOne(d => d.ElectionCandidate)
                    .WithMany(p => p.SelectElectionCandidates)
                    .HasForeignKey(d => d.ElectionCandidateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SelectElectionCandidate_ElectionCandidate");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SelectElectionCandidates)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SelectElectionCandidate_User");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("Ticket");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.AnswerUser)
                    .WithMany(p => p.TicketAnswerUsers)
                    .HasForeignKey(d => d.AnswerUserId)
                    .HasConstraintName("FK_Ticket_User1");

                entity.HasOne(d => d.Attachment)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.AttachmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Ticket_Image");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TicketUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ticket_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Barcode, "IX_User_Barcode_notnull")
                    .IsUnique()
                    .HasFilter("([Barcode] IS NOT NULL)");

                entity.HasIndex(e => e.NationalId, "IX_User_NationalId")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "IX_User_Username")
                    .IsUnique();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(1024);

                entity.Property(e => e.Barcode).HasMaxLength(50);

                entity.Property(e => e.BirthDate)
                    .IsRequired()
                    .HasMaxLength(8);

                entity.Property(e => e.CertificateId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

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
                    .HasMaxLength(500);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PostalCode).HasMaxLength(50);

                entity.Property(e => e.Qualification).HasMaxLength(50);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.CandidatePicture)
                    .WithMany(p => p.UserCandidatePictures)
                    .HasForeignKey(d => d.CandidatePictureId);

                entity.HasOne(d => d.Card)
                    .WithMany(p => p.UserCards)
                    .HasForeignKey(d => d.CardId);

                entity.HasOne(d => d.FirstPageCertificate)
                    .WithMany(p => p.UserFirstPageCertificates)
                    .HasForeignKey(d => d.FirstPageCertificateId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.NationalCard)
                    .WithMany(p => p.UserNationalCards)
                    .HasForeignKey(d => d.NationalCardId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Picture)
                    .WithMany(p => p.UserPictures)
                    .HasForeignKey(d => d.PictureId);

                entity.HasOne(d => d.SecondPageCertificate)
                    .WithMany(p => p.UserSecondPageCertificates)
                    .HasForeignKey(d => d.SecondPageCertificateId);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");

                entity.HasIndex(e => new { e.Role, e.UserId }, "IX_UserRole_Unique")
                    .IsUnique();

                entity.Property(e => e.Id);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Roles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_UserRole_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
