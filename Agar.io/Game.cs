using SFML.Graphics;
using SFML.Window;
using System;

namespace Agar.io
{
    public class Game
    {
        public void Start()
        {
            RenderWindow window = new RenderWindow(new VideoMode(1600, 900), "Game window");
            window.Closed += WindowClosed;

            while (window.IsOpen)
            {
                window.DispatchEvents();

                window.Clear(Color.Green);

                window.Display();
            }
        }

        static void WindowClosed(object sender, EventArgs e)
        {
            RenderWindow w = (RenderWindow)sender;
            w.Close();
        }
    }
}
