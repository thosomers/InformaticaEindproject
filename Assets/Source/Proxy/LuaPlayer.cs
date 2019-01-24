using Game.Objects;
using Game.Utils;
using MoonSharp.Interpreter;

namespace Game.Proxy
{
    [MoonSharpUserData]
    public class LuaPlayer : LuaObject
    {
        public readonly Player player;

        public LuaPlayer(Player player)
        {
            this.player = player;
        }
        
        
        


    }
}