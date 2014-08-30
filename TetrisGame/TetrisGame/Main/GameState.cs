using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetrisGame.Main
{
    // GameState representation. Only one state can be active at the time.
    class GameState
    {
        public const String MENU = "MENU";
        public const String PLAYING = "PLAYING";
        public const String PAUSED = "PAUSED";
        public const String GAME_OVER = "GAMEOVER";
        public String state { get; set; }

    }
}
