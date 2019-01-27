using System;
using Game.Objects;
using MoonSharp.Interpreter;

namespace Game.Utils
{
    [MoonSharpUserData]
    public class LuaObject
    {
        
    }

    [MoonSharpUserData]
    public class LuaPlayerObject : LuaObject
    {
        /// <summary>
        /// The <see cref="PlayerObject"/> which this is the LUA representation of. (Used for <see cref="LuaPlayerObject.checkManipulation"/>)
        /// </summary>
        private PlayerObject Object;
        
        /// <summary>
        /// The <see cref="Player"/> which controls this object.
        /// </summary>
        public Player Player;

        public LuaPlayerObject(Player player,PlayerObject o)
        {
            Object = o;
            Player = player;
        }


        /// <summary>
        /// Throws an Exception if not run from the Object's LUA code.
        /// </summary>
        /// <exception cref="Exception">The exception thrown when not called from this Object's script</exception>
        public void checkManipulation()
        {
            if (this.Player.Script.CurrentObject != Object)
            {
                throw new Exception("Cannot manipulate non-current object");
            }
        }


        /// <summary>
        /// Makes this object sleep for the specified number of ticks.
        /// </summary>
        /// <param name="i">The number of ticks to sleep (at least 1)</param>
        /// <returns>A LUA Yieldreq for i ticks</returns>
        public DynValue sleepTick( int i)
        {
            if (i < 1) throw new Exception("Cannot sleep for less than 1 tick");
            return DynValue.NewYieldReq(new DynValue[]{DynValue.NewNumber(i)});
        }
        
        /// <summary>
        /// Makes this Object sleep until the next Tick.
        /// </summary>
        /// <returns>A LUA Yieldreq for 1 tick</returns>
        public DynValue doNothing()
        {
            return sleepTick(1);
        }
        
        
        
        
        

    }
    
    
}