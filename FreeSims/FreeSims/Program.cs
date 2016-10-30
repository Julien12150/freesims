using System;

namespace Julien12150.FreeSims
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            using (var game = new FreeSims(args))
                game.Run();
        }
    }
}
