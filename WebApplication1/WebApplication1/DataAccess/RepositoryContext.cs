using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
        }

        protected RepositoryContext()
        {
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Musician> Musicians { get; set; }
        public DbSet<Musician_Track> Musician_Tracks { get; set; }
        public DbSet<MusicLabel> MusicLabels { get; set; }
        public DbSet<Track> Tracks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var musicians = new List<Musician>{
                new Musician{
                    IdMusician=1,
                    FirstName = "Mariusz",
                    LastName = "Pudzianowski",
                    Nickname = "pudzian"
                },
                new Musician{
                    IdMusician=2,
                    FirstName = "Marcin",
                    LastName = "Najman",
                    Nickname = "klepak"
                },
            };

            modelBuilder.Entity<Musician>(e =>
            {
                e.HasKey(e => e.IdMusician);                                           
                e.Property(e => e.FirstName).HasMaxLength(30).IsRequired();
                e.Property(e => e.LastName).HasMaxLength(50).IsRequired();
                e.Property(e => e.Nickname).HasMaxLength(20);

                e.HasData(musicians);
                e.ToTable("Musician");
            });



            var albums = new List<Album>{
                new Album{
                    IdAlbum=1,
                    AlbumName = "song",
                    PublishDate = System.DateTime.Now,
                    IdMusicLabel = 1,
                },
                new Album{
                    IdAlbum=2,
                    AlbumName = "dong",
                    PublishDate = System.DateTime.Now,
                    IdMusicLabel = 2,
                },
            };

            modelBuilder.Entity<Album>(e =>
            {
                e.HasKey(e => e.IdAlbum);
                e.Property(e => e.AlbumName).HasMaxLength(30).IsRequired();
                e.Property(e => e.PublishDate).IsRequired();
                
                e.HasOne(e=>e.MusicLabel)
                .WithMany(e=>e.Albums)
                .HasForeignKey(e => e.IdMusicLabel)
                .OnDelete(DeleteBehavior.Cascade);

                e.HasData(albums);
                e.ToTable("Album");
            });


            var tracks = new List<Track>{
                new Track{
                    IdTrack=1,
                    TrackName = "love you",
                    Duration = 100,
                    IdMusicAlbum = 1,
                },
                new Track{
                    IdTrack=2,
                    TrackName = "love me",
                    Duration = 200,
                    IdMusicAlbum = 2,
                },
            };

            modelBuilder.Entity<Track>(e =>
            {
                e.HasKey(e => e.IdTrack);
                e.Property(e => e.TrackName).HasMaxLength(20).IsRequired();
                e.Property(e => e.Duration).IsRequired();

                e.HasOne(e => e.Album)
                .WithMany(e => e.Tracks)
                .HasForeignKey(e => e.IdMusicAlbum)
                .OnDelete(DeleteBehavior.Cascade);

                e.HasData(tracks);
                e.ToTable("Track");
            });



            var musicLabels = new List<MusicLabel>{
                new MusicLabel{
                    IdMusicLabel=1,
                    Name="goIt"
                },
                new MusicLabel{
                    IdMusicLabel=2,
                    Name="takeIt"
                },
            };

            modelBuilder.Entity<MusicLabel>(e =>
            {
                e.HasKey(e => e.IdMusicLabel);
                e.Property(e => e.Name).HasMaxLength(50).IsRequired();

                e.HasData(musicLabels);
                e.ToTable("MusicLabel");
            });



            var musicTracks = new List<Musician_Track>{
                new Musician_Track{
                    IdTrack=1,
                    IdMusician=1
                },
                new Musician_Track{
                    IdTrack=2,
                    IdMusician=2
                },
            };

            modelBuilder.Entity<Musician_Track>(e =>
            {
                e.HasKey(e => new {e.IdTrack, e.IdMusician});

                e.HasOne(e => e.Musician)
                .WithMany(e => e.Musician_Tracks)
                .HasForeignKey(e => e.IdMusician) 
                .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(e => e.Track)
                .WithMany(e => e.Musician_Tracks)
                .HasForeignKey(e => e.IdTrack)
                .OnDelete(DeleteBehavior.Cascade);

                e.HasData(musicTracks);
                e.ToTable("Musician_Track");
            });



        }

    }
}
