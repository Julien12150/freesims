using System;

namespace Julien12150.FreeSims
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Game1 game = new Game1(args))
            {
                game.Run();
            }
        }
    }
#endif
}

