using System.Linq;
using LunchTime.Database.LunchTime;

namespace LunchTime.Main.Cache.DataAccess
{
    public interface ICachedMenuReader
    {
        CachedMenu GetCachedMenu(int restaurantId);
    }

    public class CachedMenuReader : ICachedMenuReader
    {
        public CachedMenu GetCachedMenu(int restaurantId)
        {
            using (var cachedMenuContext = new CachedMenuContext())
            {
                return cachedMenuContext.CachedMenus.FirstOrDefault(menu => menu.RestaurantId == restaurantId);
            }
        }
    }
}