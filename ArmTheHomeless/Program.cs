﻿using System;

namespace ArmTheHomeless.MonoGame.TP
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