using ChillMusicUWP.Data.Context;
using ChillMusicUWP.MVVM.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Streaming.Adaptive;
using Windows.UI.Xaml.Media.Imaging;

namespace ChillMusicUWP.Services
{
    public static class DbInitializerService
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                if (!File.Exists("Music.db"))
                {
                    try
                    {
                        db.Database.EnsureCreated();
                        SeedEntities(db);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error initializing database: {ex.Message}");
                    }
                }
            }
        }

        private static void SeedEntities(ApplicationDbContext db)
        {
            SeedSongs(db);
            SeedSoundCategories(db);
            SeedSounds(db);
        }

        private static void SeedSongs(ApplicationDbContext db)
        {
            if (db.Songs.Any())
            {
                return;
            }

            int songsCount = 15;
            for (int i = 1; i < songsCount+1; i++)
            {
                db.Songs.Add(new Song() { Image = $"/Assets/Images/image{i}.jpg", SongFile = $"/Assets/Songs/song{i}.mp3", Name = $"image{i}"});
            }
            db.SaveChanges();
        }

        private static void SeedSoundCategories(ApplicationDbContext db)
        {
            if (db.SoundCategories.Any())
            {
                return;
            }
            var soundCategories = new List<SoundCategory>();

            soundCategories.Add(new SoundCategory() { Name = "Музыка" });
            soundCategories.Add(new SoundCategory() { Name = "Природа" });
            soundCategories.Add(new SoundCategory() { Name = "Животные" });

            db.AddRange(soundCategories);
            db.SaveChanges();
        }

        private static void SeedSounds(ApplicationDbContext db)
        {
            if (db.Sounds.Any())
            {
                return;
            }
            var soundCategories = db.SoundCategories.ToList();
            int i = 1;
            foreach (var category in soundCategories)
            {
                for(int j = 0; j < 3; j++)
                {
                    db.Sounds.Add(new Sound() { Name = $"sound{i}", SoundFile = $"/Assets/Sounds/sound{i}.mp3", CategoryId=category.Id});
                    i++;
                }
            }
            db.SaveChanges();
        }
    }
}
