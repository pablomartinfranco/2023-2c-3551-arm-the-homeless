using System;
using ArmTheHomeless.Tanks;

namespace ArmTheHomeless
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            // using var game = new TGCGame();
            // game.Run();
            using var game = new TanksGame();
            game.Run();
        }
    }
}
