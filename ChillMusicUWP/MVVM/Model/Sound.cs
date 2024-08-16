using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChillMusicUWP.MVVM.Model
{
    public class Sound
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string SoundFile { get; set; }
        [ForeignKey(nameof(SoundCategory))]
        public int CategoryId { get; set; }
    }
}
