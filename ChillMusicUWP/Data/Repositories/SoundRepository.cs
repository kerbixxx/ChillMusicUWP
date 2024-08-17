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
    public class SoundRepository : IRepository<Sound>
    {
        private readonly ApplicationDbContext _db;
        public SoundRepository(ApplicationDbContext db)
        {
            _db = db; 
        }

        public async Task<List<Sound>> GetAllAsync()
        {
            return await _db.Sounds
                .Include(s=>s.SoundCategory).
                ToListAsync();
        }
    }
}
