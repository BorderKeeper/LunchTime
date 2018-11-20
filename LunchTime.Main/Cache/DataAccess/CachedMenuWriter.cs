using System;
using System.Data.Entity;
using LunchTime.Core.Api.Common.Enums;
using LunchTime.Database.LunchTime;

namespace LunchTime.Main.Cache.DataAccess
{
    public interface ICachedMenuWriter
    {
        int CacheMenu(int restaurantId, string menu);

        DeletionStatus DeleteCachedMenu(int restaurantId);
    }

    public class CachedMenuWriter : ICachedMenuWriter
    {
        public int CacheMenu(int restaurantId, string menu)
        {
            using (var cachedMenuContext = new CachedMenuContext())
            {
                var existingCachedMenu = cachedMenuContext.CachedMenus.Find(restaurantId);

                if (existingCachedMenu != null)
                {
                    cachedMenuContext.Entry(existingCachedMenu).CurrentValues.SetValues(new CachedMenu()
                    {
                        RestaurantId = restaurantId,
                        Menu = menu
                    });
                }
                else
                {
                    cachedMenuContext.CachedMenus.Add(new CachedMenu
                    {
                        RestaurantId = restaurantId,
                        Menu = menu
                    });
                }

                cachedMenuContext.SaveChanges();

                return restaurantId;
            }
        }

        public DeletionStatus DeleteCachedMenu(int restaurantId)
        {
            try
            {
                using (var cachedMenu = new CachedMenuContext())
                {
                    var menuToDelete = cachedMenu.CachedMenus.Find(restaurantId);

                    if (menuToDelete == null)
                    {
                        return DeletionStatus.DoesNotExist;
                    }

                    cachedMenu.Entry(menuToDelete).State = EntityState.Deleted;

                    cachedMenu.SaveChanges();
                    return DeletionStatus.Deleted;
                }
            }
            catch (InvalidOperationException)
            {
                return DeletionStatus.DoesNotExist;
            }
        }
    }
}