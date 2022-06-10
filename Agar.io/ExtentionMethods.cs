using System;
using System.Collections.Generic;

namespace Agar.io
{
    public static class ExtentionMethods
    {
        public static void EatFood(this Player player, List<Food> foods)
        {
            List<Food> eatenFoods = new List<Food>();

            foreach(Food food in foods)
            {
                if(player.playerSprite.Radius + food.foodSprite.Radius > GetFoodDistance(player, food))
                {
                    Console.WriteLine("Completed");
                    player.size += food.foodSprite.Radius;
                    eatenFoods.Add(food);
                }
            }
            player.playerSprite.Radius = player.size / 2;

            foreach (Food food in eatenFoods)
            {
                foods.Remove(food);
            }
            eatenFoods.Clear();
        }

        public static void EatPlayer(this Player player, List<Player> otherPlayers)
        {
            List<Player> eatenPlayers = new List<Player>();

            foreach(Player otherPlayer in otherPlayers)
            {
                if(player.playerSprite.Radius + otherPlayer.playerSprite.Radius > GetPLayerDistance(player, otherPlayer)
                    && player.size > otherPlayer.size)
                {
                    player.size += otherPlayer.size / 2;
                    eatenPlayers.Add(otherPlayer);
                }
            }
            player.playerSprite.Radius = player.size / 2;

            foreach(Player bot in eatenPlayers)
            {
                otherPlayers.Remove(bot);
            }
            eatenPlayers.Clear();
        }

        private static double GetFoodDistance(Player player, Food food)
        {
            double distance = Math.Sqrt(Math.Pow(player.playerSprite.Position.X + player.playerSprite.Radius / 2 - food.foodSprite.Position.X + food.foodSprite.Radius / 2, 2)
                + Math.Pow(player.playerSprite.Position.Y + player.playerSprite.Radius / 2- food.foodSprite.Position.Y + food.foodSprite.Radius / 2, 2));

            return distance;
        }

        private static double GetPLayerDistance(Player currentPlayer, Player otherPlayer)
        {
            double distance = Math.Sqrt(Math.Pow(currentPlayer.playerSprite.Position.X + currentPlayer.playerSprite.Radius / 2 - otherPlayer.playerSprite.Position.X - otherPlayer.playerSprite.Radius / 2, 2)
                + Math.Pow(currentPlayer.playerSprite.Position.Y + currentPlayer.playerSprite.Radius / 2 - otherPlayer.playerSprite.Position.Y + otherPlayer.playerSprite.Radius / 2, 2));
            
            return distance;
        }
    }
}
