using System;
using System.Linq;

namespace Technochips.FreeSims
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
			{
				if (args.Contains("/d"))
					game.Run();
				else
				{
					try
					{
						game.Run();
					}
					catch (Exception e)
					{
						CrashWindow.Run(e);
					}
				}
			}
        }
    }
}
