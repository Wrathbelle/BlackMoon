using System;

namespace BlackMoon
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
        static void Main()
        {
            Console.WriteLine("test");
            using (var game = new Game())
                game.Run();
            
        }
    }
}
