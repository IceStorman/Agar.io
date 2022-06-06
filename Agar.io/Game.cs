using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System;
using System.Collections.Generic;

namespace Agar.io
{
    public class Game
    {
        public const int default_window_width = 1600;
        public const int default_window_height = 900;
        public const string game_name = "Agar.io";
        private readonly Random random = new Random();

        public void Run()
        {
            RenderWindow window = new RenderWindow(new VideoMode(default_window_width, default_window_height), game_name);
            window.Closed += WindowClosed;

            List<Food> foods = FoodFactory();

            window.Clear(Color.Cyan);

            /*foreach(Food food in foods)
            {
                window.Draw(food.foodSprite);
                window.Display();
            }*/

            while (window.IsOpen)
            {
                window.DispatchEvents();
                //window.Clear();
                foreach(var food in foods)
                {
                    window.Draw(food.foodSprite);
                }

                window.Display();
            }
        }

        private List<Food> FoodFactory()
        {
            List<Food> foods = new List<Food>();
            foods.Capacity = Food.maxCountOfFood;

            for(int i = 0; i < foods.Capacity; i++)
            {
                foods.Add(SpawnFood(SetFoodType()));
            }
            return foods;
        }

        private Food SpawnFood(string foodType)
        {
            Food food = new Food();

            switch (foodType)
            {
                case "small":
                    food.foodColor = Color.Green;
                    food.foodSprite.Radius = 4;
                    break;
                case "medium":
                    food.foodColor = Color.White;
                    food.foodSprite.Radius = 8;
                    break;
                case "big":
                    food.foodColor = Color.Red;
                    food.foodSprite.Radius = 16;
                    break;
            }

            food.foodSprite.FillColor = food.foodColor;
            food.foodSprite.Position = SetRandomPos();

            return food;
        }

        private CircleShape SpawnPlayers(string playerType)
        {
            Player player = new Player();

            switch (playerType)
            {
                case "Blue":
                    player.playerColor = Color.Blue;
                    player.isBot = false;
                    break;
                case "Red":
                    player.playerColor = Color.Red;
                    break;
                case "White":
                    player.playerColor = Color.White;
                    break;
                case "Yellow":
                    player.playerColor = Color.Yellow;
                    break;
                case "Green":
                    player.playerColor = Color.Green;
                    break;
            }
            player.playerSprite.FillColor = player.playerColor;
            return player.playerSprite;
        }

        private string SetPlayerType()
        {
            Random rnd = new Random();

            switch(rnd.Next(0, 5))
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