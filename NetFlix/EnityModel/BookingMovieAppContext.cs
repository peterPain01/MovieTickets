using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NetFlix.EnityModel;

public partial class BookingMovieAppContext : DbContext
{
    public BookingMovieAppContext()
    {
    }

    public BookingMovieAppContext(DbContextOptions<BookingMovieAppContext> options)
        : base(options)
    {
    }

    //public virtual DbSet<Booking> Bookings { get; set; }

    //public virtual DbSet<Cinema> Cinemas { get; set; }

    //public virtual DbSet<Director> Directors { get; set; }

    //public virtual DbSet<Genre> Genres { get; set; }

    //public virtual DbSet<Movie> Movies { get; set; }

    //public virtual DbSet<Seat> Seats { get; set; }

    //public virtual DbSet<Showtime> Showtimes { get; set; }

    //public virtual DbSet<Star> Stars { get; set; }

    //public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost,1433; Initial Catalog=BookingMovieApp; TrustServerCertificate=True; User Id=as; Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Booking>(entity =>
        //{
        //    entity.HasKey(e => e.BookingId).HasName("PK__Bookings__5DE3A5B145879B6F");

        //    entity.Property(e => e.BookingId).HasColumnName("booking_id");
        //    entity.Property(e => e.BookingDatetime)
        //        .HasColumnType("datetime")
        //        .HasColumnName("booking_datetime");
        //    entity.Property(e => e.OriginalPrice).HasColumnName("original_price");
        //    entity.Property(e => e.ShowtimeId).HasColumnName("showtime_id");
        //    entity.Property(e => e.TotalPrice).HasColumnName("total_price");
        //    entity.Property(e => e.UserId).HasColumnName("user_id");

        //    entity.HasOne(d => d.Showtime).WithMany(p => p.Bookings)
        //        .HasForeignKey(d => d.ShowtimeId)
        //        .HasConstraintName("FK__Bookings__showti__0F624AF8");

        //    entity.HasOne(d => d.User).WithMany(p => p.Bookings)
        //        .HasForeignKey(d => d.UserId)
        //        .HasConstraintName("FK__Bookings__user_i__0E6E26BF");

        //    entity.HasMany(d => d.Seats).WithMany(p => p.Bookings)
        //        .UsingEntity<Dictionary<string, object>>(
        //            "BookingSeat",
        //            r => r.HasOne<Seat>().WithMany()
        //                .HasForeignKey("SeatId")
        //                .OnDelete(DeleteBehavior.ClientSetNull)
        //                .HasConstraintName("FK__BookingSe__seat___17036CC0"),
        //            l => l.HasOne<Booking>().WithMany()
        //                .HasForeignKey("BookingId")
        //                .OnDelete(DeleteBehavior.ClientSetNull)
        //                .HasConstraintName("FK__BookingSe__booki__160F4887"),
        //            j =>
        //            {
        //                j.HasKey("BookingId", "SeatId").HasName("PK__BookingS__84E57B6806DE97D1");
        //                j.ToTable("BookingSeats");
        //                j.IndexerProperty<int>("BookingId").HasColumnName("booking_id");
        //                j.IndexerProperty<int>("SeatId").HasColumnName("seat_id");
        //            });
        //});

        //modelBuilder.Entity<Cinema>(entity =>
        //{
        //    entity.HasKey(e => e.CinemaId).HasName("PK__Cinema__56628778132D7C4C");

        //    entity.ToTable("Cinema");

        //    entity.Property(e => e.CinemaId).HasColumnName("cinema_id");
        //    entity.Property(e => e.Name)
        //        .HasMaxLength(32)
        //        .IsUnicode(false)
        //        .HasColumnName("name");
        //});

        //modelBuilder.Entity<Director>(entity =>
        //{
        //    entity.HasKey(e => e.DirectorId).HasName("PK__Director__F5205E49C888C00D");

        //    entity.Property(e => e.DirectorId).HasColumnName("director_id");
        //    entity.Property(e => e.Name)
        //        .HasMaxLength(100)
        //        .IsUnicode(false)
        //        .HasColumnName("name");
        //});

        //modelBuilder.Entity<Genre>(entity =>
        //{
        //    entity.HasKey(e => e.GenreId).HasName("PK__Genres__18428D4240541A78");

        //    entity.Property(e => e.GenreId).HasColumnName("genre_id");
        //    entity.Property(e => e.Name)
        //        .HasMaxLength(100)
        //        .IsUnicode(false)
        //        .HasColumnName("name");
        //});

        //modelBuilder.Entity<Movie>(entity =>
        //{
        //    entity.HasKey(e => e.MovieId).HasName("PK__Movies__83CDF74950BD4A56");

        //    entity.Property(e => e.MovieId).HasColumnName("movie_id");
        //    entity.Property(e => e.DirectorId).HasColumnName("director_id");
        //    entity.Property(e => e.DurationMinutes).HasColumnName("duration_minutes");
        //    entity.Property(e => e.GenreId).HasColumnName("genre_id");
        //    entity.Property(e => e.PlotSummary)
        //        .HasColumnType("text")
        //        .HasColumnName("plot_summary");
        //    entity.Property(e => e.PosterUrl)
        //        .HasMaxLength(255)
        //        .IsUnicode(false)
        //        .HasColumnName("poster_url");
        //    entity.Property(e => e.PosterVerticalUrl)
        //        .HasMaxLength(200)
        //        .HasColumnName("poster_vertical_url");
        //    entity.Property(e => e.Rating)
        //        .HasColumnType("decimal(3, 1)")
        //        .HasColumnName("rating");
        //    entity.Property(e => e.ReleaseDate)
        //        .HasColumnType("date")
        //        .HasColumnName("release_date");
        //    entity.Property(e => e.Title)
        //        .HasMaxLength(255)
        //        .IsUnicode(false)
        //        .HasColumnName("title");
        //    entity.Property(e => e.TrailerUrl)
        //        .HasMaxLength(255)
        //        .IsUnicode(false)
        //        .HasColumnName("trailer_url");

        //    entity.HasOne(d => d.Director).WithMany(p => p.Movies)
        //        .HasForeignKey(d => d.DirectorId)
        //        .HasConstraintName("FK__Movies__director__403A8C7D");

        //    entity.HasOne(d => d.Genre).WithMany(p => p.Movies)
        //        .HasForeignKey(d => d.GenreId)
        //        .HasConstraintName("FK__Movies__genre_id__412EB0B6");

        //    entity.HasMany(d => d.Stars).WithMany(p => p.Movies)
        //        .UsingEntity<Dictionary<string, object>>(
        //            "MovieStar",
        //            r => r.HasOne<Star>().WithMany()
        //                .HasForeignKey("StarId")
        //                .OnDelete(DeleteBehavior.ClientSetNull)
        //                .HasConstraintName("FK__MovieStar__star___44FF419A"),
        //            l => l.HasOne<Movie>().WithMany()
        //                .HasForeignKey("MovieId")
        //                .OnDelete(DeleteBehavior.ClientSetNull)
        //                .HasConstraintName("FK__MovieStar__movie__440B1D61"),
        //            j =>
        //            {
        //                j.HasKey("MovieId", "StarId").HasName("PK__MovieSta__7B4FD59592397F55");
        //                j.ToTable("MovieStars");
        //                j.IndexerProperty<int>("MovieId").HasColumnName("movie_id");
        //                j.IndexerProperty<int>("StarId").HasColumnName("star_id");
        //            });
        //});

        //modelBuilder.Entity<Seat>(entity =>
        //{
        //    entity.HasKey(e => e.SeatId).HasName("PK__Seats__906DED9C3F4B8D5C");

        //    entity.Property(e => e.SeatId).HasColumnName("seat_id");
        //    entity.Property(e => e.Number).HasColumnName("number");
        //    entity.Property(e => e.Price).HasColumnName("price");
        //    entity.Property(e => e.Row)
        //        .HasMaxLength(1)
        //        .IsUnicode(false)
        //        .IsFixedLength()
        //        .HasColumnName("row");
        //    entity.Property(e => e.ShowtimeId).HasColumnName("showtime_id");
        //    entity.Property(e => e.Status)
        //        .HasMaxLength(12)
        //        .IsUnicode(false)
        //        .HasColumnName("status");

        //    entity.HasOne(d => d.Showtime).WithMany(p => p.Seats)
        //        .HasForeignKey(d => d.ShowtimeId)
        //        .HasConstraintName("FK__Seats__showtime___03F0984C");
        //});

        //modelBuilder.Entity<Showtime>(entity =>
        //{
        //    entity.HasKey(e => e.ShowtimeId).HasName("PK__Showtime__A406B51882E94AE7");

        //    entity.Property(e => e.ShowtimeId).HasColumnName("showtime_id");
        //    entity.Property(e => e.CinemaId).HasColumnName("cinema_id");
        //    entity.Property(e => e.MovieId).HasColumnName("movie_id");
        //    entity.Property(e => e.ShowtimeDatetime)
        //        .HasColumnType("datetime")
        //        .HasColumnName("showtime_datetime");

        //    entity.HasOne(d => d.Cinema).WithMany(p => p.Showtimes)
        //        .HasForeignKey(d => d.CinemaId)
        //        .HasConstraintName("FK__Showtimes__cinem__4AB81AF0");

        //    entity.HasOne(d => d.Movie).WithMany(p => p.Showtimes)
        //        .HasForeignKey(d => d.MovieId)
        //        .HasConstraintName("FK__Showtimes__movie__49C3F6B7");
        //});

        //modelBuilder.Entity<Star>(entity =>
        //{
        //    entity.HasKey(e => e.StarId).HasName("PK__Stars__88222DCEA547DA08");

        //    entity.Property(e => e.StarId).HasColumnName("star_id");
        //    entity.Property(e => e.Name)
        //        .HasMaxLength(100)
        //        .IsUnicode(false)
        //        .HasColumnName("name");
        //});

        //modelBuilder.Entity<User>(entity =>
        //{
        //    entity.HasKey(e => e.Id).HasName("PK__Users__3213E83F047D7E16");

        //    entity.Property(e => e.Id).HasColumnName("id");
        //    entity.Property(e => e.BirthDate)
        //        .HasColumnType("datetime")
        //        .HasColumnName("birth_date");
        //    entity.Property(e => e.Email)
        //        .HasMaxLength(100)
        //        .IsUnicode(false)
        //        .HasColumnName("email");
        //    entity.Property(e => e.FullName)
        //        .HasMaxLength(100)
        //        .IsUnicode(false)
        //        .HasColumnName("full_name");
        //    entity.Property(e => e.Gender)
        //        .HasMaxLength(10)
        //        .IsUnicode(false)
        //        .HasColumnName("gender");
        //    entity.Property(e => e.IsAdmin).HasColumnName("is_admin");
        //    entity.Property(e => e.Password)
        //        .HasMaxLength(100)
        //        .IsUnicode(false)
        //        .HasColumnName("password");
        //    entity.Property(e => e.Username)
        //        .HasMaxLength(50)
        //        .IsUnicode(false)
        //        .HasColumnName("username");
        //});

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.VoucherId).HasName("PK__Vouchers__3AEE79C11E238D77");

            entity.HasIndex(e => e.VoucherCode, "UQ__Vouchers__7F0ABCA95838A31C").IsUnique();

            entity.Property(e => e.VoucherId).HasColumnName("VoucherID");
            entity.Property(e => e.DiscountValue).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ValidFrom).HasColumnType("date");
            entity.Property(e => e.ValidUntil).HasColumnType("date");
            entity.Property(e => e.VoucherCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.VoucherType)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
