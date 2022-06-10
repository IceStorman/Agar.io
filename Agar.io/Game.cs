using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System;
using System.Threading;
using System.Collections.Generic;

namespace Agar.io
{
    public class Game
    {
        public const int default_window_width = 1600;
        public const int default_window_height = 900;
        public const string game_name = "Agar.io";
        private readonly Random random = new Random();
        public delegate void ParameterizedThreadStart(object obj);

        public void Run()
        {
            RenderWindow window = new RenderWindow(new VideoMode(default_window_width, default_window_height), game_name);
            window.Closed += WindowClosed;

            (List<Player> realPlayers, List<Player> bots) = CreatePlayers();
            List<Food> foods = CreateFood();

            while (window.IsOpen)
            {
                window.Clear(Color.Cyan);
                window.DispatchEvents();

                GetBotsInput(bots);
                GetRealPlayersInput(realPlayers);

                Move(realPlayers, bots);

                foreach(var player in realPlayers)
                {
                    player.EatFood(foods);
                    player.EatPlayer(bots);
                    window.Draw(player.playerSprite);
                }
                foreach(var bot in bots)
                {
                    bot.EatFood(foods);
                    bot.EatPlayer(realPlayers);
                    window.Draw(bot.playerSprite);
                }
                foreach(var food in foods)
                {
                    window.Draw(food.foodSprite);
                }

                window.Display();
            }
        }

        private List<Food> CreateFood()
        {
            List<Food> foods = new List<Food>()
            {
                Capacity = Food.maxCountOfFood
            };

            for(int i = 0; i < foods.Capacity; i++)
            {
                foods.Add(SpawnFood(SetFoodType()));
            }
            return foods;
        }

        private Food SpawnFood(string foodType)
        {
            Food food = new Food();
            Color foodColor = Color.Green;

            switch (foodType)
            {
                case "small":
                    foodColor = Color.Green;
                    food.foodSprite.Radius = 4;
                    break;
                case "medium":
                    foodColor = Color.White;
                    food.foodSprite.Radius = 8;
                    break;
                case "big":
                    foodColor = Color.Red;
                    food.foodSprite.Radius = 16;
                    break;
            }

            food.foodSprite.FillColor = foodColor;
            food.foodSprite.Position = SetRandomPos();

            return food;
        }
        
        private string SetFoodType()
        {
            switch (random.Next(0, 3))
            {
                case 0:
                    return "small";
                case 1:
                    return "medium";
                case 2:
                    return "big";
                default:
                    return "";
            }
        }

        private void GetBotsInput(List<Player> bots)
        {
            foreach (var bot in bots)
            {
                Vector2f botInput = new Vector2f();
                switch(random.Next(0, 4))
                {
                    case 0:
                        botInput = new Vector2f(0, -1);
                        break;
                    case 1:
                        botInput = new Vector2f(-1, 0);
                        break;
                    case 2:
                        botInput = new Vector2f(0, 1);
                        break;
                    case 3:
                        botInput = new Vector2f(1, 0);
                        break;
                }
                bot.direction = botInput;
            }
        }

        private void GetRealPlayersInput(List<Player> realPlayers)
        {
            foreach (var player in realPlayers)
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                    player.direction = new Vector2f(0, -1);
                else if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                    player.direction = new Vector2f(-1, 0);
                else if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                    player.direction = new Vector2f(0, 1);
                else if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                    player.direction = new Vector2f(1, 0);
                else
                    player.direction = new Vector2f(0, 0);
            }
        }

        private void Move(List<Player> realPlayers, List<Player> bots)
        {
            foreach(var player in realPlayers)
            {
                player.playerSprite.Position += player.direction * player.speed;
            }
            foreach(var bot in bots)
            {
                bot.playerSprite.Position += bot.direction * bot.speed;
            }
        }

        private (List<Player>, List<Player>) CreatePlayers()
        {
            List<Player> realPlayers = new List<Player>()
            {
                Capacity = Player.maxRealPlayers
            };
            List<Player> bots = new List<Player>()
            {
                Capacity = Player.maxBots
            };

            for(int i = 0; i < realPlayers.Capacity; i++)
            {
                realPlayers.Add(SpawnPlayers(SetPlayerType()));
            }
            for(int i = 0; i < bots.Capacity; i++)
            {
                bots.Add(SpawnPlayers(SetPlayerType()));
            }

            return (realPlayers, bots);
        }

        private Player SpawnPlayers(string playerType)
        {
            Player player = new Player();
            Color playerColor = Color.White;

            switch (playerType)
            {
                case "Blue":
                    playerColor = Color.Blue;
                    break;
                case "Red":
                    playerColor = Color.Red;
                    break;
                case "White":
                    playerColor = Color.White;
                    break;
                case "Yellow":
                    playerColor = Color.Yellow;
                    break;
                case "Green":
                    playerColor = Color.Green;
                    break;
            }
            player.playerSprite.FillColor = playerColor;
            player.playerSprite.Radius = player.size / 2;
            player.playerSprite.Origin = new Vector2f(player.playerSprite.Radius, player.playerSprite.Radius);
            player.playerSprite.Position = SetRandomPos();
            return player;
        }

        private string SetPlayerType()
        {
            switch(random.Next(0, 5))
            {
                case 0:
                    return "Blue";
                case 1:
                    return "Red";
                case 2:
                    return "White";
                case 3:
                    return "Yellow";
                case 4:
                    return "Green";
            }
            return "";
        }

        private Vector2f SetRandomPos()
        {
            Vector2f newRandomPos = new Vector2f(random.Next(0, default_window_width), random.Next(0, default_window_height));
            return newRandomPos;
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            RenderWindow w = (RenderWindow)sender;
            w.Close();
        }
    }
}