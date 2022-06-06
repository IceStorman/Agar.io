using SFML.Graphics;

namespace Agar.io
{
    public class Player
    {
        public static double size = 16;
        public CircleShape playerSprite;
        public Color playerColor;
        public bool isBot = true;

        public static int countOfPlayers;
        public static int maxCountOfPlayers = 5;
    }
}
