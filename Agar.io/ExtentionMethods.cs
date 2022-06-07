using System.Collections.Generic;

namespace Agar.io
{
    public static class ExtentionMethods
    {
        public static void EatFood(this Player player, List<Food> foods)
        {
            foreach(Food food in foods)
            {
                if(player.playerSprite.Position == food.foodSprite.Position)
                {
                    player.playerSprite.Radius += food.foodSprite.Radius;
                    //foods.Remove(food);
                }
            }
        }
    }
}
