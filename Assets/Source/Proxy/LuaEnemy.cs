using Game.Objects;
using Game.Utils;
using MoonSharp.Interpreter;

namespace Game.Proxy
{
    [MoonSharpUserData]
    public class LuaEnemy : LuaObject
    {
        public readonly Player Player;

        public LuaEnemy(Player player)
        {
            Player = player;
        }
    }
}