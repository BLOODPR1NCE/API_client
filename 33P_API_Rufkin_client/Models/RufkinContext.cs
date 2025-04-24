using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace _33P_API_Rufkin_client.Models;

public partial class RufkinContext : DbContext
{
    public RufkinContext()
    {
    }

    public RufkinContext(DbContextOptions<RufkinContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BooksGenre> BooksGenres { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=ngknn.ru;Port=5442;Database=Rufkin;Username=23P;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("authors_pkey");

            entity.ToTable("authors", "33P_API_Rufkin");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Birthdate).HasColumnName("birthdate");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.Fullname)
                .HasMaxLength(200)
                .HasColumnName("fullname");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("books_pkey");

            entity.ToTable("books", "33P_API_Rufkin");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Author).HasColumnName("author");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
            entity.Property(e => e.YearPublication).HasColumnName("year_publication");

            entity.HasOne(d => d.AuthorNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.Author)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("books_author_fkey");
        });

        modelBuilder.Entity<BooksGenre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("books_genres_pkey");

            entity.ToTable("books_genres", "33P_API_Rufkin");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Book).HasColumnName("book");
            entity.Property(e => e.Genre).HasColumnName("genre");

            entity.HasOne(d => d.BookNavigation).WithMany(p => p.BooksGenres)
                .HasForeignKey(d => d.Book)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("books_genres_book_fkey");

            entity.HasOne(d => d.GenreNavigation).WithMany(p => p.BooksGenres)
                .HasForeignKey(d => d.Genre)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("books_genres_genre_fkey");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("genres_pkey");

            entity.ToTable("genres", "33P_API_Rufkin");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
