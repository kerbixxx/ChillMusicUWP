using ChillMusicUWP.MVVM.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChillMusicUWP.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        internal DbSet<Song> Songs {  get; set; }
        internal DbSet<Sound> Sounds { get; set; }
        internal DbSet<SoundCategory> SoundCategories { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
