using Microsoft.EntityFrameworkCore;

namespace TutorialLandonAPI
{
    public class HotelApiContext : DbContext
    {
        public HotelApiContext(DbContextOptions options):base(options){

        }

        public DbSet<RoomEntity> Rooms { get; set; }
    }
}
