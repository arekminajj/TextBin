using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TextBin.Models;

namespace TextBin.Data
{
    public class TextBinContext : DbContext
    {
        public TextBinContext (DbContextOptions<TextBinContext> options)
            : base(options)
        {
        }

        public DbSet<TextBin.Models.Text> Text { get; set; }
    }
}
