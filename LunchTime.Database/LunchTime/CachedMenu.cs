using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace LunchTime.Database.LunchTime
{
    public class CachedMenu
    {
        [Key]
        public int RestaurantId { get; set; }        

        public string Menu { get; set; }
    }

    public class CachedMenuContext : DbContext
    {
        public CachedMenuContext() : base("LunchTime")
        {
        }

        public DbSet<CachedMenu> CachedMenus { get; set; }
    }
}
