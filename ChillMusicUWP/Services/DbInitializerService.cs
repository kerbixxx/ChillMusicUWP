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
            var songs = SeedSongs(db);
            var categories = SeedSoundCategories(db);
            var sounds = SeedSounds(db);
        }

        private static List<Song> SeedSongs(ApplicationDbContext db)
        {
            if (db.Songs.Any())
            {
                return db.Songs.ToList();
            }
            var songs = new List<Song>();

            int songsCount = 15;
            for (int i = 1; i < songsCount+1; i++)
            {
                songs.Add(new Song() { Image = $"/Assets/Images/image{songsCount}.jpg", SongFile = $"/Assets/Songs/song{songsCount}.mp3" });
            }
            db.AddRange(songs);
            db.SaveChanges();
            return songs;
        }

        private static List<SoundCategory> SeedSoundCategories(ApplicationDbContext db)
        {
            if (db.SoundCategories.Any())
            {
                return db.SoundCategories.ToList();
            }
            var soundCategories = new List<SoundCategory>();

            soundCategories.Add(new SoundCategory() { Name = "Музыка" });
            soundCategories.Add(new SoundCategory() { Name = "Природа" });
            soundCategories.Add(new SoundCategory() { Name = "Животные" });
            soundCategories.Add(new SoundCategory() { Name = "Бинауральные ритмы" });

            db.AddRange(soundCategories);
            db.SaveChanges();
            return soundCategories;
        }

        private static List<Sound> SeedSounds(ApplicationDbContext db)
        {
            if (db.Sounds.Any())
            {
                return db.Sounds.ToList();
            }
            var sounds = new List<Sound>();
            var soundCategories = db.SoundCategories.ToList();

            foreach (var category in soundCategories)
            {
                for(int i = 1; i < 10; i++)
                {
                    sounds.Add(new Sound() { Name = $"sound{sounds.Count+1}", SoundFile = $"/Assets/Sounds/sound{sounds.Count+1}.mp3", CategoryId=category.Id});
                }
            }

            db.AddRange(sounds);
            db.SaveChanges();
            return sounds;
        }
    }
}
