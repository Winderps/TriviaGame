using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TriviaNight.Models
{
    public partial class triviaDataContext : DbContext
    {
        public triviaDataContext()
        {
        }

        public triviaDataContext(DbContextOptions<triviaDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ChatMessage> ChatMessage { get; set; }
        public virtual DbSet<GameInvite> GameInvite { get; set; }
        public virtual DbSet<GamePlayer> GamePlayer { get; set; }
        public virtual DbSet<GameQuestion> GameQuestion { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<QuestionAnswer> QuestionAnswer { get; set; }
        public virtual DbSet<QuestionType> QuestionType { get; set; }
        public virtual DbSet<TriviaGame> TriviaGame { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserFriend> UserFriend { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = new StreamReader(File.Open("secret.txt", FileMode.Open)).ReadToEnd();
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatMessage>(entity =>
            {
                entity.Property(e => e.Contents)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.UserFromNavigation)
                    .WithMany(p => p.ChatMessageUserFromNavigation)
                    .HasForeignKey(d => d.UserFrom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChatMessa__UserF__2E1BDC42");

                entity.HasOne(d => d.UserToNavigation)
                    .WithMany(p => p.ChatMessageUserToNavigation)
                    .HasForeignKey(d => d.UserTo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChatMessa__UserT__2F10007B");
            });

            modelBuilder.Entity<GameInvite>(entity =>
            {
                entity.HasOne(d => d.Game)
                    .WithMany(p => p.GameInvite)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GameInvit__GameI__37A5467C");

                entity.HasOne(d => d.TargetUserNavigation)
                    .WithMany(p => p.GameInvite)
                    .HasForeignKey(d => d.TargetUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GameInvit__Targe__38996AB5");
            });

            modelBuilder.Entity<GamePlayer>(entity =>
            {
                entity.HasOne(d => d.Game)
                    .WithMany(p => p.GamePlayer)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GamePlaye__GameI__3C69FB99");
            });

            modelBuilder.Entity<GameQuestion>(entity =>
            {
                entity.Property(e => e.QuestionNumber).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.GameQuestion)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GameQuest__GameI__48CFD27E");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.GameQuestion)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GameQuest__Quest__49C3F6B7");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.Answer).HasMaxLength(30);

                entity.Property(e => e.MaxPointValue).HasDefaultValueSql("((5))");

                entity.Property(e => e.Prompt)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.TimeLimit).HasDefaultValueSql("((30))");

                entity.HasOne(d => d.QuestionTypeNavigation)
                    .WithMany(p => p.Question)
                    .HasForeignKey(d => d.QuestionType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Question__Questi__440B1D61");
            });

            modelBuilder.Entity<QuestionAnswer>(entity =>
            {
                entity.Property(e => e.DisplayText)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<QuestionType>(entity =>
            {
                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<TriviaGame>(entity =>
            {
                entity.Property(e => e.MaxPlayers).HasDefaultValueSql("((8))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('My Game')");

                entity.HasOne(d => d.HostNavigation)
                    .WithMany(p => p.TriviaGame)
                    .HasForeignKey(d => d.Host)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TriviaGame__Host__31EC6D26");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.AvatarImage)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.CreateDate).HasColumnType("date");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastIp)
                    .HasColumnName("LastIP")
                    .HasMaxLength(15);

                entity.Property(e => e.LastLogon).HasColumnType("date");

                entity.Property(e => e.Password)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Token)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.TokenExpires).HasColumnType("date");
            });

            modelBuilder.Entity<UserFriend>(entity =>
            {
                entity.HasOne(d => d.FirstUserNavigation)
                    .WithMany(p => p.UserFriendFirstUserNavigation)
                    .HasForeignKey(d => d.FirstUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserFrien__First__2A4B4B5E");

                entity.HasOne(d => d.SecondUserNavigation)
                    .WithMany(p => p.UserFriendSecondUserNavigation)
                    .HasForeignKey(d => d.SecondUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserFrien__Secon__2B3F6F97");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
