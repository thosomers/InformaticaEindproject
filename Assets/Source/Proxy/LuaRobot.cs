using Game.Objects;
using Game.Utils;
using MoonSharp.Interpreter;

namespace Game.Proxy
{
    [MoonSharpUserData]
    public class LuaRobot : LuaPlayerObject
    {
        /// <summary>
        /// The <see cref="Robot"/> which this is the LUA representation of.
        /// </summary>
        public Robot Robot;
        public LuaRobot(Player player, Robot robot) : base(player, robot)
        {
            this.Robot = robot;
        }
    }
}