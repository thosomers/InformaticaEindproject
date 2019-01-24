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
        private PlayerObject Object;
        public Player Player;

        public LuaPlayerObject(Player player,PlayerObject o)
        {
            Object = o;
            Player = player;
        }


        public void checkManipulation()
        {
            if (this.Player.Script.CurrentObject != Object)
            {
                throw new Exception("Cannot manipulate non-current object");
            }
        }


        protected DynValue sleepTick( int i)
        {
            return DynValue.NewYieldReq(new DynValue[]{DynValue.NewNumber(i)});
        }
        
        public DynValue doNothing()
        {
            return sleepTick(1);
        }
        
        
        
        
        

    }
    
    
}