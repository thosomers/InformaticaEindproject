using MoonSharp.Interpreter;
using Game.Objects;

namespace Game.Utils
{
    public class PlayerScript : Script
    {
        public readonly Player player;
        
        public PlayerScript(Player player)
        {
            this.player = player;
        }
    }
}