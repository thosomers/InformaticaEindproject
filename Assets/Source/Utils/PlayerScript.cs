using System.Collections.Generic;
using MoonSharp.Interpreter;
using Game.Objects;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;

namespace Game.Utils
{
    public class PlayerScript : Script
    {
        public readonly Player player;
        
        public PlayerScript(Player player) : base(CoreModules.Preset_HardSandbox)
        {
            this.player = player;
        }

        public PlayerObject CurrentObject = null;

        public void Setup()
        {
            this.Globals["Player"] = player.LuaPlayer;
            this.Globals["Enemy"] = player.Enemy.LuaEnemy;
            DynValue func = this.LoadString(this.player.Source.getLuaSource(), this.Globals,
                this.player.Source.getLuaName());
            func.Function.Call();
            player.Base = new Base(player,this.Globals.Get("Base"));
        }



        public Queue<List<PlayerObject>> playerObjects = new Queue<List<PlayerObject>>();

        public void runTick()
        {
            var objects = playerObjects.Dequeue();
            foreach (var obj in objects)
            {
                this.CurrentObject = obj;
                int newtick = obj.runTick(this);
                this.CurrentObject = null;
                while (newtick > playerObjects.Count-1)
                {
                   playerObjects.Enqueue(new List<PlayerObject>());
                }
                playerObjects.ElementAt(newtick-1).Add(obj);
            }
        }


        public void newObject(PlayerObject playerObject, int i)
        {
            while (playerObjects.Count < i+1)
            {
                playerObjects.Enqueue(new List<PlayerObject>());
            }
            playerObjects.ElementAt(i).Add(playerObject);
        }
    }
}