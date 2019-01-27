using Game.Objects;
using Game.Utils;
using MoonSharp.Interpreter;

namespace Game.Proxy
{
    [MoonSharpUserData]
    public class LuaBase : LuaPlayerObject
    {
        /// <summary>
        /// The <see cref="Base"/> which this is the LUA representation of.
        /// </summary>
        public Base Base;
        
        public LuaBase(Player player, Base o) : base(player, o)
        {
            this.Base = o;
        }

        
        /// <summary>
        /// Spawns a new robot when this becomes possible.
        /// </summary>
        /// <param name="func">A <see cref="DynValue"/> Function which controls the robot.</param>
        /// <returns>The Tick-Cost to preform this action.</returns>
        public DynValue spawnRobot(DynValue func)
        {
            //Test if BASE is active
            checkManipulation();
            
            //Test if robot can be made :)
            
            
            
            //Create robot
            this.Player.spawnRobot(func);
            
            
            
            //Sleep a tick?
            return sleepTick(1);
        }
        
        
        
        
        
    }
}