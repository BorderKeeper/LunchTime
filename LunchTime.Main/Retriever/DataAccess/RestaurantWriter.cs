using System;
using System.Data.Entity;
using System.Linq;
using LunchTime.Core.Api.Common.Enums;
using LunchTime.Database.LunchTime;

namespace LunchTime.Main.Retriever.DataAccess
{
    public interface IRestaurantWriter
    {
        int SaveRestaurant(LearnedRestaurant restaurant);
        int UpdateRestaurant(LearnedRestaurant restaurant);
        DeletionStatus DeleteRestaurant(int restaurantId);
    }

    public class RestaurantWriter : IRestaurantWriter
    {
        public int SaveRestaurant(LearnedRestaurant restaurant)
        {
            using (var learnedRestaurants = new LearnedRestaurantContext())
            {
                var target = learnedRestaurants.LearnedRestaurants.Add(restaurant);

                learnedRestaurants.SaveChanges();

                return target.RestaurantId;
            }
        }

        public int UpdateRestaurant(LearnedRestaurant restaurant)
        {
            using (var learnedRestaurants = new LearnedRestaurantContext())
            {
                var restaurantToModify = learnedRestaurants.LearnedRestaurants.Find(restaurant.RestaurantId);

                if (restaurantToModify != null)
                {
                    learnedRestaurants.Entry(restaurantToModify).CurrentValues.SetValues(restaurant);
                }

                learnedRestaurants.SaveChanges();
            }

            return restaurant.RestaurantId;
        }

        public DeletionStatus DeleteRestaurant(int restaurantId)
        {
            try
            {
                using (var learnedRestaurants = new LearnedRestaurantContext())
                {
                    var restaurantToRemove = learnedRestaurants.LearnedRestaurants.Find(restaurantId);

                    if (restaurantToRemove == null)
                    {
                        return DeletionStatus.DoesNotExist;
                    }

                    learnedRestaurants.Entry(restaurantToRemove).State = EntityState.Deleted;
                    learnedRestaurants.SaveChanges();
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