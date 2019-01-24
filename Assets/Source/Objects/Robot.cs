using Game.Proxy;
using Game.Utils;
using MoonSharp.Interpreter;

namespace Game.Objects
{
    public class Robot : PlayerObject
    {
        public Robot(Player player, DynValue func) : base(player, func)
        {
            this.Lua = new LuaRobot(player,this);
        }
    }
}