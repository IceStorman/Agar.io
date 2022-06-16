using System;
using SFML.Graphics;

namespace Agario
{
    public abstract class Entity
    {
        public abstract CircleShape Sprite { get; set; }
        public abstract float Size { get; set; }
    }
}