using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System;

namespace Agar.io
{
    public class Game
    {
        public const int default_window_width = 1600;
        public const int default_window_height = 900;
        public const string game_name = "Agar.io";

        public void Run()
        {
            RenderWindow window = new RenderWindow(new VideoMode(default_window_width, default_window_height), game_name);
            window.Closed += WindowClosed;

            Drawable[] foods = FoodFactory();

            window.Clear(Color.Cyan);

            while (window.IsOpen)
            {
                window.DispatchEvents();

                for (int i = 0; i < foods.Length; i++)
                {
                    window.Draw(foods[i]);
                }

                window.Display();
            }
        }

        private Drawable[] FoodFactory()
        {
            Drawable[] foods = new Drawable[Food.maxCountOfFood];

            for(int i = Food.countOfFood; i < foods.Length; i++)
            {
                Food.countOfFood++;
                foods[i] = SpawnFood(SetFoodType());
            }
            return foods;
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

        private Vector2f SetFoodPos()
        {
            Random random = new Random();
            Vector2f newFoodPos = new Vector2f(random.Next(0, default_window_width), random.Next(0, default_window_height));
            return newFoodPos;
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            RenderWindow w = (RenderWindow)sender;
            w.Close();
        }
    }
}