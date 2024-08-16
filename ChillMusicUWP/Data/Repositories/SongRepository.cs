using ChillMusicUWP.Data.Context;
using ChillMusicUWP.MVVM.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChillMusicUWP.Data.Repositories
{
    public class SongRepository : IRepository<Song>
    {
        private readonly ApplicationDbContext _db;
        public SongRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<Song>> GetAllAsync()
        {
            return await _db.Songs.ToListAsync();
        }
    }
}
