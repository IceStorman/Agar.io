using SFML.Graphics;
using SFML.Window;
using System;

namespace Agar.io
{
    public class Game
    {
        public const int default_window_width = 1600;
        public const int default_window_height = 900;

        public void Run()
        {
            RenderWindow window = new RenderWindow(new VideoMode(default_window_width, default_window_height), "Agar.io");
            window.Closed += WindowClosed;

            window.Clear(Color.Cyan);

            while (window.IsOpen)
            {
                window.DispatchEvents();

                Drawable food = SpawnFood(SetFoodType());

                window.Draw(food);

                window.Display();
            }
        }

        private CircleShape SpawnFood(string foodType)
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
            food.foodSprite.Position = SetFoodPos();

            return food.foodSprite;
        }

        private string SetFoodType()
        {
            Random rand = new Random();

            switch (rand.Next(0, 3))
            {
                case 0:
                    return "small";
                case 1:
                    return "medium";
                case 2:
                    return "big";
            }

            return "";
        }

        private SFML.System.Vector2f SetFoodPos()
        {
            Random random = new Random();
            SFML.System.Vector2f newFoodPos = new SFML.System.Vector2f(random.Next(0, default_window_width), random.Next(0, default_window_height));
            return newFoodPos;
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            RenderWindow w = (RenderWindow)sender;
            w.Close();
        }
    }
}