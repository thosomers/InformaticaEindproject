using System;
using System.Linq;
using Game.Objects;
using Game.Proxy;
using MoonSharp.Interpreter;

namespace Game.Utils
{
    public abstract class PlayerObject
    {
        private DynValue Coroutine;
        public readonly Player Player;

        public LuaPlayerObject Lua { protected set; get; }




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
        
        public int runTick(PlayerScript script)
        {
            DynValue toSleep = Coroutine.Coroutine.Resume(this.Lua);
            return toSleep.ToObject<int>();
        }
        
        
    }
}