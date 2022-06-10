using SFML.Graphics;
using SFML.System;

namespace Agar.io
{
    public class Player
    {
        public float size = 64f;
        public CircleShape playerSprite = new CircleShape();
        public float speed = 1.5f;
        public Vector2f direction;

        public const int maxRealPlayers = 1;
        public const int maxBots = 5;
        public static int countOfAlivePlayers;
    }
}
