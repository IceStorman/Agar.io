using SFML.Graphics;
using SFML.System;

namespace Agario
{
    internal class Ball : MoveableObject
    {
        public Ball(CircleShape sprite, float size, Vector2f direction, float speed)
            : base(sprite, size, direction, speed) { }
    }
}
