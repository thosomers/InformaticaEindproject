using Game.Objects;
using Game.Utils;

namespace Game.Proxy
{
    public class LuaRobot : LuaPlayerObject
    {
        public Robot Robot;
        public LuaRobot(Player player, Robot robot) : base(player, robot)
        {
            this.Robot = robot;
        }
    }
}