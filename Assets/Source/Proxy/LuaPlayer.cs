using Game.Objects;
using Game.Utils;
using MoonSharp.Interpreter;

namespace Game.Proxy
{
    [MoonSharpUserData]
    public class LuaPlayer : LuaObject
    {
        /// <summary>
        /// The <see cref="player"/> which this is the LUA representation of.
        /// </summary>
        public readonly Player player;

        public LuaPlayer(Player player)
        {
            this.player = player;
        }
        
        
        


    }
}