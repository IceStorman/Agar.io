using System;
using SFML.System;

namespace Agario
{
    internal class CustomRandom
    {
        Random random = new Random();

        public int RandomValue(int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue);
        }

        public Vector2f RandomPos(int x, int y)
        {
            Vector2f pos = new Vector2f(random.Next(0, x), random.Next(0, y));
            return pos;
        }
    }
}