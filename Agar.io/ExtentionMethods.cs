using System;
using System.Collections.Generic;

namespace Agar.io
{
    public static class ExtentionMethods
    {
        public static void EatFood(this Player player, List<Food> foods)
        {
            foreach(Food food in foods)
            {
                if(player.playerSprite.Radius + food.foodSprite.Radius > GetDistance(player, food))
                {
                    Console.WriteLine("Completed");
                    //player.playerSprite.Radius += food.foodSprite.Radius;
                    //foods.Remove(food);
                }
            }
        }

        private static double GetDistance(Player player, Food food)
        {
            double distance = Math.Sqrt(Math.Pow(player.playerSprite.Origin.X - food.foodSprite.Origin.X, 2)
                + Math.Pow(player.playerSprite.Origin.Y - food.foodSprite.Origin.Y, 2));

            return distance;
        }
    }
}
