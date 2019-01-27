using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Game.Utils
{
    public class LuaUtils
    {
        public static readonly LuaUtils instance = new LuaUtils();

        private LuaUtils()
        {
        }



        public static void Setup(Script script)
        {
            instance.setup(script);
        }





        private void print(DynValue val)
        {
            Debug.Log(val.ToString());
        }

        private void setup(Script script)
        {
            script.Globals["print"] = (Action<DynValue>) print;
        }
    }
}