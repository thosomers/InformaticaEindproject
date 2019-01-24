using Game.Objects;
using Game.Utils;
using MoonSharp.Interpreter;

namespace Game.Proxy
{
    public class LuaBase : LuaPlayerObject
    {
        public Base Base;
        
        public LuaBase(Player player, Base o) : base(player, o)
        {
            this.Base = o;
        }

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