using System;
using System.Linq;
using Game.Objects;
using Game.Proxy;
using MoonSharp.Interpreter;

namespace Game.Utils
{
    public abstract class PlayerObject
    {
        
        
        
        
        /// <summary>
        /// The Lua coroutine that controls this specific object. The coroutine is paused for a number of ticks using: <see cref="runTick"/>.
        /// </summary>
        private DynValue Coroutine;
        
        
        
        
        
        /// <summary>
        /// The <see cref="Player"/> object which controls this object. The <see cref="Coroutine"/> on this object is also run with <see cref="PlayerScript"/> attached to this object.
        /// </summary>
        public readonly Player Player;

        
        /// <summary>
        /// The <see cref="LuaPlayerObject"/> which represents this object in Lua code. (This Object is used in Lua functions to act like a 'Proxy')
        /// </summary>
        public LuaPlayerObject Lua { protected set; get; }



        /// <summary>
        /// The basic constructor for a PlayerObject: an object which is controlled by lua code.
        /// </summary>
        /// <param name="player">The <see cref="Player"/> (and therefore <see cref="PlayerScript"/>) which controls this object.</param> 
        /// <param name="func">The Lua function which controls this object (using a while loop probably)</param> 
        /// <exception cref="Exception">Exception if the Lua variable isn't a function.</exception>
        public PlayerObject(Player player, DynValue func)
        {
            this.Player = player;
            
            if (func.Type == DataType.Function)
            {
                func = player.Script.CreateCoroutine(func);
            } else {
                throw new Exception("Cannot create PlayerObject without function to run");
            }

            this.Coroutine = func;
            this.Player.Script.newObject(this, 0);
        }
        
        
        /// <summary>
        /// The function which is called for every <see cref="PlayerObject"/> every tick of the game. This function runs the attached <see cref="Coroutine"/> till the next action and retuns the Tick-Cost of the executed action.
        /// </summary>
        /// <param name="script">The <see cref="PlayerScript"/> on which this <see cref="Coroutine"/> runs.</param>
        /// <returns>the number of ticks to yield (Ticks to complete the action)</returns>
        public int runTick(PlayerScript script)
        {
            DynValue toSleep = Coroutine.Coroutine.Resume(this.Lua);
            return toSleep.ToObject<int>();
        }
        
        
    }
}