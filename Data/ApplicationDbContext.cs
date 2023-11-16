using Hotels.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Data
{
    public class ApplicationDbcontext: DbContext

    {
		internal object roomDetails;

		public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> Options ) :base(Options)
        {
            
        }
        public DbSet<Cart>  cart { get; set; }
        public DbSet<Hotel> hotel { get; set; }
        public DbSet<Invoice> invoics { get; set; }
        public DbSet<RoomDetails> RoomDetails { get; set; }
        public DbSet <Rooms> rooms { get; set; }
        


    }
}
