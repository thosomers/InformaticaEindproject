using Game.Proxy;
using Game.Utils;
using MoonSharp.Interpreter;

namespace Game.Objects
{
    public class Base : PlayerObject
    {

        public Base(Player player, DynValue func) : base(player, func)
        {
            this.Lua = new LuaBase(player, this);
        }
    }
}