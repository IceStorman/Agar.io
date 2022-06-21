using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System;
using System.IO;
using System.Collections.Generic;

namespace Agario
{
    internal class Game
    {
        private string gameName;
        private uint game_width;
        private uint game_height;

        private const int maxCountOfFood = 32;
        private const int maxCountOfPlayers = 1;
        private const int maxCountOfBots = 5;

        private static string[] gameParams;

        private RenderWindow window;

        private List<Food> foods;
        private List<Player> players;
        private List<Player> bots;
        private List<Ball> balls;

        public void Run()
        {
            Start();

            while (window.IsOpen)
            {
                Update();
            }
        }

        private string[] ReadGameParams()
        {
            StreamReader sr = new StreamReader("filik.ini");
            string[] gameParams = new string[3];

            for(int i = 0; i < gameParams.Length; i++)
            {
                gameParams[i] = sr.ReadLine();
            }
            return gameParams;
        }

        private void Start()
        {
            gameParams = ReadGameParams();

            game_width = uint.TryParse(gameParams[0], out uint width) ?
                width : 1600;
            game_height = uint.TryParse(gameParams[1], out uint height) ?
                height : 900;
            gameName = gameParams[2];

            window = new RenderWindow(new VideoMode(game_width, game_height), gameName);
            window.Closed += WindowClosed;
            foods = CreateFood();
            players = CreatePlayers(maxCountOfPlayers);
            bots = CreatePlayers(maxCountOfBots);
            balls = new List<Ball>();
        }

        private void Update()
        {
            window.DispatchEvents();

            window.Clear(Color.Cyan);

            players.GetPlayerInput();
            bots.GetBotInput();
            players.Move();
            bots.Move();

            foreach(var player in players)
            {
                player.EatFood(foods);
                player.EatPlayer(bots);
                TryShoot(player);
            }
            foreach(var bot in bots)
            {
                bot.EatFood(foods);
                bot.EatPlayer(players);
            }
            foreach(var ball in balls)
            {
                ball.Move();
            }

            RenderObject();

            window.Display();
        }

        private void RenderObject()
        {
            foreach (var player in players)
                window.Draw(player.Sprite);
            foreach (var bot in bots)
                window.Draw(bot.Sprite);
            foreach (var food in foods)
                window.Draw(food.Sprite);
            if(balls != null)
            {
                foreach (var ball in balls)
                    window.Draw(ball.Sprite);
            }
        }

        private List<Food> CreateFood()
        {
            List<Food> foods = new List<Food>()
            {
                Capacity = maxCountOfFood
            };

            for (int i = 0; i < foods.Capacity; i++)
            {
                foods.Add(SpawnFood(SetFoodType()));
            }
            return foods;
        }

        private Food SpawnFood(string foodType)
        {
            Food food = new Food
            {
                Sprite = new CircleShape()
            };
            Color foodColor = Color.Green;

            switch (foodType)
            {
                case "small":
                    foodColor = Color.Green;
                    food.Size = 8;
                    break;
                case "medium":
                    foodColor = Color.White;
                    food.Size = 16;
                    break;
                case "big":
                    foodColor = Color.Red;
                    food.Size = 32;
                    break;
            }

            food.Sprite.FillColor = foodColor;
            food.Sprite.Radius = food.Size / 2;
            food.Sprite.Position = CustomRandom.RandomPos((int)game_width, (int)game_height);

            return food;
        }

        private string SetFoodType()
        {
            switch (CustomRandom.RandomValue(0, 3))
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

        private List<Player> CreatePlayers(int maxCountOfPlayers)
        {
            List<Player> players = new List<Player>()
            {
                Capacity = maxCountOfPlayers
            };

            for (int i = 0; i < players.Capacity; i++)
            {
                players.Add(SpawnPlayers(SetPlayerType()));
            }

            return players;
        }

        private Player SpawnPlayers(string playerType)
        {
            CircleShape sprite = new CircleShape();
            Vector2f direction = new Vector2f();
            float speed = 1.5f;
            Color playerColor = Color.White;

            Player player = new Player(sprite, 0, direction, speed);

            switch (playerType)
            {
                case "Blue":
                    playerColor = Color.Blue;
                    player.Size = 40;
                    break;
                case "Red":
                    playerColor = Color.Red;
                    player.Size = 45;
                    break;
                case "White":
                    playerColor = Color.White;
                    player.Size = 50;
                    break;
                case "Yellow":
                    playerColor = Color.Yellow;
                    player.Size = 55;
                    break;
                case "Green":
                    playerColor = Color.Green;
                    player.Size = 60;
                    break;
            }

            player.Sprite.FillColor = playerColor;
            player.Sprite.Radius = player.Size / 2;
            player.Sprite.Position = CustomRandom.RandomPos((int)game_width, (int)game_height);
            return player;
        }

        private string SetPlayerType()
        {
            switch (CustomRandom.RandomValue(0, 5))
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

        private Ball SpawnBall(Player player)
        {
            CircleShape sprite = new CircleShape();
            float size = 16f;
            Vector2f direction = player.Direction != new Vector2f(0, 0) ?
                player.Direction : new Vector2f(1, 0);

            float speed = player.Speed * 2;

            Ball ball = new Ball(sprite, size, direction, speed);

            ball.Sprite.Position = player.Sprite.Position;
            ball.Sprite.Radius = size / 2;
            ball.Sprite.FillColor = player.Sprite.FillColor;
            return ball;
        }

        private void TryShoot(Player player)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                balls.Add(SpawnBall(player));
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            RenderWindow w = (RenderWindow)sender;
            w.Close();
        }
    }
}