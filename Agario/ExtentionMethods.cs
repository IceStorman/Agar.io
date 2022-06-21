using System;
using System.Collections.Generic;
using SFML.System;
using SFML.Window;

namespace Agario
{
    internal static class ExtentionMethods
    {
        public static void GetPlayerInput(this List<Player> players)
        {
            foreach(Player player in players)
            {
                Vector2f direction = new Vector2f();

                if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                    direction = new Vector2f(0, -1);
                else if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                    direction = new Vector2f(0, 1);
                else if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                    direction = new Vector2f(-1, 0);
                else if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                    direction = new Vector2f(1, 0);

                player.Direction = direction;
            }
        }

        public static void GetBotInput(this List<Player> bots)
        {
            Vector2f direction;

            foreach(var bot in bots)
            {
                switch (CustomRandom.RandomValue(0, 10))
                {
                    case 0:
                        direction = new Vector2f(0, -1);
                        break;
                    case 1:
                        direction = new Vector2f(0, 1);
                        break;
                    case 2:
                        direction = new Vector2f(-1, 0);
                        break;
                    case 3:
                        direction = new Vector2f(1, 0);
                        break;
                    default:
                        direction = new Vector2f(0, 0);
                        break;
                }
                bot.Direction = direction;
            }
        }

        public static void Move(this List<Player> players)
        {
            foreach(var player in players)
            {
                player.Move();
            }
        }

        public static void EatFood(this Player player, List<Food> foods)
        {
            List<Food> eatenFoods = new List<Food>();

            foreach (Food food in foods)
            {
                if (player.Sprite.Radius + food.Sprite.Radius > GetFoodDistance(player, food))
                {
                    player.Size += food.Sprite.Radius;
                    eatenFoods.Add(food);
                }
            }
            player.Sprite.Radius = player.Size / 2;

            foreach (Food food in eatenFoods)
            {
                foods.Remove(food);
            }
            eatenFoods.Clear();
        }

        public static void EatPlayer(this Player player, List<Player> otherPlayers)
        {
            List<Player> eatenPlayers = new List<Player>();

            foreach (Player otherPlayer in otherPlayers)
            {
                if (player.Sprite.Radius + otherPlayer.Sprite.Radius > GetPLayerDistance(player, otherPlayer)
                    && player.Size > otherPlayer.Size)
                {
                    player.Size += otherPlayer.Size / 2;
                    eatenPlayers.Add(otherPlayer);
                }
            }
            player.Sprite.Radius = player.Size / 2;

            foreach (Player bot in eatenPlayers)
            {
                otherPlayers.Remove(bot);
            }
            eatenPlayers.Clear();
        }

        private static double GetFoodDistance(Player player, Food food)
        {
            double distance = Math.Sqrt(Math.Pow(player.Sprite.Position.X + player.Sprite.Radius / 2 - food.Sprite.Position.X + food.Sprite.Radius / 2, 2)
                + Math.Pow(player.Sprite.Position.Y + player.Sprite.Radius / 2 - food.Sprite.Position.Y + food.Sprite.Radius / 2, 2));

            return distance;
        }

        private static double GetPLayerDistance(Player currentPlayer, Player otherPlayer)
        {
            double distance = Math.Sqrt(Math.Pow(currentPlayer.Sprite.Position.X + currentPlayer.Sprite.Radius / 2 - otherPlayer.Sprite.Position.X - otherPlayer.Sprite.Radius / 2, 2)
                + Math.Pow(currentPlayer.Sprite.Position.Y + currentPlayer.Sprite.Radius / 2 - otherPlayer.Sprite.Position.Y + otherPlayer.Sprite.Radius / 2, 2));

            return distance;
        }
    }
}