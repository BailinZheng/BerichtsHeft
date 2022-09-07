using BerichtsHeft.Shared;
using Microsoft.EntityFrameworkCore;

namespace BerichtsHeft.Client.Data
{
    public class DB : DbContext
    {

        public DB(DbContextOptions<DB> DB_Context) : base(DB_Context) { }

        public DbSet<DateiInfo> DateiInfos { get; set; }
    }
}
